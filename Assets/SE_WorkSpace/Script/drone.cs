//using UnityEngine;
//using UnityEngine.XR;
//using UnityEngine.XR.Interaction.Toolkit;
//using UnityEngine.InputSystem;
//using InputCommonUsages = UnityEngine.InputSystem.CommonUsages;

//public class Drone : MonoBehaviour
//{
//    // ��� ����
//    // ���� ���̽�ƽ���� ��,��, ��,�� �̵�, ������ ��/�� - ȸ��, ��/�Ʒ� - ���, �ϰ�
//    public float speed = 5.0f;
//    public float rotationSpeed = 100.0f;
//    public float ascentSpeed = 3.0f;

//    // ���� �Ѹ���
//    public GameObject seedPrefab; // ���� ������
//    public Transform seedSpawnPoint; // ������ ������ ��ġ
//    public float seedDropInterval = 0.5f; // ���� ���� ����
//    public GameObject waterPrefab; // �� �Ѹ��� ������
//    public Transform waterSpawnPoint; // �� �Ѹ��� �������� ������ ��ġ
//    public Camera droneCamera; // ��п� ������ ī�޶�
//    public Camera playerCamera; // �÷��̾��� �⺻ ī�޶�

//    private bool isDroppingSeeds = false; // ���� �Ѹ��� ����
//    private float nextDropTime = 0.0f; // ���� ���� ���� �ð�

//    private Rigidbody rb;
//    public XRController rightController;
//    public XRController leftController;
//    private GameObject activeWaterObject; // Ȱ��ȭ�� �� �Ѹ��� ������

//    void Start()
//    {
//        rb = GetComponent<Rigidbody>();

//        // �⺻ ī�޶� Ȱ��ȭ�ϰ� ��� ī�޶�� ��Ȱ��ȭ
//        playerCamera.enabled = true;
//        droneCamera.enabled = false;
//    }

//    void Update()
//    {
//        // ���� ��Ʈ�ѷ��� �̵� �Է� �� �б�
//        Vector2 leftStick = leftController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 leftStickValue) ? leftStickValue : Vector2.zero;
//        float horizontal = leftStick.x;
//        float vertical = leftStick.y;

//        // ����� �̵�
//        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * Time.deltaTime;
//        rb.MovePosition(transform.position + movement);

//        // ������ ��Ʈ�ѷ��� ȸ�� �� ���/�ϰ� �Է� �� �б�
//        Vector2 rightStick = rightController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 rightStickValue) ? rightStickValue : Vector2.zero;

//        // ����� ȸ��
//        float rotation = rightStick.x * rotationSpeed * Time.deltaTime;
//        rb.MoveRotation(rb.rotation * Quaternion.Euler(0, rotation, 0));

//        // ����� ��� �� �ϰ�
//        if (rightStick.y > 0)
//        {
//            rb.AddForce(Vector3.up * ascentSpeed, ForceMode.Acceleration);
//        }
//        else if (rightStick.y < 0)
//        {
//            rb.AddForce(Vector3.down * ascentSpeed, ForceMode.Acceleration);
//        }

//        // A ��ư �Է� �� �б�
//        bool isAButtonPressed = leftController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primaryButton, out bool aButtonValue) && aButtonValue;
//        if (isAButtonPressed)
//        {
//            if (!isDroppingSeeds)
//            {
//                isDroppingSeeds = true;
//                DropSeed(); // ������ ��� ����
//            }
//        }
//        else
//        {
//            if (isDroppingSeeds)
//            {
//                isDroppingSeeds = false;
//            }
//        }

//        // ���� �Ѹ��� ���°� ���̸� ���ݿ� ���� ������ �����մϴ�.
//        if (isDroppingSeeds && Time.time >= nextDropTime)
//        {
//            DropSeed();
//            nextDropTime = Time.time + seedDropInterval;
//        }

//        // B ��ư �Է� �� �б� (�� �Ѹ���)
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

//        // X ��ư �Է� �� �б� (���� ����)
//        bool isXButtonPressed = rightController.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.secondaryButton, out bool xButtonValue) && xButtonValue;
//        if (isXButtonPressed)
//        {
//            ToggleCameraView();
//        }
//    }

//    void DropSeed()
//    {
//        GameObject seed = Instantiate(seedPrefab, seedSpawnPoint.position, Quaternion.identity);
//        Rigidbody seedRb = seed.AddComponent<Rigidbody>(); // Rigidbody �߰�
//        seedRb.useGravity = true; // �߷� ���
//    }

//    void ToggleCameraView()
//    {
//        playerCamera.enabled = !playerCamera.enabled;
//        droneCamera.enabled = !droneCamera.enabled;
//    }
//}
