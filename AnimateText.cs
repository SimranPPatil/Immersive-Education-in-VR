using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnimateText : MonoBehaviour {

    // Use this for initialization
    private string str;
    public Canvas answers;
    public Text txt;
    void Start()
    {
        answers.enabled = false;
        answers.GetComponent<AnswerSelect>().enabled = false;
        StartCoroutine(Animate_Text("Welcome to QuatSpace - The Quaternion Demo.~\n Your Mission:  To select the correct quaternion(s) that will aim your blaster at the highlighted asteroid ~\n Controls: Y-A-X-B to select quaternions.\tRB to apply quaternion. ~\nNOTE: Use Right hand convention. Viewing direction is -z axis. \tUp vector is y axis. ~\n Initializing angles. Are you ready? ~\n#"));
      //  StartCoroutine(Animate_Text("Your Mission:  To select the correct quaternion or group of quaternions that will aim your blaster at the correct asteroid"));

    }


    IEnumerator Animate_Text(string strComplete)
    {
        int i = 0;
        str = "";
        while (i < strComplete.Length)
        {
            if (strComplete[i] == '~')
            {
                i++;
                yield return StartCoroutine(WaitForKeyDown());
            }
            if (strComplete[i] == '\n')
            {
                str = "";
             //   yield return new WaitForSeconds(1.5F);

            }
           
           
                str += strComplete[i++];
            yield return new WaitForSeconds(0.10F);
           // print(str);
          txt.text = str;
            if (strComplete[i] == '#')
            {
                answers.enabled = true;
                answers.GetComponent<AnswerSelect>().enabled = true;
                this.enabled = false;
                i++;
            }
        }
    }
    IEnumerator WaitForKeyDown()
    {
        do
        {
           // print("here");
            yield return null;
        } while (!Input.GetButton("RB"));

    }
    // Update is called once per frame
    void Update () {
	
	}
}
