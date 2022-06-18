using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject target;
    private bool isGround;
    public float damage;
    public float speed;
    bool isFinish;
    bool inRange;
    int counter=1;

    public bool CanHit { get; internal set; }

    void Update()
    {

        if (!isFinish)
        {
            if (Input.GetMouseButtonDown(0) && isGround)
            {
                if(counter == 3)
                    counter = 1;
                animator.SetBool("Attack", true);
                animator.SetInteger("CounterForPunch", counter++);
            }
            else
            {
                animator.SetBool("Attack", false);
            }

            /*if (Input.GetKeyDown(KeyCode.W) && isGround)
            {
                rb.AddForce(transform.up * 700);


            }*/
            if (Input.GetKey(KeyCode.D))
            {
                gameObject.transform.localRotation = Quaternion.LookRotation(Vector3.right);
                gameObject.transform.position += speed * Time.deltaTime * Vector3.right;
                animator.SetBool("Walk", true);

            }
            else if (Input.GetKey(KeyCode.A))
            {
                gameObject.transform.localRotation = Quaternion.LookRotation(Vector3.left);

                gameObject.transform.position -= speed * Time.deltaTime * Vector3.right;
                animator.SetBool("Walk", true);

            }
            else
            {
                animator.SetBool("Walk", false);
            }
        }
        else
        {
            animator.SetBool("Walk", false);
            animator.SetBool("Attack", false);
        }


    }
    public void enemyHit()
    {
        if(CanHit)
        {
            Debug.Log("Vurdu");
            var rand = Random.Range(-7, 7);
            var health = target.GetComponent<healthController>();
            health.decreaseHealth(damage + rand);

            if (health.isDeath)
                isFinish = true;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            inRange = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGround = true;
        }


    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "Ground")
        {
            isGround = false;
        }
    }

}
