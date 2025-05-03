using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootController: MonoBehaviour {

    public float speed;
    public int damage;
    public float destroyTimer;
    public GameObject bulletExplode;

    private Rigidbody2D rgbd2D;

    void Start () {
        rgbd2D = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
        transform.Translate (Vector3.right * Time.deltaTime * speed);
        Destroy (gameObject, destroyTimer);
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ( other.CompareTag ("Player") ) {
            PlayerController player = other.gameObject.GetComponent<PlayerController> ();
            player.Damage (damage);
            Instantiate (bulletExplode, transform.position, transform.rotation);
            Destroy (gameObject);
        }
    }
}
