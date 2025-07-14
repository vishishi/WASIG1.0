
using UnityEngine;

public class BeatMover : MonoBehaviour
{
    public float moveSpeed;

    void Update()
    {
        transform.position += Vector3.forward * moveSpeed * Time.deltaTime;
    }
}
