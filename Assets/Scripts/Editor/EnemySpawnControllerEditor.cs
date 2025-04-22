using System;
using System.IO;
using EnemySpawnSystem;
using UnityEditor;

[CustomEditor(typeof(EnemySpawnerController))]
public class EnemySpawnControllerEditor : Editor
{
    private SerializedProperty wavesProp;
    private int lastCount = -1;

    private void OnEnable()
    {
        wavesProp = serializedObject.FindProperty("waves");
        lastCount = wavesProp.arraySize;
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        serializedObject.Update();

        EditorGUILayout.PropertyField(wavesProp, true);

        serializedObject.ApplyModifiedProperties();

        // Detect if a new item was added
        if (wavesProp.arraySize > lastCount)
        {
            for (int i = lastCount; i < wavesProp.arraySize; i++)
            {
                EnemySpawnWaveSo newWave = CreateNewWaveAsset();
                wavesProp.GetArrayElementAtIndex(i).objectReferenceValue = newWave;

                EditorUtility.SetDirty(target);
                Selection.activeObject = newWave;
            }

            lastCount = wavesProp.arraySize;
            serializedObject.ApplyModifiedProperties();
        }
        else if (wavesProp.arraySize < lastCount)
        {
            lastCount = wavesProp.arraySize;
        }
    }

    private EnemySpawnWaveSo CreateNewWaveAsset()
    {
        string folderPath = "Assets/SpawnWaves";
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        EnemySpawnWaveSo newWave = CreateInstance<EnemySpawnWaveSo>();
        string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{folderPath}/Wave_{Guid.NewGuid()}.asset");

        AssetDatabase.CreateAsset(newWave, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        return newWave;
    }
}