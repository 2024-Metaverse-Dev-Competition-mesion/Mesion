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
    [SerializeField]
    private GameObject m_CusualPanel; // �κ��丮 �г�
    [SerializeField]
    private GameObject m_RealityPanel; // �κ��丮 �г�
    [SerializeField]
    private GameObject m_QualificationsPanel;
    // Start is called before the first frame update
    void Start()
    {
        m_InventoryPanel.SetActive(false); // ó���� ��ư�� ��Ȱ��ȭ�� ���
        m_CusualPanel.SetActive(false);
        m_RealityPanel.SetActive(false);
        m_QualificationsPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void PushInventoryButton()
    {
        m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);
    }

    public void PushCusualButton()
    {
        m_CusualPanel.SetActive(!m_CusualPanel.activeSelf);
    }

    public void PushRealityButton()
    {
        m_RealityPanel.SetActive(!m_RealityPanel.activeSelf);
    }

    public void PushQualificationsButton()
    {
        m_QualificationsPanel.SetActive(!m_QualificationsPanel.activeSelf);
    }

}