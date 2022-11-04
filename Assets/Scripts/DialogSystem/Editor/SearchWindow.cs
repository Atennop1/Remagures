using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialog_System.Editor
{
    public class SearchWindow : ScriptableObject, ISearchWindowProvider
    {
        private DialogGraphView _targetGraphView;
        private EditorWindow _window;

        public void Init(EditorWindow window, DialogGraphView graphView)
        {
            _targetGraphView = graphView;
            _window = window;
        }

        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            var tree = new List<SearchTreeEntry>()
            {
                new SearchTreeGroupEntry(new GUIContent("Create Element")),
                new(new GUIContent("Dialog Node"))
                {
                    userData = new DialogNode(),
                    level = 1
                }
            };
        
            return tree;
        }
        public bool OnSelectEntry(SearchTreeEntry tree, SearchWindowContext context)
        {
            var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent, context.screenMousePosition - _window.position.position);
            var localMousePosition = _targetGraphView.contentViewContainer.WorldToLocal(worldMousePosition);
        
            _targetGraphView.CreateNode("Dialog Node", localMousePosition);
            return true;
        }
    }
}
