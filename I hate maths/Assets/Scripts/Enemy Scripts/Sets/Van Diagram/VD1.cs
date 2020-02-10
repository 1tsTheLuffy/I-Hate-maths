using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class VD1 : MonoBehaviour
{
    [SerializeField] private int randomRotatePoint;
    [SerializeField] float movementSpeed;
    [SerializeField] float rotateAmount;
    [SerializeField] float distance;
    [Range(0f, 5f)]
    [SerializeField] float minDistance;

    [SerializeField] float timeToMove;
    [SerializeField] float timeToMoveToDifferentPos;

    [SerializeField] Transform[] RotatePoint;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        randomRotatePoint = Random.Range(0, RotatePoint.Length);

        timeToMove = timeToMoveToDifferentPos;
    }

    private void Update()
    {
        distance = Vector2.Distance(transform.position, RotatePoint[randomRotatePoint].position);

        //transform.RotateAround(RotatePoint[randomRotatePoint].position, Vector3.forward, -rotateAmount * Time.fixedDeltaTime);

        if (distance > minDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, RotatePoint[randomRotatePoint].position,
            movementSpeed * Time.deltaTime);
        }
        else if (distance < minDistance)
        {
            transform.RotateAround(RotatePoint[randomRotatePoint].position, Vector3.forward, -rotateAmount * Time.deltaTime);
            if (timeToMove <= 0)
            {
                randomRotatePoint = Random.Range(0, RotatePoint.Length);
                timeToMove = timeToMoveToDifferentPos;
            }else
            {
                timeToMove -= Time.deltaTime;
            }
        }
        
    }
}