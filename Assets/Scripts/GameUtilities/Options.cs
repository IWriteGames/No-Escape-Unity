using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static Options Instance;

    [Header("Panels & Buttons")]
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject SoundPanel;
    [SerializeField] private GameObject VideoPanel;
    [SerializeField] private GameObject ControlPanel;

    [SerializeField] private Button buttonSound;
    [SerializeField] private Button buttonVideo;
    [SerializeField] private Button buttonControl;

    //Sound
    [Header("Sound")]
    [SerializeField] private Slider MasterSlider;
    [SerializeField] private Slider EffectsSlider;
    [SerializeField] private Slider MusicSlider;

    [SerializeField] private TextMeshProUGUI MasterTextValue;
    [SerializeField] private TextMeshProUGUI EffectsTextValue;
    [SerializeField] private TextMeshProUGUI MusicTextValue;

    [SerializeField] private AudioMixer generalMixer;
    [SerializeField] private AudioMixerGroup effectsMixerGroup;
    [SerializeField] private AudioMixerGroup musicMixerGroup;

    //Video
    [Header("Video")]
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Toggle vSyncToggle;
    [SerializeField] private TMP_Dropdown resolutionMenu;
    [SerializeField] private TMP_Dropdown targetFPS;
    [SerializeField] private bool fullScreen;
    [SerializeField] private int resolutionOption;
    [SerializeField] private int targetFPSOption;

    //Control
    [Header("Control")]
    [SerializeField] private Slider mouseHorizontalX;
    [SerializeField] private Slider mouseVerticalY;

    [SerializeField] private TextMeshProUGUI HorizontalXTextValue;
    [SerializeField] private TextMeshProUGUI VerticalYTextValue;

    public float mouseSensitivityX;
    public float mouseSensitivityY;

    void Awake()
    {
        Instance = this;

        valueDefaultUser();
        deactiveAllPanels();
    }

    void Update()
    {
        //Audio Sliders
        generalMixer.SetFloat("MasterVolume", Mathf.Log10(MasterSlider.value) * 20);
        effectsMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(EffectsSlider.value) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(MusicSlider.value) * 20);

        MasterTextValue.text = MasterSlider.value.ToString("0.##");
        EffectsTextValue.text = EffectsSlider.value.ToString("0.##");
        MusicTextValue.text = MusicSlider.value.ToString("0.##");

        //Control Sliders
        HorizontalXTextValue.text = mouseHorizontalX.value.ToString("0.##");
        VerticalYTextValue.text = mouseVerticalY.value.ToString("0.##");

        mouseSensitivityX = mouseHorizontalX.value;
        mouseSensitivityY = mouseVerticalY.value;
    }



    private void valueDefaultUser()
    {
        /*
         * AUDIO
         */

        MasterSlider.value = PlayerPrefs.GetFloat(ConstantsGame.OptionMasterVolume);
        EffectsSlider.value = PlayerPrefs.GetFloat(ConstantsGame.OptionEffectsVolume);
        MusicSlider.value = PlayerPrefs.GetFloat(ConstantsGame.OptionMusicVolume);

        //Text Sounds
        MasterTextValue.text = PlayerPrefs.GetFloat(ConstantsGame.OptionMasterVolume).ToString("0.##");
        EffectsTextValue.text = PlayerPrefs.GetFloat(ConstantsGame.OptionEffectsVolume).ToString("0.##");
        MusicTextValue.text = PlayerPrefs.GetFloat(ConstantsGame.OptionMusicVolume).ToString("0.##");

        //Mixers
        generalMixer.SetFloat("MasterVolume", Mathf.Log10(PlayerPrefs.GetFloat(ConstantsGame.OptionMasterVolume)) * 20);
        effectsMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(PlayerPrefs.GetFloat(ConstantsGame.OptionEffectsVolume)) * 20);
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(PlayerPrefs.GetFloat(ConstantsGame.OptionMusicVolume)) * 20);


        /*
         * VIDEO
         */
        //FullScreen
        if (PlayerPrefs.GetInt(ConstantsGame.OptionFullScreen) == 0)
        {
            fullScreenToggle.isOn = false;
            fullScreen = false;
        }
        else
        {
            fullScreenToggle.isOn = true;
            fullScreen = true;
        }

        //vSync
        if (PlayerPrefs.GetInt(ConstantsGame.OptionVSync) == 0)
        {
            vSyncToggle.isOn = false;
        }
        else
        {
            vSyncToggle.isOn = true;
        }


        //Resolution
        resolutionMenu.value = PlayerPrefs.GetInt(ConstantsGame.OptionResolution);
        resolutionOption = resolutionMenu.value;
        if (PlayerPrefs.GetInt(ConstantsGame.OptionResolution) != resolutionOption)
        {
            changeResolution(resolutionOption);
        }

        //Limit FPS
        targetFPS.value = PlayerPrefs.GetInt(ConstantsGame.OptionLimitFPS);
        targetFPSOption = targetFPS.value;
        if (PlayerPrefs.GetInt(ConstantsGame.OptionLimitFPS) != targetFPSOption)
        {
            changeLimitFPS(targetFPSOption);
        }

        /*
         * CONTROL
         */
        mouseHorizontalX.value = PlayerPrefs.GetFloat(ConstantsGame.MouseHorizontalX);
        mouseVerticalY.value = PlayerPrefs.GetFloat(ConstantsGame.MouseVerticalY);

        HorizontalXTextValue.text = PlayerPrefs.GetFloat(ConstantsGame.MouseHorizontalX).ToString("0.##");
        VerticalYTextValue.text = PlayerPrefs.GetFloat(ConstantsGame.MouseVerticalY).ToString("0.##");
    }



    public void SaveSoundOptions()
    {
        PlayerPrefs.SetFloat(ConstantsGame.OptionMasterVolume, MasterSlider.value);
        PlayerPrefs.SetFloat(ConstantsGame.OptionEffectsVolume, EffectsSlider.value);
        PlayerPrefs.SetFloat(ConstantsGame.OptionMusicVolume, MusicSlider.value);
    }

    public void SaveVideoOptions()
    {

        //Save FullScreen
        if (fullScreenToggle.isOn)
        {
            PlayerPrefs.SetInt(ConstantsGame.OptionFullScreen, 1);
            fullScreen = true;
        }
        else
        {
            PlayerPrefs.SetInt(ConstantsGame.OptionFullScreen, 0);
            fullScreen = false;
        }

        if (Screen.fullScreen == false && fullScreenToggle.isOn)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        if (Screen.fullScreen == true && !fullScreenToggle.isOn)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }

        //Save vSync
        if (vSyncToggle.isOn)
        {
            QualitySettings.vSyncCount = 1;
            PlayerPrefs.SetInt(ConstantsGame.OptionVSync, 1);
        }
        else
        {
            QualitySettings.vSyncCount = 0;
            PlayerPrefs.SetInt(ConstantsGame.OptionVSync, 0);
        }

        //Resolution
        int resolutionOption = resolutionMenu.value;
        PlayerPrefs.SetInt(ConstantsGame.OptionResolution, resolutionOption);
        changeResolution(resolutionOption);

        int targetFPSOption = targetFPS.value;
        PlayerPrefs.SetInt(ConstantsGame.OptionLimitFPS, targetFPSOption);
        changeLimitFPS(targetFPSOption);
    }

    public void SaveControlOptions()
    {
        PlayerPrefs.SetFloat(ConstantsGame.MouseHorizontalX, mouseHorizontalX.value);
        PlayerPrefs.SetFloat(ConstantsGame.MouseVerticalY, mouseVerticalY.value);
    }

    public void restoreSoundOptions()
    {
        generalMixer.SetFloat("MasterVolume", Mathf.Log10(1 * 20));
        effectsMixerGroup.audioMixer.SetFloat("EffectsVolume", Mathf.Log10(1 * 20));
        musicMixerGroup.audioMixer.SetFloat("MusicVolume", Mathf.Log10(1 * 20));

        MasterSlider.value = 1;
        EffectsSlider.value = 1;
        MusicSlider.value = 1;

        MasterTextValue.text = "1";
        EffectsTextValue.text = "1";
        MusicTextValue.text = "1";
    }

    public void restoreVideoOptions()
    {
        //FullScreen
        fullScreenToggle.isOn = true;
        fullScreen = true;

        //vSync
        vSyncToggle.isOn = true;

        //Resolution
        resolutionMenu.value = 1;

        //Limit FPS
        targetFPS.value = 0;
    }

    public void restoreControlOptions()
    {
        mouseHorizontalX.value = 0.5f;
        mouseVerticalY.value = 0.5f;

        HorizontalXTextValue.text = "0.5";
        VerticalYTextValue.text = "0.5";
    }

    private void changeResolution(int resolutionOption)
    {

        if (resolutionOption == 0)
        {
            Screen.SetResolution(2560, 1440, fullScreen);
        }
        else if (resolutionOption == 1)
        {
            Screen.SetResolution(1920, 1080, fullScreen);
        }
        else if (resolutionOption == 2)
        {
            Screen.SetResolution(1366, 768, fullScreen);
        }
        else if (resolutionOption == 3)
        {
            Screen.SetResolution(1280, 800, fullScreen);
        }
    }

    private void changeLimitFPS(int targetFPSOption)
    {
        if (targetFPSOption == 0)
        {
            Application.targetFrameRate = 60;
        }
        else if (targetFPSOption == 1)
        {
            Application.targetFrameRate = 100;
        }
        else if (targetFPSOption == 2)
        {
            Application.targetFrameRate = 144;
        }
        else if (targetFPSOption == 3)
        {
            Application.targetFrameRate = -1;
        }
    }



    //Panel Management
    public void openOptionsPanel()
    {
        valueDefaultUser();
        activeSoundPanel();
    }

    public void SaveExitAllOptions()
    {
        SaveSoundOptions();
        SaveVideoOptions();
        SaveControlOptions();
        deactiveAllPanels();
    }

    public void closeExitOptionsPanel()
    {
        valueDefaultUser();
        deactiveAllPanels();
    }

    public void activeSoundPanel()
    {
        buttonSound.GetComponent<Image>().color = new Color(0.47f, 0.97f, 0.4f, 0.65f);
        buttonVideo.GetComponent<Image>().color = Color.white;
        buttonControl.GetComponent<Image>().color = Color.white;

        OptionsPanel.SetActive(true);
        SoundPanel.SetActive(true);
        VideoPanel.SetActive(false);
        ControlPanel.SetActive(false);
    }

    public void activeVideoPanel()
    {
        buttonSound.GetComponent<Image>().color = Color.white;
        buttonVideo.GetComponent<Image>().color = new Color(0.47f, 0.97f, 0.4f, 0.65f);
        buttonControl.GetComponent<Image>().color = Color.white;

        OptionsPanel.SetActive(true);
        SoundPanel.SetActive(false);
        VideoPanel.SetActive(true);
        ControlPanel.SetActive(false);
    }

    public void activeControlPanel()
    {
        buttonSound.GetComponent<Image>().color = Color.white;
        buttonVideo.GetComponent<Image>().color = Color.white;
        buttonControl.GetComponent<Image>().color = new Color(0.47f, 0.97f, 0.4f, 0.65f);

        OptionsPanel.SetActive(true);
        SoundPanel.SetActive(false);
        VideoPanel.SetActive(false);
        ControlPanel.SetActive(true);
    }

    public void deactiveAllPanels()
    {
        OptionsPanel.SetActive(false);
        SoundPanel.SetActive(false);
        VideoPanel.SetActive(false);
        ControlPanel.SetActive(false);
    }
}
