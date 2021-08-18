using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] Wave[] waves;
    [SerializeField] Wave currentWave;
    WaveData waveData;
    [SerializeField] int spawnIndex = 0;
    [SerializeField] int waveIndex = 0;
    [SerializeField] bool levelFinished = false;

    [Header("Timers")]
    [SerializeField] float spawnTimer;
    [SerializeField] float nextSpawnTime;
    [SerializeField] Transform spawnLocation;

    [SerializeField] List<Transform> waypoints = new List<Transform>();

    GameController gameController;
    UIController uiController;

    private void Start() {
        SetReferences();
        ChoosePath();
        SetupWave();
        uiController.SetWave(waveIndex + 1);
    }


    private void Update() {
        if (nextSpawnTime <= 0 && !levelFinished) {
            SpawnEnemy();
            CheckForWaveAdvance();
        } else {
            nextSpawnTime -= Time.deltaTime;
        }
    }


    void SetReferences() {
        gameController = FindObjectOfType<GameController>();
        uiController = gameController.GetComponent<UIController>();
    }


    void SetupWave() {
        currentWave = waves[waveIndex];
        waveData = currentWave.GetWaveData();
        spawnTimer = waveData.timeBetweenSpawns;
    }


    void ChoosePath() {
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        int pathToTake = Random.Range(0, paths.Length);

        foreach (Transform waypoint in paths[pathToTake].transform) {
            waypoints.Add(waypoint);
        }
    }

    void CheckForWaveAdvance() {
        if (spawnIndex >= waveData.numberOfEnemies) {
            print("FINISHED WAVE");

            if (waveIndex == waves.Length - 1) {
                print("DONE.  NO MORE WAVES");
                levelFinished = true;
            } else {
                nextSpawnTime = waveData.timeBetweenSpawns;
                waveIndex++;
                uiController.SetWave(waveIndex + 1);
                currentWave = waves[waveIndex];
                waveData = currentWave.GetWaveData();
                spawnIndex = 0;
                ChoosePath();
            }
        }
    }

    void SpawnEnemy() {
        GameObject newEnemy = Instantiate(waveData.enemy, waveData.path.transform.position, Quaternion.identity);
        newEnemy.GetComponent<Enemy>().waypoints = waypoints;
        spawnIndex++;
        nextSpawnTime = spawnTimer;
    }

}
