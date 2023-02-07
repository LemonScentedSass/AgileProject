using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ItemSystemV2;

public class Character : MonoBehaviour, IHittable
{
    public delegate void OnHit(Character source, Character target);
    public OnHit onHit;

    public delegate void OnTick(float time);
    public OnTick onTick;

    [SerializeField] public EquipItem equipItem;
    [SerializeField] private LayerMask _combatLayer;

    [Header("Transform Refs")]
    [SerializeField] protected Transform _rightHandWeaponMount;

    [SerializeField] protected float _curHealth;
    [SerializeField] protected float _maxHealth = 10f;

    [SerializeField] public bool isDead;

    protected Dictionary<EffectBase, List<Transform>> _visualEffectDictionary = new Dictionary<EffectBase, List<Transform>>();

    public Transform RightHandWeaponMount { get { return _rightHandWeaponMount; } }

    private uint _tickCounter = 0;

    public float MaxHealth { get { return _maxHealth; } set { _maxHealth = value; } }
    public float CurrentHealth { get { return _curHealth; } set { _curHealth = value; } }


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

    public void AdjustHealth(int damage)
    {
        Debug.Log("GetHit - PlayerManager");
        if (isDead == false)                                               // If player is not dead,
        {
            _curHealth -= damage;                                     // Decrease health by 1

            if (_curHealth <= 0)                                      // Check for health less than or equal to 0
            {
                isDead = true;                                         // dead bool = true
            }
        }
    }

    public void GetStunned(float length)
    {
        return;
    }

    protected virtual void Update()
    {
        if (_tickCounter > 0)
        {
            onTick(Time.deltaTime);
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

        for (int i = 0; i < targets.Length; i++)
        {
            we.DealDamage((IHittable)targets[i]);
        }
    }

    public void CallWeaponAttack()
    {
        if (equipItem != null && equipItem is WeaponEquip)
        {
            WeaponEquip we = equipItem as WeaponEquip;
            we.AttackTrigger(this, _combatLayer);
        }
    }

    public void SubscribeOnTick(TimedEffect effect)
    {
        TimedEffectToken tet = effect.GenerateToken(this, this) as TimedEffectToken;
        onTick += tet.UpdateToken;
        _tickCounter++;
    }

    public void UnsubscribeOnTick(TimedEffectToken effect)
    {
        onTick -= effect.UpdateToken;
        _tickCounter--;
    }

    public void RegisterVisualEffect(EffectBase token, Transform visualObject)
    {
        if (_visualEffectDictionary.ContainsKey(token))
        {
            if (_visualEffectDictionary[token].Contains(visualObject) == false)
            {
                _visualEffectDictionary[token].Add(visualObject);
            }
        }
        else
        {
            _visualEffectDictionary.Add(token, new List<Transform>());
            _visualEffectDictionary[token].Add(visualObject);
        }
    }

    public void UnregisterVisualEffect(EffectBase token)
    {
        if (_visualEffectDictionary.ContainsKey(token))
        {
            for (int i = _visualEffectDictionary[token].Count - 1; i >= 0; i--)
            {
                Destroy(_visualEffectDictionary[token][i].gameObject);
                _visualEffectDictionary[token].RemoveAt(i);
            }
        }
    }
}
