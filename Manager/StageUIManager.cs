using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageUIManager : UIManager
{

    [Header("StageScene")]
    [SerializeField] private GameObject _storeUI;
    [SerializeField] private GameObject _restUI;
    private GameObject _incidnetUI;

    [Header("Description")]
    [SerializeField] private GameObject DescriptionUI;
    [SerializeField] private TextMeshProUGUI DescriptionTxt;

    private StoreUI _storeUIPanel;

    [Header("Upgarde")]
    [SerializeField] private GameObject _upgradeUI;
    [SerializeField] private GameObject _upgradePanel;

    [Header("DeleteDeck")]
    [SerializeField] private GameObject _DeleteDeckUI;
    public GameObject StoreUI { get { return _storeUI; } private set { _storeUI = value; } }
    protected override void Awake()
    {
        base.Awake();
        if (_storeUI != null)
        {
            _storeUIPanel = _storeUI.GetComponent<StoreUI>();
        }

    }

    private void Start()
    {
        _incidnetUI = GameManager.Instance.ObjectPool.SpawnFromPool("IncidentScene");
        _incidnetUI.SetActive(false);
    }
    public override void OnStoreUIOn()
    {
        if (!_storeUIPanel.IsStoreUI)
        {
            _storeUI.SetActive(true);
            _storeUIPanel.IsStoreUI = true;
        }
    }
    public override void OnStoreUIOff()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        _storeUI.SetActive(false);
        _storeUIPanel.IsStoreUI = false;
        GameManager.Instance.Player.ChangeCanMove(true);
        GameManager.Instance.mapManager.SaveMap();
    }
    public override void OnRestUI()
    {
        _restUI.SetActive(true);
    }
    public override void OnUpgradeUI()
    {
        _upgradeUI.SetActive(true);
    }
    public override void OnUpgradePanel()
    {
        _upgradePanel.SetActive(true);
    }

    public override void OnCardDelet()
    {
        _DeleteDeckUI.SetActive(false);
        _storeUIPanel.IsDeleted = true;
    }

    public override void DeleteUIOn()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        if (GameManager.Instance.Player.Gold > _storeUIPanel.DeleteCardPrice)
        {
            if (!_storeUIPanel.IsDeleted)
            {
                GameManager.Instance.Player.SubGold(_storeUIPanel.DeleteCardPrice);
                _DeleteDeckUI.SetActive(true);
            }
        }
    }
    public override void DeleteUIOff()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        GameManager.Instance.Player.AddGold(_storeUIPanel.DeleteCardPrice);
        _storeUIPanel.IsDeleted = true;
        _DeleteDeckUI.SetActive(false);
    }

    public override StoreUI GetStoreUI()
    {
        return StoreUI.GetComponent<StoreUI>();
    }

    public override void OnDescriptionUIOn()
    {
        DescriptionUI.SetActive(true);
    }
    public override void DescriptionTextChange(string text)
    {

        DescriptionTxt.text = text;
    }

    public override void OnDescriptionUIOff()
    {
        DescriptionUI.SetActive(false);
    }

    public override void OnIncidnetOn()
    {
        _incidnetUI.SetActive(true);
    }

    public override void OnIncidnetOff()
    {
        _incidnetUI.SetActive(false);
        GameManager.Instance.mapManager.SaveMap();
    }
}
