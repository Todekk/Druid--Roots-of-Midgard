using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [System.Serializable]
    public struct Ability
    {
        public Image abilityImage;
        public KeyCode keyCode;
        [HideInInspector]
        public bool isCooldown;
        public float cooldown;
    }

    [Header("Abilities")]
    public Ability[] abilities;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Ability ability in abilities)
        {
            ability.abilityImage.fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!PauseMenuScript.isPaused)
        {
            for (int i = 0; i < abilities.Length; i++)
            {
                Ability ability = abilities[i];
                if (Input.GetKeyDown(ability.keyCode) && ability.isCooldown == false)
                {
                    ability.isCooldown = true;
                    ability.abilityImage.fillAmount = 1;
                }
                if (ability.isCooldown)
                {
                    ability.abilityImage.fillAmount -= 1 / ability.cooldown * Time.deltaTime;
                    if (ability.abilityImage.fillAmount <= 0)
                    {
                        ability.abilityImage.fillAmount = 0;
                        ability.isCooldown = false;
                    }
                }
                abilities[i] = ability;
            }
        }
    }
}
