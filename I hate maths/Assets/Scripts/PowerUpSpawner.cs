using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private bool isStarted = false;

    [System.Serializable]
    public class Power
    {
        public string name;
        public float x;
        public float y;
        public GameObject[] PowerUp;
        public Transform[] points;
    }

    public Power[] power;

    private void Start()
    {
        isStarted = false;
    }

    private void Update()
    {
        if(!isStarted)
        {
            StartCoroutine(Health());
            StartCoroutine(BulletPowerUp());
        }
    }

    IEnumerator Health()
    {
        isStarted = true;
        while(isStarted == true)
        {
            float delay = Random.Range(power[0].x, power[0].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, power[0].points.Length);
            Instantiate(power[0].PowerUp[0], power[0].points[i].position, Quaternion.identity);
        }
    }

    IEnumerator BulletPowerUp()
    {
        isStarted = true;
        while(isStarted == true)
        {
            float delay = Random.Range(power[0].x, power[0].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, power[0].points.Length);
            int j = Random.Range(0, power[0].PowerUp.Length);
            Instantiate(power[0].PowerUp[j], power[0].points[i].position, Quaternion.identity);
        }
    }
}
