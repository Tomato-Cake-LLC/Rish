using Sappy;
using UnityEngine.UIElements;

namespace RishUI
{
    public interface IInternalVisualElement : IElement
    {
        internal IBridge Bridge { get; }
        
        VisualElement IElement.GetVisualChild() => this as VisualElement;

        int IElement.ID => Bridge?.ID ?? -1;
        SapTargets IElement.OnMounted => Bridge.OnMounted;
        SapTargets IElement.OnUnmounted => Bridge.OnUnmounted;
        void IElement.Dirty(bool immediate) { }
    }
    
    /// <summary>
    /// Allows mounting a VisualElement to a Rish tree. The element type has Props.
    /// </summary>
    public interface IVisualElement<P> : IInternalVisualElement, ICustomPicking where P : struct
    {
        Bridge<P> Bridge { get; }
        
        void Setup(P props);

        IBridge IInternalVisualElement.Bridge => Bridge;
    }
    
    /// <summary>
    /// Allows mounting a VisualElement to a Rish tree.
    /// </summary>
    public interface IVisualElement : IVisualElement<NoProps> {
        Bridge Bridge { get; }
        Bridge<NoProps> IVisualElement<NoProps>.Bridge => Bridge;
        
        void Setup();
        void IVisualElement<NoProps>.Setup(NoProps props) => Setup();
    }
}