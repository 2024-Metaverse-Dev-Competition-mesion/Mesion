using UnityEngine;
using UnityEngine.UI;

public class Scene1_CurrencyAdder : MonoBehaviour
{
    public Button addCurrencyButton;
    public int amountToAdd = 100;  // 버튼을 누를 때마다 추가될 화폐 양

    void Start()
    {
        addCurrencyButton.onClick.AddListener(AddCurrency);  // 버튼 클릭 리스너 추가
    }

    void AddCurrency()
    {
        GameManager_tg.Instance.AddCurrency(amountToAdd);  // GameManager의 AddCurrency 메서드 호출
        Debug.Log("Scene1: " + amountToAdd + " currency added. Current total: " + GameManager_tg.Instance.currentCurrency);
    }
}
