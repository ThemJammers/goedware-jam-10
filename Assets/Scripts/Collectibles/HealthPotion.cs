using Player;
using UnityEngine;

namespace Collectibles
{
    public class HealthPotion : BasicCollectible
    {
        [SerializeField] private int healAmount = 20;

        public override void Collect(PlayerController playerController)
        {
            playerController.Heal(healAmount);
            base.Collect(playerController);
        }
    }
}
