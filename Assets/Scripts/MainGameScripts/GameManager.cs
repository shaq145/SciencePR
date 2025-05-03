using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public GameObject gameOver;
    public PlayerController player;

    // Start is called before the first frame update
    void Start() {
        player = FindObjectOfType<PlayerController> ();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public void Reset () {
        player.gameObject.SetActive (false);
        gameOver.SetActive (true);
    }

    public void Restart () {
        player.curHealth = player.maxHealth;
        player.gameObject.SetActive (true);

        gameOver.SetActive (false);
    }

    public void BTS () {
        SceneManager.LoadScene (1);
    }
}
