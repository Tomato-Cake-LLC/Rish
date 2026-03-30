using Sappy;
using UnityEngine.UIElements;

namespace RishUI
{
    public interface IElement
    {
        int ID { get; }
        
        SapTargets OnMounted { get; }
        SapTargets OnUnmounted { get; }

        VisualElement GetVisualChild();
        
        void Dirty(bool immediate);
    }
}