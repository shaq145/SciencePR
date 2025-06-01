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

    public int successDialogue;
    public int failedDialogue;

    public GameObject dialoguePanel;
    public GameObject controls;
    public TextMeshProUGUI npcName;
    public TextMeshProUGUI npcDialogue;

    public TextMeshProUGUI btnText;

    public bool questLevel;
    public int questCount;
    public int questCountRequired;
    public GameObject gate;

    public NPCQuest npcQuest;

    public TextMeshProUGUI questText;

    [Serializable]
    public class Dialogue {
        public string npcName;
        [TextArea (3, 2)]
        public string dialogue;

        public bool gameObjOff;
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

    public int stageNumberInArray;

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

        if ( questLevel ) {
            questText.text = questCount.ToString () + "/" + questCountRequired.ToString ();

            if ( questCount == questCountRequired ) {
                questText.color = Color.green;
                gate.SetActive ( false );
            }
        }
    }

    public void StartDialogue () {
        dialoguePanel.SetActive ( true );
        controls.SetActive ( false );

        if ( npcQuest != null ) {
            if ( npcQuest.questCounter != 1 ) {
                questCount++;
                npcQuest.questCounter = 1;
            }
        }
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
