using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    public RectTransform turnPanelRect;
    public Image playerTurnImage;
    public TextMeshProUGUI playerTurnText;

    public IEnumerator TurnChangeCoroutine()
    {
        turnPanelRect.sizeDelta = new Vector2(turnPanelRect.sizeDelta.x, 0);
        Color imageColor = playerTurnImage.color;
        imageColor.a = 0;
        playerTurnImage.color = imageColor;

        Color textColor = playerTurnText.color;
        textColor.a = 0;
        playerTurnText.color = textColor;

        Sequence sequence = DOTween.Sequence();
        sequence.Append(turnPanelRect.DOSizeDelta(new Vector2(turnPanelRect.sizeDelta.x, 100), KeyWordManager.flt_TurnPanelAnimeDuration));
        sequence.Join(playerTurnImage.DOFade(1, KeyWordManager.flt_TurnPanelAnimeDuration));
        sequence.Join(playerTurnText.DOFade(1, KeyWordManager.flt_TurnPanelAnimeDuration));
        sequence.AppendInterval(KeyWordManager.flt_TurnPanelDisplayDuration);
        sequence.Append(turnPanelRect.DOSizeDelta(new Vector2(turnPanelRect.sizeDelta.x, 0), KeyWordManager.flt_TurnPanelAnimeDuration));
        sequence.Join(playerTurnImage.DOFade(0, KeyWordManager.flt_TurnPanelAnimeDuration));
        sequence.Join(playerTurnText.DOFade(0, KeyWordManager.flt_TurnPanelAnimeDuration));
        sequence.Play();

        yield return sequence.WaitForCompletion();
    }
}
