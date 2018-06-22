using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class findTargets : MonoBehaviour {

	[SerializeField]
	private Text targetText = null;
	[SerializeField]
	private GameObject targetUI = null;
	private string[] targetString = null;
	private int currentTargetID;
	// Use this for initialization
	void Start () {
        // Get the Vuforia StateManager
        StateManager sm = TrackerManager.Instance.GetStateManager ();
 
        // Query the StateManager to retrieve the list of
        // currently 'active' trackables 
        //(i.e. the ones currently being tracked by Vuforia)
        IEnumerable<TrackableBehaviour> activeTrackables = sm.GetTrackableBehaviours (); //Active
 
       // Iterate through the list of active trackables
        Debug.Log ("List of trackables currently active (tracked): ");
		
		int i=0;
        foreach (TrackableBehaviour tb in activeTrackables) {
            Debug.Log("Trackable: " + tb.TrackableName);
			i++;
        }
        
        targetString = new string[i];

        i=0;    
        foreach (TrackableBehaviour tb in activeTrackables) {
            targetString[i] = tb.TrackableName;
			i++;
        }
        
        
        Invoke("nextTarget", 5);
        
        Debug.Log(targetString.Length);
	}
	
	// Update is called once per frame
	void Update () {
	}

    #region PUBLIC_METHODS

	public void nextTarget(){       
		if (currentTargetID < targetString.Length-1) currentTargetID++;			
        else currentTargetID = 0;

        targetText.text = targetString[currentTargetID];

	}

	#endregion PUBLIC_METHODS

}
