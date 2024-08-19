using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingUIPanel : MonoBehaviour
{
    
    public void OnButtonGameobjectoff()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        gameObject.SetActive(false);
    }

    public void OnButtonTitleScene()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.CreatedBlockList.Clear();
        SceneManager.LoadScene(KeyWordManager.str_TitleSceneTxt);
        SoundManager.Instance.PlayBgm((int)ClipList.MainBGM);
    }

}