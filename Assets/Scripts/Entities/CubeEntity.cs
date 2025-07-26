using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using Assets.Scripts.Managers;

namespace Assets.Scripts.Entities
{
    public class CubeEntity : MonoBehaviour
    {
        public enum CubeType { cube2, cube4, cube8, cube16, cube32, cube64, cube128, cube256, cube512 };

        [SerializeField] private CubeType _type;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float pushForce = 10f;
        public CubeType Type => _type;


        public void Initialize(bool isPlayerCube)
        {
            transform.localScale *= .4f;
            transform.DOScale(.6f, .5f).SetEase(Ease.OutBounce);

            if (!isPlayerCube)
            {
                _rigidbody.AddForce(Vector3.up * 3f, ForceMode.Impulse);
            }         
        }

        public void Push(Vector3 direction)
        {
            _rigidbody.AddForce(direction * pushForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out CubeEntity cube))
            {
                if (_rigidbody.velocity.magnitude > .8f && cube != this)
                {
                    CubeCollisionManager.Instance.NotifyCollision(this, cube);
                }
            }
        }
    }
}