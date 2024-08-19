using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HeadUI : MonoBehaviour
{
    [SerializeField] GameObject _goldText;
    [SerializeField] GameObject _hpText;

    private TextMeshProUGUI _goldMeshProUGUI;
    private TextMeshProUGUI _hpMeshProUGUI;

    private void Start()
    {
        _goldMeshProUGUI = _goldText.GetComponentInChildren<TextMeshProUGUI>();
        _goldMeshProUGUI.text = GameManager.Instance.Player.Gold.ToString();
        _hpMeshProUGUI = _hpText.GetComponentInChildren<TextMeshProUGUI>();
        _hpMeshProUGUI.text = $"{GameManager.Instance.Player.hp} / {GameManager.Instance.Player.fullhp}";
    }

    private void LateUpdate()
    {
        UpdateAll();
    }

    private void UpdateAll()
    {
        UpdateHP();
        UpdateGold();
    }

    private void UpdateGold()
    {
        _goldMeshProUGUI.text = GameManager.Instance.Player.Gold.ToString();
    }
    private void UpdateHP()
    {
        _hpMeshProUGUI.text = $"{GameManager.Instance.Player.hp} / {GameManager.Instance.Player.fullhp}";
    }

    public void SettingUIPanel()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        if (UIManager.Instance is StageUIManager)
        {
            StageUIManager.Instance.OnSettingUIPanelOn();
        }
        else if (UIManager.Instance is BattleUIManager)
        {
            BattleUIManager.Instance.OnSettingUIPanelOn();
        }
    }

    public void CardListPanel()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        if (UIManager.Instance is StageUIManager)
        {
            StageUIManager.Instance.OnPlayerDeckUIOn();
        }
        else if (UIManager.Instance is BattleUIManager)
        {
            BattleUIManager.Instance.OnPlayerDeckUIOn();
        }
    }
}