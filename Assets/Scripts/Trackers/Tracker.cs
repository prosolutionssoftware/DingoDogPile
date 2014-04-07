using UnityEngine;

public abstract class Tracker : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        Track();
    }

    public abstract void Track();
}