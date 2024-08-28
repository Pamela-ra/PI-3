using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CadastroController : MonoBehaviour
{
    public InputField nicknameInput;
    public Scrollbar userTypeScrollbar;
    public Text userTypeText;

    private static string selectedNickname;
    private string selectedUserType;

    private readonly string[] userTypes = { "Servidor", "Aluno", "Visitante" };

    void Start()
    {
        userTypeScrollbar.onValueChanged.AddListener(UpdateUserType);
        UpdateUserType(userTypeScrollbar.value); // Inicializa o texto com o valor atual do scrollbar
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

    void UpdateUserType(float value)
    {
        int index = Mathf.RoundToInt(value * (userTypes.Length - 1));
        selectedUserType = userTypes[index];
        userTypeText.text = selectedUserType;
    }

    // Método estático para obter o nickname
    public static string GetSelectedNickname()
    {
        return selectedNickname;
    }
}
