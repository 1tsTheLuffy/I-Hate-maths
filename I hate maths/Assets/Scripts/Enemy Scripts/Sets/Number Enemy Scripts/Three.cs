using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Three : MonoBehaviour
{
    private int health;
    [SerializeField] float speed;
    [SerializeField] float waitTime;

    private GameObject tempObj;
    private GameObject damageParticleInstance;
    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject damageParticle;

    private Transform bus;

    private DreamBusController controller;

    PlayerScoreManager sm;

    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        controller = GameObject.FindGameObjectWithTag("DreamBus").GetComponent<DreamBusController>();
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();

        health = 1;

        destroyParticle.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
    }

    private void Update()
    {
        if (controller == null)
            return;

        destroyParticle.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        Vector2 direction = new Vector2(bus.position.x - transform.position.x, bus.position.y - transform.position.y);
        transform.right = -direction;

        transform.position = Vector2.MoveTowards(transform.position, bus.position, speed * Time.deltaTime);

        if(health <= 0)
        {
            //Destroy(gameObject);
            rb.gravityScale = 10f;
            StartCoroutine(spawn());
        }

        Destroy(tempObj, 4f);
        Destroy(damageParticleInstance, 1.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            health = 0;
            tempObj = Instantiate(destroyParticle, transform.position, new Quaternion(-90f, 0f, 0f, 0f));
            controller.health -= 1;
            shake.C_Shake(.1f, 1f, 1f);
        }
        if (collision.CompareTag("TriangleBullet") || collision.CompareTag("ShieldBullet"))
        {
            shake.C_Shake(.1f, 2.5f, 1f);
            sm.score++;
            health = 0;
        }
        if (collision.CompareTag("Bullet1"))
        {
            damageParticleInstance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
            health = 0;
            Destroy(collision.transform.gameObject);
            sm.score++;
            shake.C_Shake(.1f, 1f, 1f);
        }
        if(collision.CompareTag("Electric"))
        {
            Destroy(gameObject);
            shake.C_Shake(.1f, 1f, 1f);
        }
    }
    
    IEnumerator spawn()
    {
        yield return new WaitForSeconds(waitTime);
        tempObj = Instantiate(destroyParticle, transform.position,new Quaternion(-90f,0f,0f,0f));
        Destroy(gameObject);

    }
}
