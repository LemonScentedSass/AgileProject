using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthUI : MonoBehaviour
{
    public UnityEngine.UI.Slider healthSlider;
    private EnemyStats enemyStats;

    // Start is called before the first frame update
    void Start()
    {
        enemyStats = GetComponent<EnemyStats>();
        healthSlider.maxValue = enemyStats.MaxHealth;
        healthSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(healthSlider.value != enemyStats.CurrentHealth)
        {
            healthSlider.value = enemyStats.CurrentHealth;
        }

        if(healthSlider != null)
        {
            healthSlider.transform.LookAt(Camera.main.gameObject.transform.position);
        }
    }
}
