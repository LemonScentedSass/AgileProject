using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Level System tracks players current xp and level, adds experience
/// when called and calculates xp to next level. When an enemy dies,
/// 
/// LevelSystem.instance.AddExperience(experience);
/// Destroy(gameObject);
/// 
/// Will add xp to the player.
/// </summary>
public class LevelSystem : MonoBehaviour
{
      public static LevelSystem instance;                               // Can be referenced from other scripts

      public int level = 0;                                             // Player current level
      public int experience;                                            // Player current EXP
      public int experienceToNextLevel;                                 // EXP to next level

      public TMPro.TMP_Text[] LevelTXT;
      public TMPro.TMP_Text[] ExpTXT;
      public UnityEngine.UI.Image[] ExpSlider;
      public bool addingExperience;
    public float check;

      private void Awake()
      {
            if (instance != null)                                       // Check for more than one player with LevelSystem script
            {
                  Debug.Log("More than one LevelSystem in scene.");
                  return;
            }

            instance = this;                                            // instance variable = this object.
            SetLevel(1);                                             // Set Level at 1 upon startup.
            check = experience;
      }

    private void Update()
    {
        UpdateSlider();
    }


    /// <summary>
    /// AddExperience() will add the value of the parameter passed
    /// to the players current xp amount. Checks for level up and 
    /// calls the SetLevel function if true.
    /// </summary>
    /// <param name="experienceToAdd"></param>
    /// <returns></returns>
    public bool AddExperience(int experienceToAdd)
      {
            experience += experienceToAdd;

            if (experience >= experienceToNextLevel)
            {
                  SetLevel(level + 1);
                  UpdateText();

            return true;
            }

            return false;
      }


      /// <summary>
      /// SetLevel calculates the amount of xp needed per level after updating
      /// the current player level accordingly. 
      /// </summary>
      /// <param name="value"></param>
      private void SetLevel(int value)
      {
            this.level = value;
            experience = experience - experienceToNextLevel;
            experienceToNextLevel = (int)(50f * (Mathf.Pow(level + 1, 2) - (5 * (level + 1)) + 8));

      }

    private void UpdateText()
    {
        for (int i = 0; i < LevelTXT.Length; i++)
        {
            LevelTXT[i].text = "Level: " + level;
        }

        for (int i = 0; i < ExpTXT.Length; i++)
        {
            ExpTXT[i].text = "EXP: " + experience + "/" + experienceToNextLevel;
        }
    }

    private void UpdateSlider()
    {
        foreach (var slider in ExpSlider)
        {
            if(slider.fillAmount != (float)experience / (float)experienceToNextLevel)
            {
                slider.fillAmount = Mathf.Lerp(slider.fillAmount, ((float)experience / (float)experienceToNextLevel), 0.005f);
            }

            if(slider.fillAmount >= (float)experience / (float)experienceToNextLevel - 0.005)
            {
                slider.fillAmount = (float)experience / (float)experienceToNextLevel;
            }
        }

    }

}
