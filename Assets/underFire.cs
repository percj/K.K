using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underFire : MonoBehaviour
{
    [SerializeField] float damage;
    float time = 0;
    private void Start()
    {
        time = 0;
    }
    private void Update()
    {
        if (time < 1)
        {
            time += Time.deltaTime;
        }
        else if (time > 1)
        {
            gameObject.SetActive(false);
            time = 0;

        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.tag == "Player" || other.tag == "Enemy")
        {
            var health = other.GetComponent<healthController>();
            health.fireDamage(damage);
        }
      

    }
    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
