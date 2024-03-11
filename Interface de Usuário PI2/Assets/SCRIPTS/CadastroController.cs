using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;

public class CadastroController : MonoBehaviour
{
    public InputField nicknameInput;

    private static string selectedNickname;

    public void Cadastrar()
    {
        // Obter dados inseridos
        selectedNickname = nicknameInput.text;

        // Carregar a próxima cena (escolha do personagem)
        SceneManager.LoadScene("EscolhaDoPersonagem");
    }

    // Método estático para obter o nickname
    public static string GetSelectedNickname()
    {
        return selectedNickname;
    }
}
