using UnityEngine;
using System.Collections;

public class World : MonoBehaviour {

	public float size = 20;
	public Transform mousePlane;

	public static World instance;

	void Awake(){
		instance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
