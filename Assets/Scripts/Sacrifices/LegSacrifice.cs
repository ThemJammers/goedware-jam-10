using UnityEngine;

namespace Sacrifices
{
    [CreateAssetMenu(menuName = "SacrificeEffect/LegSacrifice", order = 1)]
    [System.Serializable]
    public class LegSacrifice : SacrificeEffect
    {
        public override void Apply()
        {
            SacrificeController sacrificeController = SacrificeController.Instance;
            sacrificeController.PlayerMeleeModifier = positiveModifier;
            sacrificeController.PlayerSpeedModifier = negativeModifier;
            base.Apply();
        }
    }
}