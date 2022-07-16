using UnityEngine;

namespace Assets.Scripts._3D.Selecting
{
    public interface ISelector
    {
        void Check(Ray ray);
        ICustomSelectable GetSelection();
    }
}