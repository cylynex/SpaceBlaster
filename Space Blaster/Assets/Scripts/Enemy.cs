using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] int waypointIndex = 0;
    [SerializeField] float moveSpeed = 0.2f;
    float adjustedSpeed;
    Transform finaldestination;

    private void Start() {
               
        transform.position = waypoints[waypointIndex].position;
        waypointIndex++;

        adjustedSpeed = moveSpeed * Time.deltaTime;
        finaldestination = waypoints[waypoints.Count - 1];
    }

    private void Update() {
        Move();
        CheckFinish();
    }

    void Move() {
        if (waypointIndex < waypoints.Count) {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, adjustedSpeed);
            if (transform.position == waypoints[waypointIndex].position) {
                NextStop();
            }
        }
    }

    void NextStop() {
        waypointIndex++;
    }

    void CheckFinish() {
        if(transform.position == finaldestination.position) {
            Destroy(gameObject);
        }
    }


}
