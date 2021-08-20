using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Header("Options")]
    [SerializeField] float moveSpeed;
    [SerializeField] int startingHitPoints;
    [SerializeField] int pointValue;
    [SerializeField] bool canShoot;
    [SerializeField] Vector2 shootDelayRange = new Vector2(1,2.5f);
    [SerializeField] GameObject enemyProjectile;
    
    [Header("Internal")]
    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] int waypointIndex = 0;
    [SerializeField] int hitPoints;
    Transform finaldestination;
    GameController gameController;
    [SerializeField] float shootTimer = 0;

    private void Start() {
        transform.position = waypoints[waypointIndex].position;
        waypointIndex++;

        finaldestination = waypoints[waypoints.Count - 1];
        hitPoints = startingHitPoints;

        gameController = FindObjectOfType<GameController>();

        int shootSetup = Random.Range(0, 4);
        if (shootSetup == 0) {
            canShoot = false;
        } else {
            canShoot = true;
            SetShootTimer();
        }
    }

    void SetShootTimer() {
        float shootRand = Random.Range(shootDelayRange.x, shootDelayRange.y);
        shootTimer = shootRand;
    }

    private void Update() {
        Move();
        CheckShoot();
        CheckFinish();
    }

    void Move() {
        if (waypointIndex < waypoints.Count) {
            transform.position = Vector2.MoveTowards(transform.position, waypoints[waypointIndex].position, moveSpeed * Time.deltaTime);

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

    void CheckShoot() {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0) {
            Fire();
            SetShootTimer();
        }
    }

    void Fire() {
        GameObject enemyShot = Instantiate(enemyProjectile, transform.position, Quaternion.identity);
        //enemyShot.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -600f));
        enemyShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -5f);
    }


}
