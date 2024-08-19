using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class MapViewUI : MapView
{
    [Header("UI Map 세팅")]
    [SerializeField] private ScrollRect scrollRectHorizontal;
    [Tooltip("로컬과 캔버스 간의 위치 보정값")]
    [SerializeField] private float unitsToPixelsMultiplier = 10f;
    [Tooltip("시작과 끝노드와 양끝간의 공백")]
    [SerializeField] private float padding;
    [Tooltip("ScrollRect와 배경의간의 공백")]
    [SerializeField] private Vector2 backgroundPadding;
    [Tooltip("BG의 유닛당 픽셀 보정값")]
    [SerializeField] private float backgroundPPUMultiplier = 1;
    [SerializeField] private UILineRenderer uiLinePrefab;

    protected override void ClearMap()
    {
        scrollRectHorizontal.gameObject.SetActive(false);


        foreach (Transform t in scrollRectHorizontal.content)
            Destroy(t.gameObject);

        MapNodes.Clear();
        lineConnections.Clear();
    }

    private ScrollRect GetScrollRectForMap()
    {
        return scrollRectHorizontal;
    }

    protected override void CreateMapParent()
    {
        ScrollRect scrollRect = GetScrollRectForMap();
        scrollRect.gameObject.SetActive(true);

        firstParent = new GameObject("OuterMapParent");
        firstParent.transform.SetParent(scrollRect.content);
        firstParent.transform.localScale = Vector3.one;
        RectTransform fprt = firstParent.AddComponent<RectTransform>();
        Stretch(fprt);

        mapParent = new GameObject("MapParentWithAScroll");
        mapParent.transform.SetParent(firstParent.transform);
        mapParent.transform.localScale = Vector3.one;
        RectTransform mprt = mapParent.AddComponent<RectTransform>();
        Stretch(mprt);

        SetMapLength();
        ScrollToOrigin();
    }

    private void SetMapLength()
    {
        RectTransform rt = GetScrollRectForMap().content;
        Vector2 sizeDelta = rt.sizeDelta;
        float length = padding + Map.DistanceBetweenFirstAndLastLayers() * unitsToPixelsMultiplier;
        sizeDelta.x = length;
        rt.sizeDelta = sizeDelta;
    }

    private void ScrollToOrigin()
    {
        scrollRectHorizontal.normalizedPosition = Vector2.zero;
    }

    private static void Stretch(RectTransform tr)
    {
        tr.localPosition = Vector3.zero;
        tr.anchorMin = Vector2.zero;
        tr.anchorMax = Vector2.one;
        tr.sizeDelta = Vector2.zero;
        tr.anchoredPosition = Vector2.zero;
    }

    protected override MapNode CreateMapNode(Node node)
    {
        GameObject mapNodeObject = Instantiate(nodePrefab, mapParent.transform);
        MapNode mapNode = mapNodeObject.GetComponent<MapNode>();
        NodeBlueprint blueprint = GetBlueprint(node.blueprintName);
        mapNode.SetUp(node, blueprint, uncoverSprite);
        mapNode.transform.localPosition = GetNodePosition(node);
        return mapNode;
    }

    private Vector2 GetNodePosition(Node node)
    {
        float length = padding + Map.DistanceBetweenFirstAndLastLayers() * unitsToPixelsMultiplier;

        return new Vector2((padding - length) / 2f, -backgroundPadding.y / 2f) +
                       Flip(node.position) * unitsToPixelsMultiplier;
    }

    private static Vector2 Flip(Vector2 other) => new Vector2(other.y, other.x);

    protected override void SetOrientation()
    {
        // 왜인지 모르겠으나 이걸 지우면 맵이 안그려짐 차후 알아볼 예정
    }

    protected override void CreateMapBackground(Map m)
    {
        GameObject backgroundObject = new GameObject("Background");
        backgroundObject.transform.SetParent(mapParent.transform);
        backgroundObject.transform.localScale = Vector3.one;
        RectTransform rt = backgroundObject.AddComponent<RectTransform>();
        Stretch(rt);
        rt.SetAsFirstSibling();
        rt.sizeDelta = backgroundPadding;

        Image image = backgroundObject.AddComponent<Image>();
        image.color = backgroundColor;
        image.type = Image.Type.Sliced;
        image.sprite = background;
        image.pixelsPerUnitMultiplier = backgroundPPUMultiplier;
    }

    protected override void AddLineConnection(MapNode from, MapNode to)
    {
        if (uiLinePrefab == null) return;

        UILineRenderer lineRenderer = Instantiate(uiLinePrefab, mapParent.transform);
        lineRenderer.transform.SetAsFirstSibling();
        RectTransform fromRT = from.transform as RectTransform;
        RectTransform toRT = to.transform as RectTransform;
        Vector2 direction = (toRT.anchoredPosition - fromRT.anchoredPosition).normalized;
        Vector2 fromPoint = fromRT.anchoredPosition + direction * offsetFromNodes;
        Vector2 toPoint = toRT.anchoredPosition - direction * offsetFromNodes;

        lineRenderer.transform.position = from.transform.position +
                                          (Vector3)(toRT.anchoredPosition - fromRT.anchoredPosition).normalized *
                                          offsetFromNodes;

        List<Vector2> list = new List<Vector2>();
        for (int i = 0; i < linePointsCount; i++)
        {
            list.Add(Vector3.Lerp(Vector3.zero, toPoint - fromPoint +
                                                3.9f * (fromRT.anchoredPosition - toRT.anchoredPosition).normalized *
                                                offsetFromNodes, (float)i / (linePointsCount)));
        }

        lineRenderer.Points = list.ToArray();

        DottedLineRenderer dottedLine = lineRenderer.GetComponent<DottedLineRenderer>();
        if (dottedLine != null) dottedLine.ScaleMaterial();

        lineConnections.Add(new LineConnection(null, lineRenderer, from, to));
    }

}