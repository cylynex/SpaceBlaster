using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour {

    [SerializeField] Wave wave;
    EnemyData enemyData;
    [SerializeField] List<Transform> waypoints = new List<Transform>();
    [SerializeField] float moveSpeed = 1f;
    
    float adjustedSpeed;
    int wayPointIndex = 0;
    
    [SerializeField] Transform currentDestination;
    [SerializeField] Transform finalDestination;

    private void Start() {
        GetWaveData();
        GetWaypoints();        
        SetDestination();
    }

    private void Update() {
        Move();
        CheckDestroy();
    }

    private void GetWaveData() {
        enemyData = wave.GetEnemyData();
    }

    public List<Transform> GetWaypoints() {
        
        foreach (Transform waypoint in enemyData.path.transform) {
            waypoints.Add(waypoint);
        }

        transform.position = waypoints[0].transform.position;
        finalDestination = waypoints[waypoints.Count - 1];

        return waypoints;
    }

    void Move() {
        if (wayPointIndex < waypoints.Count) {
            transform.position = Vector2.MoveTowards(transform.position, currentDestination.position, adjustedSpeed);
            if (transform.position == currentDestination.position) {
                SetDestination();
            }
        }
    }

    void SetDestination() {
        adjustedSpeed = moveSpeed * Time.deltaTime;
        wayPointIndex++;
        currentDestination = waypoints[wayPointIndex];
    }

    void CheckDestroy() {
        if (transform.position == finalDestination.position) {
            Destroy(gameObject);
        }
    }



}