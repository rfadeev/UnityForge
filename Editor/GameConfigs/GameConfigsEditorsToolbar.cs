using System;
using UnityEngine;

namespace UnityForgeEditor.GameConfigs
{
    public class GameConfigsEditorsToolbar
    {
        private int toolbarIndex_ = 0;
        private string[] toolbarStrings_;
        private IGameConfigsEditor[] editors_;

        public void SetEditors(IGameConfigsEditor[] editors)
        {
            toolbarStrings_ = Array.ConvertAll(editors, x => { return x.EditorToolbarText; });
            editors_ = editors;
        }

        public void Draw()
        {
            toolbarIndex_ = GUILayout.Toolbar(toolbarIndex_, toolbarStrings_);
            editors_[toolbarIndex_].DrawEditorGUI();
        }
    }
}
