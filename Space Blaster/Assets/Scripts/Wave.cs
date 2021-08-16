using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Wave")]
public class Wave : ScriptableObject {

    [SerializeField] GameObject enemy;
    [SerializeField] GameObject path;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float timeRandomizer = 0.5f;
    [SerializeField] int numberOfEnemies = 5;
    [SerializeField] float moveSpeed = 2f;

    public EnemyData GetEnemyData() {
        EnemyData eData = new EnemyData();
        eData.enemy = enemy;
        eData.path = path;
        eData.timeBetweenSpawns = timeBetweenSpawns;
        eData.timeRandomizer = timeRandomizer;
        eData.numberOfEnemies = numberOfEnemies;
        eData.moveSpeed = moveSpeed;
        return eData;
    }

    

}