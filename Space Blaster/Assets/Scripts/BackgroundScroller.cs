using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    [SerializeField] float backgroundScrollSpeed = 0.5f;
    Material bgMaterial;
    Vector2 offset;

    private void Start() {
        bgMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, backgroundScrollSpeed);
    }

    private void Update() {
        bgMaterial.mainTextureOffset += offset * Time.deltaTime;
    }

}