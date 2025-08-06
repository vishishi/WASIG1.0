using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatMapSpawner : MonoBehaviour
{
    [Header("Audio & Map")]
    public AudioSource musicSource;
    public TextAsset beatMapJSON;

    [Header("Gameplay")]
    public GameObject[] beatPrefabs;
    public List<Transform> gridCells;
    public GameObject[] reticlePrefabs;
    public Transform barrier;

    private List<BeatEvent> beatEvents;

    [System.Serializable]
    public class PausePoint
    {
        public float time;
        public float duration;
    }

    [Header("Pause Settings")]
    public List<PausePoint> pausePoints;

    private float songTimer = 0f;
    private bool isPaused = false;
    //

    [Header("Gesture Spawnner Variables")]
    public GameObject spawnTransform;
    private Transform transform1;
    public GameObject[] gesturePrefab;

    [HideInInspector]
    public int gestureChoice;


    void Start()
    {
        beatEvents = JsonUtility.FromJson<BeatEventList>(beatMapJSON.text).events;
        transform1 = spawnTransform.transform;

        musicSource.Play();
        
        switch(gestureChoice)
        {
            case (1):
                {
                    StartCoroutine(SpawnBeatsWithPauses(gesturePrefab[0]));
                }
                break;
            case (2):
                {
                    StartCoroutine(SpawnBeatsWithPauses(gesturePrefab[1]));
                }
                break;
            case (3):
                {
                    StartCoroutine(SpawnBeatsWithPauses(gesturePrefab[2]));
                }
                break;
        }

        
    }

    IEnumerator SpawnBeatsWithPauses(GameObject gesture)
    {
        int beatIndex = 0;
        int pauseIndex = 0;

        while (beatIndex < beatEvents.Count)
        {
            if (!isPaused)
            {
                songTimer += Time.deltaTime;
            }

            // Handle any pause
            if (pauseIndex < pausePoints.Count && songTimer >= pausePoints[pauseIndex].time && !isPaused)
            {
                isPaused = true;
                float pauseDuration = pausePoints[pauseIndex].duration;
                Debug.Log($"[Pause] Pausing spawn logic for {pauseDuration}s at songTime={songTimer:F2}s");
                GameObject firstGesture = Instantiate(gesture, transform1.position, Quaternion.identity);
                Debug.Log("Gesture" + gesture.name + "spawwned at" + gesture.transform.position.ToString());
                yield return new WaitForSeconds(pauseDuration);
                Destroy(firstGesture);

                pauseIndex++;
                isPaused = false;
                continue;
            }

            // Spawn beat if it’s time
            if (!isPaused && beatIndex < beatEvents.Count && songTimer >= beatEvents[beatIndex].time)
            {
                Debug.Log($"[Spawn] Spawning Beat #{beatIndex} at {songTimer:F2}s (scheduled: {beatEvents[beatIndex].time:F2}s)");
                SpawnBeat(beatEvents[beatIndex]);
                beatIndex++;
            }

            yield return null;
        }
    }

    void SpawnBeat(BeatEvent beat)
    {
        int cellIndex = Mathf.Clamp(beat.cellIndex, 0, gridCells.Count - 1);
        int prefabIndex = Mathf.Clamp(beat.prefabIndex, 0, beatPrefabs.Length - 1);

        Transform cell = gridCells[cellIndex];
        GameObject prefab = beatPrefabs[prefabIndex];
        GameObject reticle = reticlePrefabs[Mathf.Clamp(prefabIndex, 0, reticlePrefabs.Length - 1)];

        Quaternion rotation = prefabIndex == 2
            ? Quaternion.Euler(0f, 90f, 0f)
            : Quaternion.Euler(-90f, 0f, 0f);

        GameObject instance = Instantiate(prefab, cell.position, rotation);
        Instantiate(reticle, new Vector3(cell.position.x, cell.position.y, barrier.position.z - 2f), Quaternion.identity);

        BeatMover mover = instance.AddComponent<BeatMover>();
        mover.moveSpeed = 4f;
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



