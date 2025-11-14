using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public abstract class Window : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup WindowGroup;
        [SerializeField] protected Button ActionButton;

        private void OnEnable() => 
            ActionButton.onClick.AddListener(OnButtonClick);

        private void OnDisable() => 
            ActionButton.onClick.RemoveListener(OnButtonClick);

        protected abstract void OnButtonClick();

        public abstract void Open();
        
        public abstract void Close();
    }
}