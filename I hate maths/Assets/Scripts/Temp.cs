using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    [SerializeField] int health;

    [SerializeField] Sprite[] sets;

    [SerializeField] SpriteRenderer sr;

    [SerializeField] int i = 0;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {

        transform.Rotate(0, 0, 2f);

        if(Input.GetMouseButtonDown(0))
        {
            sr.sprite = sets[i];
            i++;
            health--;
            if(i == 4)
            {
                Destroy(gameObject);
            }
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
