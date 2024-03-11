using UnityEngine;
using Cinemachine;

public class CharacterFollow : MonoBehaviour
{
    public Camera maleCharacterCamera;
    public Camera femaleCharacterCamera;

    private void Start()
    {

        
        maleCharacterCamera.gameObject.SetActive(false);
        femaleCharacterCamera.gameObject.SetActive(false);

        // Verifique a escolha do jogador nas PlayerPrefs
        int selectedGender = PlayerPrefs.GetInt("SelectedGender", 0);

        
        if (selectedGender == 0)
        {
            maleCharacterCamera.gameObject.SetActive(true);
        }
        else
        {
            femaleCharacterCamera.gameObject.SetActive(true);
        }
    }
}
