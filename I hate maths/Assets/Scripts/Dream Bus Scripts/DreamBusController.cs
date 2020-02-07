using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

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
    [Range(0f,100f)]
    [SerializeField] float explosionRadius;

    [SerializeField] float x1,x2,y1,y2;

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] GameObject[] bulletType;
    [SerializeField] GameObject[] bulletParticleType;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParticle;

    [SerializeField] Transform shootPoint;
    [SerializeField] Transform trailPoint;

    [SerializeField] LayerMask enemyMask;

    [Header("Shake")]
    [SerializeField] CameraShake shake;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        bullet = bulletType[0];
        bulletParticle = bulletParticleType[0];

        healthText.text = health.ToString();

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
            shake.elapsedTime = shake.shakeDuration;
            timer = timeBtwShoot;
        }else
        {
            timer -= Time.deltaTime;
        }

        //Electric Shooting..


        //

        //Temp
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            bullet = bulletType[1];
            timeBtwShoot = .5f;
            shake.shakeAmplitude = 8f;
            shake.shakeFrequency = 8f;
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
           // SceneManager.LoadScene(0);
        }

        if(health > 7)
        {
            sr.color = Color.blue;
        }else if(health < 10 && health > 6)
        {
            sr.color = Color.yellow;
        }else if(health > 0 && health < 6)
        {
            sr.color = Color.red;
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            health--;
        }

        healthText.text = health.ToString();

        //

        if (Input.GetKeyDown(KeyCode.R) || Input.GetMouseButtonDown(1))
        {
            Blast();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(movement.x * movementSpeed * Time.fixedDeltaTime, movement.y * movementSpeed * Time.fixedDeltaTime, rb.transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("EnemyBullet"))
        {
            Destroy(collision.transform.gameObject);
            shake.elapsedTime = .05f;
            shake.shakeAmplitude = 1f;
            shake.shakeFrequency = 1f;
            health -= 1;
        }
        if(collision.CompareTag("EnemyBomb"))
        {
            Destroy(collision.transform.gameObject);
            shake.elapsedTime = .5f;
            shake.shakeAmplitude = 2f;
            shake.shakeFrequency = 1f;
            health -= 5;
        }
    }

    private GameObject Shoot()
    {
        GameObject bulletDes = Instantiate(bullet, shootPoint.position, Quaternion.identity);
        return bulletDes;
    }

    private void Blast()
    {
        Collider2D[] Object = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyMask);
        for (int i = 0; i < Object.Length; i++)
        {
            Destroy(Object[i].transform.gameObject);
        }
        return;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
