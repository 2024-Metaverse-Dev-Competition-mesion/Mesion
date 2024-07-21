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

    // Start is called before the first frame update
    void Start()
    {
        m_InventoryPanel.SetActive(false); // 처음에 버튼을 비활성화할 경우
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PushButton()
    {
        // 인벤토리 눌림
        m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);

    }
    public void ExitButton()
    {
        // 인벤토리 눌림
        m_InventoryPanel.SetActive(false);

    }
}