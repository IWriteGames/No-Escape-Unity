using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Slider loadingBar;

    private void Awake()
    {
        Instance = this;
        loadingScreen.SetActive(false);
    }

    public void LoadScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    public void ReloadScene()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }

    public void LoadSceneWithoutLoadingScreen(string sceneName)
    {
        StartCoroutine(LoadSceneAsynchronously(sceneName));
    }

    IEnumerator LoadSceneAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            loadingBar.value = operation.progress;
            Time.timeScale = 1;
            loadingScreen.SetActive(false);
            yield return null;
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
