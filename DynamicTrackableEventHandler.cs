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
public class DynamicTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
#region PROTECTED_MEMBER_VARIABLES

protected TrackableBehaviour mTrackableBehaviour;
[SerializeField]
private int selectedARObject = 0;

#endregion // PROTECTED_MEMBER_VARIABLES

#region UNITY_MONOBEHAVIOUR_METHODS

protected virtual void Start()
{
    mTrackableBehaviour = GetComponent<TrackableBehaviour>();
    if (mTrackableBehaviour)
        mTrackableBehaviour.RegisterTrackableEventHandler(this);
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
    
    selectedARObject++;
    
    var rendererComponents = GetComponentsInChildren<Renderer>(true);
    var colliderComponents = GetComponentsInChildren<Collider>(true);
    var canvasComponents = GetComponentsInChildren<Canvas>(true);
    if (selectedARObject > rendererComponents.Length){ 
        selectedARObject = 0;
    }

    // Enable rendering:
    int i=0;
    foreach (var component in rendererComponents){
        Debug.Log(component);
        if (i == selectedARObject) component.enabled = true;                
        else component.enabled = false;
        i++;
    }

    // Enable colliders:
    i=0;
    foreach (var component in colliderComponents){
        if (i == selectedARObject) component.enabled = true;                
        else component.enabled = false;
        i++;
    }

    // Enable canvas':
    i=0;
    foreach (var component in canvasComponents){
        if (i == selectedARObject) component.enabled = true;                
        else component.enabled = false;
        i++;
    }
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
    var rendererComponents = GetComponentsInChildren<Renderer>(true);
    var colliderComponents = GetComponentsInChildren<Collider>(true);
    var canvasComponents = GetComponentsInChildren<Canvas>(true);
    if (selectedARObject > rendererComponents.Length){ // hard coded value from textEdit;
        selectedARObject = 0;
    }

    // Enable rendering:
    int i=0;
    foreach (var component in rendererComponents){
        Debug.Log(component);
        if (i == selectedARObject) component.enabled = true;                
        else component.enabled = false;
        i++;
    }

    // Enable colliders:
    i=0;
    foreach (var component in colliderComponents){
        if (i == selectedARObject) component.enabled = true;                
        else component.enabled = false;
        i++;
    }

    // Enable canvas':
    i=0;
    foreach (var component in canvasComponents){
        if (i == selectedARObject) component.enabled = true;                
        else component.enabled = false;
        i++;
    }
}


protected virtual void OnTrackingLost()
{
    var rendererComponents = GetComponentsInChildren<Renderer>(true);
    var colliderComponents = GetComponentsInChildren<Collider>(true);
    var canvasComponents = GetComponentsInChildren<Canvas>(true);

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



#endregion // PRIVATE_METHODS

}
