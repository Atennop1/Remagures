using UnityEngine;
using BehaviorDesigner.Runtime.Tasks;

namespace BehaviorDesigner.Runtime
{
    [AddComponentMenu("Behavior Designer/Behavior Game GUI")]
    public class BehaviorGameGUI : MonoBehaviour
    {
        private BehaviorManager behaviorManager;
        private Camera mainCamera;

        public void Start()
        {
            mainCamera = Camera.main;
        }

        public void OnGUI()
        {
            if (behaviorManager == null) {
                behaviorManager = BehaviorManager.instance;
            }

            if (behaviorManager == null || mainCamera == null) {
                return;
            }

            var behaviorTrees = behaviorManager.BehaviorTrees;
            for (int i = 0; i < behaviorTrees.Count; ++i) {
                var behaviorTree = behaviorTrees[i];
                string activeTasks = "";
                for (int j = 0; j < behaviorTree.activeStack.Count; ++j) {
                    var stack = behaviorTree.activeStack[j];
                    if (stack.Count == 0) {
                        continue;
                    }

                    // ignore the task if it isn't a action task
                    var task = behaviorTree.taskList[stack.Peek()];
                    if (!(task is Action)) {
                        continue;
                    }
                    activeTasks += behaviorTree.taskList[behaviorTree.activeStack[j].Peek()].FriendlyName + (j < behaviorTree.activeStack.Count - 1 ? "\n" : "");
                }
                var ownerTransform = behaviorTree.behavior.transform;
                var screenPos = Camera.main.WorldToScreenPoint(ownerTransform.position);
                var guiPoint = GUIUtility.ScreenToGUIPoint(screenPos);
                var label = new GUIContent(activeTasks);
                var dimensions = GUI.skin.label.CalcSize(label);
                // add some padding
                dimensions.x += 14;
                dimensions.y += 5;
                GUI.Box(new Rect(guiPoint.x - dimensions.x / 2, Screen.height - guiPoint.y + dimensions.y / 2, dimensions.x, dimensions.y), label);
            }
        }
    }
}