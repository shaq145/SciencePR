using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BattleInitiator : MonoBehaviour {

    public string sceneToAdd;

    public GameObject stageParent;
    public GameObject startBattleBtn;
    public GameObject enemy;

    public GameObject controls;
    public GameObject fadeOutObject;
    public Animation fadeOut;

    private void OnCollisionEnter2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            startBattleBtn.SetActive ( true );
        }
    }

    private void OnCollisionStay2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            startBattleBtn.SetActive ( true );
        }
    }

    private void OnCollisionExit2D ( Collision2D other ) {
        if ( other.gameObject.CompareTag ( "Player" ) ) {
            startBattleBtn.SetActive ( false );
        }
    }

    public void StartBattle () {
        StartCoroutine ( BattleStart () );
    }

    IEnumerator BattleStart () {
        fadeOutObject.SetActive ( true );
        fadeOut.Play ( "BattleTransition" );
        yield return new WaitForSeconds ( 1f );
        stageParent.SetActive ( false );
        SceneManager.LoadSceneAsync ( sceneToAdd, LoadSceneMode.Additive );
        controls.SetActive  ( false );
        fadeOutObject.SetActive ( false );
    }
}
