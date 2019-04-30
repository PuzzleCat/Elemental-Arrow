using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void play(){
		PlayerPrefs.SetInt ("Start", 0);
		Application.LoadLevel ("Main");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	public void level1(){
		PlayerPrefs.SetInt ("Start", 0);
		Application.LoadLevel ("Level1");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	public void level2(){
		PlayerPrefs.SetInt ("Start", 0);
		Application.LoadLevel ("Level2");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	public void level3(){
		PlayerPrefs.SetInt ("Start", 0);
		Application.LoadLevel ("Level3");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
	public void level4(){
		PlayerPrefs.SetInt ("Start", 0);
		Application.LoadLevel ("Level4");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}

	public void home(){
		Application.LoadLevel ("Start");
		Screen.orientation = ScreenOrientation.LandscapeLeft;
	}
}
