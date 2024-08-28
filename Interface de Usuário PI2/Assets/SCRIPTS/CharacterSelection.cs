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
        // Inicialize a lista de personagens com os prefabs
        characterList = new GameObject[] { masculinoCharacterPrefab, femininoCharacterPrefab };

        // Desativa ambos os personagens inicialmente
        foreach (GameObject go in characterList)
            go.SetActive(false);

        // Ativa o personagem padrão (primeiro da lista) apenas para a seleção inicial
        if (characterList.Length > 0)
        {
            characterList[0].SetActive(true);
            CharacterList.Instance.SelectedCharIndex = 0;
        }

        // Atualiza os painéis de personagens
        UpdateCharacterPanels();
    }

    public void SelecionarButton()
    {
        int selectedGender = CharacterList.Instance.SelectedCharIndex;

        // Configurar as PlayerPrefs com base na escolha do jogador
        PlayerPrefs.SetInt("SelectedGender", selectedGender);
        PlayerPrefs.Save();

        // Carregar a próxima cena
        SceneManager.LoadScene("Jogo");
    }

    [SerializeField] CharacterPanel characterPanelLeft;
    [SerializeField] CharacterPanel characterPanelRight;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // Alterna para o próximo personagem
            CharacterList.Instance.SelectedCharIndex = (CharacterList.Instance.SelectedCharIndex + 1) % characterList.Length;
            UpdateCharacterPanels();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            // Alterna para o personagem anterior
            CharacterList.Instance.SelectedCharIndex = (CharacterList.Instance.SelectedCharIndex - 1 + characterList.Length) % characterList.Length;
            UpdateCharacterPanels();
        }

        // Atualiza qual personagem está ativo com base na seleção
        for (int i = 0; i < characterList.Length; i++)
        {
            characterList[i].SetActive(i == CharacterList.Instance.SelectedCharIndex);
        }
    }

    private void UpdateCharacterPanels()
    {
        characterPanelLeft.UpdateCharacterPanel(CharacterList.Instance.GetPrevious());
        characterPanelRight.UpdateCharacterPanel(CharacterList.Instance.GetNext());
    }
}
