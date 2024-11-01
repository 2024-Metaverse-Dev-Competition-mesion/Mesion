using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PM : MonoBehaviour
{
    public CharacterController controller;
    public Transform xrCameraTransform;

    public float speed;
    public float gravity;
    public Transform petTransform;
    private bool isPetFollowing = false;
    private InputData _inputData;
    private float triggerValue;
    public GameObject panelA;
    public GameObject panelB;
    public GameObject panelC;
    Vector3 velocity;
    bool isGrounded;

    private PetRoam petRoam; // Reference to the PetRoam script

    private void Start()
    {
        _inputData = GetComponent<InputData>();
        petRoam = petTransform.GetComponent<PetRoam>(); // Get the PetRoam component
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPetFollowing)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            // 카메라의 전방 방향을 기준으로 이동 벡터 계산
            Vector3 forward = xrCameraTransform.forward;
            forward.y = 0; // 수평 이동을 위해 Y축을 고정
            forward.Normalize();

            Vector3 right = xrCameraTransform.right;
            right.y = 0; // 수평 이동을 위해 Y축을 고정
            right.Normalize();

            // 입력에 따라 이동 벡터 결정
            Vector3 move = right * x + forward * z;

            controller.Move(move * speed * Time.deltaTime);

            // 중력 적용
            velocity.y += gravity * Time.deltaTime;

            // 중력 이동 적용
            controller.Move(velocity * Time.deltaTime);
        }

        CheckPetInteraction();
    }

    void CheckPetInteraction()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool Abutton))
        {
            if (Abutton == true)
            {
                if (Vector3.Distance(transform.position, petTransform.position) < 5f)
                {
                    panelA.SetActive(true);

                    if (isPetFollowing == false)
                    {
                        isPetFollowing = !isPetFollowing;
                    }

                    if (isPetFollowing)
                    {
                        petTransform.LookAt(transform);
                        petRoam.SetFollowingPlayer(true);
                    }
                    else
                    {
                        petRoam.SetFollowingPlayer(false);
                    }
                }
            }
        }
    }

    public void OnCancel()
    {
        panelA.SetActive(false);
        isPetFollowing = false;
        petRoam.SetFollowingPlayer(false); // Ensure pet stops following
    }

    public void OnCancel2()
    {
        panelB.SetActive(false);
        panelC.SetActive(false);
        isPetFollowing = false;
        petRoam.SetFollowingPlayer(false); // Ensure pet stops following
    }
}
