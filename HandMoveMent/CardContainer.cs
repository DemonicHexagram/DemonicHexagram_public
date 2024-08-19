using config;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardContainer : MonoBehaviour
{
    [Header("BattleDeck")]
    [SerializeField]
    private Transform battleDeck;
    public TextMeshProUGUI battleDeckCount;

    [Header("Grave")]
    [SerializeField]
    private Transform grave;
    public TextMeshProUGUI graveCount;

    [Header("Constraints")]
    [SerializeField]
    private bool forceFitContainer;

    [Header("Alignment")]
    [SerializeField]
    public CardAlignment alignment = CardAlignment.Center;

    [Header("Rotation")]
    [SerializeField]
    [Range(0f, 90f)]
    private float maxCardRotation;

    [SerializeField]
    private float maxHeightDisplacement;

    [SerializeField]
    public ZoomConfig zoomConfig;

    [SerializeField]
    public AnimationSpeedConfig animationSpeedConfig;

    [SerializeField]
    public CardPlayConfig cardPlayConfig;

    [Header("Deck")]
    [SerializeField]
    private Transform cardContainer;

    public List<CardWrapper> cards = new();

    private RectTransform rectTransform;
    public CardWrapper CurrentDraggedCard { get; set; }

    [SerializeField] public LayerMask enemyLayer;

    private CardPositionManager cardPositionManager;
    private CardRotationManager cardRotationManager;
    private CardOrderManager cardOrderManager;
    private CardDragHandler cardDragHandler;

    public GameObject bezierCurve;
    public BezierCurveFixWithUI bezierCurveFixWithUI;

    public CardWrapper CurrentHoveringCard;



    private void Start()
    {
        BattleManager.Instance.battleDeck.cardContainer = this;
        rectTransform = GetComponent<RectTransform>();
        cardPositionManager = new CardPositionManager(this);
        cardRotationManager = new CardRotationManager(this);
        cardOrderManager = new CardOrderManager(this);
        cardDragHandler = new CardDragHandler(this);
        bezierCurveFixWithUI = bezierCurve.GetComponent<BezierCurveFixWithUI>();
        InitCards();
    }

    private void InitCards()
    {
        SetUpCards();
        SetCardsAnchor();
        UpdateCardCountInDeck();
    }

    private void SetCardsRotation()
    {
        cardRotationManager.SetCardsRotation(cards, maxCardRotation, maxHeightDisplacement);
    }

    void Update()
    {
        UpdateCards();
        if (CurrentDraggedCard != null)
        {
            cardDragHandler.CastRayToFindEnemy();
        }
    }

    public void DrawCardFromDeck(GameObject gameObject)
    {
        if (cards.Count > 0)
        {
            CardWrapper cardWrapper = gameObject.GetComponent<CardWrapper>();
            gameObject.transform.position = battleDeck.position;

            if (cardWrapper != null)
            {
                cards.Add(cardWrapper);
                InitializeCard(cardWrapper);

                Vector3 endPosition = cardWrapper.targetPosition;
                cardWrapper.DrawCard(battleDeck.position, endPosition, KeyWordManager.flt_DrawCardAnime);                
            }
            UpdateCardCountInDeck();
        }
    }

    private void InitializeCard(CardWrapper cardWrapper)
    {
        cardWrapper.zoomConfig = zoomConfig;
        cardWrapper.animationSpeedConfig = animationSpeedConfig;
        cardWrapper.container = this;

        AddOtherComponentsIfNeeded(cardWrapper);
    }

    void SetUpCards()
    {
        cards.Clear();
        foreach (Transform card in transform)
        {
            var wrapper = card.GetComponent<CardWrapper>();
            if (wrapper == null)
            {
                wrapper = card.gameObject.AddComponent<CardWrapper>();
            }
            InitializeCard(wrapper);

            cards.Add(wrapper);
        }
    }

    private void AddOtherComponentsIfNeeded(CardWrapper wrapper)
    {
        var canvas = wrapper.GetComponent<Canvas>();
        if (canvas == null)
        {
            canvas = wrapper.gameObject.AddComponent<Canvas>();
        }

        canvas.overrideSorting = true;

        if (wrapper.GetComponent<GraphicRaycaster>() == null)
        {
            wrapper.gameObject.AddComponent<GraphicRaycaster>();
        }
    }

    private void UpdateCards()
    {
        if (transform.childCount != cards.Count)
        {
            InitCards();
        }

        if (cards.Count == 0)
        {
            return;
        }
        ReOrderCards();
    }
    private void ReOrderCards()
    {
        cardPositionManager.SetCardsPosition(rectTransform, cards, forceFitContainer);
        SetCardsRotation();
        cardOrderManager.SetCardsUILayers(cards, zoomConfig.defaultSortOrder);
    }

    private void SetCardsAnchor()
    {
        foreach (CardWrapper child in cards)
        {
            child.SetAnchor(new Vector2(0, 0.5f), new Vector2(0, 0.5f));
        }
    }

    public void OnCardDragStart(CardWrapper card)
    {
        cardDragHandler.OnCardDragStart(card);
    }

    public void OnCardDragEnd()
    {
        cardDragHandler.OnCardDragEnd(CurrentDraggedCard);
    }

    public void UseCard(CardWrapper card)
    {
        card.ResetCardState();
        cards.Remove(card);
        card.gameObject.SetActive(false);
        card.gameObject.transform.SetParent(GameManager.Instance.ObjectPool.gameObject.transform);
        UpdateCardCountInDeck();
        card.ReStateCards();
        ReOrderCards();
    }

    public void DiscardAllCards()
    {

        foreach (CardWrapper card in cards)
        {
            card.gameObject.SetActive(false);
            card.gameObject.transform.SetParent(GameManager.Instance.ObjectPool.gameObject.transform);
        }
    }

    public void UpdateCardCountInDeck()
    {
        battleDeckCount.text = BattleManager.Instance.battleDeck.battleDeck.Count.ToString();
        graveCount.text = BattleManager.Instance.battleDeck.grave.Count.ToString();
    }
    public void SetCurrrentHoveringCard(CardWrapper card)
    {
        CurrentHoveringCard = card;
        GameManager.Instance.cardDragged = card;
    }
    public void UnsetCurrrentHoveringCard()
    {
        CurrentHoveringCard = null;
    }
}