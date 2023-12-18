using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionsSO> questions=new List<QuestionsSO>();
    QuestionsSO currentQuestion;

    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly;

    [Header("Sprites")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite CorrectAnswerSprite;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    void Start()
    {
        timer=FindObjectOfType<Timer>();
        
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if (timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnswering)
        {
            DisplayAnswer(-1);
            setButtonState(false);
        }
    }

    public void onAnswerSelected(int index)
    {
        hasAnsweredEarly= true;
        DisplayAnswer(index);
        setButtonState(false);
        timer.cancelTimer();
    }

    void DisplayAnswer(int index)
    {
        Image buttonImage;
        if (index == currentQuestion.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct Answer!";
            buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.sprite = CorrectAnswerSprite;
        }
        else
        {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Wrong Answer! The correct answer is " + correctAnswer + ".";
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = CorrectAnswerSprite;
        }
    }

    void getNextQuestion()
    {
        if(questions.Count> 0)
        {
            setButtonState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            displayQuestion();
        }
    }

    void getRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion= questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
        
    }

    void displayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
    }

    void setButtonState(bool state)
    {
        for(int i=0;i<answerButtons.Length;i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void setDefaultButtonSprites()
    {
        for(int i = 0; i < answerButtons.Length; i++)
        {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
