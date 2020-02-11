using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket1 : MonoBehaviour
{
    private int health = 2;
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    private GameObject damageInstance;
    [SerializeField] GameObject destroyParticle;
    [SerializeField] GameObject damageParticle;

    [SerializeField] Transform shootPoint;
    private Transform Bus;

    PlayerScoreManager sm;

    Rigidbody2D rb;
    CameraShake shake;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();
        Bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        sm = GameObject.FindGameObjectWithTag("SM").GetComponent<PlayerScoreManager>();
    }

    private void Update()
    {
        if (Bus == null)
            return;

        Vector2 direction = new Vector2(Bus.position.x - transform.position.x, Bus.position.y - transform.position.y);
        transform.right = -direction;

        if(health <= 0)
        {
            Destroy(gameObject);
            sm.score++;
        }

        Destroy(damageInstance, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bullet1"))
        {
            shake.C_Shake(.1f, 1f, 1f);
            damageInstance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
            Destroy(collision.transform.gameObject);
            health -= 1;
        }
        if (collision.CompareTag("TriangleBullet"))
        {
            shake.C_Shake(.1f, 2.5f, 1f);
            health = 0;
        }
        if (collision.CompareTag("Electric"))
        {
            shake.C_Shake(.1f, 1f, 1f);
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1.2f);
    }
}
