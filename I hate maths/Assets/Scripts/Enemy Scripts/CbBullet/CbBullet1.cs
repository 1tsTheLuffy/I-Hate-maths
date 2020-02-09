using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CbBullet1 : MonoBehaviour
{
    [SerializeField] float fireSpeed;

    [SerializeField] GameObject destroyParticle;

    [SerializeField] Vector2 MoveDirection;


    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Invoke("Disable",3f);
    }

    private void Update()
    {
        //Destroy(gameObject, 2f);
    }

    private void FixedUpdate()
    {
        transform.Translate(MoveDirection * fireSpeed * Time.fixedDeltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        MoveDirection = dir;
    }

    private void OnDestroy()
    {
        GameObject instance = Instantiate(destroyParticle, transform.position, Quaternion.identity);
        Destroy(instance, 1f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);   
    }

}
