using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2;
using UnityEngine.Events;
using GameManager;

public class Weapon : MonoBehaviour
{
      [SerializeField] protected GameObject mainHandWeapon;             // Prefab to spawn in hand
      [SerializeField] public EquipItem equipItemSO;                    // Reference to Scriptable with item stats

      [field: SerializeField] public UnityEvent OnAttack { get; set; }
      [field: SerializeField] public UnityEvent OnWeaponChange { get; set; }

      PlayerManager pm;

      private void Start()
      {
            pm = GetComponent<PlayerManager>();
      }

      private void Update()
      {
            UseWeapon();
      }

      private void UseWeapon()
      {
            OnAttack?.Invoke();
      }
}
