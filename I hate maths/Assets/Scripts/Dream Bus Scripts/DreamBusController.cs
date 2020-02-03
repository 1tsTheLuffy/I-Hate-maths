using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class DreamBusController : MonoBehaviour
{
    private Vector2 mousePos;
    public Vector2 movement;
    public int health = 10;
    [Range(0f,200f)]
    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwShoot;
    [SerializeField] float bulletDestroyTime;

    [SerializeField] float x1,x2,y1,y2;

    [SerializeField] GameObject[] bulletType;
    [SerializeField] GameObject[] bulletParticleType;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParticle;

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
    SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        if(virtualCamera != null)
        {
            virtualNoiseCamera = virtualCamera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
        }

        bullet = bulletType[0];
        bulletParticle = bulletParticleType[0];

        timer = timeBtwShoot;

        Cursor.visible = false;
    }

    private void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePos;

        transform.position = new Vector3(Mathf.Clamp(transform.position.x,x1,x2), Mathf.Clamp(transform.position.y, y1, y2), transform.position.z);

        //For Shooting and everything related to shooting..

        if((Input.GetKey(KeyCode.Z) || Input.GetMouseButton(0)) && timer <= 0)
        {
            Shoot();
            Instantiate(bulletParticle, shootPoint.position, shootPoint.rotation * new Quaternion(0f,90,0f,0f));
            elapsedTime = shakeDuration;
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }

        //Temp
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            bullet = bulletType[1];
            timeBtwShoot = .5f;
            shakeAmplitude = 8f;
            shakeFrequency = 8f;
        }
        else if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            bullet = bulletType[0];
            timer = .35f;
        }
        //

        //

        // EveryThing related to health..

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(health > 7)
        {
            sr.color = Color.blue;
        }else if(health < 7 && health > 4)
        {
            sr.color = Color.yellow;
        }else if(health > 0 && health < 4)
        {
            sr.color = Color.red;
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
