using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameManager;
using UnityEngine.SceneManagement;

public class MenuButtonClicks : MonoBehaviour
{
    public Animator anim;
    public Image background;

    public GameObject characterSkills;

    public TMPro.TMP_Text itemTXT;
    public TMPro.TMP_Text itemUpgradeTXT;
    public TMPro.TMP_Text magicTXT;
    public TMPro.TMP_Text magicUpgradeTXT;

    public Button ItemUpgradeButton;
    public Button MagicUpgradeButton;

    public Image ItemImage;
    public Image MagicImage;

    public GameObject OptionMenu;

    public GameObject[] Skills;
    private bool canCloseMenu;

    public Button exitToMenuButton;
    public Button quitToDesktopButton;

    private Hotbar hotbar;
    private CurrentInventory inventory;

    public GameObject ItemLock;
    public GameObject MagicLock;

    // Start is called before the first frame update
    void Start()
    {
        hotbar = PlayerManager.pm.GetComponentInChildren<Hotbar>();
        inventory = PlayerManager.pm.GetComponent<CurrentInventory>();
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

        if(ItemImage.sprite == null)
        {
            ItemLock.SetActive(true);
        }
        else
        {
            ItemLock.SetActive(false);
        }

        if (MagicImage.sprite == null)
        {
            MagicLock.SetActive(true);
        }
        else
        {
            MagicLock.SetActive(false);
        }

        ItemImage.sprite = inventory.hotbarItemImage.sprite;
        MagicImage.sprite = inventory.hotbarMagicImage.sprite;

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

        if (canCloseMenu == false)
        {
            anim.SetBool("open", false);
            canCloseMenu = true;
        }
        else
        {
            canCloseMenu = false;
            anim.SetBool("open", true);

            characterSkills.SetActive(true);
        }
    }
    public void ButtonclickGreen()
    {
       //increase base damage

    }
    public void ButtonclickOrange()
    {
       //increase item level
    }
    public void ButtonclickRed()
    {
        //increase magic level
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

    public void ExitToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
