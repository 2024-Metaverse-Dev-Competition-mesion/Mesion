using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PetSelector : MonoBehaviour
{
    public Image selectedPetImage;
    private string selectedPetName;

    public void SelectPet(int petIndex)
    {
        selectedPetName = "Pet" + petIndex;
        PlayerPrefs.SetString("SelectedPet", selectedPetName);
        SceneManager.LoadScene("DisplayPetScene");
    }

    public void AssignImage(Sprite petSprite)
    {
        selectedPetImage.sprite = petSprite;
    }
}
