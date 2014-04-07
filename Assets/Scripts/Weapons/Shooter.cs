using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

    public Rigidbody2D bullet;
    public float speed = 20f;

    private PlayerController player;
    private Animator anim;

    void Awake() 
    {
        anim = transform.root.gameObject.GetComponent<Animator>();
        player = transform.root.GetComponent<PlayerController>();
    
    }

    void Update() 
    {
        if (Input.GetButtonDown("Fire1")) 
        {
            anim.SetTrigger("Shoot");
            audio.Play();

            if (player.isFacingRight)
            {
                Rigidbody2D 'Instance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
                'Instance.velocity = new Vector2(speed, 0);
            }
            else 
            {
                Rigidbody2D 'Instance = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector3(0, 0, 180f))) as Rigidbody2D;
                'Instance.velocity = new Vector2(-speed, 0);                    
            }
        }    
    }

}