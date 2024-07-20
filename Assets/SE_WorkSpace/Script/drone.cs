//using UnityEngine;
//using UnityEngine.XR;
//using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem;
//using InputCommonUsages = UnityEngine.InputSystem.CommonUsages;

//public class Drone : MonoBehaviour
//{
//    // 드론 조종
//    // 왼쪽 조이스틱으로 좌,우, 앞,뒤 이동, 오른쪽 좌/우 - 회전, 위/아래 - 상승, 하강
//    public float speed = 5.0f;
//    public float rotationSpeed = 100.0f;
//    public float ascentSpeed = 3.0f;

//    // 씨앗 뿌리기
//    public GameObject seedPrefab; // 씨앗 프리팹
//    public Transform seedSpawnPoint; // 씨앗이 생성될 위치
//    public float seedDropInterval = 0.5f; // 씨앗 생성 간격
//    public GameObject waterPrefab; // 물 뿌리기 프리팹
//    public Transform waterSpawnPoint; // 물 뿌리기 프리팹이 생성될 위치
//    public Camera droneCamera; // 드론에 부착된 카메라
//    public Camera playerCamera; // 플레이어의 기본 카메라

//    private bool isDroppingSeeds = false; // 씨앗 뿌리기 상태
//    private float nextDropTime = 0.0f; // 다음 씨앗 생성 시간

//    private Rigidbody rb;
//    public XRController rightController;
//    public XRController leftController;
//    private GameObject activeWaterObject; // 활성화된 물 뿌리기 프리팹

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();

//        // 기본 카메라를 활성화하고 드론 카메라는 비활성화
//        playerCamera.enabled = true;
//        droneCamera.enabled = false;
//    }

//    void Update()
//    {
//        // 왼쪽 컨트롤러의 이동 입력 값 읽기
//        Vector2 leftStick = leftController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 leftStickValue) ? leftStickValue : Vector2.zero;
//        float horizontal = leftStick.x;
//        float vertical = leftStick.y;

//        // 드론의 이동
//        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
//        rb.MovePosition(transform.position + movement);

//        // 오른쪽 컨트롤러의 회전 및 상승/하강 입력 값 읽기
//        Vector2 rightStick = rightController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 rightStickValue) ? rightStickValue : Vector2.zero;

//        // 드론의 회전
//        float rotation = rightStick.x * rotationSpeed * Time.deltaTime;
//        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));

//        // 드론의 상승 및 하강
//        if (rightStick.y > 0)
//        {
//            rb.AddForce(Vector3.up * ascentSpeed, ForceMode.Acceleration);
//        }
//        else if (rightStick.y < 0)
//        {
//            rb.AddForce(Vector3.down * ascentSpeed, ForceMode.Acceleration);
//        }

//        // A 버튼 입력 값 읽기
//        bool isAButtonPressed = leftController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool aButtonValue) && aButtonValue;
//        if (isAButtonPressed)
//        {
//            if (!isDroppingSeeds)
//            {
//                isDroppingSeeds = true;
//                DropSeed(); // 씨앗을 즉시 생성
//            }
//        }
//        else
//        {
//            if (isDroppingSeeds)
//            {
//                isDroppingSeeds = false;
//            }
//        }

//        // 씨앗 뿌리기 상태가 참이면 간격에 따라 씨앗을 생성합니다.
//        if (isDroppingSeeds && Time.time >= nextDropTime)
//        {
//            DropSeed();
//            nextDropTime = Time.time + seedDropInterval;
//        }

//        // B 버튼 입력 값 읽기 (물 뿌리기)
//        bool isBButtonPressed = rightController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool bButtonValue) && bButtonValue;
//        if (isBButtonPressed)
//        {
//            if (activeWaterObject == null)
//            {
//                activeWaterObject = Instantiate(waterPrefab, waterSpawnPoint.position, waterSpawnPoint.rotation, waterSpawnPoint);
//            }
//        }
//        else
//        {
//            if (activeWaterObject != null)
//            {
//                Destroy(activeWaterObject);
//                activeWaterObject = null;
//            }
//        }

//        // X 버튼 입력 값 읽기 (시점 변경)
//        bool isXButtonPressed = rightController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool xButtonValue) && xButtonValue;
//        if (isXButtonPressed)
//        {
//            ToggleCameraView();
//        }
//    }

//    void DropSeed()
//    {
//        GameObject seed = Instantiate(seedPrefab, seedSpawnPoint.position, Quaternion.identity);
//        Rigidbody seedRb = seed.AddComponent<Rigidbody>(); // Rigidbody 추가
//        seedRb.useGravity = true; // 중력 사용
//    }

//    void ToggleCameraView()
//    {
//        playerCamera.enabled = !playerCamera.enabled;
//        droneCamera.enabled = !droneCamera.enabled;
//    }
//}
