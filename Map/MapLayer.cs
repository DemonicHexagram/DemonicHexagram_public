using UnityEngine;


[System.Serializable]
public class MapLayer
{
    public NodeType nodeType;
    public FloatMinMax distanceFromPreviousLayer;
    [Tooltip("레이어간 간격")]
    public float nodesApartDistance;
    [Tooltip("위치가 변하는 비율 낮을수록 직선이 됨")]
    [Range(0f, 1f)] public float randomizePosition;
    [Tooltip("다른 노드 타입으로 변할 확률")]
    [Range(0f, 1f)] public float randomizeNodes;
}