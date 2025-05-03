using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObtainableEnvironment : MonoBehaviour {

    public int counter;
    public int itemDialogueCounter;

    [Serializable]
    public class Dialogue {
        public string itemName;
        [TextArea ( 3, 2 )]
        public string dialogue;
    }

    public List<Dialogue> itemDialogueList;

    public GameObject dialoguePanel;
    public GameObject controls;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDialogueText;

    public GameObject interactBtn;

    public TextMeshProUGUI btnText;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        itemNameText.text = itemDialogueList[itemDialogueCounter].itemName;
        itemDialogueText.text = itemDialogueList [ itemDialogueCounter ].dialogue;


        if ( itemDialogueList.Count - 1 == itemDialogueCounter ) {
            btnText.text = "End";
        } else {
            btnText.text = "Next";
        }
    }

    public void StartDialogue () {
        dialoguePanel.SetActive ( true );
        controls.SetActive ( false );
    }

    public void DialogueController () {
        if ( itemDialogueList.Count - 1 > itemDialogueCounter ) {
            itemDialogueCounter++;
        } else {
            itemDialogueCounter = 0;
            counter = 1;
            dialoguePanel.SetActive ( false );
            controls.SetActive ( true );
        }
    }
}
