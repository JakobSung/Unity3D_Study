using UnityEngine;
using System.Collections;
using System;

public class MonsterCtrl : MonoBehaviour {

	private Transform playerTr;
	private Transform monsterTr;
	private NavMeshAgent navMeshAgent;
	public enum MonStatus { idle, trace, attack, die };
	
	public MonStatus MonsterStatus = MonStatus.idle;
	
	public float traceDist = 10.0f;
	public float attackDist = 2.0f;
	
	private bool isDie = false;
	
	Animator animator = null;
	
	private GameUI _gameUI;
	
	// Use this for initialization
	void Start () 
	{
		monsterTr = this.gameObject.GetComponent<Transform>();
		playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
		navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
		animator = this.GetComponent<Animator>();
		
		StartCoroutine(this.CheckDistance());
		StartCoroutine(this.MonsterAction());
			
		_gameUI = GameObject.Find("GameUI").GetComponent<GameUI>();
	}
	
	private IEnumerator CheckDistance()
	{
		while(!isDie)
		{
			yield return new WaitForSeconds(0.2f);
			
			float dist = Vector3.Distance(playerTr.position, monsterTr.position);
			
			
		 	if	(dist <= attackDist)
			{
				MonsterStatus = MonStatus.attack;
			}
			else if(dist <= traceDist)
			{
				MonsterStatus = MonStatus.trace;
			}
			else
			{
				MonsterStatus = MonStatus.idle; 
			}
		}
	}
	
	private IEnumerator MonsterAction()
	{
		while(!isDie){
			switch(MonsterStatus){
				case(MonStatus.idle):{
					navMeshAgent.Stop();
					
					animator.SetBool("IsTrace", false);
					animator.SetBool("IsAttack", false);
					
					break;
				}
				case(MonStatus.attack):{
					navMeshAgent.Stop();
					
					animator.SetBool("IsAttack", true);
					
					
					break;
				}
				case(MonStatus.trace):{
					navMeshAgent.Resume();
					navMeshAgent.destination = playerTr.position;
					animator.SetBool("IsTrace", true);
					animator.SetBool("IsAttack", false);
					break;
				}			
		
			}
			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
	public int hp = 100;
	
	void OnCollisionEnter(Collision coll)
	{
		if(coll.gameObject.tag == "BULLET")
		{
			Destroy(coll.gameObject);
			
			animator.SetTrigger("IsHit");
			
			CreateBloodEffect(coll.transform.position);
			
			hp -= 10;
		}
		
		if(hp <= 0)
		{
			Die();
		}
	}
	
	void Die()
	{
		isDie = true;
		StopAllCoroutines();
		MonsterStatus = MonStatus.die;
		navMeshAgent.Stop();
	
		animator.SetTrigger("IsDie");
		
		gameObject.GetComponentInChildren<CapsuleCollider>().enabled = false;
		
		foreach(Collider coll in gameObject.GetComponentsInChildren<SphereCollider>())
		{
			coll.enabled = false;
		}
		
		_gameUI.DispScore(50);
	}

    private void CreateBloodEffect(Vector3 position)
    {
        GameObject blood = (GameObject)Instantiate(bloodEffect, position, Quaternion.identity);
		Destroy(blood, 2.0f);
    }

    public GameObject bloodEffect;
	public GameObject bloodDecal;
	
	void OnPlayerDie()
	{
		StopAllCoroutines();
		navMeshAgent.Stop();
		animator.SetTrigger("IsPlayerDie");
	}
	
	void OnEnable()
	{
		PlayerControl.OnPlayerDieEvent += OnPlayerDie;
	}
	
	void OnDisalbe()
	{
		PlayerControl.OnPlayerDieEvent -= OnPlayerDie;
		
	}
	
	
}
