using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace UnityForgeEditor
{
    public static class UnityForgeEditorUtils
    {
        private const float LIST_FIELD_REMOVE_BUTTON_WIDTH = 20.0f;
        private const string RESOURCES_FOLDER_PATH = "Resources/";

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

        public static string ResourcesPathField<T>(string label, string path) where T : UnityEngine.Object
        {
            var result = path;
            var obj = Resources.Load(path);

            EditorGUI.BeginChangeCheck();

            var newObj = EditorGUILayout.ObjectField(label, obj, typeof(T), false) as T;

            if (EditorGUI.EndChangeCheck())
            {
                // For "None" object picker option return empty string as resources path
                if (newObj == null)
                {
                    result = String.Empty;
                }
                else
                {
                    var newPath = AssetDatabase.GetAssetPath(newObj);
                    if (newPath.Contains(RESOURCES_FOLDER_PATH))
                    {
                        // For paths containing several Resources folders like "Assets/A/Resources/C/D/E/Resources/F/G/Resources/File.extension"
                        // save full resources path "C/D/E/Resources/F/G/Resources/File"
                        var startIndex = newPath.IndexOf(RESOURCES_FOLDER_PATH) + RESOURCES_FOLDER_PATH.Length;
                        result = newPath.Substring(startIndex).Replace(Path.GetExtension(newPath), String.Empty);
                    }
                }
            }

            return result;
        }
    }
}
