using UnityEngine;
using UnityEditor;

namespace Lab5Games.AI.Editor
{
    using Node = BehaviorTreeNode;


    public static class BehaviorTreeUtils
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

            AssetDatabase.SaveAssets();
        }
    }
}
