using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class MapConfig : ScriptableObject
{
    public List<NodeBlueprint> nodeBlueprints;
    public int GridWidth => Mathf.Max(numOfPreBossNodes.max, numOfStartingNodes.max);


    public IntMinMax numOfPreBossNodes;

    public IntMinMax numOfStartingNodes;

    [Tooltip("노드간 연결하는 선이 많아짐")]
    public int extraPaths;
    public List<MapLayer> layers;
}