using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curve2 : MonoBehaviour
{
    private bool isGoing;
    [SerializeField] int FollowType;
    [SerializeField] int i = 0;
    [SerializeField] int randomNum;
    [SerializeField] float speed;
    [SerializeField] float t;
    [SerializeField] int routeNum;

    [SerializeField] Transform[] Route;

    private Vector2 objPos;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        routeNum = 0;
        t = 0;

        isGoing = true;
    }

    private void Update()
    {
        if (isGoing)
            StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        //        isGoing = false;

        Vector2 p0 = Route[routeNum].GetChild(0).position;
        Vector2 p1 = Route[routeNum].GetChild(1).position;
        Vector2 p2 = Route[routeNum].GetChild(2).position;
        Vector2 p3 = Route[routeNum].GetChild(3).position;


        while (t <= 1)
        {
            t += Time.deltaTime * speed;
            objPos = Mathf.Pow(1 - t, 3) * p0 + 3 * Mathf.Pow(1 - t, 2) * t * p1 +
                3 * (1 - t) * Mathf.Pow(t, 2) * p2 + Mathf.Pow(t, 3) * p3;

            transform.position = objPos;

            yield return new WaitForEndOfFrame();

        }

        t = 0f;

        routeNum += 1;

        if(FollowType == 1)
        {
            if (routeNum > Route.Length - 1)
            {
                Destroy(gameObject);
                Debug.Log("Destroyed!!");
            }
        }else if(FollowType == 2)
        {
            if(routeNum > Route.Length - 1)
            {
                routeNum = 0; 
            }
        }
    }
}
