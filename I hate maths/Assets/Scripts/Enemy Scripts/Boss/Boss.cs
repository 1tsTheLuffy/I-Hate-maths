using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Boss : MonoBehaviour
{
    public float health;
    public float currentHealth;

    private int randomMovePoint;

    [Header("Floats")]
    [SerializeField] int eventType;
    [SerializeReference] int secondStageEvent;
    [SerializeField] float movementSpeed;
    [SerializeField] float chaseSpeed;

    [Header("Timers")]
    [SerializeField] float startTime;
    [SerializeField] float timeToMoveToNextState;
    [SerializeField] float timer;
    [SerializeField] float timeToShoot;

    [Space]
    [SerializeField] Vector2 target;
    [Space]

    [Header("GameObjects")]
    [SerializeField] GameObject[] Bullet;
    [SerializeField] GameObject damageParticle;

    [Header("Transform")]
    [SerializeField] Transform[] movePoints;
    [SerializeField] Transform[] shootPoint;
    [SerializeField] Transform bus;

    [SerializeField] HealthBar healthBar;

    [Header("Color")]
    [SerializeField] Color[] hitColor;

    CameraShake shake;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        bus = GameObject.FindGameObjectWithTag("DreamBus").transform;
        shake = GameObject.FindGameObjectWithTag("CameraShake").GetComponent<CameraShake>();

        target = new Vector2(bus.position.x, bus.position.y);

        startTime = timeToMoveToNextState;
        timer = timeToShoot;

        health = 50;
        currentHealth = health;
        healthBar.setMaxHealth(health);


        eventType = Random.Range(1, 4);
        secondStageEvent = Random.Range(1, 3);
        randomMovePoint = Random.Range(0, movePoints.Length);
    }

    private void Update()
    {
        if (health > 25)                            // +++++++++++ FIRST STAGE OF THE BOSS +++++++++++++ \\
        {
            if (eventType == 1)
            {
                animator.SetBool("isGun", true);
                animator.SetBool("isThorn", false);

                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);

                if (timer <= 0)
                {
                    shoot();
                    timer = timeToShoot;
                }
                else
                {
                    timer -= Time.deltaTime;
                }

                if (Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
                {
                    randomMovePoint = Random.Range(0, movePoints.Length);
                }

                if (startTime <= 0)
                {
                    eventType = Random.Range(1, 4);
                    if (eventType == 2)
                    {
                        chaseSpeed = Random.Range(20f, 26f);
                        target = new Vector2(bus.position.x, bus.position.y);
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }
            else if (eventType == 2)
            {
                animator.SetBool("isThorn", true);
                animator.SetBool("isGun", false);
                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
            }
            else if (eventType == 3)
            {
                animator.SetBool("isThorn", false);
                animator.SetBool("isGun", false);

                if (startTime <= 0)
                {
                    eventType = Random.Range(1, 4);
                    if (eventType == 3)
                    {
                        eventType = 2;
                    }
                    startTime = timeToMoveToNextState;
                }
                else
                {
                    startTime -= Time.deltaTime;
                }
            }

            if (transform.position.x == target.x && transform.position.y == target.y)
            {
                eventType = 2;
                target = new Vector2(bus.position.x, bus.position.y);
                //randomMovePoint = Random.Range(0, movePoints.Length);
                return;
            }
        }
        if (health <= 25)                   // +++++++++++ SECOND STAGE OF THE BOSS +++++++++++++ \\
        {
            sr.color = Color.magenta;
            animator.SetTrigger("Second");

            if(secondStageEvent == 1)
            {
                transform.Rotate(0f, 0f, 10f);

                transform.position = Vector2.MoveTowards(transform.position, movePoints[randomMovePoint].position, movementSpeed * Time.deltaTime);

                if(Vector2.Distance(transform.position, movePoints[randomMovePoint].position) < .2f)
                {
                    randomMovePoint = Random.Range(0, movePoints.Length);
                }
             
                if(timer <= 0)
                {
                    shoot();
                    timer = .07f;
                }else
                {
                    timer -= Time.deltaTime;
                }

                if(startTime <= 0)
                {
                    secondStageEvent = Random.Range(1, 4);
                    if(secondStageEvent == 2)
                    {
                        chaseSpeed = Random.Range(20f, 28f);
                        target = new Vector2(bus.position.x, bus.position.y);
                    }
                    startTime = timeToMoveToNextState;
                }else
                {
                    startTime -= Time.deltaTime;
                }
            }else if(secondStageEvent == 2)
            {
                transform.position = Vector2.MoveTowards(transform.position, target, chaseSpeed * Time.deltaTime);
                if(timer <= 0)
                {
                    shoot();
                    timer = timeToShoot;
                }else
                {
                    timer -= Time.deltaTime;
                }
                return;
            }else if(secondStageEvent == 3)
            {
                if (health < 20)
                {
                    animator.SetBool("isHealing", true);
                    sr.color = Color.green;
                    health += .1f;
                    startTime = .01f;
                    if (startTime <= 0)
                    {
                        secondStageEvent = Random.Range(1, 4);
                        if (secondStageEvent == 3)
                        {
                            secondStageEvent = 1;
                            return;
                        }
                        animator.SetBool("isHealing", false);
                        startTime = timeToMoveToNextState;
                        return;
                    }
                    else
                    {
                        startTime -= Time.deltaTime * 2f;
                    }
                }else
                {
                    secondStageEvent = 1;
                }
                return;
            }
        } 

        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if(Input.GetKeyDown(KeyCode.L))
        {
            health = 24;
        }

        healthBar.setHealth(health);
    }


    // +++++++ TRIGGER +++++++ \\
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("DreamBus"))
        {
            shake.C_Shake(.07f, 2.5f, .8f);
            int randomEvent = Random.Range(1, 3);
            if(randomEvent == 1)
            {
                eventType = 1;

            }
            else if(randomEvent == 2)
            {
                eventType = 2;
            }
           // randomMovePoint = Random.Range(0, movePoints.Length);
            return;
        }

        if(collision.CompareTag("Bullet1"))
        {
            if(eventType == 3)
            {
                shake.C_Shake(.08f, 1f, .8f);
                StartCoroutine(hitFlash());
                Destroy(collision.transform.gameObject);
                health -= 1; 
                GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
                Destroy(instance, 1.2f);
            }
            if(eventType == 2)
            {
                shake.C_Shake(.08f, 1f, .8f);
                StartCoroutine(hitFlash());
                Destroy(collision.transform.gameObject);
                health -= 1;
                eventType = 1;
            }
            if (health < 25)
            {
                if (secondStageEvent == 1)
                {
                    shake.C_Shake(.08f, 1f, .8f);
                    Destroy(collision.transform.gameObject);
                    health -= 1;
                    GameObject instance = Instantiate(damageParticle, collision.transform.position, Quaternion.identity);
                    Destroy(instance, 1.2f);
                }
            }

        }
        if(collision.CompareTag("Electric"))
        {
            shake.C_Shake(.08f, 3f, .8f);
            Destroy(collision.transform.gameObject);
            health -= 5f;
        }
    }

    private void shoot()
    {
        for (int i = 0; i < 3; i++)
        {
            Instantiate(Bullet[i], shootPoint[i].position, shootPoint[i].rotation);
        }
    }

    IEnumerator hitFlash(float x = 1f)
    {
        if (health > 40f && health < 50f)
        {
            sr.color = hitColor[0];
        } else if (health > 30f && health < 50f)
        {
            sr.color = hitColor[1];
        } else if (health > 25f && health < 30f)
        {
            sr.color = hitColor[2];
        }

        yield return new WaitForSeconds(.1f);

        Color color = hitColor[3];
        sr.color = color;
    }
}