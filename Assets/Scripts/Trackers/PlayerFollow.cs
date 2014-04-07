using UnityEngine;

public class FollowPlayer : Tracker
{
	public Vector3 offset;			
	
	//public Transform player;	
    

    #region interface
    public override void Track() {
        transform.position = player.position + offset;
    }
    #endregion
}
