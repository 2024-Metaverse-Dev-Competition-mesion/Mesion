using UnityEngine;

public class PlayerMovement1 : MonoBehaviour
{
    public float speed = 5.0f; // 이동 속도
    public float mouseSensitivity = 100.0f; // 마우스 민감도
    public GameObject uiPanel; // Space 키를 눌렀을 때 표시할 UI 패널
    private float xRotation = 0f;
    public Transform petTransform; // 펫의 Transform

    private bool isPetFollowing = false; // 펫이 플레이어를 바라보는지 여부

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서를 잠금 상태로 설정

        // Rigidbody 설정
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.freezeRotation = true; // 회전 고정
        }
        // UI 패널을 초기에는 비활성화 상태로 설정
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
    }

    void Update()
    {
        CheckPetInteraction(); // 펫과의 상호작용 체크
        if (!isPetFollowing)
        {
            MovePlayer();
            RotatePlayer();
        }
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

    void CheckPetInteraction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vector3.Distance(transform.position, petTransform.position) < 5f)
            {
                ToggleUIPanel();
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

    void ToggleUIPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }
    }
}
