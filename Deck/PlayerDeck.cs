using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeck : MonoBehaviour
{
    public List<int> playerDeckList;

    private void Start()
    {
        Initialize();
    }

    public void Initialize()
    {
        playerDeckList = new List<int>();

        for (int i = 0; i < 5; i++)
        {
            playerDeckList.Add(0);
        }
        for (int i = 0; i < 5; i++)
        {
            playerDeckList.Add(2);
        }

        Debug.Log("PlayerDeck Initialized: " + string.Join(", ", playerDeckList));
        GameManager.Instance.Player.playerDeck = this;
        playerDeckList.Add(4);
        playerDeckList.Add(16);
        playerDeckList.Add(24);

    }


    public void AddCardToDeck(int selectedCard)
    {
        playerDeckList.Add(selectedCard);
    }

    public void UpgradeCardToDeck(int selectedCard, int upgradeCard)
    {
        if (selectedCard >= 0 && selectedCard < playerDeckList.Count)
        {
            playerDeckList[selectedCard] = upgradeCard;
        }
        else
        {
            Debug.LogError("인덱스가 리스트의 범위를 벗어났습니다.");
        }
    }



    public void RemoveCardFromDeck(int selectedCard)
    {
        playerDeckList.Remove(selectedCard);
    }

    public void ResetPlayerDeck()
    {
        playerDeckList.Clear();
    }

    public void InitializeBattleDeck(BattleDeck battleDeck)
    {
        battleDeck.battleDeck = new List<int>(playerDeckList);
        battleDeck.Shuffle(battleDeck.battleDeck);
        Debug.Log("BattleDeck Initialized: " + string.Join(", ", battleDeck.battleDeck));
    }
}
