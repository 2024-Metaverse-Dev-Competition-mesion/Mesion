using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit.Samples.StarterAssets;
using TMPro;
using LazyFollow = UnityEngine.XR.Interaction.Toolkit.UI.LazyFollow;

// Goal ����ü ����
public struct Goal
{
    public GoalManager.OnboardingGoals CurrentGoal; // ���� ��ǥ
    public bool Completed; // �Ϸ� ����

    public Goal(GoalManager.OnboardingGoals goal)
    {
        CurrentGoal = goal;
        Completed = false; // �ʱ� ���´� �Ϸ���� ����
    }
}

public class GoalManager : MonoBehaviour
{
    // �º��� ��ǥ�� �����ϴ� ������
    public enum OnboardingGoals
    {
        Empty,
        FindSurfaces,
        SelectWorld,
        EnterWorld,
        //TapSurface,
    }

    Queue<Goal> m_OnboardingGoals; // �º��� ��ǥ�� ��� ť
    Goal m_CurrentGoal; // ���� ��ǥ
    bool m_AllGoalsFinished; // ��� ��ǥ�� �Ϸ�Ǿ����� ����
    int m_SurfacesTapped; // �ǵ� ǥ���� ��
    int m_CurrentGoalIndex = 0; // ���� ��ǥ �ε���

    [Serializable]
    class Step
    {
        [SerializeField]
        public GameObject stepObject; // �ܰ� ��ü

    }

    [SerializeField]
    List<Step> m_StepList = new List<Step>(); // �ܰ� ����Ʈ

    [SerializeField]
    public TextMeshProUGUI m_StepButtonTextField; // �ܰ� ��ư �ؽ�Ʈ �ʵ�

    [SerializeField]
    public GameObject m_SkipButton; // �ǳʶٱ� ��ư

    [SerializeField]
    public GameObject m_StartNewButton; // ����� ��ư

    [SerializeField]
    public GameObject m_LoadButton; // �ҷ����� ��ư

    [SerializeField]
    public GameObject m_ExitButton; // ���� ��ư

    [SerializeField]
    GameObject m_LearnButton; // �н� ��ư

    [SerializeField]
    GameObject m_LearnModal; // �н� ���

    [SerializeField]
    Button m_LearnModalButton; // �н� ��� ��ư

    [SerializeField]
    GameObject m_CoachingUIParent; // ��Ī UI �θ� ��ü

    [SerializeField]
    FadeMaterial m_FadeMaterial; // ���̵� ����

    [SerializeField]
    Toggle m_PassthroughToggle; // �н����� ���

    [SerializeField]
    LazyFollow m_GoalPanelLazyFollow; // ��ǥ �г� LazyFollow

    [SerializeField]
    ARPlaneManager m_ARPlaneManager; // AR ��� ������

    Vector3 m_TargetOffset = new Vector3(-.5f, -.25f, 1.5f); // ��ǥ ������

    void Start()
    {
        // �º��� ��ǥ �ʱ�ȭ
        m_OnboardingGoals = new Queue<Goal>();
        var welcomeGoal = new Goal(OnboardingGoals.Empty);
        var findSurfaceGoal = new Goal(OnboardingGoals.FindSurfaces);
        var selectWorldGoal = new Goal(OnboardingGoals.SelectWorld);
        var enterWorldGoal = new Goal(OnboardingGoals.EnterWorld);

        m_OnboardingGoals.Enqueue(welcomeGoal);
        m_OnboardingGoals.Enqueue(findSurfaceGoal);
        m_OnboardingGoals.Enqueue(selectWorldGoal);
        m_OnboardingGoals.Enqueue(enterWorldGoal);

        //��ǥ �ϳ� ����
        m_CurrentGoal = m_OnboardingGoals.Dequeue();

        //�ʱ�ȭ
        if (m_FadeMaterial != null)
        {
            m_FadeMaterial.FadeSkybox(false);

            if (m_PassthroughToggle != null)
                m_PassthroughToggle.isOn = false;
        }

        if (m_LearnButton != null)
        {
            m_LearnButton.GetComponent<Button>().onClick.AddListener(OpenModal); ;
            m_LearnButton.SetActive(false);
        }

        if (m_LearnModal != null)
        {
            m_LearnModal.transform.localScale = Vector3.zero;
        }

        if (m_LearnModalButton != null)
        {
            m_LearnModalButton.onClick.AddListener(CloseModal);
        }
    }

    // ��� ����
    void OpenModal()
    {
        if (m_LearnModal != null)
        {
            m_LearnModal.transform.localScale = Vector3.one;
        }
    }

    // ��� �ݱ�
    void CloseModal()
    {
        if (m_LearnModal != null)
        {
            m_LearnModal.transform.localScale = Vector3.zero;
        }
    }

    void Update()
    {
        if (!m_AllGoalsFinished)
        {
            ProcessGoals();
        }

        // ����� �Է� (����Ƽ ������)
#if UNITY_EDITOR
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            CompleteGoal();
        }
#endif
    }

    // ��ǥ ó��
    void ProcessGoals()
    {
        if (!m_CurrentGoal.Completed)//False
        {
            switch (m_CurrentGoal.CurrentGoal)
            {
                case OnboardingGoals.Empty:
                    m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.Follow;
                    break;
                case OnboardingGoals.FindSurfaces:
                    m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.Follow;
                    break;
                case OnboardingGoals.SelectWorld:
                    m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.Follow;
                    break;
                case OnboardingGoals.EnterWorld:
                    m_GoalPanelLazyFollow.positionFollowMode = LazyFollow.PositionFollowMode.None;
                    break;
            }
        }
    }

    // ��ǥ �Ϸ�
    void CompleteGoal()
    {
        m_CurrentGoal.Completed = true;
        m_CurrentGoalIndex++;
        // ��ǥ�� ���� �ִ� ���
        if (m_OnboardingGoals.Count > 0)
        {
            // ť���� ���� ��ǥ�� ������
            m_CurrentGoal = m_OnboardingGoals.Dequeue();

            // ���� �ܰ��� ��ü�� ��Ȱ��ȭ
            m_StepList[m_CurrentGoalIndex - 1].stepObject.SetActive(false);

            // ���� �ܰ��� ��ü�� Ȱ��ȭ
            m_StepList[m_CurrentGoalIndex].stepObject.SetActive(true);
        }
        else
        {
            // ��� ��ǥ�� �Ϸ�� ���
            m_AllGoalsFinished = true;
            ForceEndAllGoals();
        }

        // ���� ��ǥ�� OnboardingGoals.FindSurfaces�� ���
        if (m_CurrentGoal.CurrentGoal == OnboardingGoals.FindSurfaces)
        {
            if (m_FadeMaterial != null)
                m_FadeMaterial.FadeSkybox(true);

            if (m_PassthroughToggle != null)
                m_PassthroughToggle.isOn = true;

            if (m_LearnButton != null)
            {
                m_LearnButton.SetActive(true);
            }

            StartCoroutine(TurnOnPlanes());
        }
    }

    // ��� Ȱ��ȭ �ڷ�ƾ
    public IEnumerator TurnOnPlanes()
    {
        yield return new WaitForSeconds(1f);
        m_ARPlaneManager.enabled = true;
    }

    // ��ǥ ���� �Ϸ�
    public void ForceCompleteGoal()
    {
        CompleteGoal();
    }

    // ��� ��ǥ ���� ����
    public void ForceEndAllGoals()
    {
        m_CoachingUIParent.transform.localScale = Vector3.zero;

        if (m_FadeMaterial != null)
        {
            m_FadeMaterial.FadeSkybox(true);

            if (m_PassthroughToggle != null)
                m_PassthroughToggle.isOn = true;
        }

        if (m_LearnButton != null)
        {
            m_LearnButton.SetActive(false);
        }

        if (m_LearnModal != null)
        {
            m_LearnModal.transform.localScale = Vector3.zero;
        }

        StartCoroutine(TurnOnPlanes());
    }

    // ��Ī ����
    public void ResetCoaching()
    {
        m_CoachingUIParent.transform.localScale = Vector3.one;

        m_OnboardingGoals.Clear();
        m_OnboardingGoals = new Queue<Goal>();
        var welcomeGoal = new Goal(OnboardingGoals.Empty);
        var findSurfaceGoal = new Goal(OnboardingGoals.FindSurfaces);
        var selectWorldGoal = new Goal(OnboardingGoals.SelectWorld);
        var enterWorldGoal = new Goal(OnboardingGoals.EnterWorld);

        m_OnboardingGoals.Enqueue(welcomeGoal);
        m_OnboardingGoals.Enqueue(findSurfaceGoal);
        m_OnboardingGoals.Enqueue(selectWorldGoal);
        m_OnboardingGoals.Enqueue(enterWorldGoal);

        m_CurrentGoal = m_OnboardingGoals.Dequeue();
        m_AllGoalsFinished = false;

        if (m_LearnButton != null)
        {
            m_LearnButton.SetActive(false);
        }

        if (m_LearnModal != null)
        {
            m_LearnModal.transform.localScale = Vector3.zero;
        }

        m_CurrentGoalIndex = 0;
    }
    // ���α׷� ����
    public void ForceQuitApplication()
    {
        // ���ø����̼� ���� ����
        Application.Quit();

#if UNITY_EDITOR
        // Unity �����Ϳ��� ���� ���� ��� �÷��� ��带 ����
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
