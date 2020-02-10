﻿
/* A Fire is responsible for shooting bullets in multiple directioon depending upon the angle 
 The gameobject does not required any type of script for shooring projectiles if this script is attached
 Every Gameobject requires different types of fire Scipt for now.......*/ 




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [SerializeField] float angle;
    [SerializeField] float startAngle;
    [SerializeField] float endAngle;
    [SerializeField] float angleStep;

    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] Transform shootPoint;

    ObjectPooler pooler;

    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();

        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            for (int i = 0; i < pooler.size; i++)
            {
                Shoot();
            }

            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }
    }

    // Update is called once per frame
    public void Shoot()
    {

        angleStep = ((endAngle - startAngle) / pooler.size) * .5f;
        angle = startAngle;

        for (int i = 0; i < pooler.size; i++)
        {
            float x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 dir = new Vector3(x, y, 0);
            Vector2 bulletDir = (dir - transform.position).normalized;
            GameObject obj = ObjectPooler.instance.GetFromPool(shootPoint.position, shootPoint.rotation);
            
            obj.GetComponent<CbBullet1>().SetMoveDirection(bulletDir);
            angle += angleStep;
        }
    }


}