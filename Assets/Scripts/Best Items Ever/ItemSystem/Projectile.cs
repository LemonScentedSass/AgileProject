using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
      private float _speed;
      private Character _owner;
      private IProjectileObject _projectileObject;
      private float _distance;

      public void Init(float speed, Character owner, IProjectileObject projectileObject, float distance)
      {
            _speed = speed;
            _distance = distance;
            _owner = owner;
            _projectileObject = projectileObject;
      }

      private void Update()
      {
            if (Vector3.Distance(_owner.transform.position, this.transform.position) <= _distance)
            {
                  this.transform.position += Camera.main.transform.forward * _speed * Time.deltaTime;
            }
            else
            {
                  Destroy(this.gameObject);
            }
      }

      private void OnCollisionEnter(Collision other)
      {
            Debug.Log($"Projectile Collided with {other.gameObject.name}");

            if (other.gameObject.GetComponent<IHittable>() != null)
            {
                  other.gameObject.GetComponent<IHittable>().AdjustHealth(_projectileObject.DamageRange);
                  Debug.Log($"Projectile Damaged {other.gameObject.name}");
                  Destroy(gameObject);
            }
      }
}
