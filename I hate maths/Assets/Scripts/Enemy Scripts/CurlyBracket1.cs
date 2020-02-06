using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket1 : MonoBehaviour
{
    private Transform Bus;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
    }

    private void Update()
    {
        Vector2 direction = new Vector2(Bus.position.x - transform.position.x, Bus.position.y - transform.position.y);
        transform.right = -direction;
    }
}
