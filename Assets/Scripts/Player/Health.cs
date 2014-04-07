using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour
{
    public SpriteRenderer healthBar;			// Reference to the sprite renderer of the health bar.
    public float health = 10f;					// Initial player health.
    public AudioClip[] injuryClips;				// Array of clips to play when player is damanged.
    public float injuryRecoil = 10f;			// Amount the player recoils from injury.
    public float damageAmount = 10f;			// The amount a player is damaged by enemy. 
    public float damageFrequency = 2f;		    // How often player can be damaged.

    private Vector3 healthScale;				// Sets size of the health bar.
    private float lastInjured;			        // Previous damange time.

    private PlayerController playerController;	// PlayerController script
    private Animator anim;

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
        healthScale = healthBar.transform.localScale;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
        {
            if (Time.time > lastInjured + damageFrequency)
            {
                if (health > 0f)
                {
                    TakeDamage(coll.transform);
                    lastInjured = Time.time;
                }
                else
                {
                    Collider2D[] cols = GetComponents<Collider2D>();
                    foreach (Collider2D c in cols)
                    {
                        c.isTrigger = true;
                    }

                    SpriteRenderer[] spr = GetComponentsInChildren<SpriteRenderer>();
                    foreach (SpriteRenderer s in spr)
                    {
                        s.sortingLayerName = "UI";
                    }

                    playerController.enabled = false;
                    GetComponentInChildren<Gun>().enabled = false;
                    anim.SetTrigger("Die");
                }
            }
        }
    }

    void TakeDamage(Transform enemy)
    {
        health -= damageAmount;
        UpdateHealthBar();

        playerController.canJump = false;
        Vector3 hurtVector = transform.position - enemy.position + Vector3.up * 5f;
        rigidbody2D.AddForce(hurtVector * injuryRecoil);

        int i = Random.Range(0, injuryClips.Length);
        AudioSource.PlayClipAtPoint(injuryClips[i], transform.position);
    }

    public void UpdateHealthBar()
    {
        //decrement modifier is 0.01f, since default enemy damageAmount is 10% total health.
        healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - health * 0.01f);
        healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, 1, 1);
    }
}