using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Inputs
{
    public class DragInput : IInputStrategy
    {
        public Vector2 GetInput()
        {
            if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (Input.GetMouseButton(0))
                {
                    return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                }
            }

            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    if (touch.phase == TouchPhase.Moved)
                    {
                        return touch.deltaPosition;
                    }
                }
            }

            return Vector2.zero;
        }
    }
}