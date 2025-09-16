using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

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
    private float lastCheckpointTime = 0f; // restart point for retries
    private int currentPauseIndex = 0;

    private Coroutine spawnRoutine; // keep track of active spawn coroutine

    [Header("Gesture Spawner Variables")]
    public GameObject spawnTransform;
    private Transform transform1;
    public GameObject[] gesturePrefab;

    [HideInInspector] public int gestureChoice;
    [HideInInspector] public bool hasChosen;

    private float snippetTime;

    void Start()
    {
        // Load beat events from JSON
        beatEvents = JsonUtility.FromJson<BeatEventList>(beatMapJSON.text).events;
        transform1 = spawnTransform.transform;

        // Start waiting for tutorial sequence
        StartCoroutine(MainLoop());
        //StartCoroutine(WaitForChoice());
    }

    private void Update()
    {
        snippetTime = dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime;
    }



    IEnumerator MainLoop()
    {
        int i = 0;
        GameObject gestureInstance;//Instantiate(gesturePrefab[i], transform1.position, Quaternion.identity);
        // Step through pause points
        while (currentPauseIndex < pausePoints.Count)
        {
  
            PausePoint currentPause = pausePoints[currentPauseIndex];

            // Wait until dialogue reaches the right snippet index
            yield return new WaitUntil(() => dialogueManager.currentSnippetIndex == currentPause.dialoguePoint);
            //if (hasChosen)
            //{
            //    //yield return new WaitForSeconds(snippetTime);
                
            //    switch (gestureChoice)
            //    { 
                  
            //        case 1:
            //            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
            //            i = 0;
            //            gestureInstance = Instantiate(gesturePrefab[i], transform1.position, Quaternion.identity);
            //            Debug.Log("prefab instantiated!");
            //            yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Gesture));
            //            GameObject[] allInstances = GameObject.FindGameObjectsWithTag("Gesture Prefabs");
            //            foreach (var instance in allInstances)
            //            {
            //                Destroy(instance);
            //                Debug.Log(instance.name + " has been " + "<color=red>DESTROYED</color>");
            //            }
                     
            //            StopAllCoroutines();
            //            Debug.Log("<color=red> ALL COROUTINES HAVE STOPPED</color>");

            //            break;
            //        case 2:
            //            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
            //            i = 1;
            //            gestureInstance = Instantiate(gesturePrefab[i], transform1.position, Quaternion.identity);
            //            Debug.Log("prefab instantiated!");
            //            yield return new WaitUntil(() => scoreCounter.isCompleted(TutorialPhase.Gesture));
            //            GameObject[] a = GameObject.FindGameObjectsWithTag("Gesture Prefabs");
            //            foreach(var instance in a)
            //            {
            //                Destroy(instance);
            //                Debug.Log(instance.name + " has been " + "<color=red>DESTROYED</color>");
            //            }
                        
            //            StopAllCoroutines();
            //            Debug.Log("<color=red>ALL COROUTINES HAVE STOPPED</color>");

            //            break;
            //        case 3:
            //            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime);
            //            i = 2;
            //            gestureInstance = Instantiate(gesturePrefab[i], transform1.position, Quaternion.identity);
            //            GameObject[] b = GameObject.FindGameObjectsWithTag("Gesture Prefabs");
            //            foreach (var instance in b)
            //            {
            //                Destroy(instance);
            //                Debug.Log(instance.name + " has been " + "<color=red>DESTROYED</color> ");
            //            }
            //            Debug.Log("<color=red>ALL COROUTINES HAVE STOPPED</color>");

            //            break;

            //    }

                
               


            //}
            // Wait out the dialogue snippet duration
            Debug.Log(" <color=#FF00FF> Beat Spawner: </color> " + "read the snippet number as " + dialogueManager.currentSnippetIndex.ToString());
            Debug.Log(" <color=#FF00FF> Beat Spawner: </color> " + "read the snippet time as " + snippetTime.ToString());
            yield return new WaitForSeconds(dialogueManager.dialogueSnippets[currentPause.dialoguePoint].snippetTime);

            // Record checkpoint and start music/spawning
            lastCheckpointTime = currentPause.time;
            StartSpawnFromCheckpoint(lastCheckpointTime);

            // Wait until the tutorial phase is marked complete
            yield return new WaitUntil(() => scoreCounter.isCompleted(currentPause.requiredPhase));

         
            StopMusicAndSpawns();

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

    // 🔹 Public helpers
    public void StartSpawnFromCheckpoint(float startTime)
    {
        // Stop old routine if still running
        if (spawnRoutine != null)
            StopCoroutine(spawnRoutine);

        knp.time = startTime;
        knp.Play();
        spawnRoutine = StartCoroutine(SpawnBeatsFromTime(startTime));
    }

    public void RestartFromCheckpoint()
    {
        if (!scoreCounter.isCompleted(TutorialPhase.Gesture))
        {
            Debug.Log("[Tutorial] Restarting from checkpoint at " + lastCheckpointTime + "s");
            StopMusicAndSpawns();
            StartSpawnFromCheckpoint(lastCheckpointTime);
        }

        else
        {
            StopAllCoroutines();
        }
    }

    public void StopMusicAndSpawns()
    {
        knp.Stop();
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
            spawnRoutine = null;
        }


    }


    public void Extermination()
    {
        StopCoroutine(spawnRoutine);
        GameObject[] tutorialBeats = GameObject.FindGameObjectsWithTag("Beat");
        foreach (var beat in tutorialBeats)
        {
            Destroy(beat);
        }
        GameObject[] tutorialReticles = GameObject.FindGameObjectsWithTag("Reticle");
        foreach (var reticle in tutorialReticles)
        {
            Destroy(reticle);
        }
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



    IEnumerator SpawnBeatsWithPauses(GameObject gesture)
    {
        while (true)
        {
          
            knp.Play();

            int beatIndex = 0;
            int pauseIndex = 0;

            while (beatIndex < beatEvents.Count)
            {
                if (pauseIndex < pausePoints.Count)
                {
                    yield return new WaitUntil(() => pausePoints[pauseIndex].dialoguePoint == dialogueManager.currentSnippetIndex);
                    yield return new WaitForSeconds(dialogueManager.dialogueSnippets[dialogueManager.currentSnippetIndex].snippetTime + 2);

                    pauseIndex++;
                    continue;
                }

                songTimer = knp.time;
                if (songTimer >= beatEvents[beatIndex].time)
                {
                    Debug.Log($"[Spawn] Spawning Beat #{beatIndex} at {songTimer:F2}s (scheduled: {beatEvents[beatIndex].time:F2}s)");
                    SpawnBeat(beatEvents[beatIndex]);
                    beatIndex++;
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

