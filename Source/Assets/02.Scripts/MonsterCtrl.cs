using UnityEngine;
using System.Collections;

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
	
	// Use this for initialization
	void Start () 
	{
		monsterTr = this.gameObject.GetComponent<Transform>();
		playerTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
		navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
		animator = this.GetComponent<Animator>();
		
		StartCoroutine(this.CheckDistance());
		StartCoroutine(this.MonsterAction());
		
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
}
