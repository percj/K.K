using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class underfirecontroller : MonoBehaviour
{
    [SerializeField] List<GameObject> fire;
    float enableTime;
    [SerializeField] float enableEllapsed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
}
