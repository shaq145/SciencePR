using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour   {

    [SerializeField]
    private Image healthBarImage;
    public PlayerController theHealth;
    public TextMeshProUGUI healthText;

    void Update () {
        float value = (float) theHealth.curHealth / theHealth.maxHealth;

        healthBarImage.fillAmount = value;
        healthBarImage.color = Color.Lerp (Color.red, Color.green, value);
        healthText.text = "HP: " + theHealth.curHealth + "/" + theHealth.maxHealth;
    }
}
