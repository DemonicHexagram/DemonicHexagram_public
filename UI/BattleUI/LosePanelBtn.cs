using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanelBtn : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneLoader.LoadScene(KeyWordManager.str_TitleSceneTxt);
        GameManager.Instance.ResetGame();

    }
}
