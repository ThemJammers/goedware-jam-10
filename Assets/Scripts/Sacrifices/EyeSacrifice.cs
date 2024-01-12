using Core;
using UnityEngine;

namespace Sacrifices
{
    [CreateAssetMenu(menuName = "SacrificeEffect/EyeSacrifice", order = 1)]
    [System.Serializable]
    public class EyeSacrifice : SacrificeEffect
    {
        public override void Apply()
        {
            SacrificeController sacrificeController = SacrificeController.Instance;
            sacrificeController.PlayerCadenceModifier = positiveModifier;
            PostFxController.Instance.DecreaseVisibility();
            base.Apply();
        }
    }
}