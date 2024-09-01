using UnityEngine;
using UnityEngine.UI;

public class PetSelectManager : MonoBehaviour
{
    public GameObject petSelectionPanel;
    public GameObject petDisplayPanel;
    public RawImage petDisplayImage; // RawImage 컴포넌트 사용
    public Texture[] petTextures; // 텍스처 배열

    private int selectedPetIndex = -1;

    void Awake()
    {
        petSelectionPanel.SetActive(true);
    }
    // 버튼에서 호출할 메서드
    public void SelectPet(int petIndex)
    {
        selectedPetIndex = petIndex;
        ShowPetDisplayPanel();
    }

    // 펫 선택 패널을 숨기고 펫 표시 패널을 보여줌
    void ShowPetDisplayPanel()
    {
        petSelectionPanel.SetActive(false);
        petDisplayPanel.SetActive(true);
        petDisplayImage.texture = petTextures[selectedPetIndex];
    }

    // 필요에 따라 다시 선택 패널로 돌아가기 위한 메서드
    public void BackToSelection()
    {
        petDisplayPanel.SetActive(false);
        petSelectionPanel.SetActive(true);
    }
}
