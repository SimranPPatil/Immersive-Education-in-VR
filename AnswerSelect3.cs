using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AnswerSelect3 : MonoBehaviour
{
    public Renderer ring, star;
    public Canvas wrong, next, curr, right;
    public GameObject ship, asteroid;
    public Button optA, optB, optC, optD;
    public Text AText, BText, CText, DText;
    bool flagA, flagB, flagC, flagD;
    ColorBlock temp, temp2;
    float speedOfRotation = 200000000000;
    int correct = 0;
 bool canControl = true;
    // Use this for initialization
    void Start()
    {
        temp2 = optA.colors;
        ring.enabled = true;
        flagA = false;
        flagB = false;
        flagC = false;
        flagD = false;
    }
    // Update is called once per frame
    void Update()
    {
        print(flagA);
        //flag = flag2;
        // print(Input.GetAxis("Oculus_GearVR_LThumbstickX"));
        // if(Input.GetAxis("Oculus_GearVR_LThumbstickX") > 0.9)
        //if (Input.GetButton("RB"))
        print(correct);
        if (correct != 0 && correct < 120)
        {
            ship.transform.Rotate(Vector3.up * speedOfRotation, 20 * Time.deltaTime);
            correct++;
        }
        if (correct >= 120 && correct < 170)
        {
            ship.transform.Rotate(Vector3.left * speedOfRotation, 20 * Time.deltaTime);
            correct++;
        }

        if (correct == 170)
        {
            right.enabled = false;

            ring.enabled = false;
            next.enabled = true;
            asteroid.GetComponent<MeshRenderer>().enabled = false;
            star.enabled = true;
            next.GetComponent<AnswerSelect4>().enabled = true;
            curr.GetComponent<AnswerSelect3>().enabled = false;

        }
       
             
        if (Input.GetButtonDown("RB") && canControl == true)
        {
            if (flagC == true && flagB == false && flagA == false && flagD == true)  //////////////////////////////////////////////////////////////////////////////
            { //shoot
              //ship.transform.Rotate()
                curr.enabled = false;
                right.enabled = true;
                correct = 1;
                canControl = false;

            }
            else
            {
                canControl = false;
                curr.enabled = false;
                wrong.enabled = true;
                StartCoroutine(delay());
            }
        }
        if (Input.GetButtonDown("Y") && flagA == false)
        {
            print("A-B");

            temp = optA.colors;
            temp.normalColor = optA.colors.highlightedColor;
            optA.colors = temp;
            AText.color = temp.disabledColor;


            /*
            //temp.normalColor = Color.white;
            optA.colors = temp2;
            AText.color = Color.black;

            temp = optB.colors;
            temp.normalColor = optB.colors.highlightedColor;
            optB.colors = temp;
            BText.color = temp.disabledColor;
            */
            StartCoroutine(wait(1));// delay(ref flagA);

            //   flagA = 1;
        }
        else if (Input.GetButtonDown("Y") && flagA == true)
        {
            optA.colors = temp2;
            AText.color = Color.black;
            StartCoroutine(wait(1));// delay(ref flagA);
                                    //   delay(ref flagA);

            //flagA = 0;
        }
        if (Input.GetButtonDown("B") && flagB == false)
        {
            //flag = 3;
            print("B-C");
            temp = optB.colors;
            temp.normalColor = optB.colors.highlightedColor;
            optB.colors = temp;
            BText.color = temp.disabledColor;


            /*
            optB.colors = temp2;
            BText.color = Color.black;

            temp = optC.colors;
            temp.normalColor = optC.colors.highlightedColor;
            optC.colors = temp;
            CText.color = temp.disabledColor;
             delay());
            */
            StartCoroutine(wait(2));// delay(ref flagA);
                                    //  delay(ref flagB);

            //            flagB = 1;
        }
        else if (Input.GetButtonDown("B") && flagB == true)
        {
            optB.colors = temp2;
            BText.color = Color.black;
            StartCoroutine(wait(2));// delay(ref flagA);
                                    //   delay(ref flagB);

            //flagB = 0;
        }

        if (Input.GetButtonDown("X") && flagC == false)
        {
            print("C-D");
            temp = optC.colors;
            temp.normalColor = optC.colors.highlightedColor;
            optC.colors = temp;
            CText.color = temp.disabledColor;
            /*
            //flag = 4;
            optC.colors = temp2;
            CText.color = Color.black;

            temp = optD.colors;
            temp.normalColor = optD.colors.highlightedColor;
            optD.colors = temp;
            DText.color = temp.disabledColor;
             delay());
            */
            StartCoroutine(wait(3));// delay(ref flagA);
                                    //    delay(ref flagC);

            //            flagC = 1;
        }
        else if (Input.GetButtonDown("X") && flagC == true)
        {
            optC.colors = temp2;
            CText.color = Color.black;
            StartCoroutine(wait(3));// delay(ref flagA);
                                    // delay(ref flagC);

            //          flagC = 0;
        }

        if (Input.GetButtonDown("A") && flagD == false)
        {
            print("D-A");
            temp = optD.colors;
            temp.normalColor = optD.colors.highlightedColor;
            optD.colors = temp;
            DText.color = temp.disabledColor;
            //flag = 1;
            /*optD.colors = temp2;
            DText.color = Color.black;

            temp = optA.colors;
            temp.normalColor = optA.colors.highlightedColor;
            optA.colors = temp;
            AText.color = temp.disabledColor;
             delay());
            */
            StartCoroutine(wait(4));// delay(ref flagA);
                                    // delay(ref flagD);
                                    //            flagD = 1;
        }
        else if (Input.GetButtonDown("A") && flagD == true)
        {
            optD.colors = temp2;
            DText.color = Color.black;
            StartCoroutine(wait(4));// delay(ref flagA);
                                    //    delay(ref flagD);

            //            flagD = 0;

        }

        // }

    }

    IEnumerator wait(int i)
    {
        yield return new WaitForSeconds(0.01F);
        //        flagA = !flagA;
        switch (i)
        {
            case 1:
                flagA = !flagA;
                break;
            case 2:
                flagB = !flagB;
                break;
            case 3:
                flagC = !flagC;
                break;
            case 4:
                flagD = !flagD;
                break;

        }
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(1.5F);
        wrong.enabled = false;
        curr.enabled = true;
        canControl = true;

    }
}
