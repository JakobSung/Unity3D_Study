using UnityEngine;
using System.Collections;

public class WallCtrl : MonoBehaviour {


	public GameObject sparkEffet;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision coll){
		if(coll.collider.tag == "BULLET")
		{
			//불꽃 튀기는 효과 파티클 추가
			GameObject spark = (GameObject)Instantiate(sparkEffet, coll.transform.position, Quaternion.identity);	
			
			Destroy(spark, spark.GetComponent<ParticleSystem>().duration + 0.1f);
			
			
			Destroy(coll.gameObject);
		}
		
	}
}
