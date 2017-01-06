using UnityEngine;
using System.Collections;

public class Asteroids : MonoBehaviour {
    private float maxRotationSpeed;
    private Vector3 rotSpeed;
    // Use this for initialization
    void Start () {
        maxRotationSpeed = 25;
        rotSpeed = Random.insideUnitSphere * maxRotationSpeed;
    }

    // Update is called once per frame
    void Update () {
        GetComponent<Transform>().Rotate(rotSpeed * Time.deltaTime);
    
    }
}
