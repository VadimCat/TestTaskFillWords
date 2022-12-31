using System;
using Ji2Core.Core.Audio;
using Newtonsoft.Json;
using UnityEngine;

namespace UI.Screens
{
    public class FailEffectProvider
    {
        private readonly string _configPath = "FailAnimationConfigs/FailConfig";
        private readonly string _shakingConfigPath = "FailAnimationConfigs/ShakingFailEffectConfig";
        private readonly string _audioConfigPath = "FailAnimationConfigs/AudioFailEffectConfig";

        private readonly Transform _shakingRootTransform;
        private readonly AudioService _audioService;

        private FailConfig _failConfig;
        private ShakingFailEffectConfig _shakingFailEffectConfig;
        private AudioFailEffectConfig _audioFailEffectConfig;

        public FailEffectProvider(Transform shakingRootTransform, AudioService audioService)
        {
            _shakingRootTransform = shakingRootTransform;
            _audioService = audioService;
        }

        public void LoadConfigs()
        {
            _failConfig = GetConfigFrom<FailConfig>(_configPath);

            switch (_failConfig.FailEffect)
            {
                case FailEffect.Shaking:
                    _shakingFailEffectConfig = GetConfigFrom<ShakingFailEffectConfig>(_shakingConfigPath);
                    break;
                case FailEffect.Audio:
                    _audioFailEffectConfig = GetConfigFrom<AudioFailEffectConfig>(_audioConfigPath);
                    break;
            }
        }

        private TConfig GetConfigFrom<TConfig>(string path)
        {
            var config = Resources.Load<TextAsset>(path);
            return JsonConvert.DeserializeObject<TConfig>(config.text);
        }

        public IFailEffect GetFailEffect()
        {
            switch (_failConfig.FailEffect)
            {
                case FailEffect.Shaking:
                    return new ShakingFailEffect(_shakingRootTransform, _shakingFailEffectConfig);
                    break;
                case FailEffect.Audio:

                    return new AudioFailEffect(_audioService, _audioFailEffectConfig);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}