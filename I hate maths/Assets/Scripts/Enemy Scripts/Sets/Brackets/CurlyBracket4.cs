﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket4 : MonoBehaviour
{
    private int health = 1;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject[] Bullet;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform[] shootPoint;

    PlayerScoreManager sm;
 
    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();
    }

    private void Update()
    {
        if(timer <= 0)
        {
            Instantiate(Bullet[0], shootPoint[0].position, shootPoint[0].rotation);
            Instantiate(Bullet[1], shootPoint[1].position, shootPoint[1].rotation);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            shake.C_Shake(.1f, 1f, 1f);
            Destroy(collision.transform.gameObject);
            sm.score++;
            health = 0;
        }
        if (collision.CompareTag("TriangleBullet") || collision.CompareTag("ShieldBullet"))
        {
            shake.C_Shake(.1f, 2.5f, 1f);
            sm.score++;
            health = 0;
        }
        if (collision.CompareTag("Electric"))
        {
            Destroy(gameObject);
            shake.C_Shake(.1f, 2f, 1f);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0f, 0f, 25f);
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1.2f);
    }
}
