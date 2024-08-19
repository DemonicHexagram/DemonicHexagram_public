using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class DamageTextManager : MonoBehaviour
{
    // 싱글톤 인스턴스
    public static DamageTextManager Instance { get; private set; }

    public GameObject damageTextPrefab;
    public Canvas canvas;

    // 기본 색상 설정
    public Color defaultStartColor = Color.red;
    public Color defaultEndColor = Color.clear;

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 색상 변경을 지원하는 메서드
    public void ShowDamageText(Vector3 position, int damageAmount, Color? startColor = null, Color? endColor = null)
    {
        if (damageAmount > 0)
        {
            // 색상이 제공되지 않으면 기본 색상 사용
            Color effectiveStartColor = startColor ?? defaultStartColor;
            Color effectiveEndColor = endColor ?? defaultEndColor;

            // Instantiate the damage text prefab
            GameObject damageTextObject = Instantiate(damageTextPrefab, canvas.transform);
            TextMeshProUGUI damageText = damageTextObject.GetComponent<TextMeshProUGUI>();
            damageText.text = damageAmount.ToString();
            damageText.color = effectiveStartColor; // 시작 색상 설정

            // Set the position of the damage text
            damageTextObject.transform.position = Camera.main.WorldToScreenPoint(position);

            // Animate the damage text using DOTween
            RectTransform rectTransform = damageTextObject.GetComponent<RectTransform>();

            // 트윙크 애니메이션 시퀀스
            Sequence sequence = DOTween.Sequence();

            // 텍스트가 위로 올라가면서 투명해지도록 설정
            sequence.Append(rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + 100f, 1.5f).SetEase(Ease.OutQuad));
            sequence.Join(damageText.DOFade(0, 1.5f)); // 투명도 변화
            sequence.Join(damageText.DOColor(effectiveEndColor, 1.5f)); // 색상 변화

            // 애니메이션 완료 후 오브젝트 삭제
            sequence.OnComplete(() => Destroy(damageTextObject));
        }
    }
}
