using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    [SerializeField] TextMeshProUGUI waveLabel;
    [SerializeField] TextMeshProUGUI pointsLabel;

    public void SetWave(int waveNumber) {
        waveLabel.text = "WAVE " + waveNumber;
    }

    public void SetPoints(int points) {
        pointsLabel.text = points.ToString();
    }

}