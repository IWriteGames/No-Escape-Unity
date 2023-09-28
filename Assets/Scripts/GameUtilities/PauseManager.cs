using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    //Singleton
    public static PauseManager Instance;

    //UI
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject buttonsPanel;
    [SerializeField] private GameObject UIPlayer;


    //Values
    public bool IsPause { get; set; }

    private void Awake() 
    {
        Instance = this;
        IsPause = false;
        pausePanel.SetActive(false);
        showButtonsPanel();
        UIPlayer.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update() 
    {
        PauseGameButton();
    }

    void PauseGameButton()
    {
        if(!GameManager.Instance.playerDefeat)
        {
            if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (IsPause)
        {
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            pausePanel.SetActive(false);
            UIPlayer.SetActive(true);
            IsPause = false;
        }
        else
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            pausePanel.SetActive(true);
            showButtonsPanel();
            Options.Instance.deactiveAllPanels();
            UIPlayer.SetActive(false);
            IsPause = true;
        }
    }

    public void showButtonsPanel()
    {
        buttonsPanel.SetActive(true);
    }

    public void hiddenButtonsPanel()
    {
        buttonsPanel.SetActive(false);
    }

    
}
