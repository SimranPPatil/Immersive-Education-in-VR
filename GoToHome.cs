using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GoToHome : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Home"))
            SceneManager.LoadScene("withfps");
	}
}
