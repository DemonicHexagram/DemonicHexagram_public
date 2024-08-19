using UnityEngine;

public class DeleteDeckUI : PlayerDeckListUI
{
    override protected void OnEnable()
    {
        int childsize = GameManager.Instance.Player.playerDeck.playerDeckList.Count;

        for (int i = 0; i < childsize; i++)
        {
            GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagDeck);

            Card card = obj.GetComponent<Card>();

            card.CardNumber = GameManager.Instance.Player.playerDeck.playerDeckList[i];
            card.ButtonType = CardButtonType.Delete;
            card.seletCardIndex = i;
            card.LoadCardData();
            obj.transform.SetParent(ParentTransform.transform, false);
        }
    }
}
