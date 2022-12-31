using System.Collections.Generic;
using Zenject;

namespace Core
{
    public class UpdateService : ITickable
    {
        private List<IUpdatable> updatables = new();

        public void Add(IUpdatable updatable)
        {
            if (!updatables.Contains(updatable))
                updatables.Add(updatable);
        }

        public void Remove(IUpdatable updatable)
        {
            if (updatables.Contains(updatable))
                updatables.Remove(updatable);
        }

        public void Tick()
        {
            for (int i = 0; i < updatables.Count; i++)
            {
                updatables[i].OnUpdate();
            }
        }
    }

    public interface IUpdatable
    {
        public void OnUpdate();
    }
}