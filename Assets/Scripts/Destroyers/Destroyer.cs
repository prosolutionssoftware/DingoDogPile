using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour
{  
    public virtual void DestroyGameObject()
    {        
        Destroy(gameObject);
    }

    public virtual void Destroy() {
        Destroy(gameObject);
    }
}