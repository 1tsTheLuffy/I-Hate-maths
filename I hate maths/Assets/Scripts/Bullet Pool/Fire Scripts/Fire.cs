﻿
/* A Fire is responsible for shooting bullets in multiple directioon depending upon the angle 
 The gameobject does not required any type of script for shooring projectiles if this script is attached
 Every Gameobject requires different types of fire Scipt for now.......*/ 




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    [Header("Number of bullets")]
    [SerializeField] int size;

    [Header("Direction")]
    [SerializeField] float angle;
    [SerializeField] float startAngle;
    [SerializeField] float endAngle;
    [SerializeField] float angleStep;

    [Header("Time")]
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [Header("Transform")]
    [SerializeField] Transform shootPoint;

    ObjectPool pool;

    private void Start()
    {
        timer = timeBtwSpawn;
        pool = GetComponent<ObjectPool>();
      //  InvokeRepeating("Shoot", 0, 1.8f);
    }

    private void Update()
    {
        if(timer <= 0)
        {
            Shoot();
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }
    }
    public void Shoot()
    {

        angleStep = ((endAngle - startAngle) / size) * .5f;
        angle = startAngle;

        for (int i = 0; i < size; i++)
        {
            float x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 dir = new Vector3(x, y, 0);
            Vector2 bulletDir = (dir - transform.position).normalized;

            GameObject bul = pool.GetFromPool();
            bul.transform.position = shootPoint.position;
            bul.transform.rotation = shootPoint.rotation;
            bul.SetActive(true);
            bul.GetComponent<CbBullet1>().SetMoveDirection(bulletDir);

            angle += angleStep;
        }
    }


}
