using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBeatSpawner : MonoBehaviour
{
    [Header("References")]
    public TutorialDialogueManager dialogueManager;
    public TutorialSequencer stageSequencer;
    public TutorialScoreCounter scoreCounter;

    [Header("Audio & Map")]
    public AudioSource knp; // your music track
    public TextAsset beatMapJSON;

    [Header("Gameplay")]
    public GameObject[] beatPrefabs;
    public List<Transform> gridCells;
    public GameObject[] reticlePrefabs;
    public Transform barrier;

    [System.Serializable]
    public class PausePoint
    {
        public float time;                   // where in the track this pause starts
        public int dialoguePoint;            // dialogue index that triggers it
        public TutorialPhase requiredPhase;  // tutorial phase tied to this pause
    }

    [Header("Pause Settings")]
    public List<PausePoint> pausePoints;

    private List<BeatEvent> beatEvents;
    private float songTimer = 0f;
    private bool isPaused = false;
    private float lastCheckpointTime = 0f; // restart point for retries
    private int currentPauseIndex = 0;

    [Header("Gesture Spawner Variables")]
    public GameObject spawnTransform;
    private Transform transform1;
    public GameObject[] gesturePrefab;

    [HideInInspector] public int gestureChoice;
    [HideInInspector] public bool hasChosen;

    void Start()
    {
        // Load beat events from JSON
        beatEvents = JsonUtility.FromJson<BeatEventList>(beatMapJSON.text).events;
        transform1 = spawnTransform.transform;

        // Start waiting for tutorial sequence
        StartCoroutine(MainLoop());
    }

    IEnumerator MainLoop()
    {
        // Wait until tutorial "has started"
        yield return new WaitUntil(() => stageSequencer.hasStarted);

        // Step through pause points
        while (currentPauseIndex < pausePoints.Count)
        {
            PausePoint currentPause = pausePoints[currentPauseIndex];

            // Wait until dialogue reaches the right snippet index
            yield return new WaitUntil(() => dialogueManager.currentSnippetIndex == currentPause.dialoguePoint);

            // Wait out the dialogue snippet duration
            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[currentPause.dialoguePoint].snippetTime);

            // Record checkpoint and start music/spawning
            lastCheckpointTime = currentPause.time;
            knp.time = lastCheckpointTime;
            knp.Play();
            yield return StartCoroutine(SpawnBeatsFromTime(lastCheckpointTime));

            // Wait until the tutorial phase is marked complete
            yield return new WaitUntil(() => scoreCounter.isCompleted(currentPause.requiredPhase));

            // Move to the next pause point
            currentPauseIndex++;
        }
    }

    IEnumerator SpawnBeatsFromTime(float startTime)
    {
        int beatIndex = beatEvents.FindIndex(b => b.time >= startTime);

        while (beatIndex < beatEvents.Count && knp.isPlaying)
        {
            songTimer = knp.time; // sync timer to audio source

            if (songTimer >= beatEvents[beatIndex].time)
            {
                SpawnBeat(beatEvents[beatIndex]);
                beatIndex++;
            }

            yield return null;
        }
    }

    public void RestartFromCheckpoint()
    {
        Debug.Log("[Tutorial] Restarting from checkpoint at " + lastCheckpointTime + "s");

        // Stop music and coroutines
        knp.Stop();
        StopAllCoroutines();

        // Restart main loop from last checkpoint
        knp.time = lastCheckpointTime;
        knp.Play();
        StartCoroutine(SpawnBeatsFromTime(lastCheckpointTime));
    }

    public void SpawnBeat(BeatEvent beat)
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

    IEnumerator WaitForChoice()
    {
        while (true)
        {
            yield return new WaitUntil(() => hasChosen);
            switch (gestureChoice)
            {
                case 1:
                    StartCoroutine(SpawnBeatsWithPauses(gesturePrefab[0]));
                    break;
                case 2:
                    StartCoroutine(SpawnBeatsWithPauses(gesturePrefab[1]));
                    break;
                case 3:
                    StartCoroutine(SpawnBeatsWithPauses(gesturePrefab[2]));
                    break;
            }
            break;
        }
    }

    // Kept your original version of "SpawnBeatsWithPauses" for gestures to stay compatible
    IEnumerator SpawnBeatsWithPauses(GameObject gesture)
    {
        while (true)
        {
            yield return new WaitUntil(() => stageSequencer.hasStarted);
            knp.Play();

            int beatIndex = 0;
            int pauseIndex = 0;

            while (beatIndex < beatEvents.Count)
            {
                if (pauseIndex < pausePoints.Count && !isPaused)
                {
                    isPaused = true;
                    yield return new WaitUntil(() => pausePoints[pauseIndex].dialoguePoint == dialogueManager.currentSnippetIndex);
                    yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);

                    pauseIndex++;
                    isPaused = false;
                    continue;
                }

                if (!isPaused && beatIndex < beatEvents.Count)
                {
                    songTimer = knp.time;
                    if (songTimer >= beatEvents[beatIndex].time)
                    {
                        Debug.Log($"[Spawn] Spawning Beat #{beatIndex} at {songTimer:F2}s (scheduled: {beatEvents[beatIndex].time:F2}s)");
                        SpawnBeat(beatEvents[beatIndex]);
                        beatIndex++;
                    }
                }

                yield return null;
            }

            yield return null;
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

