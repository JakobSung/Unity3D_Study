using UnityEngine;
using System.Collections;

public class BarrelCtrl : MonoBehaviour {

	public GameObject effect;
	
	private Transform tran;
	// Use this for initialization
	void Start () {
		tran = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision coll){
		if(coll.collider.tag =="BULLET"){
			
			Destroy(coll.gameObject);
			
			ExpBarrel();
			
			
			
		}
	}
	
	private void ExpBarrel(){
		
		Instantiate(effect, tran.position, Quaternion.identity);
		
		Collider[] colls = Physics.OverlapSphere(tran.position, 10.0f);
		
		foreach(Collider coll in colls){
			Rigidbody rigid = coll.GetComponent<Rigidbody>();
			if(rigid != null){
				rigid.AddExplosionForce(1000.0f, tran.position,10.0f, 300.0f);
			}
			
			
			Destroy(gameObject, 5.0f);
		} 
		
		
	}
}
