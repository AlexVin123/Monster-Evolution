#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WMAO
{
    [ExecuteAlways]
    public static class Materials
    {
        private static int _proccessedFilesCount;
        private static string _path;
        private static string _root = "Assets";
        private static string[] _files;

        private static AssetOptimizerSettings _settings;

        public static void OptimizeMaterials()
        {
            _settings = AssetOptimizerSettings.GetSettings();

            if (_settings == null)
            {
                Debug.LogError("Asset Optimizer Settings can't be found. Create and put it on Resources folder.");
                return;
            }

            if (IsProccessRequired() == false)
                return;

            _proccessedFilesCount = 0;

            _files = Directory.GetFiles(_root + _path, "*.mat", SearchOption.AllDirectories);

            foreach (var filePath in _files)
                ProcessOptimization(filePath);

            Logger.CloseLog(_proccessedFilesCount, AssetType.Materials);
            Debug.Log($"{AssetType.Materials}: {_proccessedFilesCount} files has been optimized.");
        }

        private static void ProcessOptimization(string filePath)
        {
            Material material = AssetDatabase.LoadMainAssetAtPath(filePath) as Material;

            if (material != null && IsImportRequired(material))
            {
                material.enableInstancing = _settings.EnableGPUInstancing;
                AssetDatabase.ImportAsset(filePath);
                Resources.UnloadUnusedAssets();
                Logger.AddLog(filePath, AssetType.Materials);
                _proccessedFilesCount++;
            }
        }

        private static bool IsImportRequired(Material material)
        {
            if (_settings.OverrideGPUInstancing && material.enableInstancing != _settings.EnableGPUInstancing) return true;

            return false;
        }

        private static bool IsProccessRequired()
        {
            if (_settings.OverrideGPUInstancing) return true;

            return false;
        }
    }
}
#endif