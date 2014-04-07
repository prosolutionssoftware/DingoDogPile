using UnityEngine;
using System.Collections;

public class CharacterDestroyer : Destroyer
{
    public GameObject FX;
    public GameObject cam;
    public GameObject healthBar;
    public int waitBeforeReload = 2;

    private const string player = "Player";
	private Collider2D coll;

    void OnTriggerEnter2D(Collider2D coll)
    {
		this.coll = coll;
        if (coll.gameObject.tag == player)
        {
            CleanupPlayer();
            Destroy();
            StartCoroutine("ReloadGame");
        }
        else
        {
            Destroy();
        }
    }

    void CleanupPlayer()
    {
        cam.GetComponent<CameraTracker>().enabled = false;
        if (healthBar.activeSelf)
        {
            healthBar.SetActive(false);
        }
    }

    public override void Destroy() {
        Instantiate(FX, coll.transform.position, transform.rotation);
        Destroy(coll.gameObject);
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(waitBeforeReload);
        Application.LoadLevel(Application.loadedLevel);
    }
}