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
            node.guid = GUID.Generate().ToString();

            tree.AddNode(node);

            AssetDatabase.AddObjectToAsset(node, tree);
            AssetDatabase.SaveAssets();

            return node;
        }

        public static void DeleateNode(BehaviorTree tree, Node node)
        {
            tree.RemoveNode(node);

            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public static void AddChild(Node parent, Node child)
        {
            RootNode rootNode = parent as RootNode;
            if(rootNode)
            {
                rootNode.child = child;
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if(decorator)
            {
                decorator.child = child;
            }

            CompositeNode composite = parent as CompositeNode;
            if(composite)
            {
                composite.children.Add(child);
            }

            EditorUtility.SetDirty(parent);
            AssetDatabase.SaveAssets();
        }

        public static void RemoveChild(Node parent, Node child)
        {
            RootNode rootNode = parent as RootNode;
            if (rootNode)
            {
                rootNode.child = null;
            }

            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = null;
            }

            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.children.Remove(child);
            }

            EditorUtility.SetDirty(parent);
            AssetDatabase.SaveAssets();
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
