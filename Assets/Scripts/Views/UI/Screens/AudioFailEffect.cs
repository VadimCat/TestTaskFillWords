using System.Threading.Tasks;
using Core.Audio;

namespace Views.UI.Screens
{
    public class AudioFailEffect : IFailEffect
    {
        private readonly AudioService _audioService;
        private readonly AudioFailEffectConfig _config;

        public AudioFailEffect(AudioService audioService, AudioFailEffectConfig config)
        {
            _audioService = audioService;
            _config = config;
        }
        
        public async Task PlayAnimation()
        {
            await _audioService.PlaySfxAsync(_config.clipName);
        }
    }
}