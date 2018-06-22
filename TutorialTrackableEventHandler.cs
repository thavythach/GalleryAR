/*==============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using Vuforia;

/// <summary>
///     A custom handler that implements the ITrackableEventHandler interface.
/// </summary>
public class TutorialTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    [SerializeField]
    private int selectedARObject = 0;
    public static int AR_RENDER_IDX = 0, AR_COLLIDER_IDX = 1, AR_CANVAS_IDX = 2;
    
    /**
      * define the selected arguments id if video or audio
      **/
    
    // videoclips
    private int[] selectedARObject_video00_id = {7, 7, 0}; // render,collider,canvas - conan
    
    // audioclips
    private int[] selectedARObject_audio00_id = {8, 8, 1}; // render,collider,canvas - 15-s polyester audioclipp
    
    private float[,] transformations;
    int len_tutorialInstantiedObjects;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
            len_tutorialInstantiedObjects = mTrackableBehaviour.gameObject.GetComponentInChildren<Transform>().childCount;
            transformations = new float[len_tutorialInstantiedObjects,3];
            var tutorialInstantiedObjects = mTrackableBehaviour.gameObject.GetComponentInChildren<Transform>();
            
            int i = 0; 
            foreach (Transform tr in tutorialInstantiedObjects.transform){
                Debug.Log(string.Format("Begin Log {0}",i));
                transformations[0,0] = tr.position.x;
                transformations[0,1] = tr.position.y;
                transformations[0,2] = tr.position.z;
                transformations[1,0] = tr.rotation.x;
                transformations[1,1] = tr.rotation.y;
                transformations[1,2] = tr.rotation.z;
                transformations[2,0] = tr.localScale.x;
                transformations[2,1] = tr.localScale.y;
                transformations[2,2] = tr.localScale.z;
                Debug.Log(string.Format("End Log {0}",i));
                i++;
            }
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    // to enable new objects
    public void onNextARObject(){
        mTrackableBehaviour.GetComponentInChildren<ArtGalleryVideoController>().Pause();
        mTrackableBehaviour.GetComponentInChildren<AudioController>().Pause();
        selectedARObject++;

        // reset position of every object except video, audio, and te
        if (selectedARObject < len_tutorialInstantiedObjects){
            if (selectedARObject == 7 || selectedARObject == 8 || selectedARObject == 9){
                // do nothing
            } else {
                mTrackableBehaviour.gameObject.transform.position = new Vector3(transformations[0,0], transformations[0,1], transformations[0,2]);
                var tutorialInstantiedObjects = mTrackableBehaviour.gameObject.GetComponentInChildren<Transform>();
                foreach (Transform tr in tutorialInstantiedObjects.transform.GetChild(selectedARObject)){
                    // Debug.Log("yolo");
                    // Debug.Log(tr.position.x);
                    // Debug.Log(tr.position.y);
                    // Debug.Log(tr.position.z);
                    //Debug.Log("realyolo"); 
                    tr.position = new Vector3(transformations[0,0], transformations[0,1], transformations[0,2]);
                    //tr.eulerAngles = new Vector3(transformations[1,0], transformations[1,1], transformations[1,2]);
                    tr.localScale = new Vector3(transformations[2,0], transformations[2,1], transformations[2,2]);
                    // Debug.Log(transformations[0, 0]); // pos.x
                    // Debug.Log(transformations[0, 1]); // pos.y
                    // Debug.Log(transformations[0, 2]); // pos.z
                }
            }
            
        }
        
        TutorialRenderHandler();
    }

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " found");
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NOT_FOUND)
        {
            Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {        
        TutorialRenderHandler();
    }


    protected virtual void OnTrackingLost()
    {
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        // TODO: requires testing with multiple videos
        mTrackableBehaviour.GetComponentInChildren<ArtGalleryVideoController>().Pause();

        // Disable rendering:
        foreach (var component in rendererComponents)
            component.enabled = false;

        // Disable colliders:
        foreach (var component in colliderComponents)
            component.enabled = false;

        // Disable canvas':
        foreach (var component in canvasComponents)
            component.enabled = false;

        
    }

    #endregion // PROTECTED_METHODS

    #region PRIVATE_METHODS
    
    private void TutorialRenderHandler(){
        
        var rendererComponents = GetComponentsInChildren<Renderer>(true);
        var colliderComponents = GetComponentsInChildren<Collider>(true);
        var canvasComponents = GetComponentsInChildren<Canvas>(true);

        if (selectedARObject > rendererComponents.Length){ 
            selectedARObject = 0;
        }

        // Enable rendering:
        int i=0;
        foreach (var component in rendererComponents){
            if (i == selectedARObject) component.enabled = true; 
            else  component.enabled = false;



            i++;
        }

        // Enable colliders:
        i=0;
        foreach (var component in colliderComponents){
            if (i == selectedARObject) component.enabled = true; 
            else  component.enabled = false;
 

            i++;
        }

        // Enable canvas':
        i=0;
        foreach (var component in canvasComponents){
            
            component.enabled = false;
            
            // video00
            if (i == selectedARObject_video00_id[AR_CANVAS_IDX] && selectedARObject == selectedARObject_video00_id[AR_RENDER_IDX]){
                component.enabled = true;
            } 

            // audio00
            if (i == selectedARObject_audio00_id[AR_CANVAS_IDX] && selectedARObject == selectedARObject_audio00_id[AR_RENDER_IDX]){
                component.enabled = true;
            }
            

            i++;
        }
    }

    #endregion // PRIVATE_METHODS

}
