using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public float leftrightspeed;
    private float oldPosition;

    public float distance;
    private Transform target;
    public float followspeed;
    public Quaternion quaternion;
    public float speed;
    [SerializeField] float damage;
    [SerializeField] GameObject ko;
    [SerializeField] TimerController timer;

    bool inRange;
    bool isFinish;

    [SerializeField] Transform Player;

    private Animator anim;
    private int counter=1;

    public bool CanHit { get; set; }

    void Start()
    {

        Physics2D.queriesStartInColliders = false;
        target = Player;
        anim = GetComponent<Animator>();
        quaternion = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isFinish)
        {
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
            timer.finish = true;
            ko.SetActive(true);
        }

    }

    void EnemyMove()
    {
        if (!anim.GetBool("Attack"))
        {
            if (quaternion.eulerAngles.y == 90)
                    transform.position += Vector3.right * Time.deltaTime * speed;
            else
                    transform.position -= Vector3.right * Time.deltaTime * speed;
        }

    }

    public void enemyHit()
    {
        if (inRange)
        {
            Debug.Log("Vurdu");
            var rand = Random.Range(-7, 7);
            var health = Player.GetComponent<healthController>();
            health.decreaseHealth(damage + rand);

            if (health.isDeath)
                isFinish = true;
        }

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
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            anim.SetBool("Attack", false);
        }
    }
}
