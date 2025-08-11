using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starstick : MonoBehaviour

//This script handles the movement and color of the lightsticks. It makes sure that they are moving randomly and that they can synch their behaviour
//at certain moments of gameplay, such as fanchant and the super charge mode.
{
    [Header("Movement variables")]

    public float wavingSpeedMin;
    public float wavingSpeedMax;
    private float wavingSpeed;
    public float wavingRange;
    public float movementWaitTime;
    public float wavingTiltAmount;
    public float lerpSpeedDuration;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private float previousWavingSpeed;
    private float newWavingSpeed;
    private float lerpTime;

    [Header("Color variables")]
    [ColorUsage(true, true)]
    public Color[] colors;
    [ColorUsage(true, true)]
    public Color overrideColor;

    private Renderer myRenderer;
    private Material[] myMaterials;
    private Material materialCopy;
    private Color copyColor;

    [HideInInspector]
    public ScoreCounter score;

    private bool isTesting = true;
    public bool isCheering = false;

    [HideInInspector]
    public BeatMapSpawner spawner;


    private float targetSpeed;
    // Each stick's own phases (for random mode)
    private float localPhaseX, localPhaseY, localPhaseZ;

    //shared phases for sync mode
    private static float globalPhaseX, globalPhaseY, globalPhaseZ;
    private static int lastPhaseUpdateFrame = -1; // ensures one update per frame

    //flag set by game events
    public static bool allSynced = false;

    //auto end time for sync windows
    private static float syncUntilTime = -1f;

    // NEW: smooth visual blend between local and global motion (0 = local, 1 = full sync)
    private float syncBlend = 0f;
    [SerializeField] private float syncBlendSpeed = 3f; // units per second for blend in/out

    void Start()
    {
        spawner = FindAnyObjectByType<BeatMapSpawner>();
        score = FindAnyObjectByType<ScoreCounter>();

        //initialise movement variables
        initialPosition = gameObject.transform.position;
        initialRotation = gameObject.transform.rotation;
        wavingSpeed = Random.Range(wavingSpeedMin, wavingSpeedMax);
        previousWavingSpeed = wavingSpeed;
        newWavingSpeed = wavingSpeed;

        targetSpeed = wavingSpeed;
        localPhaseX = localPhaseY = localPhaseZ = 0f;

        //initialise color changing variables
        myRenderer = GetComponent<Renderer>();
        myMaterials = myRenderer.materials;
        materialCopy = new Material(myMaterials[1]);
        myMaterials[1] = materialCopy;
        myRenderer.materials = myMaterials;

        //assing random color
        copyColor = colors[Random.Range(0, colors.Length)];
        materialCopy.EnableKeyword("_EMISSION");
        myMaterials[1].SetColor("_EmissionColor", copyColor);
        Debug.Log("<color=#" + ColorUtility.ToHtmlStringRGB(copyColor) + ">Current color is: " + copyColor.ToString() + "</color>");

        //start coroutines
        StartCoroutine(ColorSwitcher());
        StartCoroutine(UpdatePhases());

        // NEW: test sequence so you can see the smooth transition in action

    }

    void Update()
    {
        // NEW: drive blend toward target (smoothly fade into/out of sync)
        float targetBlend = allSynced ? 1f : 0f;
        syncBlend = Mathf.MoveTowards(syncBlend, targetBlend, Time.deltaTime * syncBlendSpeed);

        // Always compute both local and global motions, then blend
        float localX = Mathf.Sin(localPhaseX) * wavingRange;
        float localY = Mathf.Sin(localPhaseY) * wavingRange * 0.5f;
        float localTilt = Mathf.Sin(localPhaseX) * wavingTiltAmount;

        float globalX = Mathf.Sin(globalPhaseX) * wavingRange;
        float globalY = Mathf.Sin(globalPhaseY) * wavingRange * 0.5f;
        float globalTilt = Mathf.Sin(globalPhaseX) * wavingTiltAmount;

        float x = Mathf.Lerp(localX, globalX, syncBlend);
        float y = Mathf.Lerp(localY, globalY, syncBlend);
        float tilt = Mathf.Lerp(localTilt, globalTilt, syncBlend);

        transform.position = initialPosition + new Vector3(x, y, 0);
        transform.rotation = initialRotation * Quaternion.Euler(0, 0, tilt);
    }

    IEnumerator UpdatePhases()
    {
        while (true)
        {
            // NEW: auto-exit sync when the window expires (checked once per frame across all sticks)
            if (allSynced && syncUntilTime > 0f && Time.time >= syncUntilTime)
            {
                Debug.Log("[Sync] Auto-ending sync window");
                allSynced = false;
                syncUntilTime = -1f;
            }

            if (allSynced)
            {
                // All sticks share phases, but update them only once per frame globally
                if (lastPhaseUpdateFrame != Time.frameCount)
                {
                    float delta = wavingSpeed * Time.deltaTime;
                    globalPhaseX = Mathf.Repeat(globalPhaseX + delta, Mathf.PI * 2f);
                    globalPhaseY = Mathf.Repeat(globalPhaseY + delta * 0.7f, Mathf.PI * 2f);
                    globalPhaseZ = Mathf.Repeat(globalPhaseZ + delta * 1.3f, Mathf.PI * 2f);

                    lastPhaseUpdateFrame = Time.frameCount;
                }
            }
            else
            {
                // This stick advances its own local phases
                float delta = wavingSpeed * Time.deltaTime;
                localPhaseX = Mathf.Repeat(localPhaseX + delta, Mathf.PI * 2f);
                localPhaseY = Mathf.Repeat(localPhaseY + delta * 0.7f, Mathf.PI * 2f);
                localPhaseZ = Mathf.Repeat(localPhaseZ + delta * 1.3f, Mathf.PI * 2f);
            }

            yield return null; // wait a frame
        }
    }

    IEnumerator ColorSwitcher()
    //looping coroutine that changes the color of the light stick if you get super charged
    {
        while (true)
        {
            yield return new WaitUntil(() => score.isSuperCharged);
            myMaterials[1].SetColor("_EmissionColor", overrideColor);

            yield return new WaitUntil(() => !score.isSuperCharged);
            myMaterials[1].SetColor("_EmissionColor", copyColor);
        }
    }

    // NEW: call this from gameplay to sync everyone for a duration (seamless entry)
    public void TriggerSync(float duration)
    {
        // Seed shared phases from THIS instance so there is no pop when entering sync
        globalPhaseX = localPhaseX;
        globalPhaseY = localPhaseY;
        globalPhaseZ = localPhaseZ;

        allSynced = true;
        syncUntilTime = Time.time + Mathf.Max(0f, duration);

        Debug.Log($"[Sync] Triggered for {duration:F2}s (blend in over ~{1f / syncBlendSpeed:F2}s)");
    }

    // NEW: optional helper to force start/stop without duration
    public static void SetSynced(bool enabled)
    {
        allSynced = enabled;
        syncUntilTime = -1f;
        Debug.Log($"[Sync] SetSynced({enabled})");
    }
}

    //Test coroutine to verify timing & smoothness: wait 5s, sync 2s, repeat x3
  //  private IEnumerator TestSyncSequence()
    //{
      //  for (int i = 0; i < 3; i++)
        //{
          //  Debug.Log($"[TestSync] Waiting 5 seconds before sync #{i + 1}...");
            //yield return new WaitForSeconds(5f);
            //
            //Debug.Log($"[TestSync] Starting sync #{i + 1} for 2 seconds (blended)!");
            //TriggerSync(2f);
            //
            //yield return new WaitForSeconds(2f);
            //Debug.Log($"[TestSync] Sync #{i + 1} duration elapsed. (Blend will fade out)");
       // }

        //Debug.Log("[TestSync] Sequence complete.");
    //}
//}




