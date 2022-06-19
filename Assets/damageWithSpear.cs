using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damageWithSpear : MonoBehaviour
{
    List<healthController> health;
    [SerializeField] float damage;
    private void Start()
    {
        health = new List<healthController>();
    }
    public void SpearDamage()
    {
        foreach (healthController controller in health)
        {
            controller.decreaseHealth(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        healthController healt;
        if (other.TryGetComponent<healthController>(out healt))
        {
            if (!health.Contains(healt))
                health.Add(healt);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        healthController healt;
        if (other.TryGetComponent<healthController>(out healt))
        {
            if (health.Contains(healt))
                health.Remove(healt);
        }
    }
   
}
