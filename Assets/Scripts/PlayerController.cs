using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	public static float speed = 10f;
	public Text score;

	private Rigidbody rb;
	private int count;
	private int set;
	private GameObject[] reActivate;
	void Start(){
		reActivate = GameObject.FindGameObjectsWithTag ("Pick Up");
		rb = GetComponent<Rigidbody> ();
		count = 0;
		set = 0;
		setCount ();
	}

	void FixedUpdate(){
		if (SystemInfo.deviceType == DeviceType.Desktop) {
			float moveHorizontal = Input.GetAxis ("Horizontal");
			float moveVertical = Input.GetAxis ("Vertical");
			Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
			rb.AddForce (movement * speed);
		} else {
			Vector3 movement = new Vector3 (Input.acceleration.x, 0.0f, Input.acceleration.y);
			rb.AddForce(movement * speed);
		}
	}

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Pick Up")){
			other.gameObject.SetActive(false);
			count++;
			setCount();
		}
	}

	void setCount(){
		score.text = "Score: " + set.ToString () + " | " + count.ToString ();
		if (count == 9) {
			count = 0;
			set++;
			StartCoroutine(cubeDelay());

		}
		score.text = "Score: " + set.ToString () + " | " + count.ToString ();
	}

	IEnumerator cubeDelay(){
		yield return new WaitForSeconds (3f);
		foreach (GameObject item in reActivate) {
			Debug.Log ("game object");
			item.SetActive (true);
		}
	}

}
