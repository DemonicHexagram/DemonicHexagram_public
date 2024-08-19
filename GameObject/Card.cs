using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [Header("CardNumbers")]
    [SerializeField] protected int _cardNumber;
    [SerializeField] protected int selectedCard;
    public int seletCardIndex;
    public int _code;

    [Header("ButtonType")]
    [SerializeField] protected CardButtonType buttonType;
    private CardData _cardData;

    [Header("ChildGameObject")]
    [SerializeField] private GameObject _costObj;
    [SerializeField] private GameObject _damageObj;
    [SerializeField] private GameObject _shieldObj;
    [SerializeField] private GameObject _maskObj;
    [SerializeField] private GameObject _FrameObj;
    [SerializeField] private GameObject _gradeObj;

    [Header("ChildImageCheck")]
    [SerializeField] private Image _cardFrontImage;
    private Image _costImage;
    private Image _damageImage;
    private Image _shieldImage;
    private Image _frameImage;
    private Image _gradeImage;
    private Image _maskImage;
    private Image _backGroundeImage;
    private Image _IconImage;

    [Header("ChildTextMeshProUGUI")]
    [SerializeField] protected TextMeshProUGUI _nameTxt;
    [SerializeField] protected TextMeshProUGUI _describleTxt;

    protected TextMeshProUGUI _costTxt;
    protected TextMeshProUGUI _damageTxt;
    protected TextMeshProUGUI _shieldTxt;

    bool _isSelectCard = false;

    public CardData CardData { get { return _cardData; } set { _cardData = value; } }
    public int CardNumber
    {
        get { return _cardNumber; }
        set { _cardNumber = value; }
    }
    public CardButtonType ButtonType { get { return buttonType; } set { buttonType = value; } }

    //마우스 검출 관련 
    Ray ray;
    RaycastHit hit;
    Enemy enemy;
    GameObject hitObject;
    //

    private void Awake()
    {
        _cardData = new CardData();
        _costImage = _costObj.GetComponent<Image>();
        _damageImage = _damageObj.GetComponent<Image>();
        _shieldImage = _shieldObj.GetComponent<Image>();
        _maskImage = _maskObj.GetComponent<Image>();
        _frameImage = _FrameObj.GetComponent<Image>();
        _gradeImage = _gradeObj.GetComponent<Image>();

        for (int i = 0; i < _maskObj.transform.childCount; i++)
        {
            Image ChildImage = _maskObj.transform.GetChild(i).gameObject.GetComponent<Image>();

            if (_maskObj.transform.GetChild(i).gameObject.name.CompareTo("BackGround") == 0)
            {
                _backGroundeImage = ChildImage;
            }

            else if (_maskObj.transform.GetChild(i).gameObject.name.CompareTo("MaskInIcon") == 0)
            {
                _IconImage = ChildImage;
            }
        }

        _costTxt = _costObj.GetComponentInChildren<TextMeshProUGUI>();
        _damageTxt = _damageObj.GetComponentInChildren<TextMeshProUGUI>();
        _shieldTxt = _shieldObj.GetComponentInChildren<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        //StartCoroutine("LoadCardDataRanderer");
        LoadCardData();
    }
    public void LoadCardData(int cardNumber)
    {
        _cardData = GameManager.Instance.DataManager.CardList[cardNumber];
        CardNumber = cardNumber;
        UpdateAll();
    }

    public void LoadCardData()
    {
        LoadCardData(_cardNumber);
    }

    public IEnumerator LoadCardDataRanderer()
    {
        yield return null;
        LoadCardData(_cardNumber);
    }

    public void OnUseCard(Enemy enemy)
    {
        CardData cardData = _cardData;
        BattleManager.Instance._curcost -= cardData.Cost;
        foreach (StatusEffect effect in Enum.GetValues(typeof(StatusEffect)))
        {

        }
        BattleManager.Instance.UseThisCard(cardData.Type, cardData.Damage, cardData.Shield, cardData.Draw, cardData.Mana, cardData.Effect, cardData.Elemental, enemy, cardData.Count);


    }
    public void UpdateCardUI()
    {
        _code = CardData.CardCode;
        _nameTxt.text = CardData.Name.ToString();
        _describleTxt.text = CardData.Describle.ToString();
        _costTxt.text = CardData.Cost.ToString();
        _damageTxt.text = CardData.Damage.ToString();
        _shieldTxt.text = CardData.Shield.ToString();
    }
    public void UpdateCardUI(CardData cardData)
    {
        _code = cardData.CardCode;
        _nameTxt.text = cardData.Name.ToString();
        _describleTxt.text = cardData.Describle.ToString();
        _costTxt.text = cardData.Cost.ToString();
        _damageTxt.text = cardData.Damage.ToString();
        _shieldTxt.text = cardData.Shield.ToString();
    }

    private void Update()
    {
        if (buttonType != CardButtonType.UI)
        {
            _describleTxt.text =
                string.Format(CardData.Describle, CardData.Damage
                + (GameManager.Instance.Player.activeEffects.ContainsKey(StatusEffect.Strength) ? CardData.Damage : 0));

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                enemy = hit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    _describleTxt.text =
                        string.Format(CardData.Describle, Mathf.Ceil(CardData.Damage
                        * ((GameManager.Instance.Player.activeEffects.ContainsKey(StatusEffect.Strength) ? 2 : 1)
                        * (GameManager.Instance.Player.activeEffects.ContainsKey(StatusEffect.Weak) ? 0.5f : 1)
                            )));
                }
            }
        }
    }
    public CardData UpdateCardData()
    {
        CardData cardData = _cardData.DeepCopy();
        if (GameManager.Instance.Player.activeEffects.ContainsKey(StatusEffect.Strength) && GameManager.Instance.Player.activeEffects[StatusEffect.Strength] > 0)
        {
            cardData.Damage *= 2;
        }

        return cardData;
    }
    public void UpgradeCard()
    {
        selectedCard = SelectedCardCode();
        //gameObject.SetActive(false);
        StageUIManager.Instance.OnUpgradePanel();
        //GameObject con = GameObject.Find("UpgardPreview");
        //transform.SetParent(con.transform);
        GameObject selectCardPreviewObject = GameObject.Find("SelectCard"); // 오브젝트 이름 사용
        if (selectCardPreviewObject != null)
        {
            SelectCardPreview selectCardPreview = selectCardPreviewObject.GetComponent<SelectCardPreview>();
            if (selectCardPreview != null)
            {
                selectCardPreview.CardPreviewUpdate(selectedCard - 1);
            }
            else
            {
                Debug.LogError("SelectCardPreview 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("SelectCardPreview 오브젝트를 찾을 수 없습니다.");
        }
        GameObject upgradeCardPreviewObject = GameObject.Find("UpgradeCard"); // 오브젝트 이름 사용
        if (upgradeCardPreviewObject != null)
        {
            UpgradeCardPreview upgradeCardPreview = upgradeCardPreviewObject.GetComponent<UpgradeCardPreview>();
            if (upgradeCardPreview != null)
            {
                upgradeCardPreview.UpgradeCardPreviewUpdate(seletCardIndex, selectedCard);
            }
            else
            {
                Debug.LogError("SelectCardPreview 컴포넌트를 찾을 수 없습니다.");
            }
        }
        else
        {
            Debug.LogError("SelectCardPreview 오브젝트를 찾을 수 없습니다.");
        }
    }
    public void OnButtonTypeCard()
    {
        switch (buttonType)
        {
            case CardButtonType.Idle:
                ScaleCard();
                break;
            case CardButtonType.Upgrade:
                UpgradeCard();
                break;
            case CardButtonType.Buy:
                BuyCard();
                break;
            case CardButtonType.Select:
                SelectCard();
                break;
            case CardButtonType.Delete:
                DeleteCard();
                break;
            case CardButtonType.UI:
                StageUIManager.Instance.DeleteUIOn();
                break;
            default:
                break;
        }
    }
    private void DeleteCard()
    {
        SoundManager.Instance.PlaySfx(SFX.CardDraw);

        StageUIManager.Instance.OnCardDelet();

        GameManager.Instance.Player.playerDeck.RemoveCardFromDeck(selectedCard);

    }
    public int CardPrice()
    {
        int AddPrice = 0;
        switch (_cardData.Grade)
        {
            case CardGrade.Commmod:
                AddPrice = 50;
                break;
            case CardGrade.UnCommon:
                AddPrice = 75;
                break;
            case CardGrade.Rare:
                AddPrice = 100;
                break;
            default:
                break;
        }

        int price = AddPrice + CardNumber;

        return price;
    }
    private void ScaleCard()
    {
        // 미구현
    }
    private void BuyCard()
    {
        SoundManager.Instance.PlaySfx(SFX.CardDraw);
        GameObject cardObj = EventSystem.current.currentSelectedGameObject;

        int subPrice = CardPrice();

        if (subPrice > GameManager.Instance.Player.Gold)
        {
            return;
        }

        int gold = GameManager.Instance.Player.Gold - subPrice;

        GameManager.Instance.Player.Gold = gold;

        GameManager.Instance.Player.playerDeck.AddCardToDeck(CardNumber);

        StageUIManager.Instance.GetStoreUI().FindCardPriceText(cardObj);
        cardObj.SetActive(false);
    }
    private void SelectCard()
    {
        if (!_isSelectCard)
        {
            GameManager.Instance.Player.playerDeck.AddCardToDeck(CardNumber);
            _isSelectCard = true;
        }
    }
    public int SelectedCardCode()
    {
        selectedCard = _code;
        return selectedCard;
    }
    public void UpdateAll()
    {
        CardData cardData = UpdateCardData();
        UpdateCardUI(cardData);
        UpdateSprite();
    }
    private void UpdateSprite()
    {
        UpdateCardSpriteData();
        UpdateCardGradeSprite();
    }
    private void UpdateCardSpriteData()
    {
        Elemental cardType = CardData.Elemental;

        CardSpriteData cardSpriteDataList = GameManager.Instance.SpriteManager.CardResourcessList[(int)cardType];

        switch (cardType)
        {
            case Elemental.None:
                SpriteSetting(cardSpriteDataList);
                break;
            case Elemental.Fire:
                SpriteSetting(cardSpriteDataList);
                break;
            case Elemental.Water:
                SpriteSetting(cardSpriteDataList);
                break;
            case Elemental.Thunder:
                SpriteSetting(cardSpriteDataList);
                break;
            default:
                break;
        }
    }
    private void UpdateCardGradeSprite()
    {
        int number = 0;

        if (CardNumber % 2 == 0)
        {
            number = 0;
        }
        else if (CardNumber % 2 == 1)
        {
            number = 1;
        }

        Sprite cardGradeType = GameManager.Instance.SpriteManager.CardGradeList[number];
        _gradeImage.sprite = cardGradeType;
    }
    private void SpriteSetting(CardSpriteData cardSpriteData)
    {
        _cardFrontImage.sprite = cardSpriteData.CardFront;
        _costImage.sprite = cardSpriteData.CardCost;
        _damageImage.sprite = cardSpriteData.CardDamage;
        _shieldImage.sprite = cardSpriteData.CardShield;
        _frameImage.sprite = cardSpriteData.CardFrame;
        _maskImage.sprite = cardSpriteData.CardMask;
        _backGroundeImage.sprite = cardSpriteData.CardBackGround;
    }

    public void ResetCard()
    {
        buttonType = CardButtonType.Idle;
        _isSelectCard = false;
    }
}


