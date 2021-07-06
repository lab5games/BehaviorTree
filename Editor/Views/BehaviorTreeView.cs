using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab5Games.AI.Editor
{
    public class BehaviorTreeView : GraphView
    {
        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits> { }

        BehaviorTree tree;
        BehaviorTreeEditor treeEditor;

        public Action<NodeView> NodeSelected;

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);

            Undo.undoRedoPerformed += OnUndoRedo;
        }

        private void OnUndoRedo()
        {
            CreateView(tree, treeEditor);
            
            AssetDatabase.SaveAssets();
            BehaviorTreeEditor.dataChanged = false;
        }

        internal void CreateView(BehaviorTree tree, BehaviorTreeEditor treeEditor)
        {
            this.tree = tree;
            this.treeEditor = treeEditor;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements);
            graphViewChanged += OnGraphViewChanged;

            CreateNodes();
            CreateEdges();
        }

        private void CreateNodes()
        {
            if(tree.rootNode == null)
            {
                RootNode rootNode = BehaviorTreeEditorUtils.CreateNode(tree, typeof(RootNode)) as RootNode;
                rootNode.position = new Vector2(100, 100);
                tree.rootNode = rootNode;

                EditorUtility.SetDirty(tree);
                
                AssetDatabase.SaveAssets();
                BehaviorTreeEditor.dataChanged = false;
            }

            tree.nodes.ForEach(n => CreateNodeView(n));
        }

        private void CreateEdges()
        {
            tree.nodes.ForEach(n =>
            {
                var children = BehaviorTreeUtils.GetChildren(n);
                children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);

                    Edge edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                });
            });
        }

        private NodeView FindNodeView(BehaviorTreeNode treeNode)
        {
            return GetNodeByGuid(treeNode.GUID) as NodeView;
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList().Where(endPort =>
            endPort.direction != startPort.direction &&
            endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
        {
            if(graphViewChange.elementsToRemove != null)
            {
                graphViewChange.elementsToRemove.ForEach(e =>
                {
                    NodeView nodeView = e as NodeView;

                    if(nodeView != null)
                    {
                        if(nodeView.treeNode is RootNode)
                        {
                            tree.rootNode = null;
                        }

                        BehaviorTreeEditorUtils.DeleateNode(tree, nodeView.treeNode);
                    }

                    Edge edge = e as Edge;
                    if(edge != null)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;

                        BehaviorTreeEditorUtils.RemoveChild(parentView.treeNode, childView.treeNode);
                    }
                });
            }

            if(graphViewChange.edgesToCreate != null)
            {
                graphViewChange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;

                    BehaviorTreeEditorUtils.AddChild(parentView.treeNode, childView.treeNode);
                });
            }

            if(graphViewChange.movedElements != null)
            {


                
            }

            return graphViewChange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            // composite nodes
            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach(var type in types)
                {
                    evt.menu.AppendAction($"Composite/{BehaviorTreeEditorUtils.RenameNode(type.Name)}", (a) => CreateNode(type, a.eventInfo.localMousePosition));
                }
            }
            // decorator nodes
            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"Decorator/{BehaviorTreeEditorUtils.RenameNode(type.Name)}", (a) => CreateNode(type, a.eventInfo.localMousePosition));
                }
            }
            // action nodes
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"Action/{BehaviorTreeEditorUtils.RenameNode(type.Name)}", (a) => CreateNode(type, a.eventInfo.localMousePosition));
                }
            }
        }

        void CreateNode(System.Type nodeType, Vector2 position)
        {
            var windowRoot = treeEditor.rootVisualElement;
            var windowMousePos = windowRoot.ChangeCoordinatesTo(windowRoot.parent, position);
            var graphMousePos = this.contentViewContainer.WorldToLocal(windowMousePos);

            var node = BehaviorTreeEditorUtils.CreateNode(tree, nodeType);
            node.position = graphMousePos;
            
            CreateNodeView(node);
        }

        void CreateNodeView(BehaviorTreeNode treeNode)
        {
            NodeView view = new NodeView(treeNode);
            view.NodeSelected = this.NodeSelected;

            AddElement(view);
        }
    }
}
