using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    [SerializeField] float gameBoardUnits = 16f;
    [SerializeField] float mousePosition;

    [SerializeField] float moveSpeed = 10f;

    private void Start() {
        
    }

    private void Update() {
        Move();
    }


    private void Move() {
        float deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        float newXPos = transform.position.x + deltaX;

        float deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;
        float newYPos = transform.position.y + deltaY;

        transform.position = new Vector2(newXPos, newYPos);

    }

}