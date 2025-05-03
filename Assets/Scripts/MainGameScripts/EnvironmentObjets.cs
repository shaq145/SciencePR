using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObjets : MonoBehaviour {

    public int npcCounter;

    public bool xRequired;
    public bool yRequired;

    public float xPosRequired;
    public float yPosRequired;

    public GameObject interactBtn;

    [SerializeField]
    private DialogueManager dialogueManager;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start () {
        dialogueManager = FindAnyObjectByType<DialogueManager> ();
        playerController = FindAnyObjectByType<PlayerController> ();
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnCollisionEnter2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            if ( xRequired && playerController.GetAnimHorizontal () >= xPosRequired ) {
                dialogueManager.counter = npcCounter;
                interactBtn.SetActive ( true );

            } else if ( yRequired && playerController.GetAnimVertical () >= yPosRequired ) {
                dialogueManager.counter = npcCounter;
                interactBtn.SetActive ( true );
            } else {
                interactBtn.SetActive ( false );
            }
        }
    }

    private void OnCollisionStay2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            if ( xRequired && playerController.GetAnimHorizontal () >= xPosRequired ) {
                dialogueManager.counter = npcCounter;
                interactBtn.SetActive ( true );

            } else if ( yRequired && playerController.GetAnimVertical () >= yPosRequired ) {
                dialogueManager.counter = npcCounter;
                interactBtn.SetActive ( true );
            } else {
                interactBtn.SetActive ( false );
            }
        }
    }

    private void OnCollisionExit2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            dialogueManager.counter = 0;
            interactBtn.SetActive ( false );
        }
    }
}
