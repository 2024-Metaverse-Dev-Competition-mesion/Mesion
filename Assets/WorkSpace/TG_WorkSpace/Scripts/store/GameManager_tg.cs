using UnityEngine;
using UnityEngine.UI;  // UI 관련 클래스를 사용하기 위해 필요
using System.Collections.Generic;  // List<>를 사용하기 위해 추가

public class GameManager_tg : MonoBehaviour
{
    public static GameManager_tg Instance;  // Singleton 인스턴스

    public int currentCurrency = 1000;  // 초기 통화 설정
    public List<Item> playerInventory = new List<Item>();  // 플레이어 인벤토리
    public Text currencyText;  // UI 텍스트 참조

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
        // 초기 통화 값을 UI에 업데이트
        UpdateCurrencyUI();
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
    void UpdateCurrencyUI()
    {
        if (currencyText != null)
        {
            currencyText.text = "Currency: " + currentCurrency.ToString();
        }
    }
}
