using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelection : MonoBehaviour
{
    public GameObject masculinoCharacterPrefab;
    public GameObject femininoCharacterPrefab;

    public Transform spawnPoint;

    private GameObject[] characterList;

    private void Start()
    {
        characterList = new GameObject[2];
        UpdateCharacterPanels();

        for (int i = 0; i < 2; i++)
            characterList[i] = transform.GetChild(i).gameObject;

        foreach (GameObject go in characterList)
            go.SetActive(false);

        if (characterList[0])
            characterList[0].SetActive(true);

        CharacterList.Instance.SelectedCharIndex = 0;
    }

    public void SelecionarButton()
    {
        int selectedGender = CharacterList.Instance.SelectedCharIndex;

        // Configurar as PlayerPrefs com base na escolha do jogador
        PlayerPrefs.SetInt("SelectedGender", selectedGender);
        PlayerPrefs.Save();


        SceneManager.LoadScene("Jogo");
    }

    [SerializeField] CharacterPanel characterPanelLeft;
    [SerializeField] CharacterPanel characterPanelRight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CharacterList.Instance.SelectedCharIndex++;
            UpdateCharacterPanels();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CharacterList.Instance.SelectedCharIndex--;
            UpdateCharacterPanels();
        }
    }

    private void UpdateCharacterPanels()
    {
        characterPanelLeft.UpdateCharacterPanel(CharacterList.Instance.GetPrevious());
        characterPanelRight.UpdateCharacterPanel(CharacterList.Instance.GetNext());
    }
}