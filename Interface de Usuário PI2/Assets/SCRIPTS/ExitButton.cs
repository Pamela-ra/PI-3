using UnityEngine;
using UnityEngine.UI;

public class ExitButton : MonoBehaviour
{
    public GameObject feedbackPanel;
    public Button yesButton;
    public Button noButton;

    private void Start()
    {
        yesButton.onClick.AddListener(ShowFeedbackPanel);
        noButton.onClick.AddListener(QuitApplication);

    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (feedbackPanel.activeSelf)
            {
                feedbackPanel.SetActive(false);
            }
        }
    }


    public void OnExitButtonPressed()
    {
        
        feedbackPanel.SetActive(true);
    }

    
    public void ShowFeedbackPanel()
    {

        Application.OpenURL("https://docs.google.com/forms/d/1UZGa7dKKZAuLrIBVk5wYTRvddfI-nJZ0HhnVCn8Eusw/edit");
        QuitApplication();
    }

    
    public void QuitApplication()
    {
        
        Application.Quit();
    }
}
