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

    [SerializeField] VanFire vanFire;
    [SerializeField] ObjectPooler pooler;

    Rigidbody2D rb;
    CameraShake shake;
    SpriteRenderer sprite;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        vanFire = GetComponent<VanFire>();
        pooler = GetComponent<ObjectPooler>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        randomRotatePoint = Random.Range(0, RotatePoint.Length);
        eventType = 1;

        timeToMove = timeToMoveToDifferentPos;
        target = new Vector2(bus.position.x, bus.position.y);

        health = 10f;
        pooler.size = 20;
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
            vanFire.startAngle = 0;
            vanFire.endAngle = -360;
            chaseSpeed = 55f;
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
            shake.C_Shake(.1f, 3f, .7f);
            return;
        }

        if(collision.CompareTag("Bullet1") || collision.CompareTag("TriangleBullet") || collision.CompareTag("ShieldBullet"))
        {
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 2.5f, 1f);
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