using System.Collections.Generic;
using UnityEngine;

public class CardPositionManager
{
    private CardContainer container;

    public CardPositionManager(CardContainer cardContainer)
    {
        container = cardContainer;
    }

    public void SetCardsPosition(RectTransform rectTransform, List<CardWrapper> cards, bool forceFitContainer)
    {
        float cardsTotalWidth = 0;
        for (int i = 0; i < cards.Count; i++)
        {
            cardsTotalWidth += cards[i].width * cards[i].transform.lossyScale.x;
        }

        float containerWidth = rectTransform.rect.width * container.transform.lossyScale.x;
        if (forceFitContainer && cardsTotalWidth > containerWidth)
        {
            DistributeChildrenToFitContainer(rectTransform, cards, cardsTotalWidth);
        }
        else
        {
            DistributeChildrenWithoutOverlap(rectTransform, cards, cardsTotalWidth);
        }
    }

    private void DistributeChildrenToFitContainer(RectTransform rectTransform, List<CardWrapper> cards, float childrenTotalWidth)
    {
        var width = rectTransform.rect.width * container.transform.lossyScale.x;
        var distanceBetweenChildren = (width - childrenTotalWidth) / (cards.Count - 1);
        var currentX = container.transform.position.x - width / 2;
        foreach (CardWrapper child in cards)
        {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentX + adjustedChildWidth / 2, container.transform.position.y);
            currentX += adjustedChildWidth + distanceBetweenChildren;
        }
    }

    private void DistributeChildrenWithoutOverlap(RectTransform rectTransform, List<CardWrapper> cards, float childrenTotalWidth)
    {
        var currentPosition = GetAnchorPositionByAlignment(rectTransform, childrenTotalWidth);
        foreach (CardWrapper child in cards)
        {
            var adjustedChildWidth = child.width * child.transform.lossyScale.x;
            child.targetPosition = new Vector2(currentPosition + adjustedChildWidth / 2, container.transform.position.y);
            currentPosition += adjustedChildWidth;
        }
    }

    private float GetAnchorPositionByAlignment(RectTransform rectTransform, float childrenWidth)
    {
        var containerWidthInGlobalSpace = rectTransform.rect.width * container.transform.lossyScale.x;
        switch (container.alignment)
        {
            case CardAlignment.Left:
                return container.transform.position.x - containerWidthInGlobalSpace / 2;
            case CardAlignment.Center:
                return container.transform.position.x - childrenWidth / 2;
            case CardAlignment.Right:
                return container.transform.position.x + containerWidthInGlobalSpace / 2 - childrenWidth;
            default:
                return 0;
        }
    }
}
