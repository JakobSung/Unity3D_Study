using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]
public class Anim
{
	public AnimationClip idel;
	public AnimationClip forward;
	public AnimationClip back;
	public AnimationClip left;
	public AnimationClip right;
	
}

public class PlayerControl : MonoBehaviour 
{
	private float h = 0.0f;
	private float v = 0.0f;
	
	private Transform tr;
	public float moveSpeed = 10.0f;
	public float rotSpeed = 100.0f;
	
	public Anim anim;
	
	public Animation _animation;
    
   
	
	// Use this for initialization
	void Start () 
	{
		this.tr = GetComponent<Transform>();	
		
		this._animation = GetComponentInChildren<Animation>();
		
		this._animation.clip = anim.idel;
		
		_animation.Play();
	}
	
	// Update is called once per frame
	void Update () 
	{
		h = Input.GetAxis("Horizontal");
		v = Input.GetAxis("Vertical");
		
		//Debug.Log("Horizontal " + h.ToString());
		//Debug.Log("Vertical " + v.ToString());
	
		Vector3 moveVector = (Vector3.forward * v) + (Vector3.right * h);
		
		tr.Translate(moveVector.normalized * Time.deltaTime * moveSpeed, Space.Self);
		
		tr.Rotate(Vector3.up *  Time.deltaTime * rotSpeed * Input.GetAxis("Mouse X"));
		
		
		if(v >= 0.1f){
			_animation.CrossFade(anim.forward.name, 0.3f);
		}
		else if(v <= -0.1f){
			_animation.CrossFade(anim.back.name, 0.3f);
		}
		else if(h >= 0.1f){
			_animation.CrossFade(anim.right.name, 0.3f);
		}
		else if(h <= -0.1f){
			_animation.CrossFade(anim.left.name, 0.3f);
		}
		else{
			_animation.CrossFade(anim.idel.name, 0.3f);
		}
	}
	
	public int hp = 100;
    
    public Image imgHpbar;
	
	void OnTriggerEnter(Collider coll)
	{
		if(coll.gameObject.tag == "PUNCH")
		{
			Debug.Log("Player Hit from PUNCH");
			hp -= 10;
            
            imgHpbar.fillAmount = (float)hp / 100;
		}
		
		if(hp <= 0)
		{
			PlayerDie();
		}
	}
	
	public delegate void PlayerDieHandler();
	public static event PlayerDieHandler OnPlayerDieEvent;
	
	void PlayerDie()
	{
		Debug.Log("Player Die");
		
		//GameObject[] monsters = GameObject.FindGameObjectsWithTag("MONSTER");
		
		//foreach(GameObject mon in monsters)
		//{
		//	mon.SendMessage("OnPlayerDie", SendMessageOptions.DontRequireReceiver);
		//}
		
		if	(OnPlayerDieEvent != null)
		{
			OnPlayerDieEvent.Invoke();
		}
		
		
		GameMgr.Instance.isGameOver = true;
	}
	
	
}
