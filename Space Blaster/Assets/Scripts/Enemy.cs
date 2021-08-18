using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Options")]
    [SerializeField] float moveSpeed;
    [SerializeField] int startingHitPoints;
    [SerializeField] int pointValue;

    [Header("Internal")]
    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] int waypointIndex = 0;
    [SerializeField] int hitPoints;
    Transform finaldestination;
    GameController gameController;

    private void Start() {
        transform.position = waypoints[waypointIndex].position;
        waypointIndex++;

        finaldestination = waypoints[waypoints.Count - 1];
        hitPoints = startingHitPoints;

        gameController = FindObjectOfType<GameController>();

    }

    private void Update() {
        Move();
        CheckFinish();
    }

    void Move() {
        if (waypointIndex < waypoints.Count) {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);

            /*
            // Determine which direction to rotate towards
            Vector3 targetDirection = waypoints[waypointIndex].position - transform.position;

            // The step size is equal to speed times frame time.
            float singleStep = 1 * Time.deltaTime;

            // Rotate the forward vector towards the target direction by one step
            Vector2 newDirection = Vector2.RotateTowards(transform.forward, targetDirection, singleStep, 0.0f);

            // Draw a ray pointing at our target in
            Debug.DrawRay(transform.position, newDirection, Color.red);

            // Calculate a rotation a step closer to the target and applies rotation to this object
            transform.rotation = Quaternion.LookRotation(newDirection);
            */

            //transform.LookAt(waypoints[waypointIndex].position);
            Vector3 dir = waypoints[waypointIndex].position - this.transform.position;
            transform.Translate(dir.normalized * 1 * Time.deltaTime, Space.World);

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
            DestroyEnemy();
        }
        UpdateColor();
    }

    void DestroyEnemy() {
        gameController.ScorePoints(pointValue);
        Destroy(gameObject);
    }

    void UpdateColor() {
        float percentHP = (((float)hitPoints / (float)startingHitPoints) * 100);
        if (percentHP < 31) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(253, 50, 11, 255);
        } else if (percentHP < 61) {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(98, 209, 250, 255);
        }
    }


}
