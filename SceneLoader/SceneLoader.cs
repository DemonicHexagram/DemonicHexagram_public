using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static string NextSceneName;  // 로딩 후 전환될 씬 이름

    public static void LoadScene(string sceneName)
    {
        NextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");  
    }
}