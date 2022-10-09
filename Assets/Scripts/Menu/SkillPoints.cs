using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillPoints : Selectable
{
    //GOES ONTO EACH SKILL NODE
    public TMP_Text nameTXT;
    public string SkillName;
    public TMP_Text descriptionTXT;
    public string skillDescription;
    public int SkillPointRequirement = 1;
    public GameObject SkillDescriptionGO;
    public Slider previousSlider;
    private bool spentSkill = false;
    private bool buttonpress = false;
    public bool previousNode = false;

    public GameObject[] NextSkill;
    public bool skillRequirements;

    public GameObject UnlockButton;
    Button button;
    Image selectedIcon;
    public Sprite notSelected;
    public Sprite Selected;

    public SkillPoints PreviousNodeScript;



    private bool selected = false;


    // Start is called before the first frame update
    void Start()
    {
        previousSlider.interactable = false;
        descriptionTXT.text = skillDescription;
        nameTXT.text = SkillName;
        UnlockButton.SetActive(false);
        previousSlider.value = 0;
    }

    // Update is called once per frame
    void Update()
    { 
       if(IsHighlighted() == true | selected == true)
       {
            descriptionTXT.text = skillDescription;
            nameTXT.text = SkillName;
            Debug.Log(SkillName);
            SkillDescriptionGO.SetActive(true);
       }
       else
       {
            SkillDescriptionGO.SetActive(false);
       }

       
       if(IsHighlighted() != true && buttonpress == false)
       {
            if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
            {
                selected = false;
            }
       }
       

       if(IsPressed() == true)
       {
            ClickedSkill();
       }


        if (spentSkill == true)
        {
            previousSlider.value = Mathf.Lerp(0, 1, 0.0002f);
            if (previousSlider.value > previousSlider.value - 0.1f)
            {
                previousSlider.value = previousSlider.maxValue;
                spentSkill = false;
            }
        }
       

    }




    //Clicks SkillNode
    public void ClickedSkill()
    {
        selected = true;

        if(skillRequirements == true)
        {
            if(previousSlider == null)
            {
                //shows unlock button
                UnlockButton.SetActive(true);
            }
            else
            {
                if(PreviousNodeScript.previousNode == true)
                {
                    UnlockButton.SetActive(true);
                }
            }
           
        }
        

    }

    public  void UnlockSkill(Image clickedButton)
    {
        previousNode = true;
        buttonpress = true;
        //Unlock skill, fill slider if one, and unlock disappears
        UnlockButton.GetComponentInChildren<TMP_Text>().text = "Unlocked";
        if (selectedIcon != null)
        {
            selectedIcon.sprite = notSelected;
        }
        selectedIcon = clickedButton;
        selectedIcon.sprite = Selected;

        //fill slider
        if (previousSlider != null)
        {
            spentSkill = true;
        }
        UnlockButton.GetComponent<Button>().interactable = false;
        buttonpress = false;




    }

}
