using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DreamBusController : MonoBehaviour
{
    public Vector2 movement;
    [Range(0f,200f)]
    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwShoot;
    [SerializeField] float trailParticleDestroyTime;

    [SerializeField] GameObject[] bulletType;
    [SerializeField] GameObject bullet;

    [SerializeField] Transform shootPoint;
    [SerializeField] Transform trailPoint;

    [Header("Camera Shake")]
    [SerializeField] CinemachineVirtualCamera virtualCamera;
    private CinemachineBasicMultiChannelPerlin virtualNoiseCamera;
    public float shakeDuration;
    public float elapsedTime;
    public float shakeAmplitude;
    public float shakeFrequency;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if(virtualCamera != null)
        {
            virtualNoiseCamera = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        bullet = bulletType[0];

        timer = timeBtwShoot;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        //For Shooting..

        if(Input.GetKey(KeyCode.Z) && timer <= 0)
        {
            Shoot();
            elapsedTime = shakeDuration;
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }

        //

        CameraShake();
    }

    private void FixedUpdate()
    {
        transform.Translate(movement.x * movementSpeed * Time.fixedDeltaTime, movement.y * movementSpeed * Time.fixedDeltaTime, rb.transform.position.z);
    }

    private GameObject Shoot()
    {
        GameObject bulletDes = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        return bulletDes;
    }

    private void CameraShake()
    {
        if (elapsedTime > 0)
        {
            virtualNoiseCamera.m_AmplitudeGain = shakeAmplitude;
            virtualNoiseCamera.m_FrequencyGain = shakeFrequency;
            elapsedTime -= Time.deltaTime;
        }
        else
        {
            elapsedTime = 0;
            virtualNoiseCamera.m_FrequencyGain = 0;
            virtualNoiseCamera.m_AmplitudeGain = 0;
        }
    }
}
