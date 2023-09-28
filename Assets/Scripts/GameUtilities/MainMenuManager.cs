using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] private GameObject creditsPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject buttonsPanel;


    private void Awake() 
    {
        Cursor.lockState = CursorLockMode.None;
        MenuDefault();
    }

    public void OpenCredits()
    {
        buttonsPanel.SetActive(false);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(true);
    }

    public void OpenOptions()
    {
        optionsPanel.SetActive(true);
        creditsPanel.SetActive(false);
        buttonsPanel.SetActive(false);
    }

    public void MenuDefault()
    {
        buttonsPanel.SetActive(true);
        optionsPanel.SetActive(false);
        creditsPanel.SetActive(false);
    }


}
