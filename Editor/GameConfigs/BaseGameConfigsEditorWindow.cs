using UnityEditor;
using UnityEngine;

namespace UnityForgeEditor.GameConfigs
{
    public abstract class BaseGameConfigsEditorWindow : EditorWindow
    {
        private GameConfigsEditorsToolbar editorsToolbar_ = new GameConfigsEditorsToolbar();

        protected abstract bool AreGameConfigsLoaded { get; } 
        protected abstract void LoadGameConfigs();
        protected abstract void SaveGameConfigs();

        protected void SetEditors(IGameConfigsEditor[] editors)
        {
            editorsToolbar_.SetEditors(editors);
        }

        private void OnGUI()
        {
            ControlPanelGUI();
            EditorGUILayout.Space();
            SubeditorSelectGUI();
        }

        private void ControlPanelGUI()
        {
            EditorGUILayout.BeginVertical(GUI.skin.box);
            {
                if (GUILayout.Button("Load configs"))
                {
                    LoadGameConfigs();
                }

                if (AreGameConfigsLoaded)
                {
                    if (GUILayout.Button("Save configs"))
                    {
                        SaveGameConfigs();
                    }
                }
            }
            EditorGUILayout.EndVertical();
        }

        private void SubeditorSelectGUI()
        {
            if (AreGameConfigsLoaded)
            {
                editorsToolbar_.Draw();
            }
            else
            {
                EditorGUILayout.LabelField("Please load configs to start working");
            }
        }
    }
}
