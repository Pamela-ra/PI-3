using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class Controller : MonoBehaviour
{
    [SerializeField] private TMP_InputField nicknameInput;

    private static string selectedNickname;

    public void Cadastrar()
    {
        // Obter dados inseridos
        selectedNickname = nicknameInput.text;

        // Carregar a próxima cena (escolha do personagem)
        SceneManager.LoadScene("Escolha Do Personagem");
    }

    // Método estático para obter o nickname
    public static string GetSelectedNickname()
    {
        return selectedNickname;
    }
}
