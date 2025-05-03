using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss: MonoBehaviour {

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
    public Transform player;
    //public ItemsPickup itemPickup;
    public GameObject deathParticle;

    [Header ("Health Potion")]
    private int randomDropPotion;
    public GameObject healthPotion;

    private ScreenShake screenShake;
    private EnemyWeapons theEnemyWeapons;

    // Start is called before the first frame update
    void Start () {
        curHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        speedThreshold = speed;
        screenShake = FindObjectOfType<ScreenShake> ();
        if ( !destroyable ) {
            theEnemyWeapons = GetComponentInChildren<EnemyWeapons> ();
        }
    }

    // Update is called once per frame
    void Update () {
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
        } else {
            targetIndicator.SetActive (false);
        }

        if ( curHealth <= 0 ) {
            //int rand = Random.Range (0, itemPickup.pickupItemName.Length);
            //randomDropPotion = Random.Range (1, 4);
            //itemPickup.randomChoose = rand;
            //GameObject newItemPickup = Instantiate (itemPickup.gameObject, transform.position, transform.rotation);
            //newItemPickup.name = itemPickup.pickupItemName [ rand ];
            //DropPotion (randomDropPotion);

            Instantiate (deathParticle, transform.position, transform.rotation);
            screenShake.Shake (camShakeAmt, camShakeLength);
            gameObject.SetActive (false);
        }
    }

    public void Damage (int dmg) {
        curHealth -= dmg;
    }

    public void DropPotion (int randomDrop) {
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
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ( other.CompareTag ("Player") ) {
            if ( !destroyable ) {
                move = true;
                theEnemyWeapons.attack = true;
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
