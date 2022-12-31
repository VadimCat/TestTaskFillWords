using System.Threading.Tasks;
using UnityEngine;

namespace Utils
{
    public static class AsyncOperationExtensions
    {
        public static Task ToTask(this AsyncOperation asyncOperation)
        {
            TaskCompletionSource<bool> taskCompletionSource = new TaskCompletionSource<bool>();
            asyncOperation.completed += _ => taskCompletionSource.SetResult(asyncOperation.isDone);
            return taskCompletionSource.Task;
        }
    }
}