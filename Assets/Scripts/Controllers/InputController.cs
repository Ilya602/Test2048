using Assets.Scripts.Inputs;
using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class InputController : MonoBehaviour
    {
        [SerializeField] private float _dragSense = .7f;
        private float _currSens = 0f;
        private IInputStrategy _currentInput;


        public void Initialize(IInputStrategy strategy)
        {
            SetInputType(strategy);
        }

        private void SetInputType(IInputStrategy strategy)
        {
            switch (strategy)
            {
                case DragInput dragInput:
                    _currentInput = strategy;
                    _currSens = _dragSense;
                    break;
            }
        }

        public Vector2 GetInput()
        {
            return _currentInput?.GetInput() * _currSens ?? Vector2.zero;
        }
    }
}