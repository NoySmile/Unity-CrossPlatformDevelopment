using UnityEngine;

namespace CrossPlatformDevelopment.Reuseable
{
    public interface IAttachable
    {
        void Attach(Transform target);
        void Detach();
        void Revert();
    }
}