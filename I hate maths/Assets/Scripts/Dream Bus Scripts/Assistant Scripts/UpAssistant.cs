using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class UpAssistant : MonoBehaviour
{
    private int health;
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    public static GameObject instance;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletIntantiate;
    [SerializeField] GameObject destroyParticle;

    [SerializeField] Transform shootPoint;

    private Transform bus;

    [SerializeField] Color[] hitColor;

    CameraShake shake;
    Rigidbody2D rb;
    SpriteRenderer sr;

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
        sr = GetComponent<SpriteRenderer>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        sr.color = Color.green;

        timer = timeBtwSpawn;
        health = 10;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            shake.C_Shake(.08f, .8f, .5f);
            GameObject instance = Instantiate(bulletIntantiate, shootPoint.position, shootPoint.rotation * new Quaternion(0f, 90f, 0f, 0f));
            Instantiate(bullet, shootPoint.position, Quaternion.identity);
            timer = timeBtwSpawn;
            Destroy(instance, 1.2f);
        }else
        {
            timer -= Time.deltaTime;
        }

        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, bus.position + new Vector3(x, y, 0), movementSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet") || collision.CompareTag("EnemyPoolBullet") || collision.CompareTag("Enemy"))
        {
            Destroy(collision.transform.gameObject);
            StartCoroutine(Hit());
            shake.C_Shake(.08f, .5f, .5f);
            health -= 1;
        }
    }


    IEnumerator Hit()
    {
        if (health > 6 && health < 10)
        {
            sr.color = hitColor[0];
        }
        else if (health > 3 && health < 6)
        {
            sr.color = hitColor[1];
        }
        else if (health < 3)
        {
            sr.color = hitColor[2];
        }

        yield return new WaitForSeconds(.1f);

        sr.color = Color.green;
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1.2f);
    }
}
