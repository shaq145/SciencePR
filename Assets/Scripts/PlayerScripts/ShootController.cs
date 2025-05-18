using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController: MonoBehaviour {
    
    public float speed;
    public GameObject target;

    private Rigidbody2D rgbd2D;

    public Transform projectileVisual;
    public float spinSpeed;

    private QuestionManager questionManager;

    void Start () {
        rgbd2D = GetComponent<Rigidbody2D> ();
        target = GameObject.FindGameObjectWithTag ( "Enemies" );
        questionManager = FindAnyObjectByType<QuestionManager> ();
    }

    // Update is called once per frame
    void Update () {
        transform.position = Vector3.MoveTowards ( transform.position, target.transform.position, speed * Time.deltaTime );
        projectileVisual.Rotate ( 0, 0, spinSpeed * Time.deltaTime );
    }

    void OnTriggerEnter2D (Collider2D other) {
        if ( other.gameObject.tag == "Enemies" ) {
            questionManager.PerformRangeDamage ();
        }
        Destroy (gameObject);
    }
}
