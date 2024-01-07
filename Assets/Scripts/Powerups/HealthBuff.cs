using Core;
using UnityEngine;

namespace Powerups
{
    [CreateAssetMenu(menuName = "Powerups/HealthBuff")]
    public class HealthBuff : PowerupEffect
    {
        public int amount;

        public override void Apply(GameCharacter character)
        {
            character.Heal(amount);
        }
    }
}