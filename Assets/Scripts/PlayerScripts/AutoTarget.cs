using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoTarget: MonoBehaviour {

    [Header ("Auto Target/Finding Closest Enemy")]
    public bool find;
    public List<Enemies> enemies = new List<Enemies> ();

    [Header ("Enemy Found")]
    public int enemyCount;
    public Enemies closestTargetEnemy;
    public CircleCollider2D searchCollider;

    [Header ("DestroyableObject")]
    public int destroyableObjectCount;
    public GameObject destroyableObject;
    public bool foundDestroyable;

    [Header ("Shooting")]
    public GameObject attackBtn;
    public GameObject captureBtn;
    public ShootController bulletPrefab;
    public string bulletName;

    [Header ( "Capture Mode" )]
    public bool isCapturing = false;
    public GameObject captureWave;


    //private InventoryManager theInventoryManager;

    void Start () {
        //theInventoryManager = FindObjectOfType<InventoryManager> ();
    }

    // Update is called once per frame
    void Update () {
        if ( find ) {
            float distanceToClosestEnemy = searchCollider.radius * 4.5f;
            foreach ( Enemies currentEnemy in enemies ) {
                float distanceToEnemy = (currentEnemy.transform.position - this.transform.position).sqrMagnitude;
                if ( distanceToEnemy < distanceToClosestEnemy ) {
                    distanceToClosestEnemy = distanceToEnemy;
                    closestTargetEnemy = currentEnemy.gameObject.GetComponent<Enemies> ();
                    RotateTowards (closestTargetEnemy.gameObject.transform.position);
                }
            }

            for ( int i = 0; i < enemies.Count; i++ ) {
                if ( enemies [ i ] == closestTargetEnemy ) {
                    if ( !enemies [ i ].destroyable ) {
                        enemies [ i ].target = true;
                    }
                } else {
                    if ( !enemies [ i ].destroyable ) {
                        enemies [ i ].target = false;
                    }
                }
            }
        }

        if ( foundDestroyable ) {
            if ( destroyableObjectCount != 0 ) {
                RotateTowards (destroyableObject.gameObject.transform.position);
            }
        }


        if ( enemyCount != 0 ) {
            find = true;
            if ( closestTargetEnemy != null ) {
                if ( closestTargetEnemy.curHealth >= 0 ) {
                    attackBtn.SetActive ( true );
                    captureBtn.SetActive ( false );
                } else {
                    attackBtn.SetActive ( false );
                    captureBtn.SetActive ( true );

                }
            }
        } else if ( destroyableObjectCount != 0 ) {
            foundDestroyable = true;
            attackBtn.SetActive (true);
        } else {
            find = false;
            foundDestroyable = false;
            attackBtn.SetActive (false);
            captureBtn.SetActive ( false );
            closestTargetEnemy = null;
            isCapturing = false;
        }

        if ( isCapturing ) {
            captureWave.SetActive ( true );
        } else {
            captureWave.SetActive ( false );
        }
    }

    public void StartCapture () {
        closestTargetEnemy.isCapturing = true;
        isCapturing = true;
    }

    public void StopCapture () {
        closestTargetEnemy.isCapturing = false;
        isCapturing = false;
    }

    private void RotateTowards (Vector2 target) {
        var offset = 0f;
        Vector2 direction = target - (Vector2) transform.position;
        direction.Normalize ();
        float angle = Mathf.Atan2 (direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler (Vector3.forward * (angle + offset));
    }

    public void Attack () {
        //bulletPrefab.name = bulletName;
        GameObject bullet = (GameObject) Instantiate (bulletPrefab.gameObject, transform.position, transform.rotation);
        bullet.name = bulletName;
    }



    void OnTriggerEnter2D (Collider2D other) {
        if ( other.gameObject.CompareTag ("Enemies") && !other.isTrigger ) {
            enemyCount += 1;
            destroyableObjectCount = 0;
            destroyableObject = null;
            enemies.Add (other.gameObject.GetComponent<Enemies> ());
        } else if ( other.gameObject.CompareTag ("DestroyableObject") && !other.isTrigger && enemyCount == 0 ) {
            destroyableObjectCount += 1;
            destroyableObject = other.gameObject;
        }
    }

    void OnTriggerExit2D (Collider2D other) {
        if ( other.gameObject.CompareTag ("Enemies") && !other.isTrigger ) {
            enemyCount -= 1;
            other.gameObject.GetComponent<Enemies> ().target = false;
            enemies.Remove (other.gameObject.GetComponent<Enemies> ());
        } else if ( other.gameObject.CompareTag ("DestroyableObject") && !other.isTrigger && enemyCount == 0 ) {
            if ( destroyableObjectCount <= 0 ) {
                destroyableObjectCount = 0;
                destroyableObject = null;
            } else {
                destroyableObjectCount -= 1;
            }
        }
    }
}
