using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour
{
    private bool isStarted;
    private bool isBombBracketI;
    private bool isCB2Started;
    PlayerScoreManager sm;

    [System.Serializable]
    public class GetEnemyData
    {
        public string name;
        public float x;
        public float y;
        public GameObject[] Enemy;
        public Transform[] EnemyPosition;
    }

    public GetEnemyData[] getEnemyData;

    private void Start()
    {
        isStarted = false;
        isBombBracketI = false;
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();
        //StartCoroutine(One());
    }

    private void Update()
    {
        if(!isStarted)
        {
            StartCoroutine(One());
            StartCoroutine(Two());
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            //isStarted = false;
        }
        if(sm.score > 25)
        {
            if(isBombBracketI == false)
            {
                StartCoroutine(BombBracket());
            }
        }

        if(sm.score > 5)
        {
            if(isCB2Started == false)
            {
                StartCoroutine(CB2());
            }
        }
    }

    IEnumerator One()
    {
        isStarted = true;
        while(isStarted == true)
        {
            float delay = Random.Range(getEnemyData[0].x, getEnemyData[0].y);
            int i = Random.Range(0, getEnemyData[0].EnemyPosition.Length);
            Instantiate(getEnemyData[0].Enemy[0], getEnemyData[0].EnemyPosition[i].position, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }

    IEnumerator Two()
    {
        isStarted = true;
        while(isStarted == true)
        {
            float delay = Random.Range(getEnemyData[1].x, getEnemyData[1].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[1].EnemyPosition.Length);
            Instantiate(getEnemyData[1].Enemy[0], getEnemyData[1].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator BombBracket()
    {
        isBombBracketI = true;
        while(isBombBracketI == true)
        {
            float delay = Random.Range(getEnemyData[2].x, getEnemyData[2].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[2].EnemyPosition.Length);
            Instantiate(getEnemyData[2].Enemy[0], getEnemyData[2].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator CB2()
    {
        isCB2Started = true;
        while(isCB2Started == true)
        {
            float delay = Random.Range(getEnemyData[3].x, getEnemyData[3].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0,getEnemyData[3].EnemyPosition.Length);
            Instantiate(getEnemyData[3].Enemy[0], getEnemyData[3].EnemyPosition[i].position, Quaternion.identity);
        }
    }
}
