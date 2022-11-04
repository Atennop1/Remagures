using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Dialog_System.Editor
{
    public class DialogGraph : EditorWindow
    {
        private DialogGraphView _graphView;
        private string _fileName = "New Narrative";

        [MenuItem("Graph/DialogGraph")]
        public static void OpenDialogGraphWindow()
        {
            var window = GetWindow<DialogGraph>();
            window.titleContent = new GUIContent("Dialog Graph");
        }
    
        private void OnEnable()
        {
            ConstructGraph();
            GenerateToolbar();
        }
    
        public void OnDisable()
        {
            rootVisualElement.Remove(_graphView);
        }
    
        private void ConstructGraph()
        {
            _graphView = new DialogGraphView(this)
            {
                name = "Dialog Graph"
            };
        
            _graphView.StretchToParentSize();
            rootVisualElement.Add(_graphView);
        }
    
        private void GenerateToolbar()
        {
            var toolbar = new Toolbar();

            var fileNameTextField = new TextField("File name:");
            fileNameTextField.SetValueWithoutNotify(_fileName);
            fileNameTextField.MarkDirtyRepaint();
            fileNameTextField.RegisterValueChangedCallback(evt => _fileName = evt.newValue);
            toolbar.Add(fileNameTextField);

            toolbar.Add(new Button(() => RequestDataOperation(true)) {text = "Save Data"});
            toolbar.Add(new Button(() => RequestDataOperation(false)) {text = "Load Data"});

            rootVisualElement.Add(toolbar);
        }

        private void RequestDataOperation(bool saving)
        {
            if (string.IsNullOrEmpty(_fileName))
            {
                EditorUtility.DisplayDialog("Invalid File Name!", "Please enter a valid file name.", "OK");
                return;
            }
            var utility = GraphSaveUtility.GetInstance(_graphView);
            if (saving)
                utility.SaveGraph(_fileName);
            else
                utility.LoadGraph(_fileName);
        }
    }
}
