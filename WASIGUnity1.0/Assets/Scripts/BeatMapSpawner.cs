using System.Collections.Generic;
using UnityEngine;
using static Oculus.Interaction.Context;

public class BeatMapSpawner : MonoBehaviour
{
    [Header("Audio & Map")]
    public AudioSource musicSource;
    public TextAsset beatMapJSON;

    [Header("Gameplay")]
    public GameObject[] beatPrefabs;
    public List<Transform> gridCells;
    public GameObject [] reticlePrefabs;
    public Transform barrier;

    private List<BeatEvent> beatEvents;
    private int nextBeatIndex = 0;
    

    void Start()
    {
        // Validate setup
        if (musicSource == null || beatMapJSON == null || beatPrefabs.Length == 0 || gridCells.Count != 9)
        {
            Debug.LogError("BeatMapSpawner is not properly configured.");
            enabled = false;
            return;
        }

        // Parse JSON

        beatEvents = JsonUtility.FromJson<BeatEventList>(beatMapJSON.text).events;

        nextBeatIndex = 0;
        musicSource.Play();
    }

    void Update()
    {
        if (nextBeatIndex >= beatEvents.Count)
            return;

        while (nextBeatIndex < beatEvents.Count &&
               musicSource.time >= beatEvents[nextBeatIndex].time)
        {
            SpawnBeat(beatEvents[nextBeatIndex]);
            nextBeatIndex++;
        }
    }

    void SpawnBeat(BeatEvent beat)
    {
        int cellIndex = Mathf.Clamp(beat.cellIndex, 0, gridCells.Count - 1);
        int prefabIndex = Mathf.Clamp(beat.prefabIndex, 0, beatPrefabs.Length - 1);

        Transform cell = gridCells[cellIndex];
        GameObject prefab = beatPrefabs[prefabIndex];
        GameObject reticle;

        Quaternion rotation;




        if (prefabIndex == 2)
        {
            rotation = Quaternion.Euler(0f, 90f, 0f);
            reticle = reticlePrefabs[2];
        }

        else if (prefabIndex == 1)
        {
            rotation = Quaternion.Euler(-90f, 0f, 0f);
            reticle = reticlePrefabs[1];
        }

        else if (prefabIndex == 0)
        {
            rotation = Quaternion.Euler(-90f, 0f, 0f);
            reticle = reticlePrefabs[0];
        }

        else
        {
            rotation = Quaternion.identity;
            reticle = reticlePrefabs[0];
        }





            GameObject instance = Instantiate(prefab, cell.position, rotation);

        Debug.Log($"[Beat {beat.time:F2}s] Spawned {prefab.name} at cell {cellIndex}");

        Instantiate (reticle, new Vector3 (cell.position.x, cell.position.y, barrier.position.z-2f), Quaternion.identity);

       

        // Add forward movement
        BeatMover mover = instance.AddComponent<BeatMover>();
        mover.moveSpeed = 4f; // Set speed as you prefer
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

