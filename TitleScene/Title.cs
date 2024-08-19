using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    private RectTransform _titleImage;
    [SerializeField] private RectTransform _startPos;
    [SerializeField] private RectTransform _endPos;

    void Start()
    {
        _titleImage = GetComponent<RectTransform>();

        if (_titleImage != null && _startPos != null && _endPos != null)
        {
            _titleImage.anchoredPosition = _startPos.anchoredPosition;
            _titleImage.DOAnchorPos(_endPos.anchoredPosition, 3f)
                .SetEase(Ease.InOutQuad);
        }
    }
}
