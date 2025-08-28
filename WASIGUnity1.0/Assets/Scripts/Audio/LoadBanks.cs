using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class LoadBanks : MonoBehaviour
{
    [BankRef] public string bankName;

    private void Awake()
    {
        RuntimeManager.LoadBank(bankName, true);
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDestroy()
    {
        RuntimeManager.UnloadBank(bankName);
    }



    // Update is called once per frame
    void Update()
    {
        if (RuntimeManager.AnyBankLoading())
            Debug.Log(RuntimeManager.AnyBankLoading());
        
    }
}
