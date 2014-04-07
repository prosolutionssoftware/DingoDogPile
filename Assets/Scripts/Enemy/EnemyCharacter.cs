using UnityEngine;
using System.Collections;

public class EnemyCharacter : MonoBehaviour
{
    public float speed = 2f;		    
    public int health = 2;       
    public Sprite deadSprite;			
    public Sprite damagedSprite;		
    public AudioClip[] deathClips;    	        
    public int points = 50;
    public GameObject pointsUI;

    private SpriteRenderer spriteRenderer;			
    private Transform enemyFront;		
    private bool isDead = false;			
    private Score score;				


    void Awake()
    {
        enemyFront = transform.Find("frontCheck").transform;
        spriteRenderer = transform.Find("body").GetComponent<SpriteRenderer>();        
        score = GameObject.Find("Score").GetComponent<Score>();
    }

    void FixedUpdate()
    {
        Collider2D[] coll = Physics2D.OverlapPointAll(enemyFront.position, 1);
        foreach (Collider2D c in coll)
        {
            if (c.tag == "Obstacle")
            {
                Turn();
                break;
            }
        }

        rigidbody2D.velocity = new Vector2(transform.localScale.x * speed, rigidbody2D.velocity.y);

        if (health == 1 && damagedSprite != null)
        {
            spriteRenderer.sprite = damagedSprite;
        }
        if (health <= 0 && !isDead)
        {
            Die();
        }
    }

    public void Damage()
    {
        health--;
    }

    void Die()
    {
        SpriteRenderer[] otherRenderers = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer s in otherRenderers)
        {
            s.enabled = false;
        }
               
        spriteRenderer.enabled = true;
        spriteRenderer.sprite = deadSprite;

        isDead = true;

        rigidbody2D.fixedAngle = false;
        rigidbody2D.AddTorque(Random.Range(-180f, 180f));

        Collider2D[] cols = GetComponents<Collider2D>();
        foreach (Collider2D c in cols)
        {
            c.isTrigger = true;
        }

        int i = Random.Range(0, deathClips.Length);
        AudioSource.PlayClipAtPoint(deathClips[i], transform.position);

        Vector3 scorePos;
        scorePos = transform.position;
        scorePos.y += 1.5f;

        score.score += points;
        Instantiate(pointsUI, scorePos, Quaternion.identity);
    }
    
    public void Turn()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
