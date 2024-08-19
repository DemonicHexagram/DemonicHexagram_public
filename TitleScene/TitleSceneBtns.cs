using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleSceneBtn : MonoBehaviour
{
    public void NewGame()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.isNewGame = true;
        GameManager.Instance.ResetGame();
        GameManager.Instance.DataManager.CountingData.Init();
        SceneManager.LoadScene(KeyWordManager.str_MovingStageSceneTxt);
        SoundManager.Instance.PlayBgm((int)ClipList.Battle4);
    }
    public void LoadGame()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.isNewGame = false;
        SceneManager.LoadScene(KeyWordManager.str_MovingStageSceneTxt);
        SoundManager.Instance.PlayBgm((int)ClipList.Battle4);
    }
    public void QuitGame()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        Application.Quit();
    }
    public void Library()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.isNewGame = false;
        SceneManager.LoadScene("Library");
    }
    public void BackToTitle()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.isNewGame = false;
        SceneManager.LoadScene(KeyWordManager.str_TitleSceneTxt);
    }
}
