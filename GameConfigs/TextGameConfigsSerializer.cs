using System.IO;
using UnityEngine;
using UnityForge.Serialization;

namespace UnityForge.GameConfigs
{
    public abstract class TextGameConfigsSerializer<TData>
    {
        private readonly string gameConfigsResourcesPath_;
        private readonly string gameConfigsFullPath_;
        private readonly string gameConfigsExtension_;

        public TextGameConfigsSerializer(string gameConfigsPath, string gameConfigsFullPath, string gameConfigsExtension)
        {
            gameConfigsResourcesPath_ = gameConfigsPath;
            gameConfigsFullPath_ = gameConfigsFullPath;
            gameConfigsExtension_ = gameConfigsExtension;
        }

        protected abstract TData ReadDataFromText(string text);
        protected abstract string WriteDataToText(TData data);

        public void LoadConfigFromText(ILoader<TData> gameConfig, string fileName)
        {
            var fileResourcesPath = GetConfigFileResourcesPath(fileName);
            var fileTextAsset = Resources.Load<TextAsset>(fileResourcesPath);
            if (fileTextAsset != null)
            {
                var data = ReadDataFromText(fileTextAsset.text);
                gameConfig.Load(data);
            }
            else
            {
                Debug.LogErrorFormat("Failed to load configs from resources path {0}", fileResourcesPath);
            }
        }

#if UNITY_EDITOR
        public void SaveConfigToText(ISaver<TData> gameConfig, string fileName)
        {
            var fileFullPath = GetConfigFileFullPath(fileName);
            if (File.Exists(fileFullPath))
            {
                var data = gameConfig.Save();
                var text = WriteDataToText(data);

                File.WriteAllText(fileFullPath, text);
                UnityEditor.AssetDatabase.ImportAsset(fileFullPath);
            }
            else
            {
                Debug.LogErrorFormat("Failed to save configs since file for path do not exist {0}", fileFullPath);
            }
        }
#endif

        private string GetConfigFileResourcesPath(string fileName)
        {
            return gameConfigsResourcesPath_ + "/" + fileName;
        }

#if UNITY_EDITOR
        private string GetConfigFileFullPath(string fileName)
        {
#if UNITY_EDITOR_WIN
            var gameConfigsFullPath = gameConfigsFullPath_.Replace("/", "\\");
#else
            var gameConfigsFullPath = gameConfigsFullPath_;
#endif
            return Path.Combine(gameConfigsFullPath, fileName + gameConfigsExtension_);
        }
#endif
    }
}
