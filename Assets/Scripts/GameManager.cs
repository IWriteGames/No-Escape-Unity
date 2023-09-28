using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    //Singleton
    public static GameManager Instance;

    [SerializeField] private GameObject panelDefeat;
    [SerializeField] private TMP_Text defeatText;

    public int round = 1;
    public int totalEnemiesDefeat;

    public bool playerDefeat = false;

    private void Awake() 
    {
        Instance = this;
        panelDefeat.SetActive(false);
    }
    
    public void ChangeRound()
    {
        round++;
        UIManager.Instance.UpdateRound(round);
    }

    public void Defeat()
    {
        Cursor.lockState = CursorLockMode.None;
        playerDefeat = true;
        panelDefeat.SetActive(true);
        defeatText.text = "Rounds: " + round + "\n" + "Zombies Defeat: " + totalEnemiesDefeat;
    }

}
