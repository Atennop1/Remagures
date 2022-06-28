using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

public class SearchWindow : ScriptableObject, ISearchWindowProvider
{
    private DialogGraphView targetGraphView;
    private EditorWindow window;

    public void Init(EditorWindow window, DialogGraphView graphView)
    {
        targetGraphView = graphView;
        this.window = window;
    }

    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>()
        {
            new SearchTreeGroupEntry(new GUIContent("Create Element"), 0),
            new SearchTreeEntry(new GUIContent("Dialog Node"))
            {
                userData = new DialogNode(),
                level = 1
            }
        };
        return tree;
    }
    public bool OnSelectEntry(SearchTreeEntry tree, SearchWindowContext context)
    {
        var worldMousePosition = window.rootVisualElement.ChangeCoordinatesTo(window.rootVisualElement.parent, context.screenMousePosition - window.position.position);
        var localMousePosition = targetGraphView.contentViewContainer.WorldToLocal(worldMousePosition);
        targetGraphView.CreateNode("Dialog Node", localMousePosition);
        return true;
    }
}
