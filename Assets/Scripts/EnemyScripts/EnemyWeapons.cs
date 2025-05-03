using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapons: MonoBehaviour {

    public float angle;
    public bool attack;
    public GameObject enemyBulletPrefab;

    public float firePerSecond;
    private float firePerSecondThreshold;

    private PlayerController player;

    // Start is called before the first frame update
    void Start () {
        //player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
        //RotateTowards (player.gameObject.transform.position);

        firePerSecondThreshold = firePerSecond;
    }

    // Update is called once per frame
    void Update () {
        if ( player == null ) {
            player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
        }

        if ( attack ) {
            RotateTowards (player.gameObject.transform.position);

            firePerSecond -= Time.deltaTime;

            if ( firePerSecond <= 0 ) {
                Shoot ();
                firePerSecond = firePerSecondThreshold;
            }
        }
    }

    void Shoot () {
        GameObject bullet = (GameObject) Instantiate (enemyBulletPrefab, transform.position, transform.rotation);
    }

    private void RotateTowards (Vector2 target) {
        var offset = 0f;
        Vector2 direction = target - (Vector2) transform.position;
        direction.Normalize ();
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (Vector3.forward * (angle + offset));
    }
}
