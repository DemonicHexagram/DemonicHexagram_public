using config;
using DG.Tweening;
using UnityEngine;

public class CardMovement
{
    private RectTransform rectTransform;
    private CardWrapper cardWrapper;

    public CardMovement(CardWrapper wrapper, RectTransform rect)
    {
        cardWrapper = wrapper;
        rectTransform = rect;
    }

    public void UpdatePosition(Vector2 targetPosition, float targetVerticalDisplacement, AnimationSpeedConfig animationSpeedConfig, ZoomConfig zoomConfig, bool isHovered, bool isDragged, Vector2 dragStartPos)
    {
        if (!isDragged)
        {
            var target = new Vector2(targetPosition.x, targetPosition.y + targetVerticalDisplacement);
            if (isHovered && zoomConfig.overrideYPosition != -1)
            {
                float overrideYPosition = zoomConfig.overrideYPosition * Screen.height;
                target = new Vector2(target.x, overrideYPosition);
            }

            var distance = Vector2.Distance(rectTransform.position, target);
            var repositionSpeed = rectTransform.position.y > target.y || rectTransform.position.y < 0
                ? animationSpeedConfig.releasePosition
                : animationSpeedConfig.position;

            float duration = distance / repositionSpeed;
            duration = Mathf.Clamp(duration, 0.1f, 0.5f);

            rectTransform.DOMove(target, duration).SetEase(Ease.OutQuad).OnComplete(() => cardWrapper.HasReachedTarget = true);
        }
        else if (isDragged && cardWrapper.currentCard.CardData.Type != 0)
        {
            // 드래그 중인 카드의 위치를 마우스 위치로 업데이트
            var delta = (Vector2)Input.mousePosition + dragStartPos;
            rectTransform.position = new Vector2(delta.x, delta.y);
            cardWrapper.HasReachedTarget = false;
        }
    }

    public void DrawCard(Vector3 startPosition, Vector3 endPosition, float duration)
    {
        cardWrapper.transform.position = startPosition;
        rectTransform.DOMove(endPosition, duration).SetEase(Ease.OutQuad).OnComplete(() => cardWrapper.HasReachedTarget = true);
    }
}
