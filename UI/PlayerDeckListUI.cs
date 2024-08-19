using UnityEngine;

public class PlayerDeckListUI : MonoBehaviour
{
    [SerializeField] protected Transform ParentTransform;
    virtual protected void OnEnable()
    {
        int size = GameManager.Instance.Player.playerDeck.playerDeckList.Count;
        for (int i = 0; i < size; i++)
        {
            GameObject obj = GameManager.Instance.ObjectPool.SpawnFromPool(KeyWordManager.str_PoolTagDeck);
            obj.transform.SetParent(ParentTransform);
            Card card = obj.GetComponent<Card>();

            card.CardNumber = GameManager.Instance.Player.playerDeck.playerDeckList[i];
            card.seletCardIndex = i;
            card.LoadCardData();
        }
    }

    private void OnDisable()
    {
        int size = ParentTransform.childCount;
        for (int i = 0; i < size; i++)
        {
            ParentTransform.GetChild(0).gameObject.SetActive(false);
            ParentTransform.GetChild(0).SetParent(GameManager.Instance.ObjectPool.gameObject.transform);
        }
    }

    public void OnButtonObjectOff()
    {
        SoundManager.Instance.PlaySfx(SFX.ButtonClick);
        gameObject.SetActive(false);
    }
}
