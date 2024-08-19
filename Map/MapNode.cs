using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class MapNode : MonoBehaviour, IPointerEnterHandler,  IPointerExitHandler
{
    public SpriteRenderer sr;
    public Image image;
    public SpriteRenderer visitedCircle;
    public Image circleImage;
    public Image visitedCircleImage;
    public Sprite uncoverSprite;

    public Node Node { get; private set; }
    public NodeBlueprint Blueprint { get; private set; }

    private float initialScale;

    public void SetUp(Node node, NodeBlueprint blueprint, Sprite uncoverSprite)
    {
        Node = node;
        Blueprint = blueprint;
        this.uncoverSprite = uncoverSprite;
        if (sr != null) sr.sprite = uncoverSprite;
        if (image != null) image.sprite = uncoverSprite;
        if (node.nodeType == NodeType.Boss) transform.localScale *= 1.5f;
        if (sr != null) initialScale = sr.transform.localScale.x;
        if (image != null) initialScale = image.transform.localScale.x;

        if (visitedCircle != null)
        {
            visitedCircle.color = MapView.Instance.visitedColor;
            visitedCircle.gameObject.SetActive(false);
        }

        if (circleImage != null)
        {
            circleImage.color = MapView.Instance.visitedColor;
            circleImage.gameObject.SetActive(false);
        }

        SetState(NodeStates.Locked);
    }

    public void SetState(NodeStates state)
    {
        if (visitedCircle != null) visitedCircle.gameObject.SetActive(false);
        if (circleImage != null) circleImage.gameObject.SetActive(false);

        switch (state)
        {
            case NodeStates.Locked:
                if (sr != null)
                {
                    sr.DOKill();
                    sr.color = MapView.Instance.lockedColor;
                    sr.sprite = Blueprint.sprite;
                }

                if (image != null)
                {
                    image.DOKill();
                    image.color = MapView.Instance.lockedColor;
                    image.sprite = Blueprint.sprite;
                }

                break;
            case NodeStates.Covered:
                if (sr != null)
                {
                    sr.DOKill();
                    sr.color = MapView.Instance.lockedColor;
                    sr.sprite = uncoverSprite;
                }

                if (image != null)
                {
                    image.DOKill();
                    image.color = MapView.Instance.lockedColor;
                    image.sprite = uncoverSprite;
                }

                break;
            case NodeStates.Visited:
                if (sr != null)
                {
                    sr.DOKill();
                    sr.color = MapView.Instance.visitedColor;
                }

                if (image != null)
                {
                    image.DOKill();
                    image.color = MapView.Instance.visitedColor;
                }

                if (visitedCircle != null) visitedCircle.gameObject.SetActive(true);
                if (circleImage != null) circleImage.gameObject.SetActive(true);
                break;
            case NodeStates.Attainable:
                if (sr != null)
                {
                    sr.color = MapView.Instance.lockedColor;
                    sr.DOKill();
                    sr.DOColor(MapView.Instance.visitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                    sr.sprite = Blueprint.sprite;
                }

                if (image != null)
                {
                    image.color = MapView.Instance.lockedColor;
                    image.DOKill();
                    image.DOColor(MapView.Instance.visitedColor, 0.5f).SetLoops(-1, LoopType.Yoyo);
                    image.sprite = Blueprint.sprite;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void OnPointerEnter(PointerEventData data)
    {
        if (sr != null)
        {
            sr.transform.DOKill();
            sr.transform.DOScale(initialScale * KeyWordManager.flt_HoverScaleFactor, KeyWordManager.flt_NodeAnimeDuration);
        }

        if (image != null)
        {
            image.transform.DOKill();
            image.transform.DOScale(initialScale * KeyWordManager.flt_HoverScaleFactor, KeyWordManager.flt_NodeAnimeDuration);
        }
    }

    public void OnPointerExit(PointerEventData data)
    {
        if (sr != null)
        {
            sr.transform.DOKill();
            sr.transform.DOScale(initialScale, KeyWordManager.flt_NodeAnimeDuration);
        }

        if (image != null)
        {
            image.transform.DOKill();
            image.transform.DOScale(initialScale, KeyWordManager.flt_NodeAnimeDuration);
        }
    }

    public void ShowSwirlAnimation()
    {
        if (visitedCircleImage == null)
            return;

        visitedCircleImage.fillAmount = 0;

        DOTween.To(() => visitedCircleImage.fillAmount, x => visitedCircleImage.fillAmount = x, 1f, KeyWordManager.flt_NodeAnimeDuration);
    }
}