using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MovementXbox : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Home"))
            SceneManager.LoadScene("withfps");


        Vector3 pos = transform.localPosition;
        Vector3 rot = transform.eulerAngles;

        pos.z += Input.GetAxis("Oculus_GearVR_LThumbstickX") * Time.deltaTime;
        pos.x -= Input.GetAxis("Oculus_GearVR_LThumbstickY") * Time.deltaTime;

        rot.y -= Input.GetAxis("Oculus_GearVR_RThumbstickY") * 80 * Time.deltaTime;

        transform.localPosition = pos;
        transform.eulerAngles = rot;
    }
}
