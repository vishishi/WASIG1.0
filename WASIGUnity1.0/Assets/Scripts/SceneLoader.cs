using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    //list of banks to load
    [FMODUnity.BankRef]
    public List<string> Banks;

    //name of scene to load and switch to
    //public string Scene = null;


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadSceneAsync());
        Debug.Log("Scene change coroutine started!");
    }





    IEnumerator LoadSceneAsync()
    {
        AsyncOperation async = SceneManager.LoadSceneAsync("Main");

        async.allowSceneActivation = false;

        foreach(var bank in Banks)
        {
            FMODUnity.RuntimeManager.LoadBank(bank, true);
        }

        while (FMODUnity.RuntimeManager.AnyBankLoading())
        {
            yield return null;
        }

        async.allowSceneActivation = true;
        Debug.Log("Scene has been allowed!");

        while (!async.isDone)
        {
            yield return null;
        }
        Destroy(this.gameObject);


    }
}
