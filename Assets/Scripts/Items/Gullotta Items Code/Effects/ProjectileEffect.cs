using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EffectSystem
{
    [CreateAssetMenu(menuName = "Item Effect/Projectile")]
    public class ProjectileEffect : EffectBase
    {
        public bool doesComeBack = false;
        public float comeBackTime = 0f;
        public bool explodes = false;
        public float explodeRadius;
        public float explosionTime;
        public GameObject projectileObject;
        private GameObject ITEM;
        public Vector3 objectPool;
        public float speed;
        public Direction direction;
        public bool hasTravelTime = false;
        public float TravelTime = 0f;
        public GameObject ExplosionPrefab;
       

        public override void UseEffect(Transform user)
        {
            if(ITEM == null)
            {
                objectPool = (GameManager.PlayerManager.pm.transform.position + GameManager.PlayerManager.pm.transform.up * 50);
                ITEM = Instantiate(projectileObject, user.transform);
            }

            ITEM.SetActive(true);
            Debug.Log("ayo");
            ITEM.transform.position = user.position + new Vector3(0,1,0) + user.transform.forward;

            switch (direction)
            {
                case Direction.Forward:
                    ITEM.transform.forward = user.forward;
                    break;
                case Direction.Backward:
                    ITEM.transform.forward = -user.forward;
                    break;
                case Direction.Left:
                    ITEM.transform.forward = -user.right;
                    break;
                case Direction.Right:
                    ITEM.transform.forward = user.right;
                    break;
            }

            if (ITEM.GetComponent<ProjectileMotion>() == null)
            {
                ITEM.AddComponent<ProjectileMotion>();
            }

            ProjectileMotion pm = ITEM.GetComponent<ProjectileMotion>();

            pm.speed = speed;
            pm.comesBack = doesComeBack;
            pm.comebackTime = comeBackTime;
            pm.user = user;
            pm.objectPool = objectPool;
            pm.explodes = explodes;
            pm.explodeRadius = explodeRadius;
            pm.ExplosionPrefab = ExplosionPrefab;
            pm.explosionTime = explosionTime;
            pm.travelTime = hasTravelTime;
            pm.TravelTime = TravelTime;
        }

        public enum Direction
        {
            Forward,
            Backward,
            Left,
            Right
        }
    }
}