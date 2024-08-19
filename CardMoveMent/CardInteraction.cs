using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardInteraction
{
    private CardWrapper cardWrapper;
    private Canvas canvas;
    private bool isDragged = false;
    private Vector2 dragStartPos;

    public CardInteraction(CardWrapper wrapper, Canvas parentCanvas)
    {
        cardWrapper = wrapper;
        canvas = parentCanvas;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (isDragged)
        {
            return;
        }

        if (cardWrapper.HasReachedTarget)
        {
            if (cardWrapper.zoomConfig.bringToFrontOnHover)
            {
                if (canvas != null)
                {
                    canvas.sortingOrder = cardWrapper.zoomConfig.zoomedSortOrder;
                }
            }
            cardWrapper.isHovered = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isDragged)
        {
            cardWrapper.isHovered = true;
            return;
        }
        if (canvas != null)
        {
            canvas.sortingOrder = cardWrapper.uiLayer;
        }
        cardWrapper.isHovered = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragged = true;
        dragStartPos = new Vector2(cardWrapper.transform.position.x - eventData.position.x, cardWrapper.transform.position.y - eventData.position.y);
        cardWrapper.rectTransform.DOKill();
        cardWrapper.container.OnCardDragStart(cardWrapper);
        cardWrapper.HasReachedTarget = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragged = false;
        cardWrapper.container.OnCardDragEnd();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isDragged && cardWrapper.currentCard.CardData.Type != 0)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out localPoint);
            cardWrapper.rectTransform.localPosition = localPoint + dragStartPos;
        }

    }
}
