using UnityEngine;
using System.Collections;

public class GrabbableObject : MonoBehaviour {

	float forceRequired = 0.5f;

	Transform graphic;

	float _mouseForce = 0;
	bool _grabbed = false;
	Vector3 _touchPos;
	// Use this for initialization
	void Start () {
		graphic = transform.GetChild(0);

		_mouseForce = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!_grabbed && Input.GetMouseButtonDown(0))
		{
			RaycastHit rh;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rh, Mathf.Infinity, 1 << LayerMask.NameToLayer("grabbables")))
			{
				if (rh.collider == collider)
				{
					_grabbed = true;
					_touchPos = rh.point;
					World.instance.mousePlane.position = rh.point;
				}
			}
		} else if (_grabbed && !Input.GetMouseButton(0))
		{
			_grabbed = false;
			Drop();
		}

		if (_grabbed)
		{
			RaycastHit rh;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out rh, Mathf.Infinity, 1 << LayerMask.NameToLayer("mousePlane")))
			{
				Debug.Log("grabbing");
				_mouseForce = Vector3.Distance(_touchPos, rh.point);
				graphic.localEulerAngles = new Vector3(0,0,-Vector3.Angle(transform.position, rh.point));
			}
			if(_mouseForce > 1)
			{
				//Drop();
			}
		}
		
	}

	void Drop() {
		Debug.Log("Drop");
		graphic.localPosition = Vector3.zero;
		graphic.localEulerAngles = Vector3.zero;
		_grabbed = false;
	}
}
