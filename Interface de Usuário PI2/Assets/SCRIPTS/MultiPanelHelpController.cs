using UnityEngine;

public class MultiPanelHelpController : MonoBehaviour
{
    [SerializeField] private GameObject helpPanel1;
    [SerializeField] private GameObject helpPanel2;
    [SerializeField] private GameObject helpPanel3;
    private bool isHelpActive = false; // Rastreador para o estado dos painéis

    void Update()
    {
        // Verificar se a tecla F2 foi pressionada para ativar ou desativar os painéis
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (isHelpActive)
            {
                HideAllHelpPanels();
            }
            else
            {
                ShowHelpPanel1();
            }
        }

        // Verificar se a tecla de seta para a direita foi pressionada e os painéis estão ativos
        if (isHelpActive && Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (helpPanel1.activeSelf && !helpPanel2.activeSelf)
            {
                ShowHelpPanel2();
            }
            else if (helpPanel2.activeSelf && !helpPanel3.activeSelf)
            {
                ShowHelpPanel3();
            }
        }
    }

    private void ShowHelpPanel1()
    {
        Debug.Log("ShowHelpPanel1 chamado");
        if (helpPanel1 != null)
        {
            helpPanel1.SetActive(true);
            isHelpActive = true;
        }
        else
        {
            Debug.LogError("helpPanel1 não está atribuído.");
        }
    }

    private void ShowHelpPanel2()
    {
        Debug.Log("ShowHelpPanel2 chamado");
        if (helpPanel2 != null)
        {
            helpPanel2.SetActive(true);
        }
        else
        {
            Debug.LogError("helpPanel2 não está atribuído.");
        }
    }

    private void ShowHelpPanel3()
    {
        Debug.Log("ShowHelpPanel3 chamado");
        if (helpPanel3 != null)
        {
            helpPanel3.SetActive(true);
        }
        else
        {
            Debug.LogError("helpPanel3 não está atribuído.");
        }
    }

    private void HideAllHelpPanels()
    {
        Debug.Log("HideAllHelpPanels chamado");
        if (helpPanel1 != null) helpPanel1.SetActive(false);
        if (helpPanel2 != null) helpPanel2.SetActive(false);
        if (helpPanel3 != null) helpPanel3.SetActive(false);
        isHelpActive = false;
    }
}
