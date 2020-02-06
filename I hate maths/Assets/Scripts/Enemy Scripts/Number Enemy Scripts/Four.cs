using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Four : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    [SerializeField] GameObject Bomb;

    [SerializeField] Transform spawnPoint;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if (timer <= 0)
        {
            Instantiate(Bomb, spawnPoint.position, Quaternion.identity);
            timer = timeBtwSpawn;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if(transform.position.x < -22f)
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.left * speed * Time.fixedDeltaTime);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
        Debug.Log("OUT!!");
    }
}
