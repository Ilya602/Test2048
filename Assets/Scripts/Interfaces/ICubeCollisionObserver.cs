using Assets.Scripts.Entities;

namespace Assets.Scripts.Interfaces
{
    public interface ICubeCollisionObserver
    {
        public void OnCubeCollision(CubeEntity a, CubeEntity b);
    }
}