using ColorGame.Common.ColorManagement;

namespace ColorGame.Common.InputManagement.Interfaces
{
    public interface IInteractable
    {
        void Interact();
        void MakeClickable(bool isClickable);
        void ChangeColor(ColorSet data);
    }
}