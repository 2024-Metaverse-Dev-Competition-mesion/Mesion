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
    private GameObject m_InventoryPanel; // �κ��丮 �г�

    // Start is called before the first frame update
    void Start()
    {
        m_InventoryPanel.SetActive(false); // ó���� ��ư�� ��Ȱ��ȭ�� ���
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PushButton()
    {
        // �κ��丮 ����
        m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);

    }
    public void ExitButton()
    {
        // �κ��丮 ����
        m_InventoryPanel.SetActive(false);

    }
}