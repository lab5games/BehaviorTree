using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using UnityEditor.Callbacks;
using System;

namespace Lab5Games.AI.Editor
{
    public class BehaviorTreeEditor : EditorWindow
    {
        public static bool dataChanged = false;
        public static bool playingMode = false;

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

            treeView.NodeSelected = OnNodeSelectionChange;

            OnSelectionChange();
        }

        private void OnEnable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
            EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
        }

        private void OnDisable()
        {
            EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
        }

        private void OnPlayModeStateChanged(PlayModeStateChange obj)
        {
            switch (obj)
            {
                case PlayModeStateChange.EnteredEditMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingEditMode:
                    break;
                case PlayModeStateChange.EnteredPlayMode:
                    OnSelectionChange();
                    break;
                case PlayModeStateChange.ExitingPlayMode:
                    break;
            }
        }

        private void OnSelectionChange()
        {
            BehaviorTree tree = Selection.activeObject as BehaviorTree;

            if(!tree)
            {
                if(Selection.activeGameObject)
                {
                    var runner = Selection.activeGameObject.GetComponent<BehaviorTreeRunner>();
                    if(runner)
                    {
                        tree = runner.tree;
                    }
                }
            }

            if(Application.isPlaying)
            {
                if(tree)
                {
                    treeView.CreateView(tree, this);
                    titleContent = new GUIContent(tree.name);

                    playingMode = true;
                }
            }
            else
            {
                if (tree && AssetDatabase.OpenAsset(tree.GetInstanceID()))
                {
                    treeView.CreateView(tree, this);
                    titleContent = new GUIContent(tree.name);

                    playingMode = false;
                }
            }
        }

        private void OnNodeSelectionChange(NodeView nodeView)
        {
            inspectorView.CreateView(nodeView);
        }
    }
}