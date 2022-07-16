using Project.Scripts.Raycast;

namespace Project.Scripts.Raycast.Selecting
{
    public interface ISelectionResponse
    {
        void OnSelect(ICustomSelectable selection);
        void OnDeselect(ICustomSelectable selection);
    }
}