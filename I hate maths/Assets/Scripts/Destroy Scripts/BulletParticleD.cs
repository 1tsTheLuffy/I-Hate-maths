using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletParticleD : MonoBehaviour
{
    private void Update()
    {
        Destroy(gameObject, 2f);
    }
}
