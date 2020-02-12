using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BottomAssistant : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    public static GameObject instance;
    [SerializeField] GameObject bullet;

    [SerializeField] Transform shootPoint;

    private Transform bus;

    Rigidbody2D rb;

    private void Awake()
    {
        if(instance == null)
        {
            instance = gameObject;
        }else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;

        timer = timeBtwSpawn;
    }

    private void FixedUpdate()
    {
        if(timer <= 0)
        {
            Instantiate(bullet, shootPoint.position, Quaternion.identity);
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, bus.position + new Vector3(x, y, 0), movementSpeed * Time.deltaTime);
    }
}
