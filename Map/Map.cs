using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Map
{
    public List<Node> nodes;
    public List<Vector2Int> path;
    public string bossNodeName;
    public string configName;

    public Map(string configName, string bossNodeName, List<Node> nodes, List<Vector2Int> path)
    {
        this.configName = configName;
        this.bossNodeName = bossNodeName;
        this.nodes = nodes;
        this.path = path;
    }

    public Node GetBossNode()
    {
        return nodes.Find(n => n.nodeType == NodeType.Boss);
    }

    public float DistanceBetweenFirstAndLastLayers()
    {
        Node bossNode = GetBossNode();
        Node firstLayerNode = nodes.Find(n => n.point.y == 0);

        if (bossNode == null || firstLayerNode == null)
            return 0f;

        return bossNode.position.y - firstLayerNode.position.y;
    }

    public Node GetNode(Vector2Int point)
    {
        return nodes.Find(n => n.point.Equals(point));
    }

    public string ToCsv()
    {
        var csv = new StringBuilder();
        csv.AppendLine("NodeType,pointX,pointY,blueprintName,positionX,positionY,incoming,outgoing");

        foreach (var node in nodes)
        {
            var incoming = string.Join("|", node.incoming.Select(p => $"{p.x}&{p.y}"));
            var outgoing = string.Join("|", node.outgoing.Select(p => $"{p.x}&{p.y}"));
            csv.AppendLine($"{(int)node.nodeType},{node.point.x},{node.point.y},{node.blueprintName},{node.position.x},{node.position.y},{incoming},{outgoing}");
        }

        var pathString = string.Join("|", path.Select(p => $"{p.x}&{p.y}"));
        csv.AppendLine($"path,{pathString}");
        csv.AppendLine($"bossNodeName,{bossNodeName}");
        csv.AppendLine($"configName,{configName}");

        return csv.ToString();
    }

    public static Map FromCsv(string csv)
    {
        var lines = csv.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        var nodes = new List<Node>();
        var path = new List<Vector2Int>();
        string bossNodeName = null;
        string configName = null;

        for (int i = 1; i < lines.Length; i++)
        {
            var values = lines[i].TrimEnd('\r').Split(',');

            if (values[0] == "path")
            {
                if (values.Length > 1)
                {
                    var pathPoints = values[1].Split('|');
                    foreach (var point in pathPoints)
                    {
                        var coords = point.Split('&');
                        if (coords.Length == 2 && int.TryParse(coords[0], out int x) && int.TryParse(coords[1], out int y))
                        {
                            path.Add(new Vector2Int(x, y));
                        }
                        else
                        {
                            Debug.LogWarning($"Invalid path point: {point}");
                        }
                    }
                }
            }
            else if (values[0] == "bossNodeName")
            {
                bossNodeName = values[1];
            }
            else if (values[0] == "configName")
            {
                configName = values[1];
            }
            else
            {
                if (values.Length < 8)
                {
                    Debug.LogWarning($"Invalid line format: {lines[i]}");
                    continue;
                }

                if (Enum.TryParse(values[0], out NodeType nodeType) &&
                    int.TryParse(values[1], out int pointX) &&
                    int.TryParse(values[2], out int pointY) &&
                    float.TryParse(values[4], out float positionX) &&
                    float.TryParse(values[5], out float positionY))
                {
                    var point = new Vector2Int(pointX, pointY);
                    var blueprintName = values[3];
                    var position = new Vector2(positionX, positionY);

                    var incoming = new List<Vector2Int>();
                    if (!string.IsNullOrEmpty(values[6]))
                    {
                        var incomingPoints = values[6].Split('|');
                        foreach (var incomingPoint in incomingPoints)
                        {
                            var coords = incomingPoint.Split('&');
                            if (coords.Length == 2 && int.TryParse(coords[0], out int incomingX) && int.TryParse(coords[1], out int incomingY))
                            {
                                incoming.Add(new Vector2Int(incomingX, incomingY));
                            }
                            else
                            {
                                Debug.LogWarning($"Invalid incoming point: {incomingPoint}");
                            }
                        }
                    }

                    var outgoing = new List<Vector2Int>();
                    if (values.Length > 7 && !string.IsNullOrEmpty(values[7]))
                    {
                        var outgoingPoints = values[7].Split('|');
                        foreach (var outgoingPoint in outgoingPoints)
                        {
                            var coords = outgoingPoint.Split('&');
                            if (coords.Length == 2 && int.TryParse(coords[0], out int outgoingX) && int.TryParse(coords[1], out int outgoingY))
                            {
                                outgoing.Add(new Vector2Int(outgoingX, outgoingY));
                            }
                            else
                            {
                                Debug.LogWarning($"Invalid outgoing point: {outgoingPoint}");
                            }
                        }
                    }

                    var node = new Node(nodeType, blueprintName, point)
                    {
                        position = position,
                        incoming = incoming,
                        outgoing = outgoing
                    };

                    nodes.Add(node);
                }
                else
                {
                    Debug.LogWarning($"Invalid node data: {lines[i]}");
                }
            }
        }
        GameManager.Instance.NodeList = nodes;
        return new Map(configName, bossNodeName, nodes, path);
    }
}