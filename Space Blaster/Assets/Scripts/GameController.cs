using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    [SerializeField] public int points = 0;
    UIController uiController;

    private void Start() {
        uiController = GetComponent<UIController>();
        UpdateScoreBoard();
    }

    public void ScorePoints(int addPoints) {
        points += addPoints;
        UpdateScoreBoard();
    }

    void UpdateScoreBoard() {
        uiController.SetPoints(points);
    }

}
