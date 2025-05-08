using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour   {

    [SerializeField]
    private Image healthBarImage;
    public int curHealth;
    public int maxHealth;
    public TextMeshProUGUI healthText;

    void Update () {
        float value = (float) curHealth / maxHealth;

        healthBarImage.fillAmount = value;
        healthBarImage.color = Color.Lerp (Color.red, Color.green, value);
        healthText.text = "HP: " + curHealth + "/" + maxHealth;
    }
}
