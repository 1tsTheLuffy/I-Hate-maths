﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class One : MonoBehaviour
{
    private int health;
    private Vector2 target;
    private float distance;
    [SerializeField] float speed;

    [SerializeField] GameObject destroyParticle;

    private CameraShake shake;

    private Transform bus;

    private Rigidbody2D rb;
    [SerializeField] DreamBusController controller;
    PlayerScoreManager sm;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;

        if(controller == null)
        {
            controller = GameObject.FindGameObjectWithTag("DreamBus").GetComponent<DreamBusController>();
        }else
        {
            return;
        }
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        health = 1;
    }

    private void LateUpdate()
    {
        target = new Vector2(bus.position.x, bus.position.y);
        
       
    }

    private void Update()
    {
        if (controller == null)
            return;

        distance = Vector2.Distance(transform.position, bus.position);

        if (sm.score > 20)
        {
            speed = 4f;
        }


        if (health == 0)
        {
            Destroy(gameObject);
            Instantiate(destroyParticle, transform.position, Quaternion.identity);
        }
    }

    private void FixedUpdate()
    {
        if (distance < 24f && (bus.position.x < transform.position.x))
        {
            transform.Rotate(0f, 0f, 8f);
            transform.position = Vector2.MoveTowards(transform.position, target, (speed * speed * 1.2f) * Time.fixedDeltaTime);
        }
        else
        {
            float randomUp = Random.Range(10f,20f);
            rb.AddForce(Vector2.up * randomUp, ForceMode2D.Force);
            transform.Translate(Vector2.left * speed * 2 * Time.fixedDeltaTime);
            float randomDown = Random.Range(10f, 20f);
            rb.AddForce(Vector2.down * randomDown, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            shake.C_Shake(.1f, 2f, 1f);
            health = 0;
            sm.score++;
            Destroy(collision.transform.gameObject);
        }
        if(collision.CompareTag("TriangleBullet") || collision.CompareTag("ShieldBullet"))
        {
            shake.C_Shake(.1f, 2.5f, 1f);
            health = 0;
            sm.score++;
        }
        if(collision.CompareTag("DreamBus"))
        {
            health = 0;
            shake.C_Shake(.1f, 2f, 1f);
            controller.health--;
        }

        if(collision.CompareTag("Electric"))
        {
            Destroy(gameObject);
            shake.C_Shake(.1f, 1f , 1f);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1.2f);
    }
}
