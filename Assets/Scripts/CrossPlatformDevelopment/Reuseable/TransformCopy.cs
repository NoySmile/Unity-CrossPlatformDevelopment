using UnityEngine;

namespace CrossPlatformDevelopment.Reuseable
{
    public class TransformCopy
    {
        public Vector3 localPosition;
        public Quaternion localRotation;
        public Vector3 localScale;

        public TransformCopy(Transform t)
        {
            localPosition = t.localPosition;
            localRotation = t.localRotation;
            localScale = t.localScale;
        }
    }
}