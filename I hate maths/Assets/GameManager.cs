using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerScoreManager sm;

    private EnemySpawer enemySpawer;

    private void Start()
    {
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();
        enemySpawer = GameObject.FindGameObjectWithTag("EnemySpawner").GetComponent<EnemySpawer>();
    }

    private void Update()
    {
        if(sm.score >= 100)
        {
            enemySpawer.StopAllCoroutines();
        }
    }
}
