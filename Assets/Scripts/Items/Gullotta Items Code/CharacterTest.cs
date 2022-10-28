using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystem;

public class CharacterTest : MonoBehaviour
{
    public UseItem useItem;
    public static CharacterTest instance;
    public bool Using;
    public Animator anim;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && Using == false)
        {
            Using = true;
            anim.Play("Fireball", 2);
        }
        
    }



    public void UseItem()
    {
        useItem.OnUseItem(transform);
    }
}
