using UnityEngine;

public class Sprinting : MonoBehaviour
{
    public Walking walkingScript;

    public float sprintSpeed;

    public float currentStamina;
    public float maxStamina;

    public float staminaLossRate;
    public float staminaRegenRate;

    public float timeBeforeRegen;
    public float timeBetweenRegenTick;

    private void Start()
    {
        currentStamina = maxStamina;   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CancelInvoke("RegenStamina");

            walkingScript.moveSpeed = sprintSpeed;
            LoseStamina();
        }

        if (currentStamina <= 0f)
        {
            walkingScript.moveSpeed = walkingScript.defaultMoveSpeed;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            CancelInvoke("LoseStamina");

            walkingScript.moveSpeed = walkingScript.defaultMoveSpeed;
            Invoke("RegenStamina", timeBeforeRegen); 
        }
    }
    
    void LoseStamina()
    {
        currentStamina -= staminaLossRate;

        //continue if stamina is left
        if (currentStamina > 0f)
        {
            Invoke("LoseStamina", 0.1f);
        }

        //stop if no stamina
        if (currentStamina <= 0f)
        {
            CancelInvoke("LoseStamina");

            currentStamina = 0f;
        }
    }

    void RegenStamina()
    {
        currentStamina += staminaRegenRate;

        //keep adding if it's not at max
        if (currentStamina < maxStamina)
        {
            Invoke("RegenStamina", timeBetweenRegenTick);
        }

        //stop if at max
        if (currentStamina >= maxStamina)
        {
            currentStamina = maxStamina;
            CancelInvoke("RegenStamina");
        }
    }
}