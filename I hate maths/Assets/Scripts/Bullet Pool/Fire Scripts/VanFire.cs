/* This Fire script is responsible for shooting bullets in multiple directioon depending upon the angle 
 The gameobject does not required any type of different script for shooring projectiles if this script is attached
 Every Gameobject requires different types of fire Scipt for now.......*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VanFire : MonoBehaviour
{
    [SerializeField] float angle;
    public float startAngle;
    public float endAngle;
    [SerializeField] float angleStep;
    [Range(0f,10f)]
    [SerializeField] float stepMultiple;

    public float timer;
    public float timeBtwSpawn;

    [SerializeField] Transform shootPoint;

    ObjectPooler pooler;

    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();

        timer = timeBtwSpawn;
    }

    private void Update()
    {
        if(timer <= 0)
        {
            for (int i = 0; i < pooler.size; i++)
            {
                Shoot();
            }
            timer = timeBtwSpawn;
        }else
        {
            timer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        angleStep = ((endAngle - startAngle) / pooler.size) * stepMultiple;
        angle = startAngle;

        for (int i = 0; i < pooler.size; i++)
        {
            float x = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float y = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector3 dir = new Vector3(x, y, 0);
            Vector2 bulletDir = (dir - transform.position).normalized;
            GameObject obj = ObjectPooler.instance.GetFromPool(shootPoint.position, Quaternion.identity);

            obj.GetComponent<VD_1_Bullet>().SetMoveDirection(bulletDir);
            angle += angleStep;
        }
    }
}
