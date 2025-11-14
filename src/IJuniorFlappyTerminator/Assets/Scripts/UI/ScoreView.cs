using TMPro;
using UnityEngine;

namespace UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private ScoreCounter _scoreCounter;
        [SerializeField] private TextMeshProUGUI _score;

        private void OnEnable() => 
            _scoreCounter.ScoreChanged += OnScoreChanged;

        private void OnDisable() => 
            _scoreCounter.ScoreChanged -= OnScoreChanged;

        private void OnScoreChanged(int score) => 
            _score.text = score.ToString();
    }
}