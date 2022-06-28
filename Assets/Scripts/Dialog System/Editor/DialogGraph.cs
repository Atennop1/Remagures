using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

public class DialogGraph : EditorWindow
{
    private DialogGraphView graphView;
    private string fileName = "New Narrative";

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
        rootVisualElement.Remove(graphView);
    }
    private void ConstructGraph()
    {
        graphView = new DialogGraphView(this)
        {
            name = "Dialog Graph"
        };
        graphView.StretchToParentSize();
        rootVisualElement.Add(graphView);
    }
    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var fileNameTextField = new TextField("File name:");
        fileNameTextField.SetValueWithoutNotify(fileName);
        fileNameTextField.MarkDirtyRepaint();
        fileNameTextField.RegisterValueChangedCallback(evt => fileName = evt.newValue);
        toolbar.Add(fileNameTextField);

        toolbar.Add(new Button(() => RequestDataOperation(true)) {text = "Save Data"});
        toolbar.Add(new Button(() => RequestDataOperation(false)) {text = "Load Data"});

        rootVisualElement.Add(toolbar);
    }
    public void RequestDataOperation(bool saving)
    {
        if (string.IsNullOrEmpty(fileName))
        {
            EditorUtility.DisplayDialog("Invalid File Name!", "Please enter a valid file name.", "OK");
            return;
        }
        GraphSaveUtility utility = GraphSaveUtility.GetInstance(graphView);
        if (saving)
            utility.SaveGraph(fileName);
        else
            utility.LoadGraph(fileName);
    }
}
