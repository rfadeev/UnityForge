using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace UnityForgeEditor
{
    public static class UnityForgeEditorUtils
    {
        private const float LIST_FIELD_REMOVE_BUTTON_WIDTH = 20.0f;

        public static void ListField<T>(string title, IList<T> items, Func<T> itemCreator, Func<T, T> itemDrawer)
        {
            EditorGUILayout.BeginVertical(UnityForgeEditorStyles.BoxStyle);
            {
                EditorGUILayout.LabelField(title, UnityForgeEditorStyles.BoldLabel);

                if (GUILayout.Button("+"))
                {
                    var item = itemCreator();
                    items.Add(item);
                }

                if (items.Count != 0)
                {
                    EditorGUILayout.BeginVertical(UnityForgeEditorStyles.BoxStyle);
                    {
                        int? removeIndex = null;

                        for (var i = 0; i < items.Count; ++i)
                        {
                            EditorGUILayout.BeginHorizontal(UnityForgeEditorStyles.BoxStyle);
                            {
                                items[i] = itemDrawer(items[i]);

                                if (GUILayout.Button("-", GUILayout.Width(LIST_FIELD_REMOVE_BUTTON_WIDTH)))
                                {
                                    removeIndex = i;
                                }
                            }
                            EditorGUILayout.EndHorizontal();
                        }

                        if (removeIndex.HasValue)
                        {
                            items.RemoveAt(removeIndex.Value);
                        }
                    }
                    EditorGUILayout.EndVertical();
                }
            }
            EditorGUILayout.EndVertical();
        }
    }
}
