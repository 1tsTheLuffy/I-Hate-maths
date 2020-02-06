using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo : MonoBehaviour
{
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject echoObj;

    private void Start()
    {
        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            GameObject instance = Instantiate(echoObj, transform.position, transform.rotation);
            timer = timeBtwSpawn;
            Destroy(instance, 2f);
        }else
        {
            timer -= Time.deltaTime;
        }
    }
}
