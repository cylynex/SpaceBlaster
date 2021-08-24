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
    [SerializeField] float enemyWeaponSpeed = 15f;

    [Header("Sounds")]
    AudioSource audio;
    [SerializeField] AudioClip deathSound;
    [SerializeField][Range(0,1)] float deathSoundVolume = 0.75f;
    
    [Header("Internal")]
    [SerializeField] public List<Transform> waypoints = new List<Transform>();
    [SerializeField] int waypointIndex = 0;
    [SerializeField] int hitPoints;
    Transform finaldestination;
    GameController gameController;
    [SerializeField] float shootTimer = 0;

    [Header("FX")]
    [SerializeField] GameObject explosion;

    private void Start() {
        transform.position = waypoints[waypointIndex].position;
        waypointIndex++;

        finaldestination = waypoints[waypoints.Count - 1];
        hitPoints = startingHitPoints;

        gameController = FindObjectOfType<GameController>();
        audio = GetComponent<AudioSource>();

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
        AudioSource.PlayClipAtPoint(deathSound, Camera.main.transform.position, deathSoundVolume);
        GameObject exploder = Instantiate(explosion,transform.position, Quaternion.identity);
        exploder.GetComponent<ParticleSystem>().Play();
        Destroy(exploder, 2f);
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
        enemyShot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -enemyWeaponSpeed);
    }


}
