using System.Collections.Generic;
using System.Linq;
using Remagures.DialogSystem.Core;
using Remagures.DialogSystem.Runtime;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialog_System.Editor
{
    public class GraphSaveUtility
    {
        private DialogGraphView _targetGraphView;
        private DialogContainer _cacheContainer;

        private IEnumerable<Edge> Edges => _targetGraphView.edges.ToList();
        private List<DialogNode> Nodes => _targetGraphView.nodes.ToList().Cast<DialogNode>().ToList();

        public static GraphSaveUtility GetInstance(DialogGraphView graphView)
        {
            return new GraphSaveUtility
            {
                _targetGraphView = graphView
            };
        }

        public void SaveGraph(string fileName)
        {
            if (!Edges.Any()) return;

            var dialogContainer = ScriptableObject.CreateInstance<DialogContainer>();
            var connectedPorts = Edges.Where(x => x.input.node != null).ToArray();

            foreach (var port in connectedPorts)
            {
                if (port.input.node is not DialogNode inputNode || port.output.node is not DialogNode outputNode) continue;

                var linkData = new DialogNodeLinkData
                {
                    BaseNodeGUID = outputNode.GUID,
                    PortName = port.output.name,
                    TargetNodeGUID = inputNode.GUID
                };

                dialogContainer.NodeLinks.Add(linkData);
            }

            foreach (var node in Nodes.Where(node => !node.EntryPoint))
            {
                var nodeData = new DialogNodeData
                {
                    Dialog = Resources.Load("Dialogs/" + fileName + "/" + node.Name) as Dialog,
                    GUID = node.GUID,
                    Name = node.Name,
                    Position = node.GetPosition().position,
                    LinkList = dialogContainer.NodeLinks.FindAll(x => x.BaseNodeGUID == node.GUID)
                };

                dialogContainer.NodeData.Add(nodeData);
            }

            if(!AssetDatabase.IsValidFolder("Assets/Resources"))
                AssetDatabase.CreateFolder("Assets", "Resources");

            AssetDatabase.CreateAsset(dialogContainer, $"Assets/Resources/{fileName}.asset");
            AssetDatabase.SaveAssets();
        }
    
        public void LoadGraph(string fileName)
        {
            _cacheContainer = Resources.Load<DialogContainer>(fileName);
            if (_cacheContainer == null)
            {
                EditorUtility.DisplayDialog("File not found!", "Target dialog graph does not exist.", "OK");
                return;
            }

            ClearGraph();
            CreateNodes();
            ConnectNodes();
        }
    
        private void ClearGraph()
        {
            Nodes.Find(x => x.EntryPoint).GUID = _cacheContainer.NodeLinks[0].BaseNodeGUID;
            foreach (var node in Nodes.Where(node => !node.EntryPoint))
            {
                Edges.Where(x => x.input.node == node).ToList().ForEach(edge => _targetGraphView.RemoveElement(edge));
                _targetGraphView.RemoveElement(node);
            }
        }
    
        private void CreateNodes()
        {
            foreach (var node in _cacheContainer.NodeData)
            {
                var tempNode = _targetGraphView.CreateDialogNode(node.Name, Vector2.zero);
                tempNode.GUID = node.GUID;
                _targetGraphView.AddElement(tempNode);

                var nodePorts = _cacheContainer.NodeLinks.Where(x => x.BaseNodeGUID == node.GUID).ToList();
                nodePorts.ForEach(x => _targetGraphView.AddChoicePort(tempNode, x.PortName));
            }
        }
    
        private void ConnectNodes()
        {
            foreach (var t in Nodes)
            {
                var connections = _cacheContainer.NodeLinks.Where(x => x.BaseNodeGUID == t.GUID).ToList();
                for (var j = 0; j < connections.Count; j++)
                {
                    var targetNodeGUID = connections[j].TargetNodeGUID;
                    var targetNode = Nodes.First(x => x.GUID == targetNodeGUID);
                
                    LinkNodes(t.outputContainer[j].Q<Port>(), (Port) targetNode.inputContainer[0]);
                    targetNode.SetPosition(new Rect(_cacheContainer.NodeData.First(x => x.GUID == targetNodeGUID).Position, _targetGraphView._defaultNodeSize));
                }
            }
        }
    
        private void LinkNodes(Port output, Port input)
        {
            var tempEdge = new Edge
            {
                output = output,
                input = input
            };
        
            tempEdge.input.Connect(tempEdge);
            tempEdge.output.Connect(tempEdge);
            _targetGraphView.Add(tempEdge);
        }
    }
}
