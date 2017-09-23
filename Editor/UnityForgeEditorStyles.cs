using UnityEngine;

namespace UnityForgeEditor
{
    public static class UnityForgeEditorStyles
    {
        private static readonly Color LIST_ITEM_SELECTED_COLOR = new Color(0.6f, 0.6f, 1.0f, 0.5f);

        public static GUIStyle BoxStyle { get { return GUI.skin.box; } }
        public static GUIStyle ListItemStyle { get; private set; }
        public static GUIStyle ListItemSelectedStyle { get; private set; }

        static UnityForgeEditorStyles()
        {
            ListItemStyle = new GUIStyle(GUI.skin.button);

            ListItemStyle.alignment = TextAnchor.MiddleLeft;
            ListItemStyle.normal.background = null;
            ListItemStyle.active.background = null;
            ListItemStyle.hover.background = null;

            var texture = new Texture2D(1, 1);
            texture.SetPixel(0, 0, LIST_ITEM_SELECTED_COLOR);
            texture.Apply();

            ListItemSelectedStyle = new GUIStyle(GUI.skin.button);
            ListItemSelectedStyle.alignment = TextAnchor.MiddleLeft;
            ListItemSelectedStyle.normal.background = texture;
            ListItemSelectedStyle.active.background = texture;
            ListItemSelectedStyle.hover.background = texture;
        }
    }
}
