/* A Fire is responsible for shooting bullets in multiple directioon depending upon the angle 
 The gameobject does not required any type of script for shooring projectiles if this script is attached
 Every Gameobject requires different types of fire Scipt for now.......*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VDFire : MonoBehaviour
{
    [Header("Number of bullets")]
    [SerializeField] int size;

    [Header("Direction")]
    [SerializeField] float angle;
    [SerializeField] float startAngle;
    [SerializeField] float endAngle;
    [SerializeField] float angleStep;

    [Header("Time")]
    [SerializeField] float timer;
    [SerializeField] float timeBtwSpawn;

    ObjectPool pool;

    private void Start()
    {
        InvokeRepeating("Shoot", 2, 2);

        pool = GetComponent<ObjectPool>();
    }

    private void Shoot()
    {
        angleStep = ((endAngle - startAngle) / size) * .5f;
        angle = startAngle;

        for (int i = 0; i < size; i++)
        {
            float x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 dir = new Vector3(x, y, 0);
            Vector2 bulletDir = (dir - transform.position).normalized;

            GameObject bul = pool.GetFromPool();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<VD_1_Bullet>().SetMoveDirection(bulletDir);

            angle += angleStep;
        }
    }
}
