using UnityEngine;

public class UpgradePenel : MonoBehaviour
{
    [SerializeField] private GameObject selectCard;
    [SerializeField] private GameObject upgradeCard;

    private Card selectcard;
    private Card upgradecard;


    private void Awake()
    {
        selectcard = selectCard.GetComponent<Card>();
        upgradecard = upgradeCard.GetComponent<Card>();

    }
    public void CardPreview(int code)
    {
        Debug.Log("업그레이드 카드 띄우기");
        CardData selectcardData = GameManager.Instance.DataManager.CardList[code-1];
        CardData upgradeData = GameManager.Instance.DataManager.CardList[code];

        selectcard.UpdateCardUI(selectcardData);
        upgradecard.UpdateCardUI(upgradeData);

    }
}

