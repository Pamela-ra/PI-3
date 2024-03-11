using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class AtivarPersonagem : MonoBehaviour
{
    public GameObject femininoCharacterPrefab;
    public GameObject masculinoCharacterPrefab;
    public TMP_Text nicknameDisplayFem;
    public TMP_Text nicknameDisplayMasc;

    private void Awake()
    {
        // Verifique o valor de SelectedGender nas PlayerPrefs
        int selectedGender = PlayerPrefs.GetInt("SelectedGender", 0);

        string selectedNickname = Controller.GetSelectedNickname();

        // Exiba o nickname acima do personagem na UI
        nicknameDisplayFem.text = selectedNickname;
        nicknameDisplayMasc.text = selectedNickname;

        // Ative o personagem correspondente com base no valor de SelectedGender
        if (selectedGender == 0)
        {
            femininoCharacterPrefab.SetActive(false);
            masculinoCharacterPrefab.SetActive(true);
        }
        else
        {
            femininoCharacterPrefab.SetActive(true);
            masculinoCharacterPrefab.SetActive(false);
        }
    }
}
