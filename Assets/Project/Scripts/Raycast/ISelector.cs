using UnityEngine;

namespace Project.Scripts.Raycast.Selecting
{
    public interface ISelector
    {
        void Check(Ray ray);
        ICustomSelectable GetSelection();
    }
}