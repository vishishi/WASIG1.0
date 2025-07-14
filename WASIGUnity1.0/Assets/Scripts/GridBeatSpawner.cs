using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBeatSpawner : MonoBehaviour
{
    [Header("References")]
    public List<Transform> gridCells;      // 9 GameObjects in 3x3 grid
    public GameObject[] beatPrefabs;       // Prefabs to spawn
    public Transform player;               // Optional, for movement

    [Header("Spawn Timing")]
    public float bpm;               // Beats per minute
    private float secondsPerBeat;

    [Range(0f, 1f)]
    public float spawnChance = 0.6f;       // Chance to spawn on a beat

    [Header("Object Behavior")]
    public bool moveToPlayer = true;
    public float moveSpeed = 2f;

    void Start()
    {
        // Safety checks
        if (gridCells.Count != 9)
        {
            Debug.LogError("You must assign exactly 9 grid cell Transforms.");
            return;
        }

        if (beatPrefabs.Length == 0)
        {
            Debug.LogError("No beat prefabs assigned!");
            return;
        }

        if (moveToPlayer && player == null)
        {
            Debug.LogWarning("MoveToPlayer enabled, but no player assigned.");
        }

        secondsPerBeat = 60f / bpm;
        StartCoroutine(SpawnBeatRoutine());
    }

    IEnumerator SpawnBeatRoutine()
    {
        int beatCount = 0;

        while (true)
        {
            if (Random.value <= spawnChance)
            {
                SpawnOnRandomCell(beatCount);
            }

            beatCount++;
            yield return new WaitForSeconds(secondsPerBeat);
        }
    }

    void SpawnOnRandomCell(int beatIndex)
    {
        int cellIndex = Random.Range(0, gridCells.Count);
        Transform cell = gridCells[cellIndex];

        GameObject prefab = beatPrefabs[Random.Range(0, beatPrefabs.Length)];
        GameObject instance = Instantiate(prefab, cell.position, Quaternion.identity);

        Debug.Log($"[Beat {beatIndex}] Spawned {prefab.name} at Grid Cell {cellIndex} (Pos: {cell.position})");

        if (moveToPlayer && player != null)
        {
            BeatMover mover = instance.AddComponent<BeatMover>();
         //   mover.target = player;

            float distance = Vector3.Distance(cell.position, player.position);
            float travelTime = secondsPerBeat;
          //  mover.moveSpeed = distance / travelTime;
        }
    }
}

