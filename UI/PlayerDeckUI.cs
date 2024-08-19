using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDeckUI : MonoBehaviour
{
    public GameObject cardButtonPrefab;

    public Transform cardUIParent;

    public List<int> deck;

    public void Initialize()
    {
        deck = GameManager.Instance.Player.playerDeck.playerDeckList;
    }
    public void OnDeck()
    {
        foreach (int cardNumber in deck)
        {
            GameObject cardObject = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagDeck);

            cardObject.transform.SetParent(cardUIParent, false);

            Card cardScript = cardObject.GetComponent<Card>();

            cardScript.CardNumber = cardNumber;

            cardScript.UpdateCardUI();
        }
    }


}
