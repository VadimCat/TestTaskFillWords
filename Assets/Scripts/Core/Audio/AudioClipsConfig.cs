using System.Collections.Generic;
using UnityEngine;

namespace Core.Audio
{
    [CreateAssetMenu]
    public class AudioClipsConfig : ScriptableObject
    {
        [SerializeField] private AudioClipConfig[] clips;
        
        private readonly Dictionary<AudioClipName, AudioClipConfig> _clipsDict = new();
        
        public void Bootstrap()
        {
            foreach (var clip in clips)
            {
                _clipsDict[clip.ClipName] = clip;
            }
        }
        
        public AudioClipConfig GetClip(AudioClipName clipName)
        {
            return _clipsDict[clipName];
        }
    }
}