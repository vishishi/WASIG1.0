using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HypeTracksAudio : MonoBehaviour
{
    [SerializeField] FMODUnity.EventReference hyperModeHTref;
    FMOD.Studio.EventInstance hyperModeHTinst;
    
    // Start is called before the first frame update
    void Start()
    {
        hyperModeHTinst = RuntimeManager.CreateInstance(hyperModeHTref);

        //we probably will trigger the start elsewhere but for now. 
        //or maybe we could turn it on? who knows 
        hyperModeHTinst.start();
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(hyperModeHTinst, transform);
        hyperModeHTinst.release();


        
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOnHyperModeHT()
    {
        hyperModeHTinst.setParameterByName("VolumeOnOff", 1, false);
    }

    public void TurnOffHyperModeHT()
    {
        hyperModeHTinst.setParameterByName("VolumeOnOff", 0, false);
    }


}
