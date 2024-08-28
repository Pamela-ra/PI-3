using UnityEngine;
using UnityEngine.UI;

public class HelpController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel;
    private bool isHelpPanelActive = false;

    void Update()
    {
        // Verificar se a tecla "H" foi pressionada
        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isHelpPanelActive)
            {
                HideHelpPanel();
            }
            else
            {
                ShowHelpPanel();
            }
        }
    }

    public void ShowHelpPanel()
    {
        Debug.Log("ShowHelpPanel chamado");
        if (helpPanel != null)
        {
            helpPanel.SetActive(true);
            isHelpPanelActive = true; // Marcar o painel como ativo
        }
        else
        {
            Debug.LogError("helpPanel não está atribuído.");
        }
    }

    public void HideHelpPanel()
    {
        Debug.Log("HideHelpPanel chamado");
        if (helpPanel != null)
        {
            helpPanel.SetActive(false);
            isHelpPanelActive = false; // Marcar o painel como inativo
        }
        else
        {
            Debug.LogError("helpPanel não está atribuído.");
        }
    }
}
