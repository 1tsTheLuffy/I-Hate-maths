using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BbBomb : MonoBehaviour
{
    [SerializeField] float force;

    [SerializeField] Vector2 up;
    [SerializeField] Vector2 side;

    [SerializeField] GameObject[] destroyParticle;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        applyForce();
    }

    private void Update()
    {
        Destroy(gameObject, 10f);
    }

    private void applyForce()
    {
        Vector2 addForce = new Vector2(side.x * force * Time.fixedDeltaTime, up.y * force * Time.fixedDeltaTime);
        rb.AddForce(addForce);
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle[0], transform.position, Quaternion.identity);
        GameObject instance_2 = Instantiate(destroyParticle[1], transform.position, Quaternion.identity);
        Destroy(instance, 1f);
        Destroy(instance_2, 1f);
    }
}
