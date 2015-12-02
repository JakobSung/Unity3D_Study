using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour 
{
	public Transform targetTr;
	public float distance = 10.0f; 
	public float height = 3.0f;
	public float damptrace = 20.0f;

	private Transform tr;

	// Use this for initialization
	void Start () 
	{
		tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	//업데이트함수 이후 호출하기 위해	
	void LateUpdate()
	{ 
	 	tr.position = Vector3.Lerp(tr.position, 
								targetTr.position 
								- (targetTr.forward * distance)
								+ (Vector3.up * height),
								Time.deltaTime * damptrace );
	
	
		tr.LookAt(targetTr.position + Vector3.up * 2);
	}
}
