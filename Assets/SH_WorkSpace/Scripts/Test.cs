using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test : MonoBehaviour
{
    public float moveSpeed = 100.0f;
    public float ascendSpeed = 100.0f;
    public float rotationSpeed = 100.0f;
    public Camera droneCamera;
    public Camera controllerCamera;

    public InputActionProperty leftMoveAction;
    public InputActionProperty rightMoveAction;
    public InputActionProperty leftRotateAction;
    public InputActionProperty ascendAction;
    public InputActionProperty descendAction;
    public InputActionProperty switchCameraAction;

    public Rigidbody rb;
    private bool isDroneView = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        SwitchCameraView(isDroneView);
    }

	void Update()
	{
		HandleMovement();
		HandleRotation();
		HandleCameraSwitch();
	}

	void HandleMovement()
	{
		// 왼쪽 조이스틱 입력 값 읽기
		Vector2 leftMoveValue = leftMoveAction.action.ReadValue<Vector2>();
		// 오른쪽 조이스틱 입력 값 읽기 (이동)
		Vector2 rightMoveValue = rightMoveAction.action.ReadValue<Vector2>();

		// 오른쪽 조이스틱을 통한 앞뒤 이동 및 좌우 이동
		float moveForwardBackward = rightMoveValue.y * moveSpeed;
		float moveLeftRight = rightMoveValue.x * moveSpeed;

		// 왼쪽 조이스틱을 통한 상승/하강
		float moveUpDown = leftMoveValue.y * ascendSpeed;

		// 왼쪽 조이스틱을 통한 좌우 회전
		float rotationAmount = leftMoveValue.x * rotationSpeed;

		// 이동 벡터 계산
		Vector3 movement = new Vector3(moveLeftRight, moveUpDown, moveForwardBackward);
		Vector3 movementDirection = transform.TransformDirection(movement);

		// 드론의 회전 설정
		transform.Rotate(0, rotationAmount * Time.deltaTime, 0);

		// 드론의 속도 설정
		rb.velocity = movementDirection * Time.deltaTime;
	}

	void HandleRotation()
	{
		Vector2 leftRotateValue = leftRotateAction.action.ReadValue<Vector2>();

		float rotateYaw = leftRotateValue.x * rotationSpeed * Time.deltaTime;

		transform.Rotate(0f, rotateYaw, 0f);
	}

	void HandleCameraSwitch()
	{
		if (switchCameraAction.action.WasPressedThisFrame())
		{
			isDroneView = !isDroneView;
			SwitchCameraView(isDroneView);
		}
	}

	void SwitchCameraView(bool isDroneView)
	{
		droneCamera.gameObject.SetActive(isDroneView);
		controllerCamera.gameObject.SetActive(!isDroneView);
	}
}
