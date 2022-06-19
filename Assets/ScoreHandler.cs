using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandler : MonoBehaviour
{
    [SerializeField] int Level;
    [SerializeField] gamemanager manager;
    [SerializeField] healthController player;
    [SerializeField] TimerController timer;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] TextMeshProUGUI text2;
    private bool writed;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!writed)
        {
            if (manager != null)
                if (manager.isfinis)
                    calculateScore();

            if (player.isDeath)
                calculateScore();
        }
        
    }

    private void calculateScore()
    {
        writed = true;
        var Score= (timer.StartTimer - (int)Time.time) * Level;
        if(text2 !=null)
           text2.text += Score;
        else
            text.text += Score;
    }
}
