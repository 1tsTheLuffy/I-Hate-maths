using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss : MonoBehaviour
{

    private int randomMovePoint;
    [SerializeField] int eventType;

    [SerializeField] float movementSpeed; 

    [Header("Timers")]
    [SerializeField] float startTime;
    [SerializeField] float timeToMoveToNextState;
    [SerializeField] float waitTime;
    [SerializeField] float goTime;

    [SerializeField] Vector2 target;

    [SerializeField] Transform[] movePoints;
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
        waitTime = goTime;

        eventType = Random.Range(1, 4);
        randomMovePoint = Random.Range(0, movePoints.Length);
    }

    private void Update()
    {
        if(eventType == 1)
        {
            animator.SetBool("isGun", true);
            animator.SetBool("isThorn", false);

            transform.Rotate(0f, 0f, 7.5f);

           // timeToMoveToNextState = Random.Range(6f, 10f);

           // randomMovePoint = Random.Range(0, movePoints.Length);
            transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);


            if(Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
            {
                randomMovePoint = Random.Range(0, movePoints.Length);
                waitTime = goTime;
                
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
                startTime = timeToMoveToNextState;
            }else
            {
                startTime -= Time.deltaTime;
            }
        }

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            eventType = 1;
            randomMovePoint = Random.Range(0, movePoints.Length);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            eventType = 1;
            return;
        }

        if(collision.CompareTag("Bullet1"))
        {
            if(eventType == 3)
            {
                Destroy(collision.transform.gameObject);
                Debug.Log("Damage!!");
            }
        }
    }
}
