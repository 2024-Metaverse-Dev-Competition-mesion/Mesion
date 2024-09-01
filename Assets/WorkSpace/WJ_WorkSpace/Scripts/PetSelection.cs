using UnityEngine;
using UnityEngine.UI;

public class PetSelection : MonoBehaviour
{
    public GameObject firstPanel;
    public GameObject secondPanel;
    public RawImage selectedPetImage;

    public Button[] petButtons;    // 4개의 펫 선택 버튼
    public Texture2D[] petTextures; // 각 펫의 텍스처 이미지

    private Texture2D selectedPet;

    void Awake()
    {
        if (firstPanel == null)
        {
            Debug.LogError("First Panel is not assigned.");
            return;
        }

        if (secondPanel == null)
        {
            Debug.LogError("Second Panel is not assigned.");
            return;
        }

        firstPanel.SetActive(true);
        secondPanel.SetActive(false);
    }

    void Start()
    {
        // 각 버튼에 클릭 이벤트 추가
        for (int i = 0; i < petButtons.Length; i++)
        {
            int index = i; // 인덱스를 저장하는 임시 변수
            petButtons[i].onClick.AddListener(() => OnPetSelected(index));
        }
    }

    void OnPetSelected(int index)
    {
        selectedPet = petTextures[index];
        firstPanel.SetActive(false);
        secondPanel.SetActive(true);
        selectedPetImage.texture = selectedPet;
    }

    public void OnCancel()
    {
        secondPanel.SetActive(false);
        firstPanel.SetActive(true);
    }

    public void OnConfirm()
    {
        Debug.Log("펫 선택 확인!");
    }
}
