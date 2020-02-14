﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public float health;

    private int randomMovePoint;
    [SerializeField] int eventType;
    [SerializeReference] int secondStageEvent;
    [SerializeField] float movementSpeed; 

    [Header("Timers")]
    [SerializeField] float startTime;
    [SerializeField] float timeToMoveToNextState;
    [SerializeField] float timer;
    [SerializeField] float timeToShoot;

    [SerializeField] Vector2 target;

    [SerializeField] GameObject[] Bullet;
    [SerializeField] GameObject damageParticle;

    [SerializeField] Transform[] movePoints;
    [SerializeField] Transform[] shootPoint;
    [SerializeField] Transform bus;

    [SerializeField] Color[] hitColor;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;

        target = new Vector2(bus.position.x, bus.position.y);

        startTime = timeToMoveToNextState;
        timer = timeToShoot;

        health = 50;

        eventType = Random.Range(1, 4);
        secondStageEvent = Random.Range(1, 3);
        randomMovePoint = Random.Range(0, movePoints.Length);
    }

    private void Update()
    {
        if (health > 25)
        {
            if (eventType == 1)
            {
                animator.SetBool("isGun", true);
                animator.SetBool("isThorn", false);

                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);

                if (timer <= 0)
                {
                    shoot();
                    timer = timeToShoot;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                if (Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
                {
                    randomMovePoint = Random.Range(0, movePoints.Length);
                }

                if (startTime <= 0)
                {
                    eventType = Random.Range(1, 4);
                    if (eventType == 2)
                    {
                        target = new Vector2(bus.position.x, bus.position.y);
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }
            else if (eventType == 2)
            {
                animator.SetBool("isThorn", true);
                animator.SetBool("isGun", false);
                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
            }
            else if (eventType == 3)
            {
                animator.SetBool("isThorn", false);
                animator.SetBool("isGun", false);

                if (startTime <= 0)
                {
                    eventType = Random.Range(1, 4);
                    if (eventType == 3)
                    {
                        eventType = 2;
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }

            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                eventType = 2;
                target = new Vector2(bus.position.x, bus.position.y);
                //randomMovePoint = Random.Range(0, movePoints.Length);
                return;
            }
        }
        if (health < 25)
        {
            sr.color = Color.magenta;
            animator.SetTrigger("Second");

            if(secondStageEvent == 1)
            {
                transform.Rotate(0f, 0f, 10f);

             
                if(timer <= 0)
                {
                    shoot();
                    timer = .08f;
                }else
                {
                    timer -= Time.deltaTime;
                }

                if(startTime <= 0)
                {
                    secondStageEvent = Random.Range(1, 3);
                    startTime = timeToMoveToNextState;
                }else
                {
                    startTime -= Time.deltaTime;
                }
            }else if(secondStageEvent == 2)
            {
                secondStageEvent = 1;
                return;
            }else if(secondStageEvent == 3)
            {
                health += .05f;
                startTime = .5f;
                if(startTime <= 0)
                {
                    secondStageEvent = Random.Range(1, 4);
                    if(secondStageEvent == 3)
                    {
                        secondStageEvent = 2;
                    }
                    startTime = timeToMoveToNextState;
                }else
                {
                    startTime -= timeToMoveToNextState;
                }
            }
        } 

        if(Input.GetKeyDown(KeyCode.L))
        {
            health = 24;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            eventType = 1;
           // randomMovePoint = Random.Range(0, movePoints.Length);
            return;
        }

        if(collision.CompareTag("Bullet1"))
        {
            if(eventType == 3)
            {
                Destroy(collision.transform.gameObject);
                health -= 1; 
                GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
                Destroy(instance, 1.2f);
            }
            if(eventType == 2)
            {
                Destroy(collision.transform.gameObject);
                health -= 1;
                eventType = 1;
            }
            if (health < 25)
            {
                if (secondStageEvent == 1)
                {
                    Destroy(collision.transform.gameObject);
                    health -= 1;
                    GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
                    Destroy(instance, 1.2f);
                }
            }
        }
    }

    private void shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Bullet[i], shootPoint[i].position, shootPoint[i].rotation);
        }
        Debug.Log("Shoot Called!!");
    }
}