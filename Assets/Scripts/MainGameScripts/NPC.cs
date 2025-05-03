using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour {

    public int npcCounter;

    public GameObject interactBtn;

    [SerializeField]
    private DialogueManager dialogueManager;
    
    // Start is called before the first frame update
    void Start() {
        dialogueManager = FindAnyObjectByType<DialogueManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    private void OnCollisionEnter2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            dialogueManager.counter = npcCounter;
            interactBtn.SetActive ( true );
        }
    }

    private void OnCollisionStay2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            dialogueManager.counter = npcCounter;
            interactBtn.SetActive ( true );
        }
    }

    private void OnCollisionExit2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            dialogueManager.counter = 0;
            interactBtn.SetActive ( false );
        }
    }
}
