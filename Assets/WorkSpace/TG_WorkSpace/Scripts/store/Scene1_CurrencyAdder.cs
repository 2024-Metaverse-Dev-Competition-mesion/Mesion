using UnityEngine;
using UnityEngine.UI;

public class Scene1_CurrencyAdder : MonoBehaviour
{
    public Button addCurrencyButton;
    public int amountToAdd = 100;  // ��ư�� ���� ������ �߰��� ȭ�� ��

    void Start()
    {
        addCurrencyButton.onClick.AddListener(AddCurrency);  // ��ư Ŭ�� ������ �߰�
    }

    void AddCurrency()
    {
        GameManager_tg.Instance.AddCurrency(amountToAdd);  // GameManager�� AddCurrency �޼��� ȣ��
        Debug.Log("Scene1: " + amountToAdd + " currency added. Current total: " + GameManager_tg.Instance.currentCurrency);
    }
}
