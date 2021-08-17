using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] public int points = 0;
    UIController uiController;

    private void Start() {
        uiController = GetComponent<UIController>();
        uiController.SetPoints(points);
    }

}
