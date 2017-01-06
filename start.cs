using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class start : MonoBehaviour {
    [SerializeField] private Canvas m_Canvas_welcome;
    [SerializeField] private Canvas m_Canvas_q;
    [SerializeField] private Image m_Image_q;
    [SerializeField] private Image m_Image_k;
    [SerializeField] private Canvas m_Canvas_k;
    bool flag;
    // Use this for initialization
    void Start () {
        flag = false;
        m_Canvas_k.enabled = false;
        m_Canvas_q.enabled = false;
        m_Image_k.enabled = false;
        m_Image_q.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Starter"))
        {
            m_Canvas_welcome.enabled = false;
            flag = true;
        }

        if(flag == true)
        {
            m_Canvas_k.enabled = true;
            m_Canvas_q.enabled = true;
            m_Image_k.enabled = true;
            m_Image_q.enabled = true;
            
            if(m_Canvas_q.transform.position.z < 12 || m_Canvas_k.transform.position.z < 12)
            {
                m_Canvas_k.transform.Translate(0, 0, 9.0f * Time.deltaTime);
                m_Canvas_q.transform.Translate(0, 0, 9.0f  *Time.deltaTime);
            }
            else
                flag = false;
        }
	}
}
