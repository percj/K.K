using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] AIController aIController;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && playerController != null)
        {
            playerController.CanHit = true;
        }
        if (other.tag == "Player" && aIController)
        {
            aIController.CanHit = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy" && playerController != null)
        {
            playerController.CanHit = false;
        }
        if (other.tag == "Player" && aIController)
        {
            aIController.CanHit = false;
        }
    }
}
