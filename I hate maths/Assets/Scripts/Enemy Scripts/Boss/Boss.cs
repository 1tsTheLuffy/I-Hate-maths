using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{
    public int health;

    private int randomMovePoint;
    [SerializeField] int eventType;

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

    Rigidbody2D rb;
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;

        target = new Vector2(bus.position.x, bus.position.y);

        startTime = timeToMoveToNextState;
        timer = timeToShoot;

        health = 50;

        eventType = Random.Range(1, 4);
        randomMovePoint = Random.Range(0, movePoints.Length);
    }

    private void Update()
    {
        if(eventType == 1)
        {
            animator.SetBool("isGun", true);
            animator.SetBool("isThorn", false);

            transform.Rotate(0f, 0f, 10f);

            transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);

            if(timer <= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    Instantiate(Bullet[i], shootPoint[i].position, shootPoint[i].rotation);
                }
                //Instantiate(Bullet[0], shootPoint[0].position, shootPoint[0].rotation);
                //Instantiate(Bullet[1], shootPoint[1].position, shootPoint[1].rotation);
                //Instantiate(Bullet[2], shootPoint[2].position, shootPoint[2].rotation);

                timer = timeToShoot;
            }else
            {
                timer -= Time.deltaTime;
            }

            if(Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
            {
                randomMovePoint = Random.Range(0, movePoints.Length);               
            }

            if(startTime <= 0)
            {
                eventType = Random.Range(1, 4);
                if(eventType == 2)
                {
                    target = new Vector2(bus.position.x, bus.position.y);
                }
                startTime = timeToMoveToNextState;
            }else
            {
                startTime -= Time.deltaTime;
            }
        }else if(eventType == 2)
        {
            animator.SetBool("isThorn", true);
            animator.SetBool("isGun", false);
            transform.Rotate(0f, 0f, 10f);
            
            transform.position = Vector2.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        }else if(eventType == 3)
        {
            animator.SetBool("isThorn", false);
            animator.SetBool("isGun", false);

            if(startTime <= 0)
            {
                eventType = Random.Range(1, 4);
                if(eventType == 3)
                {
                    eventType = 2;
                }
                startTime = timeToMoveToNextState;
            }else
            {
                startTime -= Time.deltaTime;
            }
        }

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            eventType = 2;
            target = new Vector2(bus.position.x, bus.position.y);
            //randomMovePoint = Random.Range(0, movePoints.Length);
            return;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
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
                GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
                Destroy(instance, 1.2f);
            }
            if(eventType == 2)
            {
                Destroy(collision.transform.gameObject);
                eventType = 1;
            }
        }
    }
}
