using UnityEngine;
using System.Collections;

class Projectile : MonoBehaviour
{
    public GameObject explosion;
    public float lifeTime = 1f;

    public Quaternion RandomRotation 
    {
        get 
        { 
            return Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)); 
        }
    }

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTrigger2D() 
    {
        if (collider.tag == "Enemy")
        {
            collider.gameObject.GetComponent<Enemy>().Hurt();
            OnImpact();
        }
        else if (collider.gameObject.tag != "Player" && collider.gameObject.tag != "Bullet" )
        {
            OnImpact();
        }
    }

    void OnImpact( ) 
    {
        Instantiate(explosion, transform.position, RandomRotation);
        Destroy(gameObject);
    }
}
