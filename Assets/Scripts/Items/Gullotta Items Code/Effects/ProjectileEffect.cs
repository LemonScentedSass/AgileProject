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
        public GameObject projectileObject;
        public Transform objectPool;
        public float speed;
        public Direction direction;

        public override void UseEffect(Transform user)
        {
            projectileObject.SetActive(true);
            projectileObject.transform.position = user.position + new Vector3(0,1,0) + user.transform.forward;

            switch (direction)
            {
                case Direction.Forward:
                    projectileObject.transform.forward = user.forward;
                    break;
                case Direction.Backward:
                    projectileObject.transform.forward = -user.forward;
                    break;
                case Direction.Left:
                    projectileObject.transform.forward = -user.right;
                    break;
                case Direction.Right:
                    projectileObject.transform.forward = user.right;
                    break;
            }

            if (projectileObject.GetComponent<ProjectileMotion>() == null)
            {
                projectileObject.AddComponent<ProjectileMotion>();
            }

            ProjectileMotion pm = projectileObject.GetComponent<ProjectileMotion>();

            pm.speed = speed;
            pm.comesBack = doesComeBack;
            pm.comebackTime = comeBackTime;
            pm.user = user;
            pm.objectPool = objectPool;
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