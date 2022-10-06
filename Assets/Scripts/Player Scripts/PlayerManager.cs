using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float _curHealth;
    [SerializeField] private float _maxHealth = 100f;

    [SerializeField] private float _curStamina;
    [SerializeField] private float _maxStamina = 100f;

    [SerializeField] private float potionHeal = 40f;
    public float newHealth = 0;

    private float _MAXFILLAMOUNT = 0.9f;

    public bool healthRegen = false;
    public bool stamRegen = false;

    public Image healthbar;
    public Image staminabar;

    public float staminaFILLAMOUNT;
    public float healthFILLAMOUNT;

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float MaxStamina { get { return _maxStamina; } set { _maxStamina = value; } }
    public float CurrentHealth { get { return _curHealth; } set { _curHealth = value; } }
    public float CurrentStamina { get { return _curStamina; } set { _curStamina = value; } }

    //MAKE SINGLETON

    // Start is called before the first frame update
    void Start()
    {
        //Sets current health and stamina to max
        _curHealth = _maxHealth;
        _curStamina = _maxStamina;

        //sets the healthbar and staminabar to max
        healthbar.fillAmount = _MAXFILLAMOUNT;
        staminabar.fillAmount = _MAXFILLAMOUNT;
    }

    // Update is called once per frame
    void Update()
    {
        //Displays current fill amount
        staminaFILLAMOUNT = DamageConversionStamina(CurrentStamina);
        healthFILLAMOUNT = DamageConversionHealth(CurrentHealth);

        HealthStaminaConversion(); //Converts health and stamina into fillamount for health and staminabar
        HealthStaminaChecks(); // Checks to make sure numbers dont go below or above 0 and 100


        //Regens player stamina if stamina regen is true
        if (stamRegen == true)
        {
            RegenStamina(0.005f, MaxStamina);

        }
        //Regens player health if health regen is true
        if (healthRegen == true)
        {

            RegenHealth(0.0055f, potionHeal);
        }
    }

    //Call function to take player damage. Insert damage amount
    public void PlayerTakeDamage(float damage)
    {
        CurrentHealth -= damage;
    }

    //Call function to take stamina damage. Insert damage amount
    public void PlayerTakeStaminaDamage(float stamDamage)
    {
        CurrentStamina -= stamDamage;
    }

    //Regens player stamina. Requires lerp steps.
    public void RegenStamina(float steps, float regenAmount)
    {
        //sets a regeneration amount to 0
        float _Regeneration = 0;

        if (_Regeneration == 0)
        {
            //if 0, regeneration = regen amount and current stamina
            _Regeneration = regenAmount + CurrentStamina;
        }
        //if regeneration is greater or equal to the max stamina set the regeneration to max stamina
        if (_Regeneration >= MaxStamina)
        {
            _Regeneration = MaxStamina;
        }

        //Lerps Current stamina by regeneration amount
        CurrentStamina = Mathf.Lerp(CurrentStamina, _Regeneration, steps);
        Debug.Log("fill stamina");

        //if current stamina is close to regeneration
        if (CurrentStamina >= _Regeneration - 1)
        {
            CurrentStamina = _Regeneration;
        }

        //if current stamina equals regeneration value
        if (CurrentStamina == _Regeneration)
        {
            //and if current is greater or equal to max stamina
            if (CurrentStamina >= MaxStamina)
            {
                CurrentStamina = MaxStamina;
            }

            //Turns off stamina regeneration bool if current stamina is regenerated
            stamRegen = false;
        }

    }


    //Regens player health. Requires the lerp step and healthamount
    public void RegenHealth(float steps, float regenAmount)
    {

        //if amount = 0, make regeneration amount equal to regen amount and current health
        if (newHealth == 0)
        {
            newHealth = regenAmount + CurrentHealth;
            Debug.Log(newHealth);
        }

        //if the regeneration amount is greater than the max health
        if (newHealth >= MaxHealth)
        {
            newHealth = MaxHealth;
        }

        //Lerps Current health by regeneration amount
        CurrentHealth = Mathf.Lerp(CurrentHealth, newHealth, steps);
        Debug.Log("fill health!");

        //if current health is close to potion heal just make it equal
        if (CurrentHealth >= newHealth - 1)
        {
            CurrentHealth = newHealth;
            Debug.Log("edging");
        }

        //if current health equals regeneration value
        if (CurrentHealth == newHealth)
        {
            //and if current health is greater or equal to max health
            if (CurrentHealth >= MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }
            else
            {
                newHealth = 0;
                //Turns off health regeneraton bool if current health is regenerated
                healthRegen = false;
            }

        }
    }

    //Converts regular values into values that can be used for the health and staminabar
    private float DamageConversionHealth(float value)
    {
        var conversion = (value * _MAXFILLAMOUNT) / MaxHealth;

        return conversion;
    }

    private float DamageConversionStamina(float value)
    {
        var conversion = (value * _MAXFILLAMOUNT) / MaxStamina;

        return conversion;
    }

    private void HealthStaminaChecks()
    {
        //Makes sure health doesn't go over max and under 0
        if (CurrentHealth > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        if (CurrentHealth < 0)
        {
            CurrentHealth = 0;
        }

        //Makes sure stamina doesn't go over max and under 0
        if (CurrentStamina > MaxStamina)
        {
            CurrentStamina = MaxStamina;
        }
        if (CurrentStamina < 0)
        {
            CurrentStamina = 0;
        }
    }

    private void HealthStaminaConversion()
    {
        //converts amount and fluidly change health with lerp
        if (healthbar.fillAmount != DamageConversionHealth(CurrentHealth))
        {
            healthbar.fillAmount = Mathf.Lerp(healthbar.fillAmount, DamageConversionHealth(CurrentHealth), 0.005f);
        }

        //converts amount and fluidly change stamina with lerp
        if (staminabar.fillAmount != DamageConversionStamina(CurrentStamina))
        {
            staminabar.fillAmount = Mathf.Lerp(staminabar.fillAmount, DamageConversionStamina(CurrentStamina), 0.005f);
        }
    }

}
