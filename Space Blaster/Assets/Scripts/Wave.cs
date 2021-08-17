using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave")]
public class Wave : ScriptableObject {

    [SerializeField] public GameObject enemy;
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject path;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] int numberOfEnemies = 5;

    public WaveData GetWaveData() {
        WaveData waveData = new WaveData();
        waveData.enemy = enemy;
        waveData.path = path;
        waveData.timeBetweenSpawns = timeBetweenSpawns;
        waveData.numberOfEnemies = numberOfEnemies;
        return waveData;
    }

    

}