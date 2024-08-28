using UnityEngine;

public class VisitorExperienceController : MonoBehaviour
{
    public GameObject visitorExperience;  // Arraste o GameObject 'VisitorExperience' aqui no Inspector

    void Start()
    {
        // Obter o tipo de usu�rio selecionado do PlayerPrefs
        string userType = PlayerPrefs.GetString("UserType", "Visitante");

        // Ativar ou desativar o VisitorExperience com base no tipo de usu�rio
        if (userType == "Visitante")
        {
            if (visitorExperience != null)
            {
                visitorExperience.SetActive(true);
            }
        }
        else
        {
            if (visitorExperience != null)
            {
                visitorExperience.SetActive(false);
            }
        }
    }
}
