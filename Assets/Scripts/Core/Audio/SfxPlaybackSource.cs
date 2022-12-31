using System.Threading.Tasks;
using Core.Pools;
using UnityEngine;

namespace Core.Audio
{
    public class SfxPlaybackSource : MonoBehaviour, IPoolable
    {
        [SerializeField] private AudioSource source;
        private AudioClipConfig _clipConfig;
        private TaskCompletionSource<bool> _completionSource;

        public bool IsPlaying => source.isPlaying;
        
        public void SetDependencies(AudioClipConfig clipConfig)
        {
            this._clipConfig = clipConfig;
            source.clip = clipConfig.Clip;
            source.volume = clipConfig.PlayVolume;
        }
    
        public async Task PlaybackAsync(bool isLooped = false)
        {
            source.loop = isLooped;
            source.Play();
            while (IsPlaying)
            {
                await Task.Yield();
            }
        }

        public void Pause()
        {
            source.Pause();
        }
        
        public void Stop()
        {
            source.Stop();
        }

        public void Spawn()
        {
            gameObject.SetActive(true);
        }

        public void DeSpawn()
        {
            gameObject.SetActive(false);

            source.loop = false;
            source.Stop();
            source.clip = null;
        }
    }
}