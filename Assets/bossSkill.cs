using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSkill : MonoBehaviour
{
    [SerializeField] float damage;
    public Vector3 way;
    private void Update()
    {
        transform.position += way;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            var health = collision.gameObject.GetComponent<healthController>();
            health.decreaseHealth(damage);
            Destroy(gameObject);
        }
    }
}
