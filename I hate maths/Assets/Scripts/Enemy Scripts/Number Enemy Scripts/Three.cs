﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three : MonoBehaviour
{
    private int health;
    [SerializeField] float speed;
    [SerializeField] float waitTime;

    private GameObject tempObj;
    [SerializeField] GameObject destroyParticle;

    private Transform bus;

    private DreamBusController controller; 

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        controller = GameObject.FindGameObjectWithTag("DreamBus").GetComponent<DreamBusController>();
        health = 3;
        destroyParticle.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    private void Update()
    {
        destroyParticle.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        Vector2 direction = new Vector2(bus.position.x - transform.position.x, bus.position.y - transform.position.y);
        transform.right = -direction;

        transform.position = Vector2.MoveTowards(transform.position, bus.position, speed * Time.deltaTime);

        if(health == 0)
        {
            //Destroy(gameObject);
            rb.gravityScale = 10f;
            StartCoroutine(spawn());
        }

        Destroy(tempObj, 4f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            controller.health -= 3;
            controller.shakeFrequency = .5f;
            controller.shakeAmplitude = 2f;
            controller.elapsedTime = 0.1f;
            health = 0;
        }

        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet"))
        {
            controller.shakeFrequency = .5f;
            controller.shakeAmplitude = 1f;
            controller.elapsedTime = 0.1f;
            rb.AddForce(Vector2.right * 20f * Time.deltaTime, ForceMode2D.Impulse);
            health -= 1;
            Destroy(collision.transform.gameObject);
        }
    }
    
    IEnumerator spawn()
    {
        yield return new WaitForSeconds(waitTime);
        tempObj = Instantiate(destroyParticle, transform.position,new Quaternion(-90f,0f,0f,0f));
        Destroy(gameObject);
        controller.shakeFrequency = .5f;
        controller.shakeAmplitude = 2f;
        controller.elapsedTime = 0.1f;
    }
}
