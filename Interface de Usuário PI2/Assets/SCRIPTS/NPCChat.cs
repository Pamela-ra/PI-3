using System.Collections;
using UnityEngine;
using TMPro;

public class NPCChat : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    private string[] chatLines;
    private int currentLine;

    public void StartChat(string[] lines)
    {
        chatLines = lines;
        currentLine = 0;
        StartCoroutine(DisplayChat());
    }

    private IEnumerator DisplayChat()
    {
        while (currentLine < chatLines.Length)
        {
            chatText.text = chatLines[currentLine];
            Debug.Log($"Exibindo linha {currentLine}: {chatLines[currentLine]}");
            currentLine++;
            yield return new WaitForSeconds(2.0f); // Exibir cada linha por 2 segundos
        }

        chatText.text = ""; // Limpar o texto após exibir todas as linhas
        Debug.Log("Chat concluído.");
    }
}
