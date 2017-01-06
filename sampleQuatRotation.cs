using UnityEngine;
using System.Collections;

public class sampleQuatRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
      //  float pitch = Input.GetAxis("Oculus_GearVR_LThumbstickY") * 0.25F;
       // print(pitch);

       // transform.Rotate(pitch, 0, 0);
       // float yaw_roll = Input.GetAxis("Oculus_GearVR_RThumbstickY") * 0.15F;
          transform.Rotate(0, 1, 0);
     //   transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);

        //  acc = Input.GetAxis("Acc/Dec") * 0.5F;
        // speed -= acc * Time.deltaTime;
        //transform.Translate(0, 0, -speed);
        //Debug.Log(speed);
        print(gameObject.name);

    }
}
