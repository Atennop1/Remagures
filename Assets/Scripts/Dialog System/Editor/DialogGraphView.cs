using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class DialogGraphView : GraphView
{
    public readonly Vector2 defaultNodeSize = new Vector2(150, 150);

    public DialogGraphView(EditorWindow editorWindow)
    {
        styleSheets.Add(Resources.Load<StyleSheet>("DialogGraph"));
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);

        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var grid = new GridBackground();
        Insert(0, grid);
        grid.StretchToParentSize();

        AddSearchWindow(editorWindow);
        AddElement(GenerateEntryPointNode());
    }
    public void AddSearchWindow(EditorWindow editorWindow)
    {
        var searchWindow = ScriptableObject.CreateInstance<SearchWindow>();
        searchWindow.Init(editorWindow, this);
        nodeCreationRequest = context => UnityEditor.Experimental.GraphView.SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindow);
    }
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    private Port GeneratePort(DialogNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(float));
    }
    private DialogNode GenerateEntryPointNode()
    {
        var node = new DialogNode()
        {
            title = "START",
            nodeName = "Start",
            GUID = Guid.NewGuid().ToString(),
            EntryPoint = true
        };

        var generatedPort = GetPortInstance(node, Direction.Output);
        generatedPort.portName = "Input";
        node.outputContainer.Add(generatedPort);

        node.capabilities &= ~Capabilities.Movable;
        node.capabilities &= ~Capabilities.Deletable;
        RefreshAll(node);

        node.SetPosition(new Rect(100, 200, 100, 150));
        return node;
    }
    private Port GetPortInstance(DialogNode node, Direction nodeDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, nodeDirection, capacity, typeof(float));
    }
    public void CreateNode(string nodeName, Vector2 position)
    {
        AddElement(CreateDialogNode(nodeName, position));
    }
    public DialogNode CreateDialogNode(string nodeName, Vector2 position)
    {
        var dialogNode = new DialogNode()
        {
            title = nodeName,
            nodeName = nodeName,
            GUID = Guid.NewGuid().ToString()
        };
        var inputPort = GeneratePort(dialogNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        dialogNode.inputContainer.Add(inputPort);

        dialogNode.styleSheets.Add(Resources.Load<StyleSheet>("Node"));

        CreateChoiceButton(dialogNode);

        var textField = new TextField(string.Empty);
        textField.RegisterValueChangedCallback(evt => 
        {
            dialogNode.nodeName = evt.newValue;
            dialogNode.title = evt.newValue;
        });
        textField.SetValueWithoutNotify(dialogNode.title);
        dialogNode.mainContainer.Add(textField);

        RefreshAll(dialogNode);
        dialogNode.SetPosition(new Rect(position, defaultNodeSize));

        return dialogNode;
    }
    public void AddChoicePort(DialogNode dialogNode, string overridenPortName ="")
    {
        var generatedPort = GeneratePort(dialogNode, Direction.Output);

        var oldLabel = generatedPort.contentContainer.Q<Label>("type");
        generatedPort.contentContainer.Remove(oldLabel);

        var outputPortCount = dialogNode.outputContainer.Query("connector").ToList().Count;
        generatedPort.portName = string.IsNullOrEmpty(overridenPortName) ? $"Choice {outputPortCount + 1}" : overridenPortName;

        var textField = new TextField
        {
            name = string.Empty,
            value = generatedPort.portName
        };
        textField.RegisterValueChangedCallback(evt => generatedPort.portName = evt.newValue);
        generatedPort.contentContainer.Add(new Label(" "));
        generatedPort.contentContainer.Add(textField);
        generatedPort.contentContainer.Add(new Button(() => DeletePort(dialogNode, generatedPort)) {text = "X"});

        dialogNode.outputContainer.Add(generatedPort);
        RefreshAll(dialogNode);
    }
    private void DeletePort(DialogNode node, Port port)
    {
        var targetEdge = edges.ToList().Where(x => x.output.portName == port.portName && x.output.node == port.node);
        if (!targetEdge.Any()) 
        {
            node.outputContainer.Remove(port);
            RefreshAll(node);
            return;
        }
        var edge = targetEdge.First();
        edge.input.Disconnect(edge);
        RemoveElement(edge);
        node.outputContainer.Remove(port);
        RefreshAll(node);
    }
    private void RefreshAll(DialogNode node)
    {
        node.RefreshPorts();
        node.RefreshExpandedState();
    }
    private void CreateChoiceButton(DialogNode node)
    {
        var button = new Button(() => { AddChoicePort(node); });
        button.text = "New Choice";
        node.contentContainer.Add(button);
    }
}