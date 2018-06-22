/*===============================================================================
Copyright (c) 2017 PTC Inc. All Rights Reserved.

Vuforia is a trademark of PTC Inc., registered in the United States and other 
countries.
===============================================================================*/
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.Audio;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    #region PRIVATE_MEMBERS

    private AudioSource audioSource;

    #endregion //PRIVATE_MEMBERS


    #region PUBLIC_MEMBERS

    public Button m_PlayButton;

    #endregion //PRIVATE_MEMBERS


    #region MONOBEHAVIOUR_METHODS

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {

        if (audioSource.isPlaying)
        {
            ShowPlayButton(false);

        }
        else
        {
            ShowPlayButton(true);
        }
    }

    void OnApplicationPause(bool pause)
    {
        Debug.Log("OnApplicationPause(" + pause + ") called.");
        if (pause)
            Pause();
    }

    #endregion // MONOBEHAVIOUR_METHODS


    #region PUBLIC_METHODS

    public void Play()
    {
        Debug.Log("Play Audio");
        PauseAudio(false);
        audioSource.Play();
        ShowPlayButton(false);
    }

    public void Pause()
    {
        if (audioSource)
        {
            Debug.Log("Pause Audio");
            PauseAudio(true);
            audioSource.Pause();
            ShowPlayButton(true);
        }
    }

    #endregion // PUBLIC_METHODS


    #region PRIVATE_METHODS

    private void PauseAudio(bool pause)
    {
        if (pause) audioSource.Pause();
        else audioSource.UnPause();
    }

    private void ShowPlayButton(bool enable)
    {
        m_PlayButton.enabled = enable;
        m_PlayButton.GetComponent<Image>().enabled = enable;
    }

    #endregion // PRIVATE_METHODS


    #region DELEGATES

    void HandleVideoError(AudioSource audio, string errorMsg)
    {
        Debug.LogError("Error: " + audio.clip.name + "\nError Message: " + errorMsg);
    }

    void HandleStartedEvent(AudioSource audio)
    {
        Debug.Log("Started: " + audio.clip.name);
    }

    void HandlePrepareCompleted(AudioSource audio)
    {
        Debug.Log("Prepare Completed: " + audio.clip.name);
    }

    void HandleSeekCompleted(AudioSource audio)
    {
        Debug.Log("Seek Completed: " + audio.clip.name);
    }

    void HandleLoopPointReached(AudioSource audio)
    {
        Debug.Log("Loop Point Reached: " + audio.clip.name);

        ShowPlayButton(true);
    }

    #endregion //DELEGATES

}
