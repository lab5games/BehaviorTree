using UnityEngine;
using UnityEditor;
using System.Text.RegularExpressions;

namespace Lab5Games.AI.Editor
{
    using Node = BehaviorTreeNode;


    public static class BehaviorTreeEditorUtils
    {
        public static Node CreateNode(BehaviorTree tree, System.Type nodeType)
        {
            var node = ScriptableObject.CreateInstance(nodeType) as Node;
            node.name = nodeType.Name;
            node.GUID = GUID.Generate().ToString();

            Undo.RecordObject(tree, "BT create node");

            tree.AddNode(node);

            AssetDatabase.AddObjectToAsset(node, tree);

            Undo.RegisterCreatedObjectUndo(node, "BT create node");

            BehaviorTreeEditor.dataChanged = false;

            //AssetDatabase.SaveAssets();

            return node;
        }

        public static void DeleateNode(BehaviorTree tree, Node node)
        {
            Undo.RecordObject(tree, "BT delete node");

            tree.RemoveNode(node);

            //AssetDatabase.RemoveObjectFromAsset(node);
            Undo.DestroyObjectImmediate(node);

            BehaviorTreeEditor.dataChanged = false;

            //AssetDatabase.SaveAssets();
        }

        public static void AddChild(Node parent, Node child)
        {
            RootNode rootNode = parent as RootNode;
            if(rootNode)
            {
                Undo.RecordObject(rootNode, "BT add child");

                rootNode.child = child;

                EditorUtility.SetDirty(rootNode);

                BehaviorTreeEditor.dataChanged = true;
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if(decorator)
            {
                Undo.RecordObject(decorator, "BT add child");

                decorator.child = child;

                EditorUtility.SetDirty(decorator);

                BehaviorTreeEditor.dataChanged = true;
            }

            CompositeNode composite = parent as CompositeNode;
            if(composite)
            {
                Undo.RecordObject(composite, "BT add child");

                composite.children.Add(child);

                EditorUtility.SetDirty(composite);

                BehaviorTreeEditor.dataChanged = true;
            }
        }

        public static void RemoveChild(Node parent, Node child)
        {
            RootNode rootNode = parent as RootNode;
            if (rootNode)
            {
                Undo.RecordObject(rootNode, "BT remove child");

                rootNode.child = null;

                EditorUtility.SetDirty(rootNode);

                BehaviorTreeEditor.dataChanged = true;
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                Undo.RecordObject(decorator, "BT remove child");

                decorator.child = null;

                EditorUtility.SetDirty(decorator);

                BehaviorTreeEditor.dataChanged = true;
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                Undo.RecordObject(composite, "BT remove child");

                composite.children.Remove(child);

                EditorUtility.SetDirty(composite);

                BehaviorTreeEditor.dataChanged = true;
            }
        }

        public static string RenameNode(string input)
        {
            int pos = input.IndexOf("Node");
            if (pos >= 0)
            {
                input = input.Remove(pos, 4);
            }

            return Regex.Replace(input, "([A-Z])([a-z]*)", " $1$2");
        }
    }
}
