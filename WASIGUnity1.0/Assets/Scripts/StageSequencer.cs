using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSequencer : MonoBehaviour
{
    [Header("Opening Sequence Variables")]
    public Animator lightsAnimator;
    public GameObject OVRCamera;
    public GameObject innerStage;
    public GameObject outerStage;
    public GameObject outerRing;


    
    
    public AudioSource platformSound;
    public AudioSource cheering;
    public ParticleSystem smoke;


    private Vector3 isPosition;
    private Vector3 osPosition;
    private Vector3 ovrPosition;
    private Vector3 isTargetPosition;
    private Vector3 osTargetPosition;
    private Vector3 ovrTargetPosition;
    private Vector3 circlePosition;


    [HideInInspector]
    public bool isChearing = false;
   // [HideInInspector]
    public bool hasStarted = false;



    private void Awake()
    {

        isPosition = new Vector3(innerStage.transform.position.x, innerStage.transform.position.y - 2f, innerStage.transform.position.z);
        osPosition = new Vector3(outerStage.transform.position.x, outerStage.transform.position.y - 2f, outerStage.transform.position.z);
        ovrPosition = new Vector3(OVRCamera.transform.position.x, OVRCamera.transform.position.y - 2f, OVRCamera.transform.position.z);
        circlePosition = new Vector3( -1.1877313e-07f, 6.41739988f, 2.24810004f);
    }
    void Start()
    {
       
        
        // Initialise stage animation variables
        innerStage.transform.position = isPosition;
        outerStage.transform.position = osPosition;
        OVRCamera.transform.position = ovrPosition;
        isTargetPosition = new Vector3 (isPosition.x, isPosition.y +2f, isPosition.z);
        osTargetPosition = new Vector3 (osPosition.x, osPosition.y +2f, osPosition.z);
        ovrTargetPosition = new Vector3 (ovrPosition.x, ovrPosition.y + 2f, ovrPosition.z);

      

        StartCoroutine(OpeningSequence());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator OpeningSequence()
    {
        float elapsed = 0;
        float duration = 5;
        cheering.Play();
       // FMODUnity.RuntimeManager.PlayOneShot("event:/OpeningCheer", circlePosition);
        yield return new WaitForSeconds (5.0f);
        //FMODUnity.RuntimeManager.PlayOneShotAttached("event:/PlatformSound", innerStage);
        //
        platformSound.Play();
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float moveProgress = Mathf.Clamp01(elapsed / duration);
            
            innerStage.transform.position = Vector3.Lerp(isPosition, isTargetPosition, moveProgress);
            outerStage. transform.position = Vector3.Lerp(osPosition, osTargetPosition, moveProgress);
            OVRCamera.transform.position = Vector3.Lerp(ovrPosition, ovrTargetPosition, moveProgress);


            yield return null;
        }
        isChearing = true;


        //yield return new WaitUntil(() => !cheering.isPlaying);
        //change this but just for now
        yield return new WaitForSeconds(5);

        isChearing = false;
        lightsAnimator.SetBool("hasBegan", true);
        hasStarted = true;
    
       


     //   while (elapsed < duration)
       // {  elapsed += Time.deltaTime;
           
        //}
        
        

    }
}
