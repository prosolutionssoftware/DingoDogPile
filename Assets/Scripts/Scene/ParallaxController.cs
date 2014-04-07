using UnityEngine;
using System.Collections;

public class ParallaxController : MonoBehaviour
{
    public Camera cam;
    public Transform[] backgrounds;
    public float speed = 0.1f;
    public float reductionFactor = 0.9f;
    public float smoothing = 5;

    private Vector3 currentCamPos;
    private Vector3 previousCamPos;

    void Awake()
    {
        currentCamPos = cam.transform.position;
        previousCamPos = currentCamPos;
    }

    void Update() 
    {
        currentCamPos = cam.transform.position;
		float parallax = ( previousCamPos.x - currentCamPos.x ) * speed;
        Vector3 objectPosition;

        for (int i = 0; i < backgrounds.Length; i++) 
        {
            objectPosition = backgrounds[i].transform.position;
            objectPosition.x += parallax * (i * reductionFactor + 1);
            backgrounds[i].transform.position = Vector3.Lerp(backgrounds[i].position, objectPosition, smoothing);            
        }

        previousCamPos = currentCamPos; 
    }
}
