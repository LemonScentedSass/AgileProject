using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable
{
      void AdjustHealth(int damage);

      void GetStunned(float length);
}
