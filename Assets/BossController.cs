using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float leftrightspeed;
    private float oldPosition;

    public float distance;
    private Transform target;
    public float followspeed;
    public Quaternion quaternion;
    public float speed;
    [SerializeField] float damage;

    bool inRange;
    public bool isFinish;

    [SerializeField] Transform Player;
    [SerializeField] GameObject fireball;
    [SerializeField] Transform fireballPos;
    [SerializeField] GameObject teleport;
    [SerializeField] List<Transform> teleportpos;
    float teleportTimer;
    [SerializeField]float teleportEllapsed;
    private Animator anim;
    private int counter = 1;
    private bool stop;
    public bool CanHit { get; set; }

    void Start()
    {
        Physics2D.queriesStartInColliders = false;
        target = Player;
        anim = GetComponent<Animator>();
        quaternion = transform.rotation;
    }


    IEnumerator  teleportBoss()
    {
        teleportTimer = 0;
        teleport.SetActive(true);

        var randpos = Random.Range(0,teleportpos.Count);
        yield return new WaitForSeconds(.5f);
        teleport.SetActive(false);
        transform.position = teleportpos[randpos].position;
    }

    // Update is called once per frame
    void Update()
    {

        if (!isFinish)
        {
            if (teleportEllapsed < teleportTimer)
                StartCoroutine(teleportBoss());
            else
                teleportTimer += Time.deltaTime;

            EnemyMove();
            if (target.position.x < transform.position.x)
                quaternion = Quaternion.LookRotation(Vector3.left);
            else
                quaternion = Quaternion.LookRotation(Vector3.right);

            transform.rotation = quaternion;
            if (!anim.GetBool("Attack"))
                anim.SetBool("Walk", true);
            else
                anim.SetBool("Walk", false);
        }
        else
        {
            anim.SetBool("Walk", false);
            anim.SetBool("Attack", false);
        }

        if(stop)
            anim.SetBool("Walk", false);

    }

    void EnemyMove()
    {
        if (!anim.GetBool("Attack") && !stop)
        {
            if (quaternion.eulerAngles.y == 90)
                transform.position -= Vector3.right * Time.deltaTime * speed;
            else
                transform.position += Vector3.right * Time.deltaTime * speed;
        }

    }

    public void enemyHit()
    {
        if (inRange)
        {
            var rand = Random.Range(-7, 7);
            var health = Player.GetComponent<healthController>();
            health.decreaseHealth(damage + rand);

            if (health.isDeath)
                isFinish = true;
        }

    }
    public void fireBall()
    {
        var ball = Instantiate(fireball);
       
        ball.transform.position = fireballPos.position;
        if (quaternion.eulerAngles.y == 90)
            ball.GetComponent<bossSkill>().way = Vector3.right ;
        else
            ball.GetComponent<bossSkill>().way = -Vector3.right ;
    }

    void EnemyFollow()
    {
        Vector3 targetPosition = new Vector3(target.position.x, gameObject.transform.position.y, target.position.x);
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, followspeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (counter == 4)
                counter = 1;
            anim.SetBool("Attack", true);
            anim.SetInteger("CounterForPunch", counter++);
            anim.SetBool("Walk", false);
            inRange = true;
        }
        if (other.tag == "StopPos")
        {
            stop = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            anim.SetBool("Attack", false);
        }
        if (other.tag == "StopPos")
        {
            stop = false;
        }
    }
}
