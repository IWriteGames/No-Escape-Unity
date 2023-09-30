using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text weaponText;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text textHealthBar;
    [SerializeField] private TMP_Text roundText;
    [SerializeField] private Image imageDamage;

    public bool FadeAwayCanActive = true;
    

    private void Awake() 
    {
        Instance = this;
        healthBar.value = 200;
        textHealthBar.text = "200/200";
        roundText.text = "1";
        imageDamage.GetComponent<Image>().color = new Color(0.7f, 0.06f, 0.06f, 0);
        imageDamage.enabled = false;
    }

    public void ChangeTextWeapon(string nameWeapon, int actualAmmoMagazine, int actualAmmoReserve)
    {
        weaponText.text = nameWeapon + ": ";
        ammoText.text = actualAmmoMagazine + " / " +  actualAmmoReserve;
    }

    public void UpdateHealthUI(int value)
    {
        healthBar.value = value;
        textHealthBar.text = value + "/200";
    }

    public void UpdateRound(int value)
    {
        roundText.text = value.ToString();
    }

    public IEnumerator FadeAway()
    {
        if(FadeAwayCanActive)
        {
            FadeAwayCanActive = false;
            imageDamage.enabled = true;
            float alphaImage = 1.0f;

            while (alphaImage > 0)
            {
                alphaImage -= (1.0f / 1.0f) * Time.deltaTime;

                imageDamage.GetComponent<Image>().color = new Color(0.7f, 0.06f, 0.06f, alphaImage);
                yield return null;
            }

            imageDamage.enabled = false;
        }
        FadeAwayCanActive = true;

    }

}
