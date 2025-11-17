using System;

namespace UI
{
    public class StartScreen : Window
    {
        public event Action PlayButtonClicked;

        protected override void OnButtonClick() => 
            PlayButtonClicked?.Invoke();
    }
}