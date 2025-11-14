using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Birds
{
    public class BirdInputHandler : MonoBehaviour
    {
        public event Action Moved;
        public event Action Shot;
        
        public void OnDoMove(InputAction.CallbackContext context)
        {
            if (context.performed)
                Moved?.Invoke();           
        }

        public void OnDoShot(InputAction.CallbackContext context)
        {
            if (context.performed)
                Shot?.Invoke();
        }
    }
}