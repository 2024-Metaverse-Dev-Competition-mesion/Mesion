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
		// ���� ���̽�ƽ �Է� �� �б�
		Vector2 leftMoveValue = leftMoveAction.action.ReadValue<Vector2>();
		// ������ ���̽�ƽ �Է� �� �б� (�̵�)
		Vector2 rightMoveValue = rightMoveAction.action.ReadValue<Vector2>();

		// ������ ���̽�ƽ�� ���� �յ� �̵� �� �¿� �̵�
		float moveForwardBackward = rightMoveValue.y * moveSpeed;
		float moveLeftRight = rightMoveValue.x * moveSpeed;

		// ���� ���̽�ƽ�� ���� ���/�ϰ�
		float moveUpDown = leftMoveValue.y * ascendSpeed;

		// ���� ���̽�ƽ�� ���� �¿� ȸ��
		float rotationAmount = leftMoveValue.x * rotationSpeed;

		// �̵� ���� ���
		Vector3 movement = new Vector3(moveLeftRight, moveUpDown, moveForwardBackward);
		Vector3 movementDirection = transform.TransformDirection(movement);

		// ����� ȸ�� ����
		transform.Rotate(0, rotationAmount * Time.deltaTime, 0);

		// ����� �ӵ� ����
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
