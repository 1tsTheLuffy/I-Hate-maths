using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VD1 : MonoBehaviour
{
    [SerializeField] private Vector2 target;
    [SerializeField] private int eventType;
    [SerializeField] private int randomRotatePoint;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotateAmount;
    [SerializeField] float distance;
    [Range(0f, 5f)]
    [SerializeField] float minDistance;

    [SerializeField] float timeToMove;
    [SerializeField] float timeToMoveToDifferentPos;

    [SerializeField] Transform[] RotatePoint;
    private Transform bus;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;


        randomRotatePoint = Random.Range(0, RotatePoint.Length);
        eventType = 1;

        timeToMove = timeToMoveToDifferentPos;
        target = new Vector2(bus.position.x, bus.position.y);
    }

    private void LateUpdate()
    {
        
    }
    private void Update()
    {
        distance = Vector2.Distance(transform.position, RotatePoint[randomRotatePoint].position);

        //transform.RotateAround(RotatePoint[randomRotatePoint].position, Vector3.forward, -rotateAmount * Time.fixedDeltaTime);

        if(eventType == 1)
        {
            if (distance > minDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, RotatePoint[randomRotatePoint].position,
                movementSpeed * Time.deltaTime);
            }
            else if (distance < minDistance)
            {
                transform.RotateAround(RotatePoint[randomRotatePoint].position, Vector3.forward, -rotateAmount * Time.deltaTime);
                if (timeToMove <= 0)
                {
                    randomRotatePoint = Random.Range(0, RotatePoint.Length);
                    eventType = Random.Range(1, 3);
                    if (eventType == 2)
                    {
                        target = bus.position;
                    }
                    timeToMove = timeToMoveToDifferentPos;
                }
                else
                {
                    timeToMove -= Time.deltaTime;
                }
            }
        }else if(eventType == 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, movementSpeed * Time.deltaTime);
        }

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            eventType = 1;
            randomRotatePoint = Random.Range(0, RotatePoint.Length);
            return;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            eventType = 1;
            randomRotatePoint = Random.Range(0, RotatePoint.Length);
            return;
        }
    }
}