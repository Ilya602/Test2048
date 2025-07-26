using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Image[] turnsIndicators;
        [SerializeField] private GameObject gameOverMenu;


        public void UpdateScoreText(int value)
        {
            _scoreText.text = $"Score: <color=green>{value}</color>";
        }

        public void UpdateTurnsIndicators(int value)
        {
            foreach (var indicator in turnsIndicators) { indicator.gameObject.SetActive(false); }
            for (int i = 0; i < value; i++) { turnsIndicators[i].gameObject.SetActive(true); }
        }

        public void GameOver()
        {
            gameOverMenu.SetActive(true);
        }
    }
}