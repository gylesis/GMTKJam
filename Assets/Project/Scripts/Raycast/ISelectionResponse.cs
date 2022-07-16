namespace Assets.Scripts._3D.Selecting
{
    public interface ISelectionResponse
    {
        void OnSelect(ICustomSelectable selection);
        void OnDeselect(ICustomSelectable selection);
    }
}