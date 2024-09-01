using UnityEngine;

public class Test_Panel_Manager : MonoBehaviour
{
    // Reference to the other GameObject you want to activate
    public GameObject Panel1;
    public GameObject Panel2;
    public GameObject Panel3;

    public void OnButtonClick()
    {
        Panel1.SetActive(false);
        Panel2.SetActive(true);
    }

    public void OnButtonClick2()
    {
        Panel1.SetActive(true);
        Panel2.SetActive(false);
    }
    
    public void OnButtonClick3()
    {
        Panel2.SetActive(false);
        Panel3.SetActive(true);
    }
}
