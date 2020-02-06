using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlyBracket2 : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(0f,0f,8f);
    }
}
