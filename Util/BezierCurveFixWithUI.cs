using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BezierCurveFixWithUI : MonoBehaviour
{
    public RectTransform startPoint; // 시작점
    public RectTransform controlPoint; // 제어점
    public RectTransform endPoint;

    public Canvas canvas;
    public GameObject spritePrefab; // 스프라이트 프리팹
    public GameObject arrowHeadPrefab; // 화살표 끝 부분 프리팹
    public int numPoints = 50; // 곡선의 포인트 수

    private Vector2 _mousePosition;
    private Vector2 _localPosition;

    private RectTransform _canvasRect;
    private Vector3[] positions;
    private GameObject[] sprites; // 각 포인트 지점의 스프라이트를 저장하는 배열
    private GameObject arrowHead; // 화살표 끝 부분 스프라이트
    private Camera _cam;

    public bool isEnemyAimed;

    void Start()
    {
        _canvasRect = canvas.transform as RectTransform;

        positions = new Vector3[numPoints];
        int numSprites = numPoints / 2; // 생성할 스프라이트 수
        sprites = new GameObject[numSprites];
        _cam = Camera.main;

        // 스프라이트 생성 및 비활성화
        for (int i = 0; i < sprites.Length; i++)
        {
            if (spritePrefab != null)
            {
                sprites[i] = Instantiate(spritePrefab, transform);
                sprites[i].SetActive(false);

                canvas.overrideSorting = true;
                canvas.sortingOrder = 100 + i;
            }
            else
            {
                Debug.LogError("Sprite prefab is not assigned.");
            }
        }

        // 화살표 끝 부분 스프라이트 생성 및 비활성화
        if (arrowHeadPrefab != null)
        {
            arrowHead = Instantiate(arrowHeadPrefab, transform);
            arrowHead.SetActive(false);
            canvas.overrideSorting = true;
            canvas.sortingOrder = 100 + numPoints;
        }
        else
        {
            Debug.LogError("Arrow head prefab is not assigned.");
        }

        this.gameObject.SetActive(false);
    }

    void Update()
    {
        if (_cam == null)
        {
            Debug.LogError("Camera is not assigned.");
            return;
        }

        Vector3 mousePos = _cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, -Camera.main.transform.position.z));
        mousePos.z = 0;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(_canvasRect, Input.mousePosition, canvas.worldCamera, out _localPosition);

        mousePos = _localPosition;

        endPoint.position = mousePos;

        UpdateControlPoint(); // controlPoint 업데이트
        DrawBezierCurve(); // NullReferenceException 발생 가능

        if (isEnemyAimed)
        {
            SetArrowColor(Color.red);
        }
        else
        {
            SetArrowColor(Color.white);
        }
    }

    private void OnDisable()
    {
        startPoint.position = Vector3.zero;
        controlPoint.position = Vector3.zero;
        endPoint.position = Vector3.zero;

        SetAllSpritesActive(false); // 모든 스프라이트를 비활성화
    }

    private void UpdateControlPoint()
    {
        // startPoint와 endPoint의 중간점에 약간의 오프셋을 줍니다.
        Vector2 controlPoz = Vector2.zero;
        if (endPoint.position.x > startPoint.anchoredPosition.x)
        {
            controlPoz.x = startPoint.anchoredPosition.x - (endPoint.position.x - startPoint.anchoredPosition.x)/2;
        }
        else if (endPoint.position.x < startPoint.anchoredPosition.x)
        {
            controlPoz.x = startPoint.anchoredPosition.x - (endPoint.position.x - startPoint.anchoredPosition.x)/2;
        }
        else
        {
            controlPoz.x = startPoint.anchoredPosition.x;
        }

        // Y 값
        if(endPoint.position.y - startPoint.anchoredPosition.y != 0)
        {
            controlPoz.y = endPoint.position.y + (endPoint.position.y - startPoint.anchoredPosition.y)/2;
        }
        else
        {
            controlPoz.y = endPoint.position.y;
        }

        controlPoint.position = new Vector2(controlPoz.x, controlPoz.y);
        Debug.Log($"{controlPoint.position.y},{endPoint.position.y}");
    }

    private void DrawBezierCurve()
    {
        int spriteIndex = 0;
        for (int i = 0; i < numPoints; i++)
        {
            float t = i / (float)(numPoints - 1);
            positions[i] = CalculateQuadraticBezierPoint(t, startPoint.anchoredPosition, controlPoint.position, endPoint.position);

            if (i % 2 == 0 && spriteIndex < sprites.Length)
            {
                if (sprites[spriteIndex] != null)
                {
                    sprites[spriteIndex].transform.localPosition = positions[i]; // 4번째 인덱스마다 위치 업데이트

                    // 방향 벡터 계산
                    Vector2 direction;
                    if (i < numPoints - 1)
                    {
                        direction = (positions[i + 1] - positions[i]).normalized;
                    }
                    else
                    {
                        direction = (positions[i] - positions[i - 1]).normalized;
                    }
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    sprites[spriteIndex].transform.rotation = Quaternion.Euler(0, 0, angle);
                    sprites[spriteIndex].transform.SetParent(this.transform, false);
                    sprites[spriteIndex].SetActive(true); // 4번째 인덱스마다 활성화
                    spriteIndex++;
                }
                else
                {
                    Debug.LogError("Sprite at index " + spriteIndex + " is null.");
                }
            }
        }

        // 화살표 끝 부분 위치 및 회전 설정
        if (arrowHead != null)
        {
            arrowHead.transform.localPosition = positions[numPoints - 1];

            // 방향 벡터 계산 (마지막 두 포인트 사용)
            Vector3 direction = (positions[numPoints - 1] - positions[numPoints - 2]).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            arrowHead.transform.rotation = Quaternion.Euler(0, 0, angle);
            arrowHead.transform.SetParent(this.transform, false);
            arrowHead.SetActive(true);
        }
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;

        Vector3 p = uu * p0; // (1-t)^2 * P0
        p += 2 * u * t * p1; // 2(1-t)t * P1
        p += tt * p2; // t^2 * P2

        return p;
    }

    public void SetArrowColor(Color color)
    {
        foreach (var sprite in sprites)
        {
            sprite.GetComponent<Image>().color = color;
        }

        var arrowHeadImage = arrowHead.GetComponent<Image>();
        arrowHeadImage.color = color;
    }

    public void SetAllSpritesActive(bool active)
    {
        foreach (var sprite in sprites)
        {
            if (sprite != null)
                sprite.SetActive(active);
        }

        if (arrowHead != null)
            arrowHead.SetActive(active);
    }
}
