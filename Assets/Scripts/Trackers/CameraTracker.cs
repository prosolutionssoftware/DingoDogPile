using UnityEngine;

public class CameraTracker : Tracker
{
    //public Transform player;
    public float smooth = 1f;
  
    #region Tracker
    public override void Track()
    {
        if (player != null)
        {
            float targetX = transform.position.x;
            float targetY = transform.position.y;

            targetX = Mathf.Lerp(transform.position.x, player.position.x, smooth);
            targetY = Mathf.Lerp(transform.position.y, player.position.y, smooth);

            transform.position = new Vector3(targetX, targetY, transform.position.z);
        }
    }
    #endregion
}