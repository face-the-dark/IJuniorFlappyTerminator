using System;

namespace UI
{
    public class GameOverScreen : Window
    {
        public event Action RestartButtonClicked;

        protected override void OnButtonClick() => 
            RestartButtonClicked?.Invoke();
    }
}