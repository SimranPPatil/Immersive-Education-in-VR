using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//using VRStandardAssets.Utils;


public class choose : MonoBehaviour {
    [SerializeField]
    private Canvas m_Canvas;
    //[SerializeField]
    //private VRInteractiveItem m_InteractiveItem;
    [SerializeField]
    private Color m_NewColor;
    [SerializeField]
    private Text m_Text;
    [SerializeField]
    private Image m_Image;

    public Canvas kd, quat;

    // Use this for initialization
    void Start () {
        m_NewColor = new Color(0.66f, 0.56f, 0.50f, 1);

    }
	
	// Update is called once per frame
	void Update () {
        if (kd.GetComponent<Canvas>().enabled && quat.GetComponent<Canvas>().enabled)
        {
            if (Input.GetButton("X"))
                SceneManager.LoadScene("LowPolyScene");
            if (Input.GetButton("Y"))
                SceneManager.LoadScene("VideoQuatDemo");
        }
            
        if (Input.GetButton("Select"))
        {
            if (m_Image.color == m_NewColor)
            {
                m_Canvas.enabled = false;
                m_Image.enabled = false;
            }
        }
    }
}
