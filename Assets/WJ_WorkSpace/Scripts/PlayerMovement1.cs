using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도
    public float mouseSensitivity = 100.0f; // 마우스 민감도

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 잠금 상태로 설정

        // Rigidbody 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true; // 회전 고정
        }
    }

    void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        // WASD 키 입력 처리
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // 이동 벡터 계산
        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        movement = movement.normalized * speed * Time.deltaTime;

        // Rigidbody를 사용한 이동
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            // Rigidbody가 없을 경우 기본 이동 방식
            transform.Translate(movement, Space.World);
        }
    }

    void RotatePlayer()
    {
        // 마우스 입력 처리
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 카메라 회전 제한

        // 카메라 X축 회전
        Camera.main.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // 플레이어 Y축 회전
        transform.Rotate(Vector3.up * mouseX);
    }
}
