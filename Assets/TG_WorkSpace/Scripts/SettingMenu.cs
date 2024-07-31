using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.UI;
using static GoalManager;

public class SettingMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject m_InventoryPanel; // 인벤토리 패널
    [SerializeField]
    private GameObject m_JobPanel; // 인벤토리 패널
    [SerializeField]
    private GameObject m_PetPanel; // 인벤토리 패널
    [SerializeField]
    private GameObject m_PassportPanel;
    // Start is called before the first frame update
    void Start()
    {
        m_InventoryPanel.SetActive(false); // 처음에 버튼을 비활성화할 경우
        m_JobPanel.SetActive(false);
        m_PetPanel.SetActive(false);
        m_PassportPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PushInventoryButton()
    {
        // 인벤토리 눌림
        m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);

    }
    public void ExitInventoryButton()
    {
        // 인벤토리 눌림
        m_InventoryPanel.SetActive(false);

    }
    public void PushJobButton()
    {
        // 인벤토리 눌림
        m_JobPanel.SetActive(!m_JobPanel.activeSelf);

    }
    public void ExitJobButton()
    {
        // 인벤토리 눌림
        m_JobPanel.SetActive(false);

    }
    public void PushPetButton()
    {
        // 인벤토리 눌림
        m_PetPanel.SetActive(!m_PetPanel.activeSelf);

    }
    public void ExitPetButton()
    {
        // 인벤토리 눌림
        m_PetPanel.SetActive(false);

    }
    public void PushPassportButton()
    {
        // 인벤토리 눌림
        m_PassportPanel.SetActive(!m_PassportPanel.activeSelf);

    }
    public void ExitPassportButton()
    {
        // 인벤토리 눌림
        m_PassportPanel.SetActive(false);

    }
}