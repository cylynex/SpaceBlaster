using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PU_DoublePowerLaser : MonoBehaviour {

    [SerializeField] int powerUpID;
    [SerializeField] GameObject newLaser;

    private void OnTriggerEnter2D(Collider2D collision) {
        print("power up caught");
        Player player = collision.gameObject.GetComponent<Player>();
        //player.
    }

}