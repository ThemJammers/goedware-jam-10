using Data;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WeaponsUIController : MonoBehaviour
    {
        private ToggleGroup _toggleGroup;
        private WeaponSlot[] _slots;

        private void Awake()
        {
            _toggleGroup = GetComponentInChildren<ToggleGroup>();
            _slots = _toggleGroup.GetComponentsInChildren<WeaponSlot>();
            
            foreach (var weaponSlot in _slots)
            {
                weaponSlot.gameObject.SetActive(false);
            }
        }

        public void UpdateSlotIdx(int idx, ProjectileData projectile)
        {
            var slot = _slots[idx];
            slot.gameObject.SetActive(true);
            slot.SetCollected(projectile);
        }

        public void ActivateSlotIdx(int idx)
        {
            _toggleGroup.SetAllTogglesOff();
            _slots[idx].Activate();
        }
    }
}