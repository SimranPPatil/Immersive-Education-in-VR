using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	transform.Rotate(Vector3.down * 200, 75 * Time.deltaTime);
    }
}
