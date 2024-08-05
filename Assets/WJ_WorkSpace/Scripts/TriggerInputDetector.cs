using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;
using TMPro;

public class TriggerInputDetector : MonoBehaviour
{
    private InputData _inputData;
    private float triggerValue;
    public GameObject panel;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        Debug.Log("Started inputData: " + _inputData);
    }
    // Update is called once per frame
    void Update()
    {

        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton))
        {
            if (Abutton == true)
            {
                panel.SetActive(true);
            }
            Debug.Log("A button: " + Abutton);
        }
    }
}
