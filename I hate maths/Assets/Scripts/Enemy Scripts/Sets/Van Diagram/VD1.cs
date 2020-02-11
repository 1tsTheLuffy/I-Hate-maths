using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VD1 : MonoBehaviour
{
    private float health;
    [SerializeField] private Vector2 target;
    [SerializeField] private int eventType;
    [SerializeField] private int randomRotatePoint;
    [SerializeField] float movementSpeed;
    [SerializeField] float chaseSpeed;
    [SerializeField] float rotateAmount;
    [SerializeField] float distance;
    [Range(0f, 5f)]
    [SerializeField] float minDistance;

    [SerializeField] float timeToMove;
    [SerializeField] float timeToMoveToDifferentPos;

    [SerializeField] Transform[] RotatePoint;
    private Transform bus;

    [SerializeField] Color[] damageColor;

    Rigidbody2D rb;
    SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;


        randomRotatePoint = Random.Range(0, RotatePoint.Length);
        eventType = 1;

        timeToMove = timeToMoveToDifferentPos;
        target = new Vector2(bus.position.x, bus.position.y);

        health = 10f;
    }

    private void Update()
    {
        if(bus == null)
        {
            return;
        }

        // AI Stuff..
        
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
            transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
        }

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            eventType = 1;
            randomRotatePoint = Random.Range(0, RotatePoint.Length);
            return;
        }

        if(health <= 5)
        {
            
        }

        // AI Stuff..

        // HEALTH..

        if(health <= 0)
        {
            Destroy(gameObject);
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

        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet") || collision.CompareTag("ShieldBullet"))
        {
            Destroy(collision.transform.gameObject);
            StartCoroutine(HitFlash());
            health -= 1;
        }
    }

    IEnumerator HitFlash()
    {
        if(health > 7 && health <= 10)
        {
            sprite.color = damageColor[0];
        }else if(health > 4 && health <= 7)
        {
            sprite.color = damageColor[1];
        }else if(health < 4)
        {
            sprite.color = damageColor[2];
        }

        yield return new WaitForSeconds(.1f);

        sprite.color = Color.white;
    }
}