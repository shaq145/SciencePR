using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour {

    [Header ( "DIALOGUE MANAGER" )]
    public int dialogueCounter;

    [Header ( "QUESTION MANAGER" )]
    public bool doneQuestion;

    [Header ("Boss Stats")]
    public bool boss;
    public bool hasWeakness;
    public string weakness;
    public GameObject gate;
    //public TextPopup popupText;
    public Transform popupTextPos;

    public bool move;
    public bool target;
    public bool destroyable;

    [Header ("Enemy Stats")]
    public int curHealth;
    public int maxHealth;
    public float speed;
    private float speedThreshold;

    [Header ("Movement Distance")]
    public float stoppingDistance;
    public float retreatDistance;

    [Header ("Screen Shake")]
    public float camShakeAmt;
    public float camShakeLength;

    public GameObject targetIndicator;
    public GameObject healthBar;
    public Transform player;
    //public ItemsPickup itemPickup;
    public GameObject deathParticle;

    [Header ("Health Potion")]
    private int randomDropPotion;
    public GameObject healthPotion;

    [Header ( "Capture Settings" )]
    public float shrinkSpeed = 0.2f;
    public float growSpeed = 0.3f;
    public float struggleIntensity = 0.5f;
    public float minCaptureRange = 1f;
    public float maxCaptureRange = 2f;
    public float failRange = 3f;
    public bool isCapturing = false;

    private ScreenShake screenShake;
    private EnemyWeapons theEnemyWeapons;
    private QuestionManager questionManager;
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start() {
        curHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        speedThreshold = speed;
        screenShake = FindObjectOfType<ScreenShake> ();
        if ( !destroyable ) {
            theEnemyWeapons = GetComponentInChildren<EnemyWeapons> ();
        }

        if ( !boss ) {
            popupTextPos = null;
        } else {
            popupTextPos = transform.GetChild (1);
        }

        questionManager = FindAnyObjectByType<QuestionManager> ();
        dialogueManager = FindAnyObjectByType<DialogueManager> ();
    }

    // Update is called once per frame
    void Update() {
        if ( move ) {
            speed = speedThreshold;
            if ( Vector2.Distance (transform.position, player.position) > stoppingDistance ) {
                transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
            } else if ( Vector2.Distance (transform.position, player.position) < stoppingDistance && Vector2.Distance (transform.position, player.position) > retreatDistance ) {
                transform.position = this.transform.position;
            } else if ( Vector2.Distance (transform.position, player.position) < retreatDistance ) {
                transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
            }
        } else {
            speed = 0f;
        }

        if ( target ) {
            targetIndicator.SetActive (true);
            healthBar.SetActive (true);
        } else {
            targetIndicator.SetActive (false);
            healthBar.SetActive (false);
            isCapturing = false;
        }

        if ( curHealth <= 0 ) {
            if ( !doneQuestion ) {
                questionManager.StartQuestions ();
            }
            theEnemyWeapons.attack = false;
            move = false;
        }

        if ( isCapturing ) {
            float distance = Vector3.Distance ( player.position, transform.position );

            if ( distance <= maxCaptureRange ) // Player is within capturing range
            {
                ShrinkBoss ();
            } else if ( distance >= failRange ) // Player is too far
              {
                GrowBoss ();
            }

            StruggleMovement ();
        }
    }

    public void StartCapture () {
        isCapturing = true;
        move = false;
    }

    public void StopCapture () {
        isCapturing = false;
    }

    private void ShrinkBoss () {
        transform.localScale -= Vector3.one * shrinkSpeed * Time.deltaTime;
        transform.localScale = new Vector3 (
            Mathf.Max ( transform.localScale.x, 0f ),
            Mathf.Max ( transform.localScale.y, 0f ),
            Mathf.Max ( transform.localScale.z, 0f )
        );

        if ( transform.localScale.x <= 0.1f ) // Capture success
        {
            CaptureSuccess ();
        }
    }

    private void GrowBoss () {
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        transform.localScale = new Vector3 (
            Mathf.Min ( transform.localScale.x, 4.957523f ),
            Mathf.Min ( transform.localScale.y, 4.957523f ),
            Mathf.Min ( transform.localScale.z, 4.957523f )
        );

        if ( transform.localScale.x >= 4.957523f ) // Capture fails
        {
            CaptureFailed ();
        }
    }

    private void StruggleMovement () {
        Vector2 struggleForce = new Vector2 ( Random.Range ( -1f, 1f ), Random.Range ( -1f, 1f ) ) * struggleIntensity;
        transform.position += ( Vector3 ) struggleForce * Time.deltaTime;
    }

    private void CaptureSuccess () {
        Debug.Log ( "Capture Successful!" );
        isCapturing = false;

        Instantiate ( deathParticle, transform.position, transform.rotation );
        screenShake.Shake ( camShakeAmt, camShakeLength );
        gameObject.SetActive ( false );

        dialogueManager.counter = dialogueCounter;
        dialogueManager.StartDialogue ();
    }

    private void CaptureFailed () {
        Debug.Log ( "Capture Failed! Boss escaped." );
        isCapturing = false;
        transform.localScale += Vector3.one * growSpeed * Time.deltaTime;
        transform.localScale = new Vector3 (
            Mathf.Min ( transform.localScale.x, 4.957523f ),
            Mathf.Min ( transform.localScale.y, 4.957523f ),
            Mathf.Min ( transform.localScale.z, 4.957523f )
        );
    }


    public void Damage (int dmg) {
        if ( !boss ) {
            curHealth -= dmg;
        } else {
            curHealth -= dmg;
            randomDropPotion = Random.Range (0, 4);
            DropPotion (randomDropPotion);

            //popupText.StartPopup (dmg.ToString ());
            //GameObject popupClone = Instantiate (popupText.gameObject, popupTextPos.position, popupTextPos.rotation) as GameObject;
            //popupClone.GetComponent<TextPopup> ().theTextPopup.color = Color.red;
            //popupClone.transform.parent = popupTextPos.transform;
        }
    }

    public void DropPotion (int randomDrop) {
        if ( !boss ) {
            if ( randomDrop == 2 ) {
                Instantiate (healthPotion, new Vector3 (transform.position.x + 0.5f, transform.position.y - 0.3f, transform.position.z), transform.rotation);
                Instantiate (healthPotion, new Vector3 (transform.position.x - 0.5f, transform.position.y + 0.3f, transform.position.z), transform.rotation);
            } else if ( randomDrop == 3 ) {
                Instantiate (healthPotion, new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
                Instantiate (healthPotion, new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation);
                Instantiate (healthPotion, new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z), transform.rotation);
            } else {
                Instantiate (healthPotion, transform.position, transform.rotation);
            }
        } else {
            if ( randomDrop == 2 ) {
                screenShake.Shake (camShakeAmt, camShakeLength);
                Instantiate (healthPotion, new Vector3 (transform.position.x + 0.5f, transform.position.y - 0.3f, transform.position.z), transform.rotation);
                Instantiate (healthPotion, new Vector3 (transform.position.x - 0.5f, transform.position.y + 0.3f, transform.position.z), transform.rotation);
            } else if ( randomDrop == 3 ) {
                screenShake.Shake (camShakeAmt, camShakeLength);
                Instantiate (healthPotion, new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z), transform.rotation);
                Instantiate (healthPotion, new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z), transform.rotation);
                Instantiate (healthPotion, new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z), transform.rotation);
            } else if ( randomDrop == 1 ) {
                screenShake.Shake (camShakeAmt, camShakeLength);
                Instantiate (healthPotion, transform.position, transform.rotation);
            }
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ( other.CompareTag ("Player") ) {
            if ( !destroyable ) {
                move = true;
                if ( curHealth <= 0 ) {
                    theEnemyWeapons.attack = false;
                } else {
                    theEnemyWeapons.attack = true;
                }
                
            }
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if ( other.CompareTag ("Player") ) {
            if ( !destroyable ) {
                move = false;
                theEnemyWeapons.attack = false;
            }
        }
    }
}
