using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;

    public int curHealth;
    public int maxHealth;

    public Rigidbody2D rgbd2d;
    public Animator animator;

    public VirtualJoystick joyStick;
    public Animation hurtAnimation;

    [Header ("Screen Shake")]
    public float camShakeAmt;
    public float camShakeLength;

    private SpriteRenderer spriteRenderer;
    private GameManager theGameManager;
    private ScreenShake screenShake;

    private Vector3 movement;

    // Start is called before the first frame update
    void Start() {
        rgbd2d = GetComponent<Rigidbody2D> ();
        animator = GetComponent<Animator> ();
        //joyStick = FindObjectOfType<VirtualJoystick> ();
        screenShake = FindObjectOfType<ScreenShake> ();
        theGameManager = FindObjectOfType<GameManager> ();
        spriteRenderer = GetComponent<SpriteRenderer> ();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update() {
        movement = Vector3.zero;
        movement.x = joyStick.HorizontalRaw ();
        movement.y = joyStick.VerticalRaw ();

        if ( movement != Vector3.zero ) {
            animator.SetFloat ("Horizontal", movement.x);
            animator.SetFloat ("Vertical", movement.y);
            animator.SetFloat ("Speed", movement.sqrMagnitude);
        } else {
            animator.SetFloat ("Speed", 0);
        }

        if ( curHealth <= 0 ) {
            theGameManager.Reset ();
        }
    }

    void FixedUpdate () {
        rgbd2d.MovePosition (transform.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    public void Damage (int dmg) {
        //hurtAnimation.Play ("Player_Hurt");
        screenShake.Shake (camShakeAmt, camShakeLength);
        curHealth -= dmg;
    }

    public void AddHealth (int health) {
        if ( curHealth < 20 ) {
            curHealth += health;
        }
    }

    private void OnTriggerEnter2D ( Collider2D other ) {
        if ( other.CompareTag ( "NPC" ) ) {
            spriteRenderer.sortingOrder = 7;
        }
    }
    private void OnTriggerStay2D ( Collider2D other ) {
        if ( other.CompareTag ( "NPC" ) ) {
            spriteRenderer.sortingOrder = 7;
        }
    }
    private void OnTriggerExit2D ( Collider2D other ) {
        if ( other.CompareTag ( "NPC" ) ) {
            spriteRenderer.sortingOrder = 5;
        }
    }

    public float GetAnimHorizontal () {
        return animator.GetFloat ( "Horizontal" );
    }

    public float GetAnimVertical () {
        return animator.GetFloat ( "Vertical" );
    }
}
