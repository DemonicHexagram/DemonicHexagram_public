using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleUIManager : UIManager
{
    [Header("BattleScene")]
    [SerializeField] private GameObject _panelArea;
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    private WinPanelUI _winPanelUI;
    public TextMeshProUGUI _costText;
    protected override void Awake()
    {
        base.Awake();
        if(_winPanel != null) 
        {
          _winPanelUI = _winPanel.GetComponent<WinPanelUI>();   
        }
    }
    public override void UpdateCostUI()
    {
        if( _costText != null ) 
        {
            _costText.text = BattleManager.Instance._curcost.ToString();
        }
    }

    public override void OnToMap()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _winPanel.SetActive(false);
        _panelArea.SetActive(false);
        _winPanelUI.ResetSelected();
        GameManager.Instance.mapManager.SaveMap();
        GameManager.Instance.CreatedBlockList.Clear();
        SceneManager.LoadScene(KeyWordManager.str_MovingStageSceneTxt);
    }
    public override void OnAddCard()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _winPanel.SetActive(false);
        _selectCardUI.SetActive(true);
        _winPanelUI.OnButtonSelected();
    }
    public override void OnWinPanel()
    {

        if (_winPanel != null)
        {
            _winPanel.SetActive(true);
            _panelArea.SetActive(true);
        }
        else
        {
            GameManager.Instance.CreatedBlockList.Clear();
            SceneManager.LoadScene(KeyWordManager.str_MovingStageSceneTxt);
            SoundManager.Instance.PlayBgm((int)ClipList.Battle4);

        }
    }
    public override void OnLosePanel()
    {
        SoundManager.Instance.PlaySfx(SFX.Lose);
        if (_losePanel != null)
        {
            _losePanel.SetActive(true);
            _panelArea.SetActive(true);
        }
    }
    public override void GoToWinPanelAfterCardSelecttion()
    {
        if (_selectCardUI != null) _selectCardUI.SetActive(false);
        if (_winPanel != null) _winPanel.SetActive(true);
    }

}