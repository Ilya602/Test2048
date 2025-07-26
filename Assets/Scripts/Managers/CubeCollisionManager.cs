using Assets.Scripts.Entities;
using Assets.Scripts.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class CubeCollisionManager : MonoBehaviour
    {
        public static CubeCollisionManager Instance { get; private set; }
        private List<ICubeCollisionObserver> _observers = new();


        private void Awake()
        {
            Instance = this;
        }

        public void Register(ICubeCollisionObserver observer)
        {
            if (!_observers.Contains(observer)) { _observers.Add(observer); }
        }

        public void Unregister(ICubeCollisionObserver observer)
        {
            _observers.Remove(observer);
        }

        public void NotifyCollision(CubeEntity a, CubeEntity b)
        {
            foreach (var observer in _observers) { observer.OnCubeCollision(a, b); }
        }
    }
}