using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VD1 : MonoBehaviour
{
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
}