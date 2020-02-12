using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BottomAssistant : MonoBehaviour
{
    [SerializeField] float x;
    [SerializeField] float y;
    [SerializeField] float movementSpeed;

    public static GameObject instance;

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
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, bus.position + new Vector3(x, y, 0), movementSpeed * Time.deltaTime);
    }
}
