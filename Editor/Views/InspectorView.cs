using System;
using UnityEngine.UIElements;

namespace Lab5Games.AI.Editor
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactory : UxmlFactory<InspectorView, VisualElement.UxmlTraits> { }

        UnityEditor.Editor editor;

        internal void CreateView(NodeView nodeView)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);

            editor = UnityEditor.Editor.CreateEditor(nodeView.treeNode);

            IMGUIContainer container = new IMGUIContainer(() =>
            {
                if (editor.target)
                {
                    editor.OnInspectorGUI();
                }
            });

            Add(container);
        }
    }
}
