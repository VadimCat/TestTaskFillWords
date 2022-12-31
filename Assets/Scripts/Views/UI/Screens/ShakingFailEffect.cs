using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Views.UI.Screens
{
    public class ShakingFailEffect : IFailEffect
    {
        private readonly Transform _shakingObject;
        private readonly ShakingFailEffectConfig _config;

        public ShakingFailEffect(Transform shakingObject, ShakingFailEffectConfig config)
        {
            _shakingObject = shakingObject;
            _config = config;
        }
        public async Task PlayAnimation()
        {
            await _shakingObject.DOShakePosition(_config.Duration, _config.Strength, _config.Vibrato)
                .SetLink(_shakingObject.gameObject)
                .AsyncWaitForCompletion();
        }
    }
}