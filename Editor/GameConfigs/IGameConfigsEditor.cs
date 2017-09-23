namespace UnityForgeEditor.GameConfigs
{
    public interface IGameConfigsEditor
    {
        string EditorToolbarText { get; }
        void DrawEditorGUI();
    }
}
