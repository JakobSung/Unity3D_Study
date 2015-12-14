using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class FireCtrl : MonoBehaviour {

	public MeshRenderer muzzleFlash;
	public AudioClip fireSfx;
	
	private AudioSource source;
	// Use this for initialization
	void Start () {
		source = GetComponent<AudioSource>();
		muzzleFlash.enabled = false;
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
		StartCoroutine(this.ShowMuzzleFlash());
		
		CreateBullet();
		source.PlayOneShot(fireSfx, 0.9f);
	}
	
	void CreateBullet(){
		Instantiate(bullet, firePos.position, firePos.rotation);
	}
	
	
	 IEnumerator ShowMuzzleFlash()
	 {
		 float scale = Random.Range(1.0f, 2.0f);
		 Quaternion rot = Quaternion.Euler(0,0,Random.Range(0, 360));
		 
		 muzzleFlash.transform.localScale = Vector3.one * scale;
		 muzzleFlash.transform.localRotation = rot;
		 
		 muzzleFlash.enabled = true;
		 
		 yield return new WaitForSeconds(Random.Range(0.05f, 0.3f));
		 
		 muzzleFlash.enabled = false;
	 }
	
}
