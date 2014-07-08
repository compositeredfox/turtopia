using UnityEngine;
using System.Collections;

public class Turtle : MonoBehaviour {

	public float speed = 1;
	public float speedYRatio = 0.5f;


	void Awake() {

	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.right * (Input.GetKey(KeyCode.RightArrow) ? 1 : (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0)) * Time.deltaTime * speed);
		transform.Translate(Vector3.forward * (Input.GetKey(KeyCode.DownArrow) ? -1 : (Input.GetKey(KeyCode.UpArrow) ? 1 : 0)) * Time.deltaTime * speed * speedYRatio);

	}
}
