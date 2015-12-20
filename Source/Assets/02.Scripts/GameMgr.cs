using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class GameMgr : MonoBehaviour
{

    public Transform[] points;

    public GameObject monsterPrefab;

    public float createTime = 2.0f;

    public int maxMonster = 10;

    public bool isGameOver = false;

    public static GameMgr Instance = null;

    void Awake()
    {
        Instance = this;
    }


    public List<GameObject> monsterPool = new List<GameObject>();



    // Use this for initialization
    void Start()
    {

        for (int i = 0; i < maxMonster; i++)
        {
            GameObject monster = Instantiate(monsterPrefab);
            monster.SetActive(false);
            monsterPool.Add(monster);
        }



        points = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        if (points.Length > 0)
        {
            StartCoroutine(this.CreateMonster());
        }



    }

    private IEnumerator CreateMonster()
    {
        while (!isGameOver)
        {
            yield return new WaitForSeconds(createTime);

            if (isGameOver) yield break;

            foreach (GameObject monster in monsterPool)
            {
                if (!monster.activeSelf)
                {
                    int idx = UnityEngine.Random.Range(1, points.Length);

                    monster.transform.position = points[idx].position;

                    monster.SetActive(true);
                    break;
                }
            }
        }
    }




    // Update is called once per frame
    void Update()
    {

    }
}
