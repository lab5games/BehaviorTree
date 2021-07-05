using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;

namespace Lab5Games.AI.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        [OnOpenAsset]
        public static bool OpenAsset(int instanceID, int line)
        {
            if(Selection.activeObject is BehaviorTree)
            {
                OpenWindow(Selection.activeObject.name);
                return true;
            }

            return false;
        }

        [MenuItem("Lab5Games/AI/Open BehaviorTree")]
        public static void OpenWindow()
        {
            OpenWindow("BehaviorTreeEditor");   
        }

        public static void OpenWindow(string title)
        {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent(title);
        }


        BehaviorTreeView treeView;
        InspectorView inspectorView;

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BehaviorTree/Editor/BehaviorTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            treeView = root.Q<BehaviorTreeView>();
            inspectorView = root.Q<InspectorView>();
        }

        private void OnSelectionChange()
        {
            BehaviorTree tree = Selection.activeObject as BehaviorTree;

            if(tree)
            {
                treeView.CreateView(tree);
            }
        }
    }
}