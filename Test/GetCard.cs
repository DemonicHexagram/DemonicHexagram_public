using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCard : MonoBehaviour
{
    private Card card;

    private void Start()
    {
        card = GetComponent<Card>();
    }
    public void GetThisCard()
    {
        GameManager.Instance.Player.playerDeck.AddCardToDeck(card.CardNumber);
        Debug.Log($"{card.CardNumber}의 카드를 획득함");
    }
}
