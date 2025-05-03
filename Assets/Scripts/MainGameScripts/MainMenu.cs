using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu: MonoBehaviour {

    public string gameScene;

    // Use this for initialization
    void Start () {
        Screen.SetResolution (1920, 1080, true);
    }

    // Update is called once per frame
    void Update () {

    }

    public void Play () {
        SceneManager.LoadScene (gameScene);
    }

    public void Quit () {
        Application.Quit ();
    }
}
