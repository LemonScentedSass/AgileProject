using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2;

public class Character : MonoBehaviour
{
      public delegate void OnHit(Character character);
      public OnHit onHit;

      [SerializeField] public EquipItem equipItem;
      [SerializeField] private LayerMask _combatLayer;
      [Header("Transform Refs")]
      [SerializeField] protected Transform _rightHandWeaponMount;

      public Transform RightHandWeaponMount { get { return _rightHandWeaponMount; } }

      protected virtual void Start()
      {
            WeaponEquip temp = equipItem as WeaponEquip;

            if (temp != null)
            {
                  EquipItem(equipItem, _rightHandWeaponMount);
            }
      }

      public void ToggleMesh(Transform partent, string objectName)
      {
            Transform[] objs = partent.GetComponentsInChildren<Transform>();

            foreach (Transform obj in objs)
            {
                  if (obj.name == objectName)
                  {
                        obj.gameObject.SetActive(true);
                  }
                  else if (obj != partent)
                  {
                        obj.gameObject.SetActive(false);
                  }
            }
      }

      public void EquipItem(EquipItem itemToEquip, Transform itemMount)
      {
            WeaponEquip we = itemToEquip as WeaponEquip;

            if (we != null)
            {
                  ToggleMesh(itemMount, we.weaponModel.name);
            }
      }

      public void DealDamage(Character[] targets)
      {
            WeaponEquip we = equipItem as WeaponEquip;

            Debug.Log("DealDamage - above loop");

            for (int i = 0; i < targets.Length; i++)
            {
                  Debug.Log("DealDamage - In For Loop");
                  we.DealDamage((IHittable)targets[i]);
            }
      }

      public void CallWeaponAttack()
      {
            if(equipItem != null && equipItem is WeaponEquip)
            {
                  WeaponEquip we = equipItem as WeaponEquip;
                  we.AttackTrigger(this, _combatLayer);
            }
      }
}