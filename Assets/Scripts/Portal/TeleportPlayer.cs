using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]private int SceneInt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (other.gameObject.GetComponent<FakePlayerManager>() == true)
            {
                other.gameObject.GetComponent<FakePlayerManager>().Save();
            }
            SceneManager.LoadScene(SceneInt);
        }
    }
}