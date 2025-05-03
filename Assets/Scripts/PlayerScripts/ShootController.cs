using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController: MonoBehaviour {
    
    public float speed;
    public int damage;
    public int weaknessDmg;
    public float destroyTimer;
    public GameObject bulletExplode;

    private Rigidbody2D rgbd2D;

    public Transform projectileVisual;
    public float spinSpeed;

    void Start () {
        rgbd2D = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update () {
        transform.Translate (Vector3.right * Time.deltaTime * speed);
        Destroy (gameObject, destroyTimer);

        projectileVisual.Rotate ( 0, 0, spinSpeed * Time.deltaTime );
    }

    void OnCollisionEnter2D (Collision2D other) {
        if ( other.gameObject.tag == "Enemies" ) {
            Enemies enemy = other.gameObject.GetComponent<Enemies> ();
            if ( !enemy.boss ) {
                enemy.Damage (damage);
                Destroy (gameObject);
            } else {
                if ( enemy.hasWeakness && enemy.weakness.Equals (this.gameObject.name) ) {
                    enemy.Damage (weaknessDmg);
                    Destroy (gameObject);
                } else {
                    enemy.Damage (damage);
                    Destroy (gameObject);
                }
            }
        }

        //} else if (other.gameObject.tag == "FireObject") {
        //    if ( water ) {
        //        Enemies enemy = other.gameObject.GetComponent<Enemies> ();
        //        enemy.Damage (damage);
        //        Destroy (gameObject);
        //    } else {
        //        Destroy (gameObject);
        //    }
        //}

        Instantiate (bulletExplode, transform.position, transform.rotation);
        Destroy (gameObject);
    }
}
