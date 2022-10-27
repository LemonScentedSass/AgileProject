using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;

public class MenuButtonClicks : MonoBehaviour
{
    public Animator anim;
    public Image background;

    public GameObject swordSkills;
    public GameObject itemSkills;
    public GameObject scrollSkills;
    public GameObject characterSkills;

    public GameObject OptionMenu;

    public GameObject[] Skills;
    private bool canCloseMenu;
    private bool flag;

    // Start is called before the first frame update
    void Start()
    {
        OptionMenu.SetActive(false);
        anim = GetComponent<Animator>();
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 0;
            anim.SetBool("Menu", true);
            canCloseMenu = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            canCloseMenu = true;
            anim.SetBool("open", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && canCloseMenu == true && OptionMenu.activeSelf == false)
        {
            anim.SetBool("Menu", false);
            Time.timeScale = 1;

        }
        if (Input.GetKeyDown(KeyCode.Escape) && OptionMenu.activeSelf == true)
        {
            OptionMenu.SetActive(false);
        }
    }

    public void ButtonclickBlue()
    {
        canCloseMenu = false;
        anim.SetBool("open", true);
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].SetActive(false);
        }
        characterSkills.SetActive(true);
    }
    public void ButtonclickGreen()
    {
        canCloseMenu = false;
        anim.SetBool("open", true);
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].SetActive(false);
        }
        swordSkills.SetActive(true);
    }
    public void ButtonclickOrange()
    {
        canCloseMenu = false;
        anim.SetBool("open", true);
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].SetActive(false);
        }
        itemSkills.SetActive(true);
    }
    public void ButtonclickRed()
    {
        canCloseMenu = false;
        anim.SetBool("open", true);
        for (int i = 0; i < Skills.Length; i++)
        {
            Skills[i].SetActive(false);
        }
        scrollSkills.SetActive(true);
    }

    public void ButtonClickOptions()
    {
        OptionMenu.SetActive(true);
    }

    private IEnumerator Escape()
    {
        yield return new WaitForSeconds(1f);

        canCloseMenu = true;
    }
}
