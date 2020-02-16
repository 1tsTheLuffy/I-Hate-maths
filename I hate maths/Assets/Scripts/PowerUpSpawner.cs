using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    private bool isStarted = false;
    public bool isAssitance = false;

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

        if(!isAssitance)
        {
            StartCoroutine(Assistant());
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
            float delay = Random.Range(power[1].x, power[1].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, power[1].points.Length);
            int j = Random.Range(0, power[1].PowerUp.Length);
            Instantiate(power[1].PowerUp[j], power[1].points[i].position, Quaternion.identity);
        }
    }

    public IEnumerator Assistant()
    {
        isAssitance = true;
        while(isAssitance == true)
        {
            float delay = Random.Range(power[2].x, power[2].y);
            yield return new WaitForSeconds(delay);
            int i = Random.Range(0, power[2].points.Length);
            int j = Random.Range(0, power[2].PowerUp.Length);
            Instantiate(power[2].PowerUp[j], power[2].points[i].position, Quaternion.identity);
        }
    }
}
