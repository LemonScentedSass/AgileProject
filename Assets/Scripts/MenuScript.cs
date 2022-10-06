using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public Animator menuAnim;
    public PlayerMoveScript pm;
    public GameObject MENUgo;
    public GameObject[] MenuButton;
    public GameObject[] MenuImage;
    public bool menuON = false;

    public Image currentMenuIMAGE;
    public int currentMenuINT;

    //0 Inventory
    //1 Skills
    //2 Stats
    //3 Map
    //4 Options

    // Start is called before the first frame update
    void Start()
    {
        menuAnim = GetComponent<Animator>();
        currentMenuINT = 0;
        MENUgo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            MENUgo.SetActive(true);
            //pm.MENU = true;

            menuAnim.SetBool("open", true);
            EnableMenu(currentMenuINT);
            UpdateUI();
        }

        if(menuAnim.GetCurrentAnimatorStateInfo(0).IsName("Empty") && menuAnim.GetBool("open") == false)
        {
            MENUgo.SetActive(false);
            //pm.MENU = false;
        }

        //if (pm.MENU == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //play close menu animation
                menuAnim.SetBool("open", false);

            }
        }

        if(MenuImage[currentMenuINT].activeSelf == true)
        {
            MenuButton[currentMenuINT].SetActive(false);
        }
    }

    public void EnableMenu(int menuINT)
    {
        for (int i = 0; i < MenuImage.Length; i++)
        {
            MenuImage[i].SetActive(false);
        }

        MenuImage[menuINT].SetActive(true);

        for (int i = 0; i < MenuButton.Length; i++)
        {
            MenuButton[currentMenuINT].SetActive(true);
        }
        if(MenuButton[menuINT].activeSelf == true)
        {
            MenuButton[currentMenuINT].SetActive(false);
        }

    }


    public void OnInventoryClick()
    {
        MenuButton[currentMenuINT].SetActive(true);
        currentMenuINT = 0;
        EnableMenu(currentMenuINT);
        Debug.Log("Inventory");

    }
    public void OnSkillsClick()
    {
        MenuButton[currentMenuINT].SetActive(true);
        currentMenuINT = 1;
        EnableMenu(currentMenuINT);
        Debug.Log("Skills");

    }
    public void OnStatsClick()
    {
        MenuButton[currentMenuINT].SetActive(true);
        currentMenuINT = 2;
        EnableMenu(currentMenuINT);

    }
    public void OnMapClick()
    {
        MenuButton[currentMenuINT].SetActive(true);
        currentMenuINT = 3;
        EnableMenu(currentMenuINT);

    }
    public void OnOptionsClick()
    {
        MenuButton[currentMenuINT].SetActive(true);
        currentMenuINT = 4;
        EnableMenu(currentMenuINT);

    }

    private void UpdateUI()
    { 
        MenuImage[currentMenuINT].SetActive(true);
        MenuButton[currentMenuINT].SetActive(true);
    }
}
