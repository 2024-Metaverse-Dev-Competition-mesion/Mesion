using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class GameManager_tg : MonoBehaviour
{
    public SaveManager saveManager;
    public LoadManager loadManager;

    [SerializeField]
    private InputActionReference saveAction;
    [SerializeField]
    private InputActionReference loadAction;

    private void OnEnable()
    {
        saveAction.action.Enable();
        loadAction.action.Enable();
    }

    private void OnDisable()
    {
        saveAction.action.Disable();
        loadAction.action.Disable();
    }

    void Update()
    {
        if (saveAction.action.triggered)
        {
            saveManager.Save();
        }

        if (loadAction.action.triggered)
        {
            loadManager.Load();
        }
    }
}