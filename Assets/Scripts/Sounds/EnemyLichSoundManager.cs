using System;
using System.Transactions;
using Enemies;
using UnityEngine;

namespace Sounds
{
    public class EnemyLichSoundManager : EnemySoundManager
    {
        private PatrolingEnemy _patrollingEnemy;
        public override AudioClip[] DamageTakenSound => audioClipRefs.gruntsSpectralMonster;

        private void Start()
        {
            _patrollingEnemy = GetComponent<PatrolingEnemy>();

            _patrollingEnemy.onDamageTaken.AddListener(PlayOnDamageTakenSound);
        }
    }
}