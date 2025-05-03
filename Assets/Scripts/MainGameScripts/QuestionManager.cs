using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionManager : MonoBehaviour {

    public int counter;
    public int questionNumber;
    public int correctCount;
    public int correctRequired;

    public int successDialogue;
    public int failedDialogue;

    public GameObject questionPanel;
    public GameObject controls;

    public TextMeshProUGUI questionNumberText;
    public TextMeshProUGUI questionText;

    public TextMeshProUGUI correctCountText;

    [Serializable]
    public class Questions {
        public int answerID;
        public string questionTitle;
        [TextArea ( 3, 2 )]
        public string question;
        public List<Choices> choices;
    }

    [Serializable]
    public class Choices {
        public int id;
        public string choiceName;
        public bool correctAnswer;
    }

    public List<Questions> questionList;
    public TextMeshProUGUI [] answerBtnText;
    public Enemies currentEnemy;

    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start() {
        dialogueManager = FindAnyObjectByType<DialogueManager>();
        currentEnemy = FindAnyObjectByType<Enemies>();
    }

    // Update is called once per frame
    void Update() {
        questionNumberText.text = "Question " + questionNumber.ToString ();
        correctCountText.text = "Correct: " + correctCount;
        questionText.text = questionList [counter].question;
    }

    public void SetChoicesText () {
        for ( int i = 0; i < answerBtnText.Length; i++ ) {
            answerBtnText [ i ].text = questionList [ counter ].choices [ i ].choiceName;
        }
    }

    public void Answer ( int id ) {
        if ( questionList.Count - 1 > counter ) {
            if ( id == questionList [ counter ].answerID ) {
                correctCount++;
            }
            counter++;
            questionNumber++;
            SetChoicesText ();

        } else {
            questionPanel.SetActive ( false );
            currentEnemy.doneQuestion = true;
            CheckScore ();
        }
    }

    public void StartQuestions () {
        questionPanel.SetActive ( true );
        controls.SetActive ( false );
        SetChoicesText ();
    }

    public void CheckScore () {
        if ( correctCount >= correctRequired ) {
            dialogueManager.counter = successDialogue;
        } else {
            dialogueManager.counter = failedDialogue;
        }
        dialogueManager.StartDialogue ();
    }
}
