using UnityEngine;

namespace Sacrifices
{
    [CreateAssetMenu(menuName = "SacrificeEffect/ArmSacrifice", order = 1)]
    [System.Serializable]
    public class ArmSacrifice : SacrificeEffect
    {
        public override void Apply()
        {
            SacrificeController sacrificeController = SacrificeController.Instance;
            sacrificeController.PlayerProjectileModifier = positiveModifier;
            sacrificeController.PlayerCadenceModifier = negativeModifier;
            base.Apply();
        }
    }
}