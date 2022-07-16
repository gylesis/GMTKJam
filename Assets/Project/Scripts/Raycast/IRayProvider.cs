using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public interface IRayProvider
    {
        Ray CreateRay();
    }
}