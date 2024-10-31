using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 관련 API 사용을 위해 필요
using System.Collections.Generic;

public class GameManager_tg : MonoBehaviour
{
    public static GameManager_tg Instance;  // Singleton 인스턴스

    public int currentCurrency = 1000;  // 초기 통화 설정
    public List<Item> playerInventory = new List<Item>();  // 플레이어 인벤토리
    public Text currencyText;  // UI 텍스트 참조
    public Button updateCurrencyButton;  // UpdateCurrencyUI를 호출할 버튼

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // 이 오브젝트가 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject);  // 이미 인스턴스가 존재하면 이 오브젝트를 파괴
        }
    }

    void Start()
    {
        // 씬이 전환되면 새로운 씬의 UI를 찾습니다.
        FindUIElements();

        // 초기 통화 값을 UI에 업데이트
        UpdateCurrencyUI();

        // 씬이 변경될 때마다 UI를 다시 설정하는 이벤트를 추가
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // 씬이 로드될 때마다 호출되는 이벤트 핸들러
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FindUIElements();
        UpdateCurrencyUI();  // 씬 전환 시에도 UI 업데이트
    }

    // 통화 추가 메서드
    public void AddCurrency(int amount)
    {
        currentCurrency += amount;
        Debug.Log("Added " + amount + " currency. Current total: " + currentCurrency);
        UpdateCurrencyUI();  // 통화 변경 시 UI 업데이트
    }

    // 통화 사용 메서드
    public bool SpendCurrency(int amount)
    {
        if (currentCurrency >= amount)
        {
            currentCurrency -= amount;
            Debug.Log("Spent " + amount + " currency. Current total: " + currentCurrency);
            UpdateCurrencyUI();  // 통화 변경 시 UI 업데이트
            return true;
        }
        else
        {
            Debug.Log("Not enough currency. Current total: " + currentCurrency);
            return false;
        }
    }

    // 아이템 추가 메서드
    public void AddItem(Item item)
    {
        playerInventory.Add(item);
        Debug.Log("Added " + item.itemName + " to inventory.");
    }

    // 아이템 제거 메서드
    public void RemoveItem(Item item)
    {
        if (playerInventory.Contains(item))
        {
            playerInventory.Remove(item);
            Debug.Log("Removed " + item.itemName + " from inventory.");
        }
        else
        {
            Debug.Log("Item not found in inventory.");
        }
    }

    // 통화 UI 업데이트 메서드
    public void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = currentCurrency.ToString();
        }
    }

    // 새로운 씬에서 UI 요소를 찾는 메서드
    private void FindUIElements()
    {
        // 씬 안의 "CurrencyText"라는 이름을 가진 오브젝트를 찾아 연결
        //if (currencyText == null)
        //{
            currencyText = GameObject.Find("Currency_Text_")?.GetComponent<Text>();
        //}

        // 씬 안의 "UpdateCurrencyButton"라는 이름을 가진 버튼을 찾아 연결
        if (updateCurrencyButton == null)
        {
            updateCurrencyButton = GameObject.Find("Inventory Open Button")?.GetComponent<Button>();
            if (updateCurrencyButton != null)
            {
                updateCurrencyButton.onClick.RemoveAllListeners();  // 기존 리스너 제거
                updateCurrencyButton.onClick.AddListener(UpdateCurrencyUI);  // 새 리스너 추가
                currencyText = GameObject.Find("Currency_Text_")?.GetComponent<Text>();
            }
        }
    }

    // 씬이 파괴될 때 이벤트 리스너를 제거
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
