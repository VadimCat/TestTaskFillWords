using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Views.UI
{
    public class AnimationsQueuePlayer
    {
        private readonly Queue<Func<Task>> animationsQueue = new();

        private Task currentTask;
        
        public void EnqueueAnimation(Func<Task> animation)
        {
            animationsQueue.Enqueue(animation);
            if (currentTask == null || currentTask.IsCompleted)
            {
                PlayAnimations();
            }
        }

        private async void PlayAnimations()
        {
            while (animationsQueue.TryDequeue(out var func))
            {
                currentTask = func.Invoke();
                await currentTask;
            }
        }
    }
}
