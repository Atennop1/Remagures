using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using System.Linq;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class GraphSaveUtility
{
    private DialogGraphView targetGraphView;
    private DialogContainer cacheContainer;

    private List<Edge> Edges => targetGraphView.edges.ToList();
    private List<DialogNode> Nodes => targetGraphView.nodes.ToList().Cast<DialogNode>().ToList();

    public static GraphSaveUtility GetInstance(DialogGraphView graphView)
    {
        return new GraphSaveUtility
        {
            targetGraphView = graphView
        };
    }

    public void SaveGraph(string fileName)
    {
        if (!Edges.Any()) return;

        var dialogContainer = ScriptableObject.CreateInstance<DialogContainer>();
        var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

        for (int i = 0; i < connectedPorts.Length; i++)
        {
            var outputNode = connectedPorts[i].output.node as DialogNode;
            var inputNode = connectedPorts[i].input.node as DialogNode;

            var linkData = new DialogNodeLinkData
            {
                BaseNodeGUID = outputNode.GUID,
                PortName = connectedPorts[i].output.name,
                TargetNodeGUID = inputNode.GUID
            };

            dialogContainer.NodeLinks.Add(linkData);
        }

        foreach (var node in Nodes.Where(node => !node.EntryPoint))
        {
            var nodeData = new DialogNodeData()
            {
                dialog = Resources.Load("Dialogs/" + fileName + "/" + node.nodeName) as TextAsset,
                GUID = node.GUID,
                nodeName = node.nodeName,
                position = node.GetPosition().position
            };

            nodeData.linkList = dialogContainer.NodeLinks.FindAll(x => x.BaseNodeGUID == node.GUID);
            dialogContainer.NodeData.Add(nodeData);
        }

        if(!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        AssetDatabase.CreateAsset(dialogContainer, $"Assets/Resources/{fileName}.asset");
        AssetDatabase.SaveAssets();
    }
    public void LoadGraph(string fileName)
    {
        cacheContainer = Resources.Load<DialogContainer>(fileName);
        if (cacheContainer == null)
        {
            EditorUtility.DisplayDialog("File not found!", "Target dialog graph does not exist.", "OK");
            return;
        }

        ClearGraph();
        CreateNodes();
        ConnectNodes();
    }
    public void ClearGraph()
    {
        Nodes.Find(x => x.EntryPoint).GUID = cacheContainer.NodeLinks[0].BaseNodeGUID;
        foreach (var node in Nodes)
        {
            if (node.EntryPoint) continue;
            Edges.Where(x => x.input.node == node).ToList().ForEach(edge => targetGraphView.RemoveElement(edge));
            targetGraphView.RemoveElement(node);
        }
    }
    public void CreateNodes()
    {
        foreach (var node in cacheContainer.NodeData)
        {
            var tempNode = targetGraphView.CreateDialogNode(node.nodeName, Vector2.zero);
            tempNode.GUID = node.GUID;
            targetGraphView.AddElement(tempNode);

            var nodePorts = cacheContainer.NodeLinks.Where(x => x.BaseNodeGUID == node.GUID).ToList();
            nodePorts.ForEach(x => targetGraphView.AddChoicePort(tempNode, x.PortName));
        }
    }
    public void ConnectNodes()
    {
        for (int i = 0; i < Nodes.Count; i++)
        {
            var connections = cacheContainer.NodeLinks.Where(x => x.BaseNodeGUID == Nodes[i].GUID).ToList();
            for (int j = 0; j < connections.Count; j++)
            {
                var targetNodeGUID = connections[j].TargetNodeGUID;
                var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                LinkNodes(Nodes[i].outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);
                targetNode.SetPosition(new Rect(cacheContainer.NodeData.First(x => x.GUID == targetNodeGUID).position, targetGraphView.defaultNodeSize));
            }
        }
    }
    public void LinkNodes(Port output, Port input)
    {
        var tempEdge = new Edge
        {
            output = output,
            input = input
        };
        tempEdge.input.Connect(tempEdge);
        tempEdge.output.Connect(tempEdge);
        targetGraphView.Add(tempEdge);
    }
}
