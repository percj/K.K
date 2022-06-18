using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    public bool finish;
    [SerializeField] TextMeshProUGUI timer;
    int StartTimer = 60;

    void Update()
    {
        if(!finish)
        {
            if (StartTimer - Time.deltaTime <= 0)
            {
                Time.timeScale = 0f;
            }
            else
                timer.text = (StartTimer - (int)Time.time).ToString();

        }
    }
}
