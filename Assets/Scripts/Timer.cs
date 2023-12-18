using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 20f;
    [SerializeField] float timeToShowCorrectAnswer = 5f;

    public bool loadNextQuestion;
    public bool isAnswering = false;
    public float fillFraction;
    float timerValue;
    void Update()
    {
        updateTimer();
    }

    public void cancelTimer()
    {
        timerValue = 0; 
    }
    void updateTimer()
    {
        timerValue-=Time.deltaTime;

        
        if(isAnswering)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            else
            {
                isAnswering= false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if (timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isAnswering= true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
        Debug.Log(isAnswering+" : "+timerValue+" : "+fillFraction);
    }
}
