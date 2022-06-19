using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpawner : MonoBehaviour
{
    float fireTimer;
    [SerializeField] float fireEllapsed;
    [SerializeField] GameObject fireball;
    [SerializeField] gamemanager gamemanager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!gamemanager.isfinis)
        {
            if (fireEllapsed < fireTimer)
                sendBall();
            else
                fireTimer += Time.deltaTime;
        }
        
    }

    void sendBall()
    {
        fireTimer = 0;
        Instantiate(fireball);
        var rand = UnityEngine.Random.Range(-10,10);
        fireball.transform.position = transform.position + new Vector3(rand,0,0);
    }
}
