using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [SerializeField] List<Wave> waves = new List<Wave>();
    int currentWave = 0;

    void Start() {
        print("spawner online");



    }

}
