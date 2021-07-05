using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;

namespace Lab5Games.AI.Editor
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> { }

        BehaviorTree tree;

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        internal void CreateView(BehaviorTree tree)
        {
            this.tree = tree;

            // create root node

            // create nodes

            // create edges
        }
    }
}
