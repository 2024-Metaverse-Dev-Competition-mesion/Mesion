using UnityEngine;
using UnityEngine.UI;

public class PetDisplay : MonoBehaviour
{
    public Image petDisplayImage;
    public Sprite[] petSprites; // Inspector에서 이 배열에 펫 스프라이트를 할당하세요.

    void Start()
    {
        string selectedPetName = PlayerPrefs.GetString("SelectedPet");
        int petIndex = int.Parse(selectedPetName.Substring(selectedPetName.Length - 1)) - 1;
        petDisplayImage.sprite = petSprites[petIndex];
    }
}
