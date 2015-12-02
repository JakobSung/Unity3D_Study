using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {

	public AudioClip fireSfx;
	
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
	}
	
	public GameObject bullet;
	
	public Transform firePos;
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Fire();
		}
	
	}
	
	void Fire(){
		
		
		CreateBullet();
		source.PlayOneShot(fireSfx, 0.9f);
	}
	
	void CreateBullet(){
		Instantiate(bullet, firePos.position, firePos.rotation);
		
	}
	
	
}
