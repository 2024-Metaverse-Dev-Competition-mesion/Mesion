using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneController : MonoBehaviour
{
	public float moveSpeed = 100.0f;
	public float ascendSpeed = 100.0f;
	public float rotationSpeed = 100.0f;
	public Camera droneCamera;
	public Camera controllerCamera;

	private Rigidbody rb;
	private bool isDroneView = false;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		SwitchCameraView(isDroneView);
	}

	// Update is called once per frame
	void Update()
	{
		HandleMovement();
		HandleRotation();
		HandleCameraSwitch();
	}

	void HandleMovement()
	{
		float moveForwardBackward = 0f;
		float moveLeftRight = 0f;
		float moveUpDown = 0f;

		if (Input.GetKey(KeyCode.W))
		{
			Debug.Log("Go Up");
			moveUpDown = ascendSpeed;
		}

		if (Input.GetKey(KeyCode.S))
		{
			Debug.Log("Go Down");
			moveUpDown = -ascendSpeed;
		}

		if (Input.GetKey(KeyCode.UpArrow))
		{
			Debug.Log("Go Straight");
			moveForwardBackward = moveSpeed;
		}

		if (Input.GetKey(KeyCode.DownArrow))
		{
			Debug.Log("Go Back");
			moveForwardBackward = -moveSpeed;
		}

		if (Input.GetKey(KeyCode.RightArrow))
		{
			Debug.Log("Go Right");
			moveLeftRight = -moveSpeed;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			Debug.Log("Go Left");
			moveLeftRight = moveSpeed;
		}
		/*Vector3 movement = new Vector3(moveLeftRight, moveUpDown, moveForwardBackward) * Time.deltaTime;
		rb.velocity = movement;*/

		Vector3 movement = new Vector3(moveLeftRight, moveForwardBackward, moveUpDown);
        Vector3 movementDirection = transform.TransformDirection(movement);

        rb.velocity = movementDirection * Time.deltaTime;
	}
	void HandleRotation()
	{
		float rotateRudder = 0f;

		if (Input.GetKey(KeyCode.D))
		{
			Debug.Log("Rotate Right");
			rotateRudder = rotationSpeed * Time.deltaTime;
		}

		if (Input.GetKey(KeyCode.A))
		{
			Debug.Log("Rotate Left");
			rotateRudder = -rotationSpeed * Time.deltaTime;
		}

		transform.Rotate(0f, 0f, rotateRudder);
	}

	void HandleCameraSwitch()
	{
		if (Input.GetKeyDown(KeyCode.F))
		{
			isDroneView = !isDroneView;
			Debug.Log(isDroneView);
			SwitchCameraView(isDroneView);
		}
	}

	void SwitchCameraView(bool isDroneView)
	{
		droneCamera.gameObject.SetActive(isDroneView);
		controllerCamera.gameObject.SetActive(!isDroneView);
	}
}
