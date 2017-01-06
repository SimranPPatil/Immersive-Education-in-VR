using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class quaternion : MonoBehaviour {
    [SerializeField] private Canvas instructions;
    [SerializeField] private Canvas quat1;
    [SerializeField] private Canvas quat2;
    [SerializeField] private Canvas quat3;
    [SerializeField] private Canvas welcome;
    [SerializeField] private Canvas def;
    [SerializeField] private Canvas intro;
    [SerializeField] private Canvas multiplicant_1;
    [SerializeField] private Canvas multiplicant_2;
    [SerializeField] private Canvas product;
    [SerializeField] private Canvas multiplication;
    [SerializeField] private Canvas goTo;
    public GameObject ship;
    public GameObject x;
    public GameObject y;
    public GameObject z;
    public GameObject x_a;
    public GameObject y_a;
    public GameObject z_a;
    public int count,i;
    Quaternion final;
    bool prod_flag;
    Vector3 init;

    public AudioSource audiosource;
    public AudioClip[] quatclips = new AudioClip[12];

    // Use this for initialization
    void Start () {
        count = 0;
        welcome.enabled = true;
        intro.enabled = false;
        def.enabled = false;
        quat1.enabled = false;
        quat2.enabled = false;
        quat3.enabled = false;
        instructions.enabled = true;
        multiplicant_1.enabled = false;
        multiplicant_2.enabled = false;
        multiplication.enabled = false;
        goTo.enabled = false;
        product.enabled = false;
        prod_flag = false;
        i = 0;
        init = ship.transform.position;

        audiosource.clip = quatclips[0];
        audiosource.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Home"))
            SceneManager.LoadScene("withfps");

       // print(ship.GetComponents<Vector3>().);
        if (Input.GetButtonDown("Next"))
        {
            if(prod_flag == false)
                ship.transform.localRotation = Quaternion.identity;
            count++;
            
            x.GetComponent<MeshRenderer>().enabled = true;
            y.GetComponent<MeshRenderer>().enabled = true;
            z.GetComponent<MeshRenderer>().enabled = true;
            x_a.GetComponent<MeshRenderer>().enabled = true;
            y_a.GetComponent<MeshRenderer>().enabled = true;
            z_a.GetComponent<MeshRenderer>().enabled = true;

            instructions.enabled = false;
            welcome.enabled = false;
            intro.enabled = false;
            def.enabled = false;
            quat1.enabled = false;
            quat2.enabled = false;
            quat3.enabled = false;
            multiplication.enabled = false;
            multiplicant_1.enabled = false;
            multiplicant_2.enabled = false;
            goTo.enabled = false;
            product.enabled = false;

            audiosource.Stop();


            if (count % 18 == 1)
                welcome.enabled = true;
            if (count % 18 == 2)
            {
                intro.enabled = true;
                audiosource.clip = quatclips[1];
                audiosource.Play();
            }
            if (count % 18 == 3)
            {
                def.enabled = true;
                audiosource.clip = quatclips[2];
                audiosource.Play();
            }
            if (count % 18 == 4)
            {
                quat1.enabled = true;
                audiosource.clip = quatclips[3];
                audiosource.Play();
            }
            if (count % 18 == 6)
            {
                quat2.enabled = true;
                audiosource.clip = quatclips[4];
                audiosource.Play();
            }
            if (count % 18 == 8)
            {
                quat3.enabled = true;
                audiosource.clip = quatclips[5];
                audiosource.Play();
            }
            if (count % 18 == 10)
            {
                multiplication.enabled = true;
                audiosource.clip = quatclips[6];
                audiosource.Play();
            }
            if (count % 18 == 11)
            {
                multiplicant_1.enabled = true;
                audiosource.clip = quatclips[7];
                audiosource.Play();
            }
            if (count % 18 == 13)
            {
                multiplicant_2.enabled = true;
                audiosource.clip = quatclips[8];
                audiosource.Play();
            }
            if (count % 18 == 15)
            {
                product.enabled = true;
                audiosource.clip = quatclips[9];
                audiosource.Play();
            }
            if (count % 18 == 17)
            {
                goTo.enabled = true;
                audiosource.clip = quatclips[10];
                audiosource.Play();
            }

        }
        if (count == 18)
        {
            ship.transform.position = new Vector3(ship.transform.position.x, ship.transform.position.y, ship.transform.position.z + (50 * Time.deltaTime));
            x.GetComponent<MeshRenderer>().enabled = false;
            y.GetComponent<MeshRenderer>().enabled = false;
            z.GetComponent<MeshRenderer>().enabled = false;
            x_a.GetComponent<MeshRenderer>().enabled = false;
            y_a.GetComponent<MeshRenderer>().enabled = false;
            z_a.GetComponent<MeshRenderer>().enabled = false;
            StartCoroutine(delay());
        }
            

        if (count % 18 == 5 )
		{
            quat1.enabled = true;
            ship.transform.rotation = Quaternion.RotateTowards(ship.transform.rotation, Quaternion.AngleAxis(-90, Vector3.forward), 0.25f);
			x.GetComponent<MeshRenderer>().enabled = false;
			y.GetComponent<MeshRenderer>().enabled = false;
			z.GetComponent<MeshRenderer>().enabled = true;
            x_a.GetComponent<MeshRenderer>().enabled = false;
            y_a.GetComponent<MeshRenderer>().enabled = false;
            z_a.GetComponent<MeshRenderer>().enabled = true;
        }
		if(count % 18 == 7 )
		{
            quat2.enabled = true;
            ship.transform.localRotation = Quaternion.RotateTowards(ship.transform.localRotation, Quaternion.AngleAxis(-90, Vector3.up), 0.25f);
			x.GetComponent<MeshRenderer>().enabled = false;
			y.GetComponent<MeshRenderer>().enabled = true;
			z.GetComponent<MeshRenderer>().enabled = false;
            x_a.GetComponent<MeshRenderer>().enabled = false;
            y_a.GetComponent<MeshRenderer>().enabled = true;
            z_a.GetComponent<MeshRenderer>().enabled = false;
        }
		if (count % 18 == 9 )
		{
            quat3.enabled = true;
            ship.transform.localRotation = Quaternion.RotateTowards(ship.transform.localRotation, Quaternion.AngleAxis(90, Vector3.right), 0.25f);
			x.GetComponent<MeshRenderer>().enabled = true;
			y.GetComponent<MeshRenderer>().enabled = false;
			z.GetComponent<MeshRenderer>().enabled = false;
            x_a.GetComponent<MeshRenderer>().enabled = true;
            y_a.GetComponent<MeshRenderer>().enabled = false;
            z_a.GetComponent<MeshRenderer>().enabled = false;
        }
        if (count % 18 == 12)
        {
            multiplicant_1.enabled = true;
            prod_flag = true;
            final.Set(0,0,0.38268f, 0.923879f);

            ship.transform.localRotation = Quaternion.RotateTowards(ship.transform.localRotation,final, 0.25f);
            x.GetComponent<MeshRenderer>().enabled = true;
            y.GetComponent<MeshRenderer>().enabled = true;
            z.GetComponent<MeshRenderer>().enabled = true;
            x_a.GetComponent<MeshRenderer>().enabled = true;
            y_a.GetComponent<MeshRenderer>().enabled = true;
            z_a.GetComponent<MeshRenderer>().enabled = true;
        }
        if (count % 18 == 14)
        {
            prod_flag = false;
            multiplicant_2.enabled = true;

            final.Set(0.3535533f, 0.1464466f, 0.3535533f, 0.8535533f);
            ship.transform.localRotation = Quaternion.RotateTowards(ship.transform.localRotation, final, 0.25f);
            x.GetComponent<MeshRenderer>().enabled = true;
            y.GetComponent<MeshRenderer>().enabled = true;
            z.GetComponent<MeshRenderer>().enabled = true;
            x_a.GetComponent<MeshRenderer>().enabled = true;
            y_a.GetComponent<MeshRenderer>().enabled = true;
            z_a.GetComponent<MeshRenderer>().enabled = true;
        }
        if (count % 18 == 16)
        {
            final.Set(0.3535533f, 0.1464466f, 0.3535533f, 0.8535533f);
            
            product.enabled = true;
            ship.transform.localRotation = Quaternion.RotateTowards(ship.transform.localRotation, final, 0.20f);
            x.GetComponent<MeshRenderer>().enabled = true;
            y.GetComponent<MeshRenderer>().enabled = true;
            z.GetComponent<MeshRenderer>().enabled = true;
            x_a.GetComponent<MeshRenderer>().enabled = true;
            y_a.GetComponent<MeshRenderer>().enabled = true;
            z_a.GetComponent<MeshRenderer>().enabled = true;
        }



        if (Input.GetButton("Start"))
        {
            welcome.enabled = true;
            ship.transform.position = init;
            intro.enabled = false;
            def.enabled = false;
            quat1.enabled = false;
            quat2.enabled = false;
            quat3.enabled = false;
            multiplication.enabled = false;
            multiplicant_1.enabled = false;
            multiplicant_2.enabled = false;
            product.enabled = false;
            instructions.enabled = true;
            goTo.enabled = false;
            prod_flag = false;
          count = 0;
       } 

	}

    IEnumerator delay()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("quaternion_Game2_0");
    }
}
