using System;

namespace Models
{
    [Serializable]
    public class LevelConfig
    {
        public char[,] Filling;
        public string[] AvailableWords;
    }
}
