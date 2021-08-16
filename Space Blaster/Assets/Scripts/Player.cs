﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float gameBoardUnits = 16f;
    [SerializeField] float mousePosition;

    [SerializeField] float moveSpeed = 10f;

    float xMin, xMax;
    float yMin, yMax;

    private void Start() {
        SetupBounds();
    }

    private void Update() {
        Move();
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

}