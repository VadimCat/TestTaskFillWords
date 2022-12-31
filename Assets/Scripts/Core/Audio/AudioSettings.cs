using UnityEngine;

namespace Core.Audio
{
    public class AudioSettings
    {
        private const string SfxLevelKey = "sfxLevelKey";
        private const string MusicLevelKey = "musicLevelKey";
    
        public float SfxLevel = 1;
        public float MusicLevel = 1;

        public AudioSettings()
        {
            Load();
        }
        
        public void Save()
        {
            PlayerPrefs.SetFloat(SfxLevelKey, SfxLevel);
            PlayerPrefs.SetFloat(MusicLevelKey, MusicLevel);
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(SfxLevelKey))
            {
                SfxLevel = PlayerPrefs.GetFloat(SfxLevelKey);
            }

            if (PlayerPrefs.HasKey(MusicLevelKey))
            {
                MusicLevel = PlayerPrefs.GetFloat(MusicLevelKey);
            }
        }

        public void ClearSave()
        {
            PlayerPrefs.DeleteKey(SfxLevelKey);
            PlayerPrefs.DeleteKey(MusicLevelKey);
        }
    }
}