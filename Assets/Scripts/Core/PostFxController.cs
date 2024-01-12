using System;
using Patterns;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

namespace Core
{
    public class PostFxController : Singleton<PostFxController>
    {
        [SerializeField] private Volume volume;
        [SerializeField] private VolumeProfile[] profiles;
        private float defaultVignetteIntensity;
        private VolumeProfile _activeProfile;
        private Vignette vignette;

        private void Start()
        {
            _activeProfile = volume.profile;
            volume = GetComponent<Volume>();
            volume.profile.TryGet(out vignette);
        }

        private void ApplyProfile(VolumeProfile profile)
        {
            _activeProfile = volume.profile;
            volume.profile.TryGet(out vignette);
            defaultVignetteIntensity = vignette.intensity.value;
        }

        public void ApplyProfile(int id)
        {
            ApplyProfile(profiles[id]);
        }

        public void DecreaseVisibility()
        {
            vignette.intensity.value = 0.65f;
        }

        public void ResetVisibility()
        {
            vignette.intensity.value = defaultVignetteIntensity;
        }
    }

}