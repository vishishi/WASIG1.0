using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureSpawnner : MonoBehaviour
{
    
    public GameObject spawnTransform;
    private Transform transform1;
    public GameObject [] gesturePrefab;
    public float[] spawnTime;
    public StartGame gameStarter;

    void Update()
    {
        
    }

    private void Start()
    {
        transform1 = spawnTransform.transform;
    }

    public void Gesture1 ()
    {
        StartCoroutine(SpawnGestures(gesturePrefab[0]));
    
    }

    public void Gesture2 ()
    {
        StartCoroutine(SpawnGestures(gesturePrefab[1]));
    }

    public void Gesture3 ()
    {
        StartCoroutine(SpawnGestures(gesturePrefab[2]));
    }

    IEnumerator SpawnGestures(GameObject gesture)
    {
       
        yield return new WaitUntil(() => gameStarter.hasStarted);
        Debug.Log("Gesture spawnning started!");
        yield return new WaitForSeconds(spawnTime[0]);
        GameObject firstGesture = Instantiate(gesture, transform1.position, Quaternion.identity);
        Debug.Log ("Gesture" + gesture.name + "spawwned at" + gesture.transform.position.ToString());
        yield return new WaitForSeconds(7);
        Destroy(firstGesture );
        yield return new WaitForSeconds(spawnTime[1]);
        GameObject secondGesture = Instantiate(gesture, transform1.position, Quaternion.identity);
        yield return new WaitForSeconds(7);
        Destroy(secondGesture);
        yield return new WaitForSeconds(spawnTime[2]);
        GameObject thirdGesture = Instantiate(gesture, transform1.position, Quaternion.identity);
        yield return new WaitForSeconds(7);
        Destroy(thirdGesture);
        
        yield return null;


    }


}
