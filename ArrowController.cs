using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ArrowController : MonoBehaviour {

	public GameObject arrow;
	public GameObject flameArrow;
	public GameObject iceArrow;
	public GameObject lightningArrow;
	public GameObject mudArrow;
	public GameObject vineArrow;
	public GameObject waterArrow;
	public GameObject windArrow;
	public GameObject bow;
	public Text points;
	public Text arrows;
	public Text highscore;
	public Text LevelMode;

	private Vector3 original;
	private Vector3 target;
	private Vector3 path;
	private Vector3 temp;
	private Rigidbody2D rb;
	private bool drag;
	private int score;
	private int shots;

	// Use this for initialization
	void Start () {
		original = transform.position;
		rb = GetComponent<Rigidbody2D> ();
		drag = false;
		if (PlayerPrefs.GetInt ("Start") == 0) {
			points.text = "Points: 0";
			score = 0;
			shots = 0;
			arrows.text = "Arrows: " + (shots + 5);
			if(LevelMode.text == "Level1"){
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore1");
			}
			else if(LevelMode.text == "Level2"){
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore2");
			}
			else if(LevelMode.text == "Level3"){
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore3");
			}
			else if(LevelMode.text == "Level4"){
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore4");
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (drag == false) {
			if (rb.velocity.x == 0) {
				if (rb.velocity.y > 0) {
					transform.eulerAngles = new Vector3 (0, 0, -270);
				} else if (rb.velocity.y < 0) {
					transform.eulerAngles = new Vector3 (0, 0, -90);
				} else if (rb.velocity.y == 0) {
					transform.eulerAngles = temp;
				}
			} else {
				if (rb.velocity.x > 0) {
					transform.eulerAngles = new Vector3 (0, 0, (180 * Mathf.Atan (rb.velocity.y / rb.velocity.x) / Mathf.PI));
				} else if (rb.velocity.x < 0) {
					transform.eulerAngles = new Vector3 (0, 0, (180 * Mathf.Atan (rb.velocity.y / rb.velocity.x) / Mathf.PI) - 180);
				}
			}
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		PlayerPrefs.SetInt("Start", 1);
		if (other.gameObject.CompareTag ("Wall")) {
			drag = true;
			StartCoroutine (wallHit ());
		} else if (other.gameObject.CompareTag ("Arrow")) {
			if (other.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude >= 0.5f) {
				gameObject.SetActive (false);
			}
		} else if (other.gameObject.CompareTag ("Obstacle")) {
			StartCoroutine (obstacleHit ());
			if(flameArrow.activeSelf == true){
				other.gameObject.GetComponent<ObstacleController>().burn();
			}
			else if(iceArrow.activeSelf == true){
				other.gameObject.GetComponent<ObstacleController>().freeze();
			}
			else if(lightningArrow.activeSelf == true){
				other.gameObject.GetComponent<ObstacleController>().explode();
			}
			else if(vineArrow.activeSelf == true){
				other.gameObject.GetComponent<ObstacleController>().overgrow();
			}
		} else{
			if (other.gameObject.CompareTag ("Yellow") && drag == false) {
				score = score + 15;
				points.text = "Points: " + score;
				rb.isKinematic = true;
				drag = true;
				Instantiate(gameObject);
			}
			else if (other.gameObject.CompareTag ("Red") && drag == false) {
				score = score + 10;
				points.text = "Points: " + score;
				rb.isKinematic = true;
				drag = true;
				Instantiate(gameObject);
			}
			else if (other.gameObject.CompareTag ("Blue") && drag == false) {
				score = score + 5;
				points.text = "Points: " + score;
				rb.isKinematic = true;
				drag = true;
				Instantiate(gameObject);
			}
			else if (other.gameObject.CompareTag ("Black") && drag == false) {
				score = score + 3;
				points.text = "Points: " + score;
				rb.isKinematic = true;
				drag = true;
				Instantiate(gameObject);
			}
			else if (other.gameObject.CompareTag ("White") && drag == false) {
				score = score + 1;
				points.text = "Points: " + score;
				rb.isKinematic = true;
				drag = true;
				Instantiate(gameObject);
			}
			StartCoroutine (targetHit ());
		}
	}

	void OnMouseDrag(){
		drag = true;
		temp = transform.eulerAngles;
		target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target.z = transform.position.z;
		path = target - original;
		if (path.x == 0) {
			if (path.y > 0) {
				transform.eulerAngles = new Vector3 (0, 0, -90);
			} else if (path.y < 0) {
				transform.eulerAngles = new Vector3 (0, 0, 90);
			} else if (path.y == 0) {
				transform.eulerAngles = temp;
			}
		} else {
			if (path.x > 0) {
				transform.eulerAngles = new Vector3 (0, 0, (180 * Mathf.Atan (path.y / path.x) / Mathf.PI) + 180);
			} else if (path.x < 0) {
				transform.eulerAngles = new Vector3 (0, 0, (180 * Mathf.Atan (path.y / path.x) / Mathf.PI));
			}
		}
	}

	void OnMouseUp(){
		drag = false;
		temp = transform.eulerAngles;
		target = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		target.z = transform.position.z;
		path = target - original;
		if (path.x == 0) {
			if (path.y > 0) {
				transform.eulerAngles = new Vector3 (0, 0, -90);
			} else if (path.y < 0) {
				transform.eulerAngles = new Vector3 (0, 0, 90);
			} else if (path.y == 0) {
				transform.eulerAngles = temp;
			}
		} else {
			if (path.x > 0) {
				transform.eulerAngles = new Vector3 (0, 0, (180 * Mathf.Atan (path.y / path.x) / Mathf.PI) + 180);
			} else if (path.x < 0) {
				transform.eulerAngles = new Vector3 (0, 0, (180 * Mathf.Atan (path.y / path.x) / Mathf.PI));
			}
		}
		if (windArrow.activeSelf == true) {
			rb.velocity = -path * 10;
		} else {
			rb.velocity = -path * 4;
		}
		rb.isKinematic = false;
		bow.SetActive (false);
		shots = shots - 1;
		arrows.text = "Arrows: " + (shots + 5);
	}

	IEnumerator wallHit(){
		yield return new WaitForSeconds (5);
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
		transform.position = original;
		if (shots > -5) {
			arrow.SetActive (true);
			rb.isKinematic = true;
			bow.SetActive (true);
		}
		drag = false;
	}
	IEnumerator targetHit(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
		yield return new WaitForSeconds (1);
		if (shots > -5) {
			transform.position = original;
			arrow.SetActive (true);
			rb.isKinematic = true;
			bow.SetActive (true);
		}
		drag = false;
		if (LevelMode.text == "Level1") {
			if (score > PlayerPrefs.GetInt ("HighScore1")) {
				PlayerPrefs.SetInt ("HighScore1", score);
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore1");
			}
		}
		else if (LevelMode.text == "Level2") {
			if (score > PlayerPrefs.GetInt ("HighScore2")) {
				PlayerPrefs.SetInt ("HighScore2", score);
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore2");
			}
		}
		else if (LevelMode.text == "Level3") {
			if (score > PlayerPrefs.GetInt ("HighScore3")) {
				PlayerPrefs.SetInt ("HighScore3", score);
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore3");
			}
		}
		else if (LevelMode.text == "Level4") {
			if (score > PlayerPrefs.GetInt ("HighScore4")) {
				PlayerPrefs.SetInt ("HighScore4", score);
				highscore.text = "High Score: " + PlayerPrefs.GetInt ("HighScore4");
			}
		}
	}
	IEnumerator obstacleHit(){
		yield return new WaitForSeconds (1);
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
		if (shots > -5) {
			transform.position = original;
			arrow.SetActive (true);
			rb.isKinematic = true;
			bow.SetActive (true);
		}
		drag = false;
	}
	public void flame(){
		arrow.SetActive (false);
		flameArrow.SetActive (true);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
	}
	public void ice(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (true);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
	}
	public void lightning(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (true);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
	}
	public void mud(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (true);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
	}
	public void vine(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (true);
		waterArrow.SetActive (false);
		windArrow.SetActive (false);
	}
	public void water(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (true);
		windArrow.SetActive (false);
	}
	public void wind(){
		arrow.SetActive (false);
		flameArrow.SetActive (false);
		iceArrow.SetActive (false);
		lightningArrow.SetActive (false);
		mudArrow.SetActive (false);
		vineArrow.SetActive (false);
		waterArrow.SetActive (false);
		windArrow.SetActive (true);
	}
	public bool isMud(){
		if (mudArrow.activeSelf == true) {
			return true;
		} else {
			return false;
		}
	}
}
