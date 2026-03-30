using System;
using Sappy;

namespace RishUI.StateControl
{
    public partial class SapState<T> : StateControl<T> where T : IEquatable<T>
    {
        private SapTargets<T> _onUpdate;
        private SapTargets<T> OnUpdate
        {
            set
            {
                if (_onUpdate == value) return;
                
                _onUpdate?.Remove(Sappy.SetValue);
                _onUpdate = value;
                value?.Add(Sappy.SetValue);
            }
        }

        public static SapState<T> Create(int id, IElement element, SapTargets<T> onUpdate, Action<T> setter, T initialValue)
        {
            var control = GetFromPool<SapState<T>>();
            control.Init(id, element, setter, initialValue);
            control.Setup(onUpdate);
            return control;
        }
        
        internal void Setup(SapTargets<T> onUpdate)
        {
            OnUpdate = onUpdate;
        }

        protected override void Free()
        {
            OnUpdate = null;
            ReturnToPool(this);
        }

        [SapTarget]
        public void SetValue(T value) => Value = value;
    }
}