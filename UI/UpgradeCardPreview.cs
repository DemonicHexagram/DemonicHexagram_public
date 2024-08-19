using System;
using UnityEngine;

public class UpgradeCardPreview : Card
{
    public int index;
    public void UpgradeCardPreviewUpdate(int seletCardIndex,int code)
    {
        index = seletCardIndex;
        LoadCardData(code);
    }

    public void AddUpgradeSeletCard()
    {
        GameManager.Instance.Player.playerDeck.UpgradeCardToDeck(index, _code - 1);
    }
}

