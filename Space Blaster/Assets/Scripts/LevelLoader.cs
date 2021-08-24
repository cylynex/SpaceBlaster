using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour { 

    public void LoadLevel(string levelName) {
        SceneManager.LoadScene(levelName);
    }

    public void LoadGameOver() {
        StartCoroutine(WaitThenLoad());
    }

    IEnumerator WaitThenLoad() {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("GameOver");
    }

}
