using System.Collections.Generic;

public class CardOrderManager
{
    private CardContainer container;

    public CardOrderManager(CardContainer cardContainer)
    {
        container = cardContainer;
    }

    public void SetCardsUILayers(List<CardWrapper> cards, int defaultSortOrder)
    {
        for (var i = 0; i < cards.Count; i++)
        {
            cards[i].uiLayer = defaultSortOrder - i;
        }
    }
}
