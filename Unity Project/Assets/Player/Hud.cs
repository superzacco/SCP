using System;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Image blinkBar;
    public Image sprintBar;

    public Sprinting sprinting;
    public Blinking blinking;

    private void Update()
    {
        CalculateSprintAmount();
        CalculateBlinkAmount();
    }   

    void CalculateSprintAmount()
    {
        float sprintAmtTemp = sprinting.currentStamina / 100;
        float sprintAmount = MathF.Round(sprintAmtTemp / .05f) * .05f;

        sprintBar.fillAmount = sprintAmount;
    }

    void CalculateBlinkAmount()
    {
        float blinkAmtTemp = blinking.eyeWetnessTehee / 100;
        float blinkAmount = MathF.Round(blinkAmtTemp / .05f) * .05f;

        blinkBar.fillAmount = blinkAmount;
    }
}