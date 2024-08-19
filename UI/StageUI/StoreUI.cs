using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class StoreUI : MonoBehaviour
{
    [Header("연속된 숫자 입력")]
    public int FirstNumber;
    public int SecondNumber;

    [Header("라인의 변환컴포넌트")]
    [SerializeField] private List<Transform> LineManagement;
    private List<Transform> _positionList;

    [Header("선택카드리스트 상위 변환컴포넌트")]
    [SerializeField] private Transform SelectCardParent;
    private List<Transform> _objTransform;
    private int limitCardCount = KeyWordManager.int_LimitCardCount;

    [Header("가격의 상위 변환컴포넌트")]
    [SerializeField] private Transform PriceLine;
    private List<Transform> _priceTextTransform;
    private Vector3 _offsetVec;

    List<Card> _cards;
    List<TextMeshProUGUI> _textList;

    int _deleteCardPrice = KeyWordManager.int_DeleteCardPrice;
    bool _isDeleted = false;
    bool _isStoreUI = false;

    float _setHeight = 1080;

    public bool IsDeleted { get { return _isDeleted; } set { _isDeleted = value; } }
    public bool IsStoreUI { get { return _isStoreUI; } set { _isStoreUI = value; } }

    public int DeleteCardPrice { get { return _deleteCardPrice; } private set { _deleteCardPrice = value; } }

    private void Awake()
    {
        _positionList = new List<Transform>();
        _objTransform = new List<Transform>();
        _priceTextTransform = new List<Transform>();
        _cards = new List<Card>();
        _textList = new List<TextMeshProUGUI>();
        _offsetVec.Set(0, -180, 0);
        PositionReading();
        TextTransformSetting();
        ObjectSetting();
        TextSettingSize();
    }

    private void OnEnable()
    {
        InitializeCard();
        CardSetting();
        PositionSetting();
        TextSetting();
        TextUpdate();
    }

    private void OnDisable()
    {
        DeleteCardReset();
        ChiledToObjectPoolSetting();
        ResetDeleteCard();
    }

    public void TextUpdate()
    {
        for (int i = 0; i < _cards.Count - 1; i++)
        {
            _textList[i].text = _cards[i].CardPrice().ToString();
        }
    }

    private void TextSettingSize()
    {
        for (int i = 0; i < limitCardCount; i++)
        {
            _textList.Add(null);
            _cards.Add(null);
        }
    }

    private void TextSetting()
    {
        for (int i = 0; i < _priceTextTransform.Count; i++)
        {
            TextMeshProUGUI textMeshPro = _priceTextTransform[i].gameObject.GetComponent<TextMeshProUGUI>();
            _textList[i] = textMeshPro;
            if (i != _priceTextTransform.Count - 1)
            {
                Card card = _objTransform[i].GetComponent<Card>();
                _cards[i] = card;
                textMeshPro.text = _cards[i].CardPrice().ToString();
                _textList[i].gameObject.SetActive(_objTransform[i].gameObject.activeSelf);
            }
            else
            {
                textMeshPro.text = _deleteCardPrice.ToString();
            }
        }
    }
    private void InitializeCard()
    {
        for (int i = 0; i < limitCardCount; i++)
        {
            GameObject cardobj = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagDeck);

            cardobj.transform.SetParent(SelectCardParent, false);
        }
    }
    private void DeleteCardReset()
    {
        Transform obj = SelectCardParent.GetChild(SelectCardParent.childCount - 1).transform;

        for (int i = 0; i < obj.childCount; i++)
        {
            obj.GetChild(i).gameObject.SetActive(true);
        }
    }
    private void ChiledToObjectPoolSetting()
    {
        int Length = SelectCardParent.childCount;

        for (int i = 0; i < Length; i++)
        {
            Card card = SelectCardParent.GetChild(0).GetComponent<Card>();
            card.ButtonType = CardButtonType.Idle;

            SelectCardParent.GetChild(0).gameObject.SetActive(false);
            SelectCardParent.GetChild(0).SetParent(GameManager.Instance.ObjectPool.gameObject.transform);
        }
    }
    private void PositionReading()
    {
        for (int i = 0; i < LineManagement.Count; i++)
        {
            for (int j = 0; j < LineManagement[i].childCount; j++)
            {
                Transform objtransform = LineManagement[i].GetChild(j).transform;
                _positionList.Add(objtransform);
            }
        }
    }
    private void TextTransformSetting()
    {
        for (int i = 0; i < PriceLine.childCount; i++)
        {
            Transform obj = PriceLine.GetChild(i).transform;
            _priceTextTransform.Add(obj);
        }
    }
    private void ObjectSetting()
    {
        for (int i = 0; i < _positionList.Count; i++)
        {
            _objTransform.Add(null);
        }
    }
    private void CardSetting()
    {
        for (int i = 0; i < SelectCardParent.childCount; i++)
        {
            Transform obj = SelectCardParent.GetChild(i).transform;

            if (obj.GetComponent<Card>() != null)
            {
                int number = Random.Range(0, GameManager.Instance.DataManager.CardList.Count);
                Card card = obj.GetComponent<Card>();
                card.CardNumber = number;
                card.LoadCardData();

                if (i != SelectCardParent.childCount - 1)
                {
                    card.ButtonType = CardButtonType.Buy;
                }
                else
                {
                    card.ButtonType = CardButtonType.UI;
                    for (int j = 0; j < obj.childCount; j++)
                    {
                        if (obj.GetChild(j).name == "CardFront")
                        {
                            Image image = obj.GetChild(j).GetComponent<Image>();
                            image.sprite = GameManager.Instance.SpriteManager.DeleteCardSprite;
                            continue;
                        }
                        
                        if (obj.GetChild(j).name == "Describle")
                        {

                            TextMeshProUGUI DescribleTxt = obj.GetChild(j).GetComponent<TextMeshProUGUI>();
                            DescribleTxt.text = "Delete";
                            continue;
                        }
                        obj.GetChild(j).gameObject.SetActive(false);
                    }
                }
            }
            _objTransform[i] = obj;
        }
    }
    // 포지션 세팅 
    private void PositionSetting()
    {
        int count = 0;
        for (int i = 0; i < _positionList.Count; i++)
        {
            if ((FirstNumber - 1).Equals(i) || (SecondNumber - 1).Equals(i))
            {
                count++;
                continue;
            }
            _objTransform[i - count].position = _positionList[i].position;
            int numbers = i - count;
            float _priceTransformX = _objTransform[numbers].position.x;
            float _priceTransformY = _objTransform[numbers].position.y + (_offsetVec.y * (Screen.height / _setHeight));

            _priceTextTransform[i - count].position = new Vector3(_priceTransformX, _priceTransformY, 0);
        }
    }

    public void OnDeletSelectArea()
    {
        if (!_isDeleted)
        {
            GameManager.Instance.Player.SubGold(_deleteCardPrice);
            Debug.Log(GameManager.Instance.Player.Gold);
            StageUIManager.Instance.DeleteUIOn();
            _isDeleted = true;
        }
    }
    private void ResetDeleteCard()
    {
        _isDeleted = false;
    }

    public void FindCardPriceText(GameObject gameobj)
    {

        for (int i = 0; i < _cards.Count; i++)
        {
            if (_cards[i].gameObject == gameobj)
            {
                _textList[i].gameObject.SetActive(false);
                break;
            }
        }
    }
}