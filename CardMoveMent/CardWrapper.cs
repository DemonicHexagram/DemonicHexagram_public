using config;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardWrapper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{

    public float targetRotation;
    public Vector2 targetPosition;
    public float targetVerticalDisplacement;
    public int uiLayer;

    public RectTransform rectTransform;
    private Canvas canvas;

    public ZoomConfig zoomConfig;
    public AnimationSpeedConfig animationSpeedConfig;
    public CardContainer container;

    public bool isHovered;
    public bool isDragged;
    public Vector2 dragStartPos;

    public bool HasReachedTarget { get; set; }

    public float width => rectTransform.rect.width * rectTransform.localScale.x;

    private CardMovement cardMovement;
    private CardZoom cardZoom;
    private CardInteraction cardInteraction;
    public Card currentCard;
    public BezierCurveFixWithUI bezierCurveFixWithUI;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        cardMovement = new CardMovement(this, rectTransform);
        cardZoom = new CardZoom(rectTransform);
    }

    private void Start()
    {
        currentCard = GetComponent<Card>();
        canvas = GetComponentInParent<Canvas>();
        cardInteraction = new CardInteraction(this, canvas);
        bezierCurveFixWithUI = GetComponent<BezierCurveFixWithUI>();
    }

    private void Update()
    {
        ReStateCards();
    }
    public void ReStateCards()
    {
        UpdateRotation();
        cardMovement.UpdatePosition(targetPosition, targetVerticalDisplacement, animationSpeedConfig, zoomConfig, isHovered, isDragged, dragStartPos);
        cardZoom.UpdateScale(isDragged, isHovered, zoomConfig, animationSpeedConfig);
        UpdateUILayer();
    }

    public void ResetCardState()
    {
        isHovered = false;
        isDragged = false;
        ResetTransform();
    }

    public void ResetTransform()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        transform.localScale = Vector3.one;
    }

    private void UpdateUILayer()
    {
        if (!isHovered && !isDragged)
        {
            canvas.sortingOrder = uiLayer;
        }
    }

    private void UpdateRotation()
    {
        var crtAngle = rectTransform.rotation.eulerAngles.z;
        crtAngle = crtAngle < 0 ? crtAngle + 360 : crtAngle;
        var tempTargetRotation = (isHovered || isDragged) && zoomConfig.resetRotationOnZoom ? 0 : targetRotation;
        tempTargetRotation = tempTargetRotation < 0 ? tempTargetRotation + 360 : tempTargetRotation;
        var deltaAngle = Mathf.Abs(crtAngle - tempTargetRotation);
        if (!(deltaAngle > KeyWordManager.flt_EPS)) return;

        var adjustedCurrent = deltaAngle > 180 && crtAngle < tempTargetRotation ? crtAngle + 360 : crtAngle;
        var adjustedTarget = deltaAngle > 180 && crtAngle > tempTargetRotation ? tempTargetRotation + 360 : tempTargetRotation;
        var newDelta = Mathf.Abs(adjustedCurrent - adjustedTarget);

        var nextRotation = Mathf.Lerp(adjustedCurrent, adjustedTarget, animationSpeedConfig.rotation / newDelta * Time.deltaTime);
        rectTransform.rotation = Quaternion.Euler(0, 0, nextRotation);
    }

    public void SetAnchor(Vector2 min, Vector2 max)
    {
        rectTransform.anchorMin = min;
        rectTransform.anchorMax = max;
    }

    public void DrawCard(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        cardMovement.DrawCard(startPosition, endPosition, duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (container != null && HasReachedTarget == true)
        {
            if (container.CurrentHoveringCard == null)
            {
                GameManager.Instance.isCardDragged = true;
                container.SetCurrrentHoveringCard(this);
                cardInteraction.OnPointerEnter(eventData);
            }
        }
        else
        {
            return;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (container != null && HasReachedTarget == true)
        {
            if (container.CurrentHoveringCard != null && container.CurrentHoveringCard.isDragged == false)
            {
                GameManager.Instance.isCardDragged = false;
                container.UnsetCurrrentHoveringCard();
            }
            cardInteraction.OnPointerExit(eventData);
        }
        else
        {
            
            return;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        cardInteraction.OnPointerDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        cardInteraction.OnPointerUp(eventData);
        isDragged = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isDragged = true;
        cardInteraction.OnDrag(eventData);
    }
}
