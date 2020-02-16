using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour
{
    #region boolean
    private bool isStarted;
    private bool isTwoStarted;
    private bool isBombBracketI;
    private bool isCB2Started;
    private bool isHalfStarted;
    private bool isFourStarted;
    private bool isVanStarted;
    #endregion
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
        // One..
        if(!isStarted)
        {
            StartCoroutine(One());
            StartCoroutine(Seven());
        }
        if(Input.GetKeyDown(KeyCode.F))
        {
            //isStarted = false;
        }

        // Two..
        if(sm.score > 15)
        {
            if(isTwoStarted == false)
            {
                StartCoroutine(Two());
            }
        }
        
        // Bomb Btacket..
        if(sm.score > 25)
        {
            if(isBombBracketI == false)
            {
                StartCoroutine(BombBracket());
            }
        }

        // CB2..
        if(sm.score > 15)
        {
            if(isCB2Started == false)
            {
                StartCoroutine(CB2());
            }
        }

        // Half Curl..
        if(sm.score > 10)
        {
            if(isHalfStarted == false)
            {
                StartCoroutine(HalfCurl());
            }
        }

        // Four..
        if(sm.score > 35)
        {
            if(isFourStarted == false)
            {
                StartCoroutine(Four());
            }
        }

        // VD..
        if(sm.score > 50)
        {
            if(isVanStarted == false)
            {
                StartCoroutine(VD());
            }
        }

        // SCORE LOGIC........ (Enemy movements will depend on the score of the player)....

        // For one..

        if(sm.score > 20)
        {
            getEnemyData[0].x = 2f;
            getEnemyData[0].y = 5f;
        }

        // .......SCORE LOGIC
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
        isTwoStarted = true;
        while(isTwoStarted == true)
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

    IEnumerator HalfCurl()
    {
        isHalfStarted = true;
        while(isHalfStarted == true)
        {
            float delay = Random.Range(getEnemyData[4].x, getEnemyData[4].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[4].EnemyPosition.Length);
            int j = Random.Range(0, getEnemyData[4].Enemy.Length);
            Instantiate(getEnemyData[4].Enemy[j], getEnemyData[4].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator Four()
    {
        isFourStarted = true;
        while(isFourStarted == true)
        {
            float delay = Random.Range(getEnemyData[5].x, getEnemyData[5].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[5].EnemyPosition.Length);
            Instantiate(getEnemyData[5].Enemy[0], getEnemyData[5].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator Seven()
    {
        isStarted = true;
        while(isStarted == true)
        {
            float delay = Random.Range(getEnemyData[6].x, getEnemyData[6].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[6].EnemyPosition.Length);
            Instantiate(getEnemyData[6].Enemy[0], getEnemyData[6].EnemyPosition[i].position, Quaternion.identity);
        }
    }

    IEnumerator VD()
    {
        isVanStarted = true;
        while(isVanStarted == true)
        {
            float delay = Random.Range(getEnemyData[7].x, getEnemyData[7].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, getEnemyData[7].EnemyPosition.Length);
            Instantiate(getEnemyData[7].Enemy[0], getEnemyData[7].EnemyPosition[i].position, Quaternion.identity);
        }
    }
}
