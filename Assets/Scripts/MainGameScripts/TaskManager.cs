using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TaskManager : MonoBehaviour {

    public static TaskManager Instance;

    public GameObject dialoguePanel;
    public GameObject controls;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogue;
    public TextMeshProUGUI btnText;

    public Animation taskAnimation;
    public TextMeshProUGUI taskText;

    public int taskCounter;
    public int taskDialogueCounter;

    [Serializable]
    public class Dialogue {
        public string npcName;
        [TextArea ( 3, 2 )]
        public string dialogue;
    }

    [Serializable]
    public class DialogueList {
        public string taskName;
        public List<Dialogue> dialogues;
        public GameObject exclamation;
        public GameObject relatedObject;
        public bool doorSwitchActivate; // NEW

        public Task taskObject;
    }

    public List<DialogueList> dialogueList;
    public Task currentTask;

    bool startDialogue;

    private void Awake () {
        Instance = this;
    }

    void Start () {
        ActivateCurrentTask ();
    }

    void Update () {
        if ( startDialogue ) {
            npcName.text = dialogueList [ taskCounter ].dialogues [ taskDialogueCounter ].npcName;
            npcDialogue.text = dialogueList [ taskCounter ].dialogues [ taskDialogueCounter ].dialogue;
            btnText.text = ( taskDialogueCounter >= dialogueList [ taskCounter ].dialogues.Count - 1 ) ? "End" : "Next";
        }
    }

    public void StartDialogue () {
        startDialogue = true;
        dialoguePanel.SetActive ( true );
        controls.SetActive ( false );
        taskDialogueCounter = 0;
    }

    public void NextDialogue () {
        if ( taskDialogueCounter < dialogueList [ taskCounter ].dialogues.Count - 1 ) {
            taskDialogueCounter++;
        } else {
            EndDialogue ();
        }
    }

    public void EndDialogue () {
        dialoguePanel.SetActive ( false );
        controls.SetActive ( true );

        currentTask = dialogueList [ taskCounter ].taskObject;
        if ( currentTask != null ) {
            currentTask.AllowPassKey (); // Dialogue done, now puzzle appears (if needed)
        }

        if ( currentTask.advanceAfterDialogueOnly ) {
            CompleteTask ();
        }

        if ( currentTask != null ) {
            if ( dialogueList [ taskCounter ].doorSwitchActivate ) {
                dialogueList [ taskCounter ].relatedObject.GetComponent<Task> ().requiresPassKey = true;
            }
            //currentTask.AllowPassKey ();
        }

        if ( currentTask != null && currentTask.advanceAfterDialogueOnly ) {
            CompleteTask ();
        }
        startDialogue = false;
    }

    public void CompleteTask () {
        if ( dialogueList [ taskCounter ].exclamation != null ) {
            dialogueList [ taskCounter ].exclamation.SetActive ( false );
        }

        taskCounter++;

        if ( taskCounter < dialogueList.Count ) {
            ActivateCurrentTask ();
        }
    }

    void ActivateCurrentTask () {
        if ( dialogueList [ taskCounter ].taskObject != null ) {
            currentTask = dialogueList [ taskCounter ].taskObject; // ✅ Assign the actual Task reference
        }

        if ( dialogueList [ taskCounter ].exclamation != null ) {
            SetExclamations ( dialogueList [ taskCounter ].exclamation );
        }
        PlayTaskText ( dialogueList [ taskCounter ].taskName );
    }

    public bool IsCurrentTask ( Task task ) {
        return currentTask == task;
    }

    void SetExclamations ( GameObject currentTaskObj ) {
        currentTaskObj.SetActive ( true );
    }

    void PlayTaskText ( string text ) {
        taskText.text = text;
        taskAnimation.Play (); // This is your slide/fade animation
    }

}