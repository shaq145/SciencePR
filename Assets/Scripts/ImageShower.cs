using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageShower : MonoBehaviour {

    public bool xRequired;
    public bool yRequired;

    public float xPosRequired;
    public float yPosRequired;

    public GameObject interactBtn;

    private PlayerController playerController;

    // Start is called before the first frame update
    void Start() {
        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnCollisionEnter2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            if ( (xRequired && playerController.GetAnimHorizontal () >= xPosRequired) || ( xRequired && playerController.GetAnimHorizontal () <= xPosRequired ) ) {
                interactBtn.SetActive ( true );

            } else if ( (yRequired && playerController.GetAnimVertical () >= yPosRequired) || ( yRequired && playerController.GetAnimVertical () <= yPosRequired ) ) {
                interactBtn.SetActive ( true );

            } else {
                interactBtn.SetActive ( false );
            }
        }
    }

    private void OnCollisionStay2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            if ( ( xRequired && playerController.GetAnimHorizontal () >= xPosRequired ) || ( xRequired && playerController.GetAnimHorizontal () <= xPosRequired ) ) {
                interactBtn.SetActive ( true );

            } else if ( ( yRequired && playerController.GetAnimVertical () >= yPosRequired ) || ( yRequired && playerController.GetAnimVertical () <= yPosRequired ) ) {
                interactBtn.SetActive ( true );

            } else {
                interactBtn.SetActive ( false );
            }
        }
    }

    private void OnCollisionExit2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            interactBtn.SetActive ( false );
        }
    }
}
