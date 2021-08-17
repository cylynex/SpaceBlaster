﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] int waypointIndex = 0;
    [SerializeField] float moveSpeed;
    [SerializeField] int startingHitPoints;
    [SerializeField] int hitPoints;
    Transform finaldestination;

    private void Start() {
        transform.position = waypoints[waypointIndex].position;
        waypointIndex++;

        finaldestination = waypoints[waypoints.Count - 1];
        hitPoints = startingHitPoints;

    }

    private void Update() {
        Move();
        CheckFinish();
    }

    void Move() {
        if (waypointIndex < waypoints.Count) {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);
            if (transform.position == waypoints[waypointIndex].position) {
                NextStop();
            }
        }
    }

    void NextStop() {
        waypointIndex++;
    }

    void CheckFinish() {
        if (transform.position == finaldestination.position) {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "weapon") {
            TakeDamage(collision.gameObject.GetComponent<Weapon>().damage);
            Destroy(collision.gameObject);
        }
    }

    void TakeDamage(int damageAmount) {
        hitPoints -= damageAmount;
        if (hitPoints <= 0) {
            Destroy(gameObject);
        }
        UpdateColor();
    }

    void UpdateColor() {
        float percentHP = (((float)hitPoints / (float)startingHitPoints) * 100);
        print("HPPERCENT: " + percentHP);
        if (percentHP < 31) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(253, 50, 11, 255);
        } else if (percentHP < 61) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(98, 209, 250, 255);
        }
    }


}
