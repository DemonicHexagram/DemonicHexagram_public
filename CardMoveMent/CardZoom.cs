using UnityEngine;
using config;

public class CardZoom
{
    private RectTransform rectTransform;

    public CardZoom(RectTransform rect)
    {
        rectTransform = rect;
    }

    public void UpdateScale(bool isDragged, bool isHovered, ZoomConfig zoomConfig, AnimationSpeedConfig animationSpeedConfig)
    {
        var targetZoom = (isDragged || isHovered) && zoomConfig.zoomOnHover ? zoomConfig.multiplier : 1;
        var delta = Mathf.Abs(rectTransform.localScale.x - targetZoom);
        var newZoom = Mathf.Lerp(rectTransform.localScale.x, targetZoom, animationSpeedConfig.zoom / delta * Time.deltaTime);
        rectTransform.localScale = new Vector3(newZoom, newZoom, 1);
    }
}
