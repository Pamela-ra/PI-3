using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;

public class Controller : MonoBehaviour
{
    public TMP_InputField nicknameInput;
    public TMP_Dropdown userTypeDropdown;

    private static string selectedNickname;
    private static string selectedUserType;

    private readonly string[] userTypes = { "Servidor", "Aluno", "Visitante" };

    void Start()
    {
        // Configurar o Dropdown com os tipos de usuário
        userTypeDropdown.ClearOptions();
        userTypeDropdown.AddOptions(new List<string>(userTypes));

        // Adicionar listener ao Dropdown
        userTypeDropdown.onValueChanged.AddListener(UpdateUserType);

        // Inicializa o tipo de usuário selecionado
        UpdateUserType(userTypeDropdown.value);
    }

    public void Cadastrar()
    {
        // Obter dados inseridos
        selectedNickname = nicknameInput.text;

        // Validar o nickname
        if (string.IsNullOrEmpty(selectedNickname))
        {
            Debug.LogWarning("Por favor, insira um nickname.");
            return;
        }

        // Salvar o nickname em PlayerPrefs
        PlayerPrefs.SetString("Nickname", selectedNickname);
        PlayerPrefs.SetString("UserType", selectedUserType);

        // Carregar a próxima cena (escolha do personagem ou a cena principal do jogo)
        SceneManager.LoadScene("EscolhaDoPersonagem");
    }

    void UpdateUserType(int index)
    {
        selectedUserType = userTypes[index];
    }

    // Método estático para obter o nickname
    public static string GetSelectedNickname()
    {
        return selectedNickname;
    }

    // Método estático para obter o tipo de usuário
    public static string GetSelectedUserType()
    {
        return selectedUserType;
    }
}
