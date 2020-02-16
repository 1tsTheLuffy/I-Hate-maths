using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using TMPro;

public class DreamBusController : MonoBehaviour
{
    private bool isDeHeat = false;
    private Vector2 mousePos;
    public Vector2 movement;
    public int health = 10;
    [Header("Floats")]
    [Range(0f,200f)]
    [SerializeField] float movementSpeed;
    [SerializeField] float timer;
    [SerializeField] float timeBtwShoot;
    [SerializeField] float bulletDestroyTime;
    [Range(0f,100f)]
    [SerializeField] float explosionRadius;
    [SerializeField] float x1,x2,y1,y2;
    [SerializeField] float shockTime = 1f;
    [SerializeField] float timeStart;
    [SerializeField] float timeToChangeBullet;
    [SerializeField] float heatNum;
    [SerializeField] float heatTimer;
    [SerializeField] float startHeatTimer;
    [SerializeField] int electricNumber;
 
    [Header("UI")]
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI electricPowerUpText;
    [SerializeField] TextMeshProUGUI heatNumText;

    [Header("GameObjects")]
    [SerializeField] GameObject[] bulletType;
    [SerializeField] GameObject[] bulletParticleType;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletParticle;
    [SerializeField] GameObject Electric;
    [SerializeField] GameObject healthParticle;
    [SerializeField] GameObject[] PowerUpTaken;
    [SerializeField] GameObject timerObj;
    [SerializeField] GameObject extraPowerUpParticle;

    [Header("Transforms")]
    [SerializeField] Transform shootPoint;
    [SerializeField] Transform trailPoint;
    [SerializeField] Transform shockPoint;

    [Header("Color")]
    [SerializeField] Color[] color;

    [Header("LayerMasks")]
    [SerializeField] LayerMask enemyMask;

    [Header("Shake")]
    [SerializeField] CameraShake shake;

    Rigidbody2D rb;
    [SerializeField] SpriteRenderer sr;
    Camera cam;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        bullet = bulletType[2];
        bulletParticle = bulletParticleType[0];

        healthText.text = health.ToString();
        timerText.text = timeStart.ToString("0");
        electricPowerUpText.text = electricNumber.ToString();
        heatNumText.text = heatNum.ToString("0");

        timerObj.SetActive(false);

        timer = timeBtwShoot;
        heatTimer = startHeatTimer;

        health = 200;
        electricNumber = 2;

        isDeHeat = false;

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

        if((Input.GetKey(KeyCode.Z) || Input.GetMouseButton(0)) && timer <= 0 && heatNum < 20f)
        {
            Shoot();
            Instantiate(bulletParticle, shootPoint.position, shootPoint.rotation * new Quaternion(0f,90,0f,0f));
            shake.elapsedTime = shake.shakeDuration;
            timer = timeBtwShoot;
            if(heatTimer <= 0)
            {
                heatNum++;
                heatTimer = startHeatTimer;
            }else
            {
                heatTimer -= Time.deltaTime;
            }
        }else
        {
            timer -= Time.deltaTime;
        }

        if(Input.GetMouseButtonUp(0))
        {
            if(isDeHeat == false)
            {
                StartCoroutine(DeHeat());
            }
        }

        //Electric Shooting..
        if(Input.GetKeyDown(KeyCode.Space) && shockTime <= 0 && electricNumber > 0)
        {
            Instantiate(Electric, shockPoint.position, Quaternion.identity);
            electricNumber--;
            shockTime = 1f;
        }else
        {
            shockTime -= Time.deltaTime;
        }

        //

        //Everything related to bullet switching..
        
        if(timeStart <= 0)
        {
            bullet = bulletType[0];
        }else
        {
            timeStart -= Time.deltaTime;
        }

        if(bullet == bulletType[0])
        {
            timeBtwShoot = .3f;
            bulletParticle = bulletParticleType[0];
        }
        else if(bullet == bulletType[1])
        {
            timeBtwShoot = .45f;
            bulletParticle = bulletParticleType[1];
        }else if(bullet == bulletType[2])
        {
            timeBtwShoot = .52f;
            bulletParticle = bulletParticleType[2];
        }

        //

        //Temp
        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            bullet = bulletType[2];
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
            SceneManager.LoadScene(0);
        }

        if(Input.GetKeyDown(KeyCode.Q))
        {
            health--;
        }

        if(health < 0)
        {
            health = 0;
        }
        if(health > 20)
        {
            health = 20;
        }

        healthText.text = health.ToString();

        //

        // EVERYTING RELATED TO UI......

        timerText.text = timeStart.ToString("0");
        electricPowerUpText.text = electricNumber.ToString();
        heatNumText.text = heatNum.ToString("0");

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

    // TRIGGER.

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Triggers for detecting Hit..

        if(collision.CompareTag("EnemyBullet"))
        {
            StartCoroutine(HitFlash());
            Destroy(collision.transform.gameObject);
            shake.C_Shake(0.1f, 2f, 1f);
            health -= 1;
        }
        if(collision.CompareTag("EnemyBomb"))
        {
            Destroy(collision.transform.gameObject);
            shake.C_Shake(.1f, 2f, 1f);
            health -= 5;
        }

        if(collision.CompareTag("EnemyPoolBullet"))
        {
            StartCoroutine(HitFlash());
            collision.transform.gameObject.SetActive(false);
            shake.C_Shake(.1f, 2f, 1f);
            health -= 1;
        }

        if(collision.CompareTag("VD1"))
        {
            StartCoroutine(HitFlash());
            health -= 2;
            shake.C_Shake(.1f, 3f, .7f);
        }
        if(collision.CompareTag("Set_Boss"))
        {
            health -= 2;
        }

        //

        // Trigger for detecting Power up hit..
        if(collision.CompareTag("Health"))
        {
            Destroy(collision.transform.gameObject);
            StartCoroutine(HealthFlash());
            GameObject instance = Instantiate(healthParticle, transform.position, Quaternion.identity); 
            health += 5;
            Destroy(instance, 1f);
        }
        if(collision.CompareTag("ExtraPowerUp"))
        {
            Destroy(collision.transform.gameObject);
            StartCoroutine(PowerFlash(color[4]));
            GameObject instance = Instantiate(extraPowerUpParticle, transform.position, Quaternion.identity);
            Destroy(instance, 1f);
            electricNumber++;
        }

        //

        /* bulletType[0] = Simple Bullet..
         * bulletType[1] = Triangle Bullet..
         * bulletType[2] = Shield Bullet..
        */
        if(collision.CompareTag("TriangleBulletPowerUp"))
        {
            timerObj.SetActive(true);
            GameObject instance = Instantiate(PowerUpTaken[0], transform.position, Quaternion.identity);
            StartCoroutine(PowerFlash(Color.red));
            bullet = bulletType[1];
            bulletParticle = bulletParticleType[1];
            Destroy(collision.transform.gameObject);
            Destroy(instance, 1f);
            if(bullet == bulletType[0] || bullet == bulletType[2])
            {
                timeStart = 20f;
            }else if(bullet == bulletType[1])
            {
                timeStart += 20f;
            }
        }

        if(collision.CompareTag("ShieldBulletPowerUp"))
        {
            GameObject instance = Instantiate(PowerUpTaken[1], transform.position, Quaternion.identity);
            StartCoroutine(PowerFlash(Color.green));
            bullet = bulletType[2];
            Destroy(collision.transform.gameObject);
            Destroy(instance, 1f);
            if(bullet == bulletType[0] || bullet == bulletType[1])
            {
                timeStart = 20f;
            }else if(bullet == bulletType[2])
            {
                timeStart += 20f;
            }
        }
    }

    //TRIGGER..

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

    IEnumerator HitFlash()
    {
        if(health > 10)
        {
            sr.color = color[0];
        }else if(health > 5 && health < 10)
        {
            sr.color = color[1];
        }else if(health < 5)
        {
            sr.color = color[2];
        }
        yield return new WaitForSeconds(.1f);
        sr.color = Color.blue;
    }

    IEnumerator HealthFlash()
    {
        sr.color = color[3];
        yield return new WaitForSeconds(.2f);
        sr.color = Color.blue;
    }

    IEnumerator PowerFlash(Color color)
    {
        sr.color = color;
        yield return new WaitForSeconds(.2f);
        sr.color = Color.blue;
    }

    IEnumerator DeHeat()
    {
        isDeHeat = true;
        while(isDeHeat == true)
        {
            heatNum--;
            yield return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}