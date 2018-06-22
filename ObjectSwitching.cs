using UnityEngine;

public class ObjectSwitching : MonoBehaviour {

	public int selectedARObject = 0;

	// Use this for initialization
	void Start () {
		SelectARObject();
	}
	
	// Update is called once per frame
	void Update () {

		int previousSelectedARObject = selectedARObject;

		if (Input.GetAxis("Mouse ScrollWheel") > 0f){
			if (selectedARObject >= transform.childCount - 1){
				selectedARObject = 0;
			} else {
				selectedARObject++;
			}
		}
		if (Input.GetAxis("Mouse Scrollwheel") < 0f){
			if (selectedARObject <= 0){
				selectedARObject = transform.childCount -1 ;
			} else {
				selectedARObject--;
			}
		}

		if (previousSelectedARObject != selectedARObject){
			SelectARObject();
		}
	}

	void SelectARObject (){
		
		int i =0;
		foreach (Transform ARObject in transform ){
			
			if (i == selectedARObject){
				ARObject.gameObject.SetActive(true);
			} else {
				ARObject.gameObject.SetActive(false);
			}
			i++;
		}
	}
}
