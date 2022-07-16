using UnityEngine;

namespace Project.Scripts.Raycast
{
    public interface IRayProvider
    {
        Ray CreateRay();
    }
}