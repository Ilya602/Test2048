using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Observers
{
    public class CubeCollisionObserver : MonoBehaviour, ICubeCollisionObserver
    {
        [SerializeField] private UnityEvent<CubeEntity, CubeEntity> OnCubesMerge;


        private void Start()
        {
            CubeCollisionManager.Instance.Register(this);
        }

        private void OnDestroy()
        {
            if (CubeCollisionManager.Instance != null) { CubeCollisionManager.Instance.Unregister(this); }
        }

        public void OnCubeCollision(CubeEntity a, CubeEntity b)
        {
            if (a.Type != b.Type) return;
            if (a.GetInstanceID() < b.GetInstanceID()) 
            { 
                OnCubesMerge.Invoke(a, b);
            }
        }
    }
}