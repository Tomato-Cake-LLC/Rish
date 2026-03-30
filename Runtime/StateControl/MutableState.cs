using System;
using Sappy;

namespace RishUI.StateControl
{
    public partial class MutableState<T> : StateControl<T> where T : IEquatable<T>
    {
        public static MutableState<T> Create(int id, IElement element, T initialValue)
        {
            var control = GetFromPool<MutableState<T>>();
            control.Init(id, element, null, initialValue);
            return control;
        }
        protected override void Free() => ReturnToPool(this);

        [SapTarget(nested: false)]
        public void SetValue(T value) => Value = value;
    }
}