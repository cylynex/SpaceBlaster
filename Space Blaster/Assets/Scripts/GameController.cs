using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    [SerializeField] public int points = 0;
    [SerializeField] public int playerLives = 2;
    [SerializeField] GameObject player;
    [SerializeField] Transform spawnLocation;
    UIController uiController;

    private void Start() {
        uiController = GetComponent<UIController>();
        StartGame();
    }

    public void StartGame() {
        UpdateScoreBoard();
        uiController.SetPlayerLives(playerLives);
    }

    public void ScorePoints(int addPoints) {
        points += addPoints;
        UpdateScoreBoard();
    }

    void UpdateScoreBoard() {
        uiController.SetPoints(points);
    }    

    public void SubtractPlayerLife() {
        playerLives -= 1;
        uiController.SetPlayerLives(playerLives);
    }

    public void Respawn() {
        if (playerLives > 0) {
            StartCoroutine(SpawnNewPlayer());
        } else {
            SceneManager.LoadScene("GameOver");
        }
    }

    IEnumerator SpawnNewPlayer() {
        yield return new WaitForSeconds(2f);
        GameObject newPlayer = Instantiate(player, spawnLocation.position, Quaternion.identity);
    }

}
