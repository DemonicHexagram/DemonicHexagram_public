using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MapView : MonoBehaviour
{
    public MapManager mapManager;
    public Sprite uncoverSprite;

    [Tooltip("Assets 폴더에서 사용할 수 있는 모든 MapConfig 스크립터블 객체들의 리스트입니다. " +
             "Slay The Spire의 Acts와 유사 (일반적인 레이아웃, 보스 유형 정의).")]
    public List<MapConfig> allMapConfigs;
    public GameObject nodePrefab;
    [Tooltip("맵의 시작/종료 노드의 오프셋")]
    public float orientationOffset;
    [Header("배경 설정")]
    [Tooltip("배경 스프라이트가 null인 경우 배경이 표시되지 않습니다.")]
    public Sprite background;
    public Color32 backgroundColor = Color.white;
    public float xSize;
    public float yOffset;
    [Header("라인 설정")]
    public GameObject linePrefab;
    [Tooltip("스무스한 색상 그라데이션을 위해 라인 포인트 수는 2보다 커야 합니다.")]
    [Range(3, 10)]
    public int linePointsCount = 10;
    [Tooltip("노드에서 라인 시작점까지의 거리")]
    public float offsetFromNodes = 10f;
    [Header("색상")]
    [Tooltip("노드 방문 또는 접근 가능 색상")]
    public Color32 visitedColor = Color.black;
    [Tooltip("잠긴 노드 색상")]
    public Color32 lockedColor = Color.gray;
    [Tooltip("방문한 경로 색상")]
    public Color32 lineVisitedColor = Color.white;
    [Tooltip("잠긴 경로 색상")]
    public Color32 lineLockedColor = Color.gray;

    protected GameObject firstParent;
    protected GameObject mapParent;
    private List<List<Vector2Int>> paths;
    private Camera cam;
    public readonly List<MapNode> MapNodes = new List<MapNode>();
    protected readonly List<LineConnection> lineConnections = new List<LineConnection>();

    public static MapView Instance;

    public Map Map { get; protected set; }

    private void Awake()
    {
        Instance = this;
        cam = Camera.main;
    }

    protected virtual void ClearMap()
    {
        if (firstParent != null)
            Destroy(firstParent);

        MapNodes.Clear();
        lineConnections.Clear();
    }

    public virtual void ShowMap(Map m)
    {
        if (m == null)
        {
            Debug.LogWarning("Map was null in MapView.ShowMap()");
            return;
        }

        Map = m;

        ClearMap();

        CreateMapParent();

        CreateNodes(m.nodes);

        DrawLines();

        SetOrientation();

        ResetNodesRotation();

        SetAttainableNodes();

        SetLineColors();

        CreateMapBackground(m);
    }

    protected virtual void CreateMapBackground(Map m)
    {
        if (background == null) return;

        GameObject backgroundObject = new GameObject("Background");
        backgroundObject.transform.SetParent(mapParent.transform);
        MapNode bossNode = MapNodes.FirstOrDefault(node => node.Node.nodeType == NodeType.Boss);
        float span = m.DistanceBetweenFirstAndLastLayers();
        float maxSpan = Mathf.Max(span, xSize);
        backgroundObject.transform.localPosition = new Vector3(bossNode.transform.localPosition.x, span / 2f, 0f);
        backgroundObject.transform.localRotation = Quaternion.identity;
        SpriteRenderer sr = backgroundObject.AddComponent<SpriteRenderer>();
        sr.color = backgroundColor;
        sr.drawMode = SpriteDrawMode.Sliced;
        sr.sprite = background;
        sr.size = new Vector2(xSize, span + yOffset * 2f);
    }


    protected virtual void CreateMapParent()
    {
        firstParent = new GameObject("OuterMapParent");
        mapParent = new GameObject("MapParentWithAScroll");
        mapParent.transform.SetParent(firstParent.transform);
        ScrollNonUI scrollNonUi = mapParent.AddComponent<ScrollNonUI>();
        scrollNonUi.freezeX = false;
        scrollNonUi.freezeY = true;
        BoxCollider boxCollider = mapParent.AddComponent<BoxCollider>();
        boxCollider.size = new Vector3(100, 100, 1);
    }

    protected void CreateNodes(IEnumerable<Node> nodes)
    {
        foreach (Node node in nodes)
        {
            MapNode mapNode = CreateMapNode(node);
            MapNodes.Add(mapNode);
        }
        GameManager.Instance.MapNodeList = MapNodes;
    }

    protected virtual MapNode CreateMapNode(Node node)
    {
        GameObject mapNodeObject = Instantiate(nodePrefab, mapParent.transform);
        MapNode mapNode = mapNodeObject.GetComponent<MapNode>();
        NodeBlueprint blueprint = GetBlueprint(node.blueprintName);
        mapNode.SetUp(node, blueprint, uncoverSprite);
        mapNode.transform.localPosition = node.position;

        float mapSpan = Map.DistanceBetweenFirstAndLastLayers();
        float scaleFactor = Mathf.Clamp(mapSpan / 10.0f, 0.5f, 2.0f);
        mapNode.transform.localScale = new Vector3(scaleFactor, scaleFactor, 1);

        return mapNode;
    }

    public virtual void SetAttainableNodes()
    {
        foreach (MapNode node in MapNodes)
            node.SetState(NodeStates.Covered);

        if (mapManager.CurrentMap.path.Count == 0)
        {
            foreach (MapNode node in MapNodes.Where(n => n.Node.point.y == 0))
                node.SetState(NodeStates.Attainable);
        }
        else
        {
            Vector2Int currentPoint = mapManager.CurrentMap.path.Last();
            Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

            int currentLayer = currentNode.point.y;

            foreach (MapNode node in MapNodes)
            {
                if (node.Node.point.y <= currentLayer)
                {
                    node.SetState(NodeStates.Locked);
                }
                else
                {
                    node.SetState(NodeStates.Covered);
                }
            }

            foreach (Vector2Int point in mapManager.CurrentMap.path)
            {
                MapNode mapNode = GetNode(point);
                if (mapNode != null)
                    mapNode.SetState(NodeStates.Visited);
            }

            foreach (Vector2Int point in currentNode.outgoing)
            {
                MapNode mapNode = GetNode(point);
                if (mapNode != null)
                    mapNode.SetState(NodeStates.Attainable);
            }
        }
    }

    public virtual void SetLineColors()
    {
        foreach (LineConnection connection in lineConnections)
            connection.SetColor(lineLockedColor);

        if (mapManager.CurrentMap.path.Count == 0)
            return;

        Vector2Int currentPoint = mapManager.CurrentMap.path[mapManager.CurrentMap.path.Count - 1];
        Node currentNode = mapManager.CurrentMap.GetNode(currentPoint);

        foreach (Vector2Int point in currentNode.outgoing)
        {
            LineConnection lineConnection = lineConnections.FirstOrDefault(conn => conn.from.Node == currentNode &&
                                                             conn.to.Node.point.Equals(point));
            lineConnection?.SetColor(lineVisitedColor);
        }

        if (mapManager.CurrentMap.path.Count <= 1) return;

        for (int i = 0; i < mapManager.CurrentMap.path.Count - 1; i++)
        {
            Vector2Int current = mapManager.CurrentMap.path[i];
            Vector2Int next = mapManager.CurrentMap.path[i + 1];
            LineConnection lineConnection = lineConnections.FirstOrDefault(conn => conn.@from.Node.point.Equals(current) &&
                                                             conn.to.Node.point.Equals(next));
            lineConnection?.SetColor(lineVisitedColor);
        }
    }

    protected virtual void SetOrientation()
    {
        ScrollNonUI scrollNonUi = mapParent.GetComponent<ScrollNonUI>();
        float span = mapManager.CurrentMap.DistanceBetweenFirstAndLastLayers();
        MapNode bossNode = MapNodes.FirstOrDefault(node => node.Node.nodeType == NodeType.Boss);
        Debug.Log("Map span in set orientation: " + span + " camera aspect: " + cam.aspect);

        firstParent.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, 0f);
        float offset = orientationOffset;
        offset *= cam.aspect;
        mapParent.transform.eulerAngles = Vector3.back * 90;
        firstParent.transform.localPosition += new Vector3(offset, -bossNode.transform.position.y, 0);
        if (scrollNonUi != null)
        {
            scrollNonUi.xConstraints.max = 0;
            scrollNonUi.xConstraints.min = -(span + 2f * offset);
        }
    }

    private void DrawLines()
    {
        foreach (MapNode node in MapNodes)
        {
            foreach (Vector2Int connection in node.Node.outgoing)
                AddLineConnection(node, GetNode(connection));
        }
    }

    private void ResetNodesRotation()
    {
        foreach (MapNode node in MapNodes)
            node.transform.rotation = Quaternion.identity;
    }

    protected virtual void AddLineConnection(MapNode from, MapNode to)
    {
        if (linePrefab == null) return;

        GameObject lineObject = Instantiate(linePrefab, mapParent.transform);
        LineRenderer lineRenderer = lineObject.GetComponent<LineRenderer>();
        Vector3 direction = (to.transform.position - from.transform.position).normalized;
        Vector3 fromPoint = from.transform.position + direction * offsetFromNodes;
        Vector3 toPoint = to.transform.position - direction * offsetFromNodes;

        lineObject.transform.position = fromPoint;
        lineRenderer.useWorldSpace = false;

        lineRenderer.positionCount = linePointsCount;
        for (int i = 0; i < linePointsCount; i++)
        {
            lineRenderer.SetPosition(i,
                Vector3.Lerp(Vector3.zero, toPoint - fromPoint, (float)i / (linePointsCount - 1)));
        }

        DottedLineRenderer dottedLine = lineObject.GetComponent<DottedLineRenderer>();
        if (dottedLine != null) dottedLine.ScaleMaterial();

        lineConnections.Add(new LineConnection(lineRenderer, null, from, to));
    }


    protected MapNode GetNode(Vector2Int p)
    {
        return MapNodes.Find(n => n.Node.point.Equals(p));
    }

    protected MapConfig GetConfig(string configName)
    {
        return allMapConfigs.Find(c => c.name == configName);
    }

    protected NodeBlueprint GetBlueprint(NodeType type)
    {
        MapConfig config = GetConfig(mapManager.CurrentMap.configName);
        return config.nodeBlueprints.Find(n => n.nodeType == type);
    }

    protected NodeBlueprint GetBlueprint(string blueprintName)
    {
        MapConfig config = GetConfig(mapManager.CurrentMap.configName);
        return config.nodeBlueprints.Find(n => n.name == blueprintName);
    }
}