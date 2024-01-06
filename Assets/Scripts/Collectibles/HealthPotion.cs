using ThemJammers.Player;
using UnityEngine;

namespace ThemJammers.Collectibles
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
