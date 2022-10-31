using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateBreak : MonoBehaviour, IHittable
{
    public int crateHealth = 1;

    public void GetHit(int damage)
    {
        crateHealth -= damage;
        if (crateHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void GetStunned(float length)
    {
        return;
    }
}
