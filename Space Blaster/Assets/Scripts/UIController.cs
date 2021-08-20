using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] TextMeshProUGUI waveLabel;
    [SerializeField] TextMeshProUGUI pointsLabel;
    [SerializeField] TextMeshProUGUI playerHPLabel;

    [SerializeField] GameObject playerLifeIcon;
    [SerializeField] GameObject lifeContainer;

    public void SetWave(int waveNumber) {
        waveLabel.text = "WAVE " + waveNumber;
    }

    public void SetPoints(int points) {
        pointsLabel.text = points.ToString();
    }

    public void SetPlayerHP(int hp) {
        playerHPLabel.text = hp.ToString();
    }

    public void SetPlayerLives(int playerLives) {

        foreach(Transform life in lifeContainer.transform) {
            Destroy(life.gameObject);
        }

        for (int i = 0; i < playerLives; i++) {
            GameObject plIcon = Instantiate(playerLifeIcon, transform.position, Quaternion.identity);
            plIcon.transform.SetParent(lifeContainer.transform, false);
        }
    }


}