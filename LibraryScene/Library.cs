using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Library : MonoBehaviour
{
    List<CardData> cardDatas = new List<CardData>();
    public GameObject DeckListUI;
    public ScrollRect scrollRect;
    
    void Start()
    {
        GameManager.Instance.Player.playerDeck.playerDeckList.Clear();
        cardDatas = GameManager.Instance.DataManager.ElementalSortCardList();
        for (int i = 0; i < cardDatas.Count; i++)
        {
            GameManager.Instance.Player.playerDeck.playerDeckList.Add(i);
        }
        Debug.Log(GameManager.Instance.Player.playerDeck.playerDeckList.Count);
        DeckListUI.SetActive(true);
        scrollRect.normalizedPosition = new Vector2(0, 1);
    }
}
