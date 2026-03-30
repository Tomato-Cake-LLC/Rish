#if UNITY_6000_0_OR_NEWER
using Sappy;
using UnityEngine.UIElements;

namespace RishUI
{
    public partial class UpdateBinding : CustomBinding
    {
        private SapStem OnUpdatedStem { get; } = new();
        public SapTargets OnUpdated => OnUpdatedStem.Targets;
        
        private VisualElement Element { get; }
        
        public UpdateBinding(VisualElement element)
        {
            Element = element;
            updateTrigger = BindingUpdateTrigger.WhenDirty;
        }
        
        protected override BindingResult Update(in BindingContext context)
        {
            // TODO: Test resolved style bindings
            Element.schedule.Execute(Sappy.Scheduled).Until(Sappy.IsDone);
            return base.Update(in context);
        }

        [SapTarget]
        private void Scheduled()
        {
            if (isDirty) return;
            
            OnUpdatedStem.Send();
        }

        [SapTarget]
        private bool IsDone() => !isDirty;
    }
}
#endif