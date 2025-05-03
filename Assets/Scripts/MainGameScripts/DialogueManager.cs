using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour {

    public int counter;
    public int npcDialogueCounter;

    public GameObject dialoguePanel;
    public GameObject controls;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogue;

    public TextMeshProUGUI btnText;

    [Serializable]
    public class Dialogue {
        public string npcName;
        [TextArea (3, 2)]
        public string dialogue;
        public GameObject animationToPlay;
        public GameObject[] objectToOff;
        public string sceneTransfer;
    }

    [Serializable]
    public class DialogueList {
        public string dialogueName;
        public List<Dialogue> dialogues;
    }

    public List<DialogueList> dialogueList;
    public PlayerController player;

    private Animation anim;

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animation>();
    }

    // Update is called once per frame
    void Update() {
        npcName.text = dialogueList [ counter ].dialogues [ npcDialogueCounter ].npcName;
        npcDialogue.text = dialogueList [ counter ].dialogues [ npcDialogueCounter ].dialogue;


        if ( dialogueList [ counter ].dialogues.Count - 1 == npcDialogueCounter ) {
            btnText.text = "End";
        } else {
            btnText.text = "Next";
        }
    }

    public void StartDialogue () {
        dialoguePanel.SetActive ( true );
        controls.SetActive ( false );
    }

    public void SceneTransfer () {
        SceneManager.LoadSceneAsync ( dialogueList [ counter ].dialogues [ npcDialogueCounter ].sceneTransfer );
    }

    public void DialogueController () {
        if ( dialogueList [ counter ].dialogues.Count - 1 > npcDialogueCounter ) {
            npcDialogueCounter++;
            if ( dialogueList [ counter ].dialogues [ npcDialogueCounter ].animationToPlay != null ) {
                dialogueList [ counter ].dialogues [ npcDialogueCounter ].animationToPlay.SetActive ( true );
            }

        } else {
            if ( dialogueList [ counter ].dialogues [ npcDialogueCounter ].sceneTransfer.Equals ( "" ) ) {
                if ( dialogueList [ counter ].dialogues [ npcDialogueCounter ].objectToOff != null ) {
                    foreach ( GameObject objects in dialogueList [ counter ].dialogues [ npcDialogueCounter ].objectToOff ) {
                        objects.SetActive ( false );
                    }
                }
                npcDialogueCounter = 0;
                dialoguePanel.SetActive ( false );
                controls.SetActive ( true );
                
            } else {
                dialoguePanel.SetActive ( false );
                anim.Play ( "FadeIn" );
            }
            
        }
    }
}
