using System.Threading.Tasks;
using UnityEngine;

namespace Views.UI.Screens
{
    public abstract class BaseScreen : MonoBehaviour
    {
        public virtual Task AnimateShow()
        {
            return Task.CompletedTask;
        }

        public virtual Task AnimateClose()
        {
            return Task.CompletedTask;
        }
    }
}