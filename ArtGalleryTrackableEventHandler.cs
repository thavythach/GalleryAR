/**
 * ArtGalleryTrackableEventHandler Sub-Class
 * @desc a simple animation and video handler.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class ArtGalleryTrackableEventHandler : DefaultTrackableEventHandler
{
	#region PUBLIC_MEMBER_VARIABLES
	
	public bool initialAnimate = false;

	#endregion

	#region PRIVATE_MEMBER_VARIABLES
	private bool isRendered = false;
    private GameObject animObj;
	private GameObject[] ARObjects;
	
	#endregion // PRIVATE_MEMBER_VARIABLES

	#region UNITY_MONOBEHAVIOUR_METHODS

	protected override void Start(){
		// animObj = GameObject.Find("Obj1");

		// TODO: remove hardcode
		ARObjects = new GameObject[2];
		ARObjects[0] = GameObject.Find("Obj_1");
		ARObjects[1] = GameObject.Find("Obj_2");
		base.Start();
	}

	void Update(){
		if (isRendered && initialAnimate){
			foreach ( GameObject ARObject in ARObjects){
				ARObject.transform.Rotate(20.0f * Vector3.right * Time.deltaTime);
			}
		}
	}
	
	#endregion // UNITY_MONOBEHAVIOUR_METHODS
	
 
    #region PROTECTED_METHODS

	protected override void OnTrackingFound()
    {         
        isRendered = true;
		base.OnTrackingFound();
    }
    protected override void OnTrackingLost()
    {
        //mTrackableBehaviour.GetComponentInChildren<VideoController>().Pause();
		isRendered = false;
        base.OnTrackingLost();
    }

    #endregion // PROTECTED_METHODS
}

