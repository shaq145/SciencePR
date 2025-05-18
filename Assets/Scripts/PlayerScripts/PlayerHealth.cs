using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour {

    [SerializeField] private Image healthBarImage;
    [SerializeField] private TextMeshProUGUI healthText;

    [Header ( "Health Settings" )]
    public int curHealth;
    public int maxHealth;

    [Header ( "Smooth Transition" )]
    public float healthBarSpeed = 5f; // You can tweak this in the Inspector

    private float targetFill;

    void Start () {
        targetFill = ( float ) curHealth / maxHealth;
        healthBarImage.fillAmount = targetFill;
    }

    void Update () {
        targetFill = ( float ) curHealth / maxHealth;

        // Smoothly transition the fillAmount to the target
        if ( Mathf.Abs ( healthBarImage.fillAmount - targetFill ) > 0.001f ) {
            healthBarImage.fillAmount = Mathf.Lerp ( healthBarImage.fillAmount, targetFill, Time.deltaTime * healthBarSpeed );
        } else {
            healthBarImage.fillAmount = targetFill; // Snap to exact when very close
        }

        // Optional: Color based on current health
        healthBarImage.color = Color.Lerp ( Color.red, Color.green, targetFill );

        // Update text
        healthText.text = "HP: " + curHealth + "/" + maxHealth;
    }
}