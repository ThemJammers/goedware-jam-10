using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using TMPro;
using UI;
using UnityEngine;

namespace Core
{
    public class WinController : MonoBehaviour
    {
        private int enemyCount;
        private List<Enemy> enemies = new List<Enemy>();
        [SerializeField] private GameWonPanel gameWonPanel;
        [SerializeField] private TextMeshProUGUI timerLabel;
        private DateTime timeStart;
        private DateTime timeFinished;
        private bool finished;
        
        private void Awake()
        {
            timeStart = DateTime.Now;
            enemies = FindObjectsOfType<Enemy>().ToList();
            enemyCount = enemies.Count;
            foreach (var enemy in enemies)
            {
                enemy.onDied.AddListener(OnEnemyDied);
            }
        }

        private void Update()
        {
            if (finished) return;
            if (Input.GetKeyDown(KeyCode.F1))
            {
                DebugWin();
            }

            timerLabel.text = timeStart.Subtract(DateTime.Now).ToString(@"hh\:mm\:ss");
        }

        private void OnEnemyDied(GameCharacter enemy)
        {
            enemy.onDied.RemoveAllListeners();
            enemyCount--;
            if (enemyCount == 0)
            {
                EndGame();
            }
        }

        private void DebugWin()
        {
            foreach (var enemy in enemies)
            {
                if(enemy != null) enemy.Die();
            }
        }

        private void EndGame()
        {
            timeFinished = DateTime.Now;
            timerLabel.text = timeStart.Subtract(DateTime.Now).ToString(@"hh\:mm\:ss");
            gameWonPanel.Show();
            finished = true;
        }
    }
    
}