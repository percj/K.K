using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underfirecontroller : MonoBehaviour
{
    [SerializeField] List<GameObject> fire;
    float enableTime;
    [SerializeField] float enableEllapsed;
    gamemanager gamemanager;
    void Start()
    {
        gamemanager = FindObjectOfType<gamemanager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!gamemanager.isfinis)
        {
            if (enableTime > enableEllapsed)
            {
                var rand = Random.Range(0, fire.Count);
                fire[rand].SetActive(true);
                enableTime = 0;
            }
            else
            {
                enableTime += Time.deltaTime;
            }
        }
        else
        {
            foreach (var fire in fire)
                fire.SetActive(false);
        }
    }
}
