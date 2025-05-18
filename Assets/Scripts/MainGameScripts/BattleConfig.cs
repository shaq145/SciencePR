using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleConfig : MonoBehaviour {

    public int counter;
    public GameObject [] battleScenes;

    private DialogueManager dialogueManager;

    private void Start () {
        dialogueManager = FindAnyObjectByType<DialogueManager>();

        battleScenes [ dialogueManager.stageNumberInArray ].SetActive ( true );
    }
}
