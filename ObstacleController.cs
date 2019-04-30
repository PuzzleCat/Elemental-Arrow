using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ObstacleController : MonoBehaviour {

	public GameObject wood;
	public GameObject fire;
	public GameObject ice;
	public GameObject lightning;
	public GameObject vine;

	private Rigidbody2D rb;
	private bool grow;
	private float health;

	// Use this for initialization
	void Start () {
		grow = false;
		rb = GetComponent<Rigidbody2D> ();
		health = 50.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0.0f) {
			gameObject.SetActive (false);
		}
	}

	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.CompareTag ("Obstacle")) {
			if (grow == true) {
				other.gameObject.GetComponent<ObstacleController> ().connect ();
				grow = false;
			}
		} else if (other.gameObject.CompareTag ("Arrow")) {
			if(other.gameObject.GetComponent<ArrowController>().isMud() == false){
				health = health - other.gameObject.GetComponent<Rigidbody2D> ().velocity.magnitude;
			}
			if(other.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= 0.1f){
				rb.isKinematic = false;
			}
		}
	}

	public void burn(){
		wood.SetActive (false);
		fire.SetActive (true);
		StartCoroutine (incinerate ());
	}
	public void freeze(){
		wood.SetActive (false);
		ice.SetActive (true);
		health = health / 3;
	}
	public void explode(){
		wood.SetActive (false);
		lightning.SetActive (true);
		StartCoroutine (explosion ());
	}
	public void overgrow(){
		wood.SetActive (false);
		vine.SetActive (true);
		grow = true;
	}
	public void connect(){
		wood.SetActive (false);
		vine.SetActive (true);
	}
	IEnumerator incinerate(){
		yield return new WaitForSeconds (2);
		gameObject.SetActive (false);
	}
	IEnumerator explosion(){
		yield return new WaitForSeconds (1);
		rb.AddForce (new Vector2 (Random.Range (-200, 201), Random.Range (-200, 201)), ForceMode2D.Impulse);
	}
}
