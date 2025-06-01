using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Task : MonoBehaviour {
    
    public bool xRequired;
    public bool yRequired;

    public float xPosRequired;
    public float yPosRequired;

    public bool requiresPassKey = false;
    public bool hasDialogue = true;
    public bool advanceAfterDialogueOnly;
    public string code;

    public GameObject interactBtn;
    public GameObject inputPanel;

    public Button submitButton;

    public TextMeshProUGUI resultText;
    public TMP_InputField inputField;

    public SpriteRenderer doorSwitch;
    public Sprite [] doorSwitchSprite;
    public GameObject gate; // Optional

    private bool puzzleUnlocked = false;
    private bool puzzleCompleted = false;

    private PlayerController playerController;

    void Start () {
        playerController = FindObjectOfType<PlayerController> ();
        inputPanel.SetActive ( false );
        interactBtn.SetActive ( false );
    }

    void Update () {
        if ( doorSwitch != null && doorSwitchSprite.Length > 0 ) {
            doorSwitch.sprite = puzzleCompleted ? doorSwitchSprite [ 1 ] : doorSwitchSprite [ 0 ];
        }
    }

    public void Interact () {
        if ( !puzzleUnlocked && hasDialogue ) {
            TaskManager.Instance.StartDialogue (); // Trigger dialogue first
        } else if ( requiresPassKey && !puzzleCompleted ) {
            inputPanel.SetActive ( true );
        }
    }

    public void AllowPassKey () {
        puzzleUnlocked = true;
    }

    public void SubmitCode () {
        if ( inputField.text.Equals ( code, System.StringComparison.OrdinalIgnoreCase ) ) {
            resultText.text = "Correct!";
            inputField.interactable = false;
            puzzleCompleted = true;
            if ( gate != null )
                gate.SetActive ( false );
            inputPanel.SetActive ( false );
            TaskManager.Instance.CompleteTask (); // Proceed to next quest
        } else {
            resultText.text = "Wrong Code!";
        }
    }

    private void OnCollisionEnter2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            if ( TaskManager.Instance.IsCurrentTask ( this ) ) {

                if ( xRequired && playerController.GetAnimHorizontal () >= xPosRequired ) {
                    interactBtn.SetActive ( true );
                    interactBtn.GetComponent<Button> ().onClick.RemoveAllListeners (); // Clear previous
                    interactBtn.GetComponent<Button> ().onClick.AddListener ( Interact ); // Assign this Task's Interact

                    submitButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
                    submitButton.GetComponent<Button> ().onClick.AddListener ( SubmitCode );

                } else if ( yRequired && playerController.GetAnimVertical () >= yPosRequired ) {
                    interactBtn.SetActive ( true );
                    interactBtn.GetComponent<Button> ().onClick.RemoveAllListeners (); // Clear previous
                    interactBtn.GetComponent<Button> ().onClick.AddListener ( Interact ); // Assign this Task's Interact

                    submitButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
                    submitButton.GetComponent<Button> ().onClick.AddListener ( SubmitCode );

                } else {
                    interactBtn.SetActive ( false );
                }
            }
        }
    }

    private void OnCollisionStay2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {

            if ( TaskManager.Instance.IsCurrentTask ( this ) ) {
                if ( xRequired && playerController.GetAnimHorizontal () >= xPosRequired ) {
                    interactBtn.SetActive ( true );
                    interactBtn.GetComponent<Button> ().onClick.RemoveAllListeners (); // Clear previous
                    interactBtn.GetComponent<Button> ().onClick.AddListener ( Interact ); // Assign this Task's Interact

                    submitButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
                    submitButton.GetComponent<Button> ().onClick.AddListener ( SubmitCode );

                } else if ( yRequired && playerController.GetAnimVertical () >= yPosRequired ) {
                    interactBtn.SetActive ( true );
                    interactBtn.GetComponent<Button> ().onClick.RemoveAllListeners (); // Clear previous
                    interactBtn.GetComponent<Button> ().onClick.AddListener ( Interact ); // Assign this Task's Interact

                    submitButton.GetComponent<Button> ().onClick.RemoveAllListeners ();
                    submitButton.GetComponent<Button> ().onClick.AddListener ( SubmitCode );

                } else {
                    interactBtn.SetActive ( false );
                }
            }
        }
    }

    private void OnCollisionExit2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            interactBtn.SetActive ( false );
        }
    }
}
