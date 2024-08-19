using Unity.VisualScripting;
using UnityEngine;

public class UpgradeDeckListUI : PlayerDeckListUI
{
    protected override  void OnEnable()
    {
        int size = GameManager.Instance.Player.playerDeck.playerDeckList.Count;
        for (int i = 0; i < size; i++)
        {
            if (GameManager.Instance.Player.playerDeck.playerDeckList[i] % 2 == 0)
            {
                GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagDeck);
                obj.transform.SetParent(ParentTransform);
                Card card = obj.GetComponent<Card>();
                card.ButtonType = CardButtonType.Upgrade;
                card.CardNumber = GameManager.Instance.Player.playerDeck.playerDeckList[i];
                card.seletCardIndex = i;
                card.LoadCardData();
            }
            else 
            {
                Debug.Log("없어");
            }
        }
    }

}
