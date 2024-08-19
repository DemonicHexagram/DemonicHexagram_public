using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WinPanelUI : MonoBehaviour
{
    [SerializeField] private Transform _tagetTransform;
    List<Transform> _childList;
    List<Color> _colors;
    List<bool> _selected;

    private TextMeshProUGUI goldTxt;
    private int _addGold;
    private int _minGold;
    private int _maxGold;

    private void Awake()
    {
        _childList = new List<Transform>();
        _selected = new List<bool>();
        _colors = new List<Color>();
        _addGold = 0;
        Initialize();
    }

    private void OnEnable()
    {
        for (int i = 0; i < _childList.Count; i++)
        {
            _childList[i] = _tagetTransform.GetChild(i);
            _childList[i].gameObject.SetActive(_selected[i]);
        }
        GoldButtonSetting();
    }

    private void Initialize()
    {
        for (int i = 0; i < _tagetTransform.childCount; i++)
        {
            _childList.Add(null);
            _selected.Add(true);
            _colors.Add(Color.white);
        }
    }

    public void OnButtonSelected()
    {
        GameObject currectObj = EventSystem.current.currentSelectedGameObject;

        for (int i = 0; i < _childList.Count; i++)
        {
            if (_childList[i].name == currectObj.name)
            {
                Image image = _childList[i].GetComponent<Image>();
                _colors[i] = image.color;

                image.color = Color.black;

                _childList[i].GetComponent<Button>().enabled = false;
                break;
            }
        }
    }

    public void ResetSelected()
    {
        if (_selected != null)
        {
            for (int i = 0; i < _selected.Count; i++)
            {

                Image image = _childList[i].GetComponent<Image>();
                image.color = _colors[i];

                _childList[i].GetComponent<Button>().enabled = true;
            }
            _addGold = 0;
        }
    }

    private void GoldButtonSetting()
    {
        for (int i = 0; i < _childList.Count; i++)
        {
            if (_childList[i].name == KeyWordManager.str_GoldAddButtonTxt)
            {
                goldTxt = _childList[i].GetComponentInChildren<TextMeshProUGUI>();
                break;
            }
        }

        Vector2Int currentPoint = GameManager.Instance.mapManager.
        CurrentMap.path[GameManager.Instance.mapManager.CurrentMap.path.Count - 1];

        Node currentNode = GameManager.Instance.mapManager.CurrentMap.GetNode(currentPoint);

        switch (currentNode.nodeType)
        {
            case NodeType.MinorEnemy:
                _minGold = KeyWordManager.int_MinorEnemyMinGold;
                _maxGold = KeyWordManager.int_MinorEnemyMaxGold;
                break;
            case NodeType.EliteEnemy:
                _minGold = KeyWordManager.int_EliteEnemyMinGold;
                _maxGold = KeyWordManager.int_EliteEnemyMaxGold;
                break;
            case NodeType.Boss:
                _minGold = KeyWordManager.int_BossEnemyMinGold;
                _maxGold = KeyWordManager.int_BossEnemyMaxGold;
                break;
            default:
                break;
        }

        if (_addGold == 0)
        {
            _addGold = Random.Range(_minGold, _maxGold);
        }

        goldTxt.text = _addGold.ToString();
    }

    public void WinAddGold()
    {
        SoundManager.Instance.PlaySfx(SFX.CardDraw);
        GameManager.Instance.Player.AddGold(_addGold);
        OnButtonSelected();
    }
}
