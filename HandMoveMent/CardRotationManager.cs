using System.Collections.Generic;
using UnityEngine;

public class CardRotationManager
{
    private CardContainer container;

    public CardRotationManager(CardContainer cardContainer)
    {
        container = cardContainer;
    }

    public void SetCardsRotation(List<CardWrapper> cards, float maxCardRotation, float maxHeightDisplacement)
    {
        for (var i = 0; i < cards.Count; i++)
        {
            cards[i].targetRotation = GetCardRotation(cards, i, maxCardRotation);
            cards[i].targetVerticalDisplacement = GetCardVerticalDisplacement(cards, i, maxHeightDisplacement);
        }
    }

    private float GetCardVerticalDisplacement(List<CardWrapper> cards, int index, float maxHeightDisplacement)
    {
        if (cards.Count < 3) return 0;
        return maxHeightDisplacement *
               (1 - Mathf.Pow(index - (cards.Count - 1) / 2f, 2) / Mathf.Pow((cards.Count - 1) / 2f, 2));
    }

    private float GetCardRotation(List<CardWrapper> cards, int index, float maxCardRotation)
    {
        if (cards.Count < 3) return 0;
        return -maxCardRotation * (index - (cards.Count - 1) / 2f) / ((cards.Count - 1) / 2f);
    }
}
