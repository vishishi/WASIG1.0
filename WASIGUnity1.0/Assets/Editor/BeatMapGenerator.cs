using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BeatMapGenerator : EditorWindow
{
    private float bpm = 154f;
    private float duration = 60f; // seconds
    private float spawnChance = 0.5f; // 0–1
    private int gridCellCount = 9;
    private int prefabCount = 2;
    private string fileName = "beatmap.json";

    [MenuItem("Tools/Generate Beat Map")]
    public static void ShowWindow()
    {
        GetWindow<BeatMapGenerator>("Beat Map Generator");
    }

    void OnGUI()
    {
        GUILayout.Label("Beat Map Parameters", EditorStyles.boldLabel);
        bpm = EditorGUILayout.FloatField("BPM", bpm);
        duration = EditorGUILayout.FloatField("Duration (s)", duration);
        spawnChance = EditorGUILayout.Slider("Spawn Chance", spawnChance, 0f, 1f);
        gridCellCount = EditorGUILayout.IntField("Grid Cell Count", gridCellCount);
        prefabCount = EditorGUILayout.IntField("Prefab Count", prefabCount);
        fileName = EditorGUILayout.TextField("Output File Name", fileName);

        if (GUILayout.Button("Generate Beat Map"))
        {
            GenerateBeatMap();
        }
    }

    void GenerateBeatMap()
    {
        float beatInterval = 60f / bpm;
        int totalBeats = Mathf.FloorToInt(duration / beatInterval);

        List<BeatEvent> events = new List<BeatEvent>();

        for (int i = 0; i < totalBeats; i++)
        {
            if (Random.value <= spawnChance)
            {
                float time = i * beatInterval;
                int cellIndex = Random.Range(0, gridCellCount);
                int prefabIndex = Random.Range(0, prefabCount);

                events.Add(new BeatEvent
                {
                    time = time,
                    cellIndex = cellIndex,
                    prefabIndex = prefabIndex
                });
            }
        }

        string json = JsonUtility.ToJson(new BeatEventList { events = events }, true);
        string path = EditorUtility.SaveFilePanel("Save Beat Map", Application.dataPath, fileName, "json");

        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, json);
            AssetDatabase.Refresh();
            Debug.Log($"Beat map saved to: {path}");
        }
    }

    [System.Serializable]
    public class BeatEvent
    {
        public float time;
        public int cellIndex;
        public int prefabIndex;
    }

    [System.Serializable]
    public class BeatEventList
    {
        public List<BeatEvent> events;
    }
}
