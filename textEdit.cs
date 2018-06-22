using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Vuforia;

public class textEdit : MonoBehaviour {

	[SerializeField]
	private Text tutorialText = null;
	[SerializeField]
	private GameObject tutorialUI = null;
	private string[] tutorialString = new string[11];
	private int currentTutorialID;
	// Use this for initialization
	void Start () {
		tutorialString[0] = tutorialText.text;

		// TODO: put these in a ".txt" file to read from.
		tutorialString[1] = "Find Image Marker: \"Drone\"";
		tutorialString[2] = "This is a 3D Cube that\'s been augmented onto our world";
		tutorialString[3] = "Move the cube using a finger";
		tutorialString[4] = "Rotate the cube using two fingers";
		tutorialString[5] = "Enlarge or Shrink the cube using two fingers";
		tutorialString[6] = "We can now Translate, Rotate, and Scale any object";
		tutorialString[7] = "Click Play Button to learn about the School of Product Design";
		tutorialString[8] = "Play the audio by clicking the play button for a 15-second clip on Polyester.";
		tutorialString[9] = "One of the many text patterns you'll ever see.";
		tutorialString[10] = "If you're ready to explore, then take the leap and click the Next Button";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    #region PUBLIC_METHODS

	public void nextTutorial(){
		if (currentTutorialID < tutorialString.Length-1){
			currentTutorialID++;
			tutorialText.text = tutorialString[currentTutorialID];				
		} else {
			tutorialText.text = "Enjoy the real experience!";
			skipTutorial();
		}	
	}

	public void skipTutorial(){
		SceneManager.LoadScene("Sandbox", LoadSceneMode.Single);
		//Display.displays[1].Activate();
	}
	
	// TODO: add previousTutorial() if you want

	#endregion PUBLIC_METHODS

}
