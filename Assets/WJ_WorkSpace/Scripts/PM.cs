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
    public GameObject panel;
    Vector3 velocity;
    bool isGrounded;

    private void Start()
    {
        _inputData = GetComponent<InputData>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isPetFollowing){
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
                    panel.SetActive(true);

                    isPetFollowing = !isPetFollowing;

                    if (isPetFollowing)
                    {
                        petTransform.LookAt(transform);
                        petTransform.GetComponent<PetRoam>().SetFollowingPlayer(true);
                    }
                    else
                    {
                        petTransform.GetComponent<PetRoam>().SetFollowingPlayer(false);
                    }
                }
            }
        }
    }

    public void OnCancel()
    {
        panel.SetActive(false); // Deactivate Panel A
        isPetFollowing = false;
    }
}
