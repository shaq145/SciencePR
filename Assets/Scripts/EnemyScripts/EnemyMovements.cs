using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovements : MonoBehaviour {

    public bool move;

    [Header ("Movement Distance")]
    public float stoppingDistance;
    public float retreatDistance;

    public Transform player;
    private float speed;
    private Enemies parentEnemy;

    // Start is called before the first frame update
    void Start() {
        parentEnemy = GetComponentInChildren<Enemies> ();
        player = GameObject.FindGameObjectWithTag ("Player").transform;
    }

    // Update is called once per frame
    void Update() {
        if ( move ) {
            speed = parentEnemy.speed;
            if ( Vector2.Distance (transform.position, player.position) > stoppingDistance ) {
                transform.position = Vector2.MoveTowards (transform.position, player.position, speed * Time.deltaTime);
            } else if ( (Vector2.Distance (transform.position, player.position) < stoppingDistance) && (Vector2.Distance (transform.position, player.position) > retreatDistance) ) {
                transform.position = this.transform.position;
            } else if ( Vector2.Distance (transform.position, player.position) < retreatDistance ) {
                transform.position = Vector2.MoveTowards (transform.position, player.position, -speed * Time.deltaTime);
            }
        } else {
            speed = 0f;
        }
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ( other.CompareTag ("Player") ) {
            move = true;
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if ( other.CompareTag ("Player") ) {
            move = false;
        }
    }
}
