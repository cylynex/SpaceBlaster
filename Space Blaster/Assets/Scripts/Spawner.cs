using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] Wave[] waves;
    [SerializeField] Wave currentWave;
    WaveData waveData;
    [SerializeField] int spawnIndex = 0;
    [SerializeField] int waveIndex = 0;
    [SerializeField] int numberWaves = 2;
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] bool levelFinished = false;

    [Header("OLD")]
    [SerializeField] GameObject enemy;
    [SerializeField] float spawnTimer;
    [SerializeField] float nextSpawnTime;
    [SerializeField] Transform spawnLocation;

    [SerializeField] List<Transform> waypoints = new List<Transform>();

    private void Start() {
        ChoosePath();
        currentWave = waves[waveIndex];
        waveData = currentWave.GetWaveData();
    }

    private void Update() {
        Spawn();
    }

    private void Spawn() {
        if (nextSpawnTime <= 0 && !levelFinished) {            

            print("Spawn Index: " + spawnIndex);

            GameObject newEnemy = Instantiate(waveData.enemy, spawnLocation.position, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().waypoints = waypoints;
            spawnIndex++;
            nextSpawnTime = spawnTimer;

            // Check number in wave, stop if complete and go to next wave, or continue.
            CheckForWaveAdvance();

        } else {
            nextSpawnTime -= Time.deltaTime;
        }
    }

    void ChoosePath() {
        GameObject[] paths = GameObject.FindGameObjectsWithTag("Path");
        int pathToTake = Random.Range(0, paths.Length);

        foreach (Transform waypoint in paths[pathToTake].transform) {
            waypoints.Add(waypoint);
        }
    }

    void CheckForWaveAdvance() {
        if (spawnIndex > waveData.numberOfEnemies) {
            print("FINISHED WAVE");

            if (waveIndex == waves.Length - 1) {
                print("DONE.  NO MORE WAVES");
                levelFinished = true;
            } else {
                nextSpawnTime = waveData.timeBetweenSpawns;
                waveIndex++;
                currentWave = waves[waveIndex];
                waveData = currentWave.GetWaveData();
                spawnIndex = 0;
                ChoosePath();
            }
        }
    }

}
