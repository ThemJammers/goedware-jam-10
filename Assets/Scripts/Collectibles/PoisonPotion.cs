using ThemJammers.Player;
using UnityEngine;

namespace ThemJammers.Collectibles
{
    public class PoisonPotion : BasicCollectible
    {
        [SerializeField] private int damageAmount;
        public override void Collect(PlayerController playerController)
        {
            playerController.TakeDamage(damageAmount);
            base.Collect(playerController);
        }
    }

}