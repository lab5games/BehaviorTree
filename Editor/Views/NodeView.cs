using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using System;

namespace Lab5Games.AI.Editor
{ 
    public class NodeView : Node
    {
        public BehaviorTreeNode treeNode;

        public Port input;
        public Port output;

        public Action<NodeView> NodeSelected;

#if LAB5_DEV
        public NodeView(BehaviorTreeNode treeNode) : base("Assets/BehaviorTree/Editor/NodeView.uxml")
        {
            this.treeNode = treeNode;
            this.title = BehaviorTreeEditorUtils.RenameNode(treeNode.name);
            this.viewDataKey = treeNode.GUID;

            this.style.left = treeNode.position.x;
            this.style.top = treeNode.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();

            Label descriptionLabel = this.Q<Label>("description");
            descriptionLabel.bindingPath = "description";
            descriptionLabel.Bind(new SerializedObject(treeNode));
        }
#else
        public NodeView(BehaviorTreeNode treeNode) : base("Packages/com.lab5games.behaviortree/BehaviorTree/Editor/NodeView.uxml")
        {
            this.treeNode = treeNode;
            this.title = BehaviorTreeEditorUtils.RenameNode(treeNode.name);
            this.viewDataKey = treeNode.GUID;

            this.style.left = treeNode.position.x;
            this.style.top = treeNode.position.y;

            CreateInputPorts();
            CreateOutputPorts();
            SetupClasses();

            Label descriptionLabel = this.Q<Label>("description");
            descriptionLabel.bindingPath = "description";
            descriptionLabel.Bind(new SerializedObject(treeNode));
        }
#endif

        private void SetupClasses()
        {
            if (treeNode is ActionNode)
            {
                AddToClassList("action");
            }
            else if (treeNode is DecoratorNode)
            {
                AddToClassList("decorator");
            }
            else if (treeNode is CompositeNode)
            {
                AddToClassList("composite");
            }
            else if (treeNode is RootNode)
            {
                AddToClassList("root");
            }
        }

        private void CreateInputPorts()
        {
            if(treeNode is ActionNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if(treeNode is DecoratorNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if(treeNode is CompositeNode)
            {
                input = InstantiatePort(Orientation.Vertical, Direction.Input, Port.Capacity.Single, typeof(bool));
            }
            else if(treeNode is RootNode)
            {

            }

            if(input != null)
            {
                input.portName = "";
                input.style.flexDirection = FlexDirection.Column;
                inputContainer.Add(input);
            }
        }

        private void CreateOutputPorts()
        {
            if (treeNode is ActionNode)
            {
                
            }
            else if (treeNode is DecoratorNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }
            else if (treeNode is CompositeNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Multi, typeof(bool));
            }
            else if (treeNode is RootNode)
            {
                output = InstantiatePort(Orientation.Vertical, Direction.Output, Port.Capacity.Single, typeof(bool));
            }

            if (output != null)
            {
                output.portName = "";
                output.style.flexDirection = FlexDirection.ColumnReverse;
                outputContainer.Add(output);
            }
        }

        public override void SetPosition(Rect newPos)
        {
            base.SetPosition(newPos);

            Undo.RecordObject(treeNode, "BT set position");

            treeNode.position.x = newPos.xMin;
            treeNode.position.y = newPos.yMin;

            EditorUtility.SetDirty(treeNode);
            BehaviorTreeEditor.dataChanged = true;
        }

        public override void OnSelected()
        {
            base.OnSelected();

            if(NodeSelected != null)
            {
                NodeSelected.Invoke(this);
            }
        }

        public void SortChildren()
        {
            CompositeNode composite = treeNode as CompositeNode;
            
            if (composite)
            {
                composite.children.Sort(SortByHorizontalPosition);
            }
        }

        private int SortByHorizontalPosition(BehaviorTreeNode left, BehaviorTreeNode right)
        {
            return left.position.x < right.position.x ? -1 : 1;
        }
    }
}
