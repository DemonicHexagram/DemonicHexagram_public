using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class BattleDeck : MonoBehaviour
{
    public List<int> battleDeck;
    public List<int> hand;
    public List<int> grave;
    public List<int> extinction;

    public CardContainer cardContainer;  
    private Dictionary<GameObject, Card> cardCache = new Dictionary<GameObject, Card>();  


    private void Start()
    {
        Shuffle(battleDeck);
    }

    public void Shuffle(List<int> deck)
    {
        Random rand = new Random();  
        int n = deck.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);  
            int temp = deck[i];
            deck[i] = deck[j];
            deck[j] = temp;
        }
    }
    public void DiscardAllCardsInHand()
    {
        int count = hand.Count;
        for (int i = 0; i < count; i++)
        {
            MoveCard(hand, grave, hand[0]);
        }

        cardContainer.DiscardAllCards();
    }

    private void CacheCardComponent(GameObject cardObject)
    {
        if (!cardCache.ContainsKey(cardObject))
        {
            Card cardComponent = cardObject.GetComponent<Card>();
            if (cardComponent != null)
            {
                cardCache[cardObject] = cardComponent;
            }
            else
            {
                Debug.LogError("Card component not found on the pooled object!");
            }
        }
    }

    public void DrawCard(int count)
    {
        StartCoroutine(DrawCardRoute(count));
    }

    IEnumerator DrawCardRoute(int count)
    {
        for (int i = 0; i < count; i++)
        {
            SoundManager.Instance.PlaySfx(SFX.CardDraw);

            if (battleDeck.Count == 0)
            {
                MoveGraveToBattleDeck();
            }

            if (battleDeck.Count > 0)
            {
                int cardToDraw = battleDeck[0];         
                MoveCard(battleDeck, hand, cardToDraw); 
                                                         
                GameObject cardObject = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagCard);
                cardObject.transform.SetParent(cardContainer.transform, false);
                if (cardObject != null)
                {
                    CacheCardComponent(cardObject);
                    Card cardComponent = cardCache[cardObject];
                    if (cardComponent != null)
                    {
                        cardComponent.CardNumber = cardToDraw;
                        cardComponent.LoadCardData();
                        Debug.Log(cardComponent.CardData.Damage + cardComponent.CardData.Name + cardComponent.CardData.Describle);
                    }
                }
                cardContainer.DrawCardFromDeck(cardObject);
            }
            yield return new WaitForSeconds(0.2f);

        }
    }

    private void MoveGraveToBattleDeck()
    {
        battleDeck.AddRange(grave);
        Shuffle(battleDeck);
        grave.Clear();
    }

    public void MoveCard(List<int> fromList, List<int> toList, int selectedCard)
    {
        fromList.Remove(selectedCard);
        toList.Add(selectedCard);
    }

    public void ResetBattleDecks()
    {
        battleDeck.Clear();
        hand.Clear();
        grave.Clear();
        extinction.Clear();
    }
}
