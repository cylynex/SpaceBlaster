using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Config")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float laserSpeed = 100f;

    [Header("Attachments")]
    [SerializeField] GameObject laser;
    
    [Header("Timers")]
    [SerializeField] float refireTimer = 1f;
    [SerializeField] float refireTime = 0f;
    [SerializeField] bool canFire = true;

    float xMin, xMax;
    float yMin, yMax;

    private void Start() {
        SetupBounds();
        //StartCoroutine(PrintSomething());
    }

    private void Update() {
        UpdateFireTimer();
        Move();
        Shoot();
    }

    
    void SetupBounds() {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.1f, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.9f, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.05f, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).y;
    }


    private void Move() {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        float newXPos = Mathf.Clamp((transform.position.x + deltaX), xMin, xMax);
        float newYPos = Mathf.Clamp((transform.position.y + deltaY), yMin, yMax);

        transform.position = new Vector2(newXPos, newYPos);
    }

    private void Shoot() {
        if (Input.GetButton("Fire1") && canFire) { 
            GameObject laserBeam = Instantiate(laser, transform.position, Quaternion.identity);
            laserBeam.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, laserSpeed));
            canFire = false;
            refireTimer = refireTime;
        }
    }

    IEnumerator ContinuousFire() {
        yield return new WaitForSeconds(1f);
        CreateLaser();
    }

    void CreateLaser() {
        GameObject laserBeam = Instantiate(laser, transform.position, Quaternion.identity);
        laserBeam.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, laserSpeed));
    }

    private void UpdateFireTimer() {
        if (!canFire) {
            refireTimer -= Time.deltaTime;
            if (refireTimer <= 0) {
                canFire = true;
                refireTimer = 0;
            }
        }
    }

    IEnumerator PrintSomething() {
        print("Print somethign now.");
        yield return new WaitForSeconds(3);
        print("3s has passed");

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Enemy") {
            Destroy(gameObject);
        }
    }


}