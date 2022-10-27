using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SkillPoints : Selectable
{
    //GOES ONTO EACH SKILL NODE
    //INSERT NAME, DESCRIPTION, AND SKILL POINT REQUIREMENT
    //DOESN'T NEED FROM SLIDER ON INITIAL NODE; SLIDER IS ONLY THERE FOR PREVIOUS NODES
    //IF CONTINUING NODE, PUT PREVIOUS NODE'S SCRIPT

    public string SKILLNAME;
    public string SKILLDESCRIPTION;
    public int POINTREQUIREMENT = 1;
    public SkillPoints PREVIOUSNODESCRIPT;

    public TMP_Text nameTXTGO;
    public TMP_Text descriptionTXTGO;
    public TMP_Text requirementTXTGO;

    public GameObject SkillDescriptionGO;
    public Slider UnlockSlider;
    private bool _spentSkill = false;
    public bool previousNode = false;


    public bool skillRequirements = true;

    public GameObject UnlockButton;

    private bool _selected = false;

    [SerializeField] GraphicRaycaster m_raycaster;
    PointerEventData m_PointerEventData;
    [SerializeField] EventSystem m_EventSystem;
    [SerializeField] RectTransform canvasRect;


    // Start is called before the first frame update
    void Start()
    {
        //checks for an unlock slider
        if(UnlockSlider!=null)
        {
            UnlockSlider.interactable = false;
            descriptionTXTGO.text = SKILLDESCRIPTION;
            nameTXTGO.text = SKILLNAME;
            UnlockButton.SetActive(false);
            UnlockSlider.value = 0;
        }
        descriptionTXTGO.text = SKILLDESCRIPTION;
        nameTXTGO.text = SKILLNAME;
        requirementTXTGO.text = "Skill Points: " + POINTREQUIREMENT;


    }

    // Update is called once per frame
    void Update()
    {
        //sets up a raycast to mouseposition and sends results to new list
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        //if selected and left click, raycast and get results
        if(_selected == true && Input.GetMouseButtonDown(0))
        {
            m_raycaster.Raycast(m_PointerEventData, results);
            foreach (RaycastResult result in results)
            {
                //if the raycast sees the Unlock gameobject return
                if(result.gameObject.name == "Unlock")
                {
                    return;
                }
                //if the raycast see the character skills truns selected bool false
               if(result.gameObject.name != "Unlock")
               {
                    _selected = false;
               }
            }
        }

        //Checks if selected is false to turn off the skill description
        if(_selected == false)
        {
            SkillDescriptionGO.SetActive(false);
        }
      
        //flashes description if highlighted, stays if selected bool is true
        if (IsHighlighted() == true | _selected == true)
        {
            SkillDescriptionGO.SetActive(true);
        }

        //once skill node is pressed, call function
       if(IsPressed() == true)
       {
            ClickedSkill();
       }

       //Player upgrades and slider fills
        if (_spentSkill == true)
        {
            UnlockSlider.value = Mathf.Lerp(0, 1, 0.0002f);
            if (UnlockSlider.value > UnlockSlider.value - 0.1f)
            {
                UnlockSlider.value = UnlockSlider.maxValue;
                _spentSkill = false;
            }
        }
    }

    //Clicks SkillNode
    public void ClickedSkill()
    {
        _selected = true;

        //Checks to see if skill requirements are met to unlock next node
        if(skillRequirements == true)
        {
            if(UnlockSlider == null)
            {
                //shows unlock button
                UnlockButton.SetActive(true);
            }
            else
            {
                if(PREVIOUSNODESCRIPT.previousNode == true)
                {
                    UnlockButton.SetActive(true);
                }
            }
        }
    }

    //unlocks skill
    public  void UnlockSkill(Image clickedButton)
    {
        previousNode = true;
        UnlockButton.GetComponentInChildren<TMP_Text>().text = "Unlocked";

        //fill slider
        if (UnlockSlider != null)
        {
            _spentSkill = true;
        }
        UnlockButton.GetComponent<Button>().interactable = false;
    }

}
