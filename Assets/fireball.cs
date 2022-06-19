using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireball : MonoBehaviour
{
    [SerializeField] GameObject explotion;
    [SerializeField] float damage;
    [SerializeField] float speed;
    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy")
        {
            var health = collision.gameObject.GetComponent<healthController>();
            health.fireDamage(damage);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Ground")
        {
           var a = Instantiate(explotion);
            a.transform.position = transform.position;
            a.transform.parent = collision.gameObject.transform;
            a = null;
            Destroy(gameObject);
        }
    }
}
