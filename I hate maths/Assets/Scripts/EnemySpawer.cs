using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawer : MonoBehaviour
{
    private bool isStarted;

    [System.Serializable]
    public class GetEnemyData
    {
        public float x;
        public float y;
        public GameObject[] Enemy;
        public Transform[] EnemyPosition;
    }

    public GetEnemyData[] getEnemyData;

    private void Start()
    {
        isStarted = false;
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
            int i = Random.Range(0, getEnemyData[1].EnemyPosition.Length);
            Instantiate(getEnemyData[1].Enemy[0], getEnemyData[1].EnemyPosition[i].position, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
}
