using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour
{
    [SerializeField] private Image myImage;
    private bool loadFinish;
    private bool endLogo;

    void Start()
    {
        loadFinish = false;
        endLogo = false;

        myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, 0f);
        StartCoroutine(FadeEffect());

        #if UNITY_EDITOR
            PlayerPrefs.DeleteAll();
        #endif

        if (PlayerPrefs.GetInt(ConstantsGame.FirstTimeOpenGame) == 0)
        {
            //*OPTIONS*//

            //Sound
            PlayerPrefs.SetFloat(ConstantsGame.OptionMasterVolume, 1);
            PlayerPrefs.SetFloat(ConstantsGame.OptionEffectsVolume, 1);
            PlayerPrefs.SetFloat(ConstantsGame.OptionMusicVolume, 1);

            //Video
            PlayerPrefs.SetInt(ConstantsGame.OptionFullScreen, 1);
            PlayerPrefs.SetInt(ConstantsGame.OptionVSync, 1);
            PlayerPrefs.SetInt(ConstantsGame.OptionResolution, 1);
            PlayerPrefs.SetInt(ConstantsGame.OptionLimitFPS, 0);

            //Control
            PlayerPrefs.SetFloat(ConstantsGame.MouseHorizontalX, 0.5f);
            PlayerPrefs.SetFloat(ConstantsGame.MouseVerticalY, 0.5f);

            PlayerPrefs.SetInt(ConstantsGame.FirstTimeOpenGame, 1);
        }

        Application.targetFrameRate = PlayerPrefs.GetInt(ConstantsGame.OptionLimitFPS);

        loadFinish = true;
    }

    private void Update() 
    {
        if(loadFinish && endLogo)
        {
            SceneManager.LoadScene(ConstantsGame.SceneMainMenu);
        }
    }

    //Image Screen
    private IEnumerator FadeEffect()
    {
        float fadeCount = 0;

        while (fadeCount < 1.0f)
        {
            fadeCount += 0.01f;
            yield return new WaitForSeconds(0.01f);
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, fadeCount);
        }

        while (fadeCount > 0.01f)
        {
            fadeCount -= 0.01f;
            yield return new WaitForSeconds(-0.01f);
            myImage.color = new Color(myImage.color.r, myImage.color.g, myImage.color.b, fadeCount);
        }
        endLogo = true;
    }

}

