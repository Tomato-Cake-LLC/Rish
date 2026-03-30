using UnityEngine.UIElements;

namespace RishUI
{
    public interface IVisualManipulator
    {
        bool Evaluate(VisualElement descendant);
        void Manipulate(VisualManipulationPhase phase, IManipulable descendant);
    }
}