
using UnityEngine;

namespace ThemJammers.Enemies
{
    [RequireComponent(typeof(RandomMovement))]
    public class PatrolingEnemy : Enemy
    {
        private RandomMovement _randomMovement;

        protected override void Awake()
        {
            base.Awake();
            _randomMovement = GetComponent<RandomMovement>();
            _randomMovement.StartMoving();
        }

        protected override void ScanForPlayer()
        {
            base.ScanForPlayer();
            if (_playerTransform)
            {
                _randomMovement.StopMoving();
            }
        }
    }
}
