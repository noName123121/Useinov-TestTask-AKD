using UnityEngine;

namespace Useinov.TestTask.Runtime
{
    public interface IInputHandler
    {
        void ReadInput();
        Vector2 GetMoveInput();
        Vector2 GetLookInput();
        IInteractable GetInteractable();
        void ConsumeInput();
    }
}