using System.Threading.Tasks;
using Core.Pools;
using UnityEngine;
using UnityEngine.Audio;
using Utils;
using Zenject;

namespace Core.Audio
{
    public class AudioService : MonoBehaviour, IInitializable
    {
        private const string SfxKey = "SfxVolume";
        private const string MusicKey = "MusicVolume";

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private AudioClipsConfig clipsConfig;
        [SerializeField] private AudioMixer mixer;
        
        [SerializeField] private SfxPlaybackSource playbackSource;

        public ReactiveProperty<float> sfxVolume;
        public ReactiveProperty<float> musicVolume;

        private AudioSettings audioSettings;
        private Pool<SfxPlaybackSource> sfxPlaybackPool;

        public void Initialize()
        {
            sfxPlaybackPool = new Pool<SfxPlaybackSource>(playbackSource, transform);
            audioSettings = new();
            clipsConfig.Bootstrap();
            sfxVolume = new ReactiveProperty<float>((audioSettings.SfxLevel * audioConfig.MaxSfxLevel).ToAudioLevel());
            musicVolume = new ReactiveProperty<float>((audioSettings.MusicLevel * audioConfig.MaxMusicLevel).ToAudioLevel());
            
            mixer.SetFloat(SfxKey, sfxVolume.Value);
            mixer.SetFloat(MusicKey, musicVolume.Value);
        }

        public void SetSfxLevel(float level)
        {
            var groupVolume = (audioConfig.MaxSfxLevel * level).ToAudioLevel();
            sfxSource.outputAudioMixerGroup.audioMixer.SetFloat(MusicKey, groupVolume);

            audioSettings.SfxLevel = level;
            sfxVolume.Value = level;
            
            audioSettings.Save();
        }

        public void SetMusicLevel(float level)
        {
            var groupVolume = (audioConfig.MaxMusicLevel * level).ToAudioLevel();
            musicSource.outputAudioMixerGroup.audioMixer.SetFloat(SfxKey, groupVolume);

            audioSettings.MusicLevel = level;
            musicVolume.Value = level;
            
            audioSettings.Save();
        }

        public void PlayMusic(AudioClipName clipName)
        {
            var clipConfig = clipsConfig.GetClip(clipName);
            musicSource.clip = clipConfig.Clip;
            musicSource.Play();
            musicSource.clip.LoadAudioData();
        }
        
        public async Task PlaySfxAsync(AudioClipName clipName)
        {
            var source = sfxPlaybackPool.Spawn();
            var clipConfig = clipsConfig.GetClip(clipName);
            source.SetDependencies(clipConfig);
            await source.PlaybackAsync();
            sfxPlaybackPool.DeSpawn(source);
        }

        public SfxPlaybackSource GetPlaybackSource(AudioClipName clipName)
        {
            var source = sfxPlaybackPool.Spawn();
            var clipConfig = clipsConfig.GetClip(clipName);
            source.SetDependencies(clipConfig);
            return source;
        }

        public void ReleasePlaybackSource(SfxPlaybackSource playbackSource)
        {
            sfxPlaybackPool.DeSpawn(playbackSource);
        }
    }
    
    public enum AudioClipName
    {
        Fail0
    }
}