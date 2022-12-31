using System.Threading.Tasks;

namespace Core.States
{
    public interface IState
    {
        public Task Enter();
        public Task Exit();
    }
}