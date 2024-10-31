using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragHandler : MonoBehaviour
{
	private Vector3 offset;
	private bool isDragging = false;
	private Vector3 startPosition;
	private Transform originalParent;
	private DroneAssemblyManager assemblyManager;

	public float dropTolerance = 0.5f;  // ��� ������ ���� ���� ����

	void Start()
	{
		startPosition = transform.position;
		originalParent = transform.parent;

		assemblyManager = FindObjectOfType<DroneAssemblyManager>();

		if (assemblyManager == null)
		{
			Debug.LogError("DroneAssemblyManager�� ���� �����ϴ�.");
		}
	}

	void OnMouseDown()
	{
		offset = gameObject.transform.position - GetMouseWorldPos();
		isDragging = true;
		Debug.Log("OnMouseDown: Dragging started on " + gameObject.name);
	}

	void OnMouseDrag()
	{
		if (isDragging)
		{
			transform.position = GetMouseWorldPos() + offset;
		}
	}

	void OnMouseUp()
	{
		isDragging = false;
		Debug.Log("OnMouseUp: Dragging ended on " + gameObject.name);

		// DropZone�� �浹 ���θ� Ȯ��
		RaycastHit hit;
		if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			DropHandler dropHandler = hit.collider.GetComponent<DropHandler>();
			if (dropHandler != null && dropHandler.targetTag == gameObject.tag)
			{
				// ��������� �Ÿ��� ���
				float distanceToDropZone = Vector3.Distance(transform.position, hit.transform.position);

				// ��ü�� ��ġ�� ������� ���� ���� ���� ������ ��� ����
				if (distanceToDropZone <= dropTolerance)
				{
					// �ùٸ� DropZone�� ��ӵ� ���
					transform.position = hit.transform.position;
					transform.SetParent(hit.transform);
					Debug.Log("Dropped at " + hit.collider.name);
					assemblyManager.PlaceComponent(gameObject.tag);
				}
				else
				{
					// ���� ������ ���� ��� ���� ��ġ�� ���ư���
					Debug.Log("Dropped too far from drop zone, returning to start position.");
					transform.position = startPosition;
					transform.SetParent(originalParent);
				}
			}
			else
			{
				// �ùٸ��� ���� DropZone�� ��ӵ� ���
				transform.position = startPosition;
				transform.SetParent(originalParent);
				Debug.Log("Dropped at invalid position, returning to start position.");
			}
		}
		else
		{
			// �ƹ��͵� �浹���� ���� ��� ���� ��ġ�� ���ư���
			transform.position = startPosition;
			transform.SetParent(originalParent);
			Debug.Log("Dropped outside any valid zone, returning to start position.");
		}
	}

	private Vector3 GetMouseWorldPos()
	{
		Vector3 mousePoint = Input.mousePosition;
		mousePoint.z = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;

		return Camera.main.ScreenToWorldPoint(mousePoint);
	}
}
