using UnityEngine;
using System.Collections;

public class MyGizmo : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public Color _color = Color.green;
	public float _radius = 0.3f;
	
	void OnDrawGizmos(){
		Gizmos.color = _color;
		Gizmos.DrawSphere(transform.position, _radius);
	}
}
