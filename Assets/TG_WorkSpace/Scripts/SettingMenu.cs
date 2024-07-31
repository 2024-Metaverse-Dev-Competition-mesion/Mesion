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
    private GameObject m_JobPanel; // �κ��丮 �г�
    [SerializeField]
    private GameObject m_PetPanel; // �κ��丮 �г�
    [SerializeField]
    private GameObject m_PassportPanel;
    // Start is called before the first frame update
    void Start()
    {
        m_InventoryPanel.SetActive(false); // ó���� ��ư�� ��Ȱ��ȭ�� ���
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
        // �κ��丮 ����
        m_InventoryPanel.SetActive(!m_InventoryPanel.activeSelf);

    }
    public void ExitInventoryButton()
    {
        // �κ��丮 ����
        m_InventoryPanel.SetActive(false);

    }
    public void PushJobButton()
    {
        // �κ��丮 ����
        m_JobPanel.SetActive(!m_JobPanel.activeSelf);

    }
    public void ExitJobButton()
    {
        // �κ��丮 ����
        m_JobPanel.SetActive(false);

    }
    public void PushPetButton()
    {
        // �κ��丮 ����
        m_PetPanel.SetActive(!m_PetPanel.activeSelf);

    }
    public void ExitPetButton()
    {
        // �κ��丮 ����
        m_PetPanel.SetActive(false);

    }
    public void PushPassportButton()
    {
        // �κ��丮 ����
        m_PassportPanel.SetActive(!m_PassportPanel.activeSelf);

    }
    public void ExitPassportButton()
    {
        // �κ��丮 ����
        m_PassportPanel.SetActive(false);

    }
}