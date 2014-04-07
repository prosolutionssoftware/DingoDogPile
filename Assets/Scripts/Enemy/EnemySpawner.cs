using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float repeatRate = 4f;
    public float time = 5f;	
    public GameObject[] enemies;


    void Start()
    {
        InvokeRepeating("Spawn", time, repeatRate);
    }


    void Spawn()
    {
        int i = Random.Range(0, enemies.Length);
        Instantiate(enemies[i], transform.position, transform.rotation);

        foreach (ParticleSystem p in GetComponentsInChildren<ParticleSystem>())
        {
            p.Play();
        }
    }
}