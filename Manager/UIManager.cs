using JetBrains.Annotations;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{

    [Header("Public")]
    [SerializeField] protected GameObject _selectCardUI;
    [SerializeField] protected GameObject _playerDeckUI;
    [SerializeField] protected GameObject _SettingUIPanel;
    protected MapManager mapManager;


    protected override void Awake()
    {
        base.Awake();
    }

    public void OnPlayerDeckUIOn()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _playerDeckUI.SetActive(true);
    }

    public void OnPlayerDeckUIOff()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _playerDeckUI.SetActive(false);
    }

    public void OnSettingUIPanelOn()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _SettingUIPanel.SetActive(true);
    }
    public void OnSettingUIPanelOff()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _SettingUIPanel.SetActive(false);
    }
    public void OnButtonTitleScene()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.CreatedBlockList.Clear();
        SceneManager.LoadScene(KeyWordManager.str_TitleSceneTxt);
        SoundManager.Instance.PlayBgm((int)ClipList.MainBGM);
        GameManager.Instance.ResetGame();

    }
    /// <summary>
    /// StageUIManager의 순수 가상함수
    /// </summary>
    public virtual void OnStoreUIOn() { }
    public virtual void OnStoreUIOff() { }
    public virtual void OnRestUI() { }
    public virtual void OnUpgradeUI() { }
    public virtual void OnUpgradePanel() { }
    public virtual void OnCardDelet() { }
    public virtual void DeleteUIOn() { }
    public virtual void DeleteUIOff() { }
    public virtual StoreUI GetStoreUI() { return null; }
    public virtual void OnDescriptionUIOn() { }
    public virtual void DescriptionTextChange(string text) { }
    public virtual void OnDescriptionUIOff() { }
    public virtual void OnIncidnetOn() { }
    public virtual void OnIncidnetOff() { }

    /// <summary>
    /// BattleUIManager
    /// </summary>
    public virtual void UpdateCostUI() { }
    public virtual void OnToMap() { }
    public virtual void OnAddCard() { }
    public virtual void OnWinPanel() { }
    public virtual void OnLosePanel() { }
    public virtual void GoToWinPanelAfterCardSelecttion() { }
}

