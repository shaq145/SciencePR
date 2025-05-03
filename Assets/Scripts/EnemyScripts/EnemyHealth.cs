using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    [SerializeField]
    private Image healthBarImage;
    public Enemies theHealth;

    void Update () {
        float value = (float) theHealth.curHealth / theHealth.maxHealth;

        healthBarImage.fillAmount = value;
        healthBarImage.color = Color.Lerp (Color.red, Color.green, value);
    }
}
