using System;
using System.Collections.Generic;

namespace RishUI.StateControl
{
    public interface IStateControl
    {
        public int ID { get; }
        void Free();
    }
    
    public abstract class StateControl<T> : IStateControl where T : IEquatable<T>
    {
        private static Dictionary<Type, IPool> Pools { get; } = new();
        
        private int ID { get; set; }
        int IStateControl.ID => ID;
        private IElement Element { get; set; }
        
        private Action<T> Setter { get; set; }

        private T _value;
        public T Value
        {
            get => _value;
            protected set
            {
                if (_value.Equals(value)) return;
                
                _value = value;

                if (Setter != null)
                {
                    Setter(value);
                }
                else
                {
                    Element.Dirty(false);
                }
            }
        }

        protected void Init(int id, IElement element, Action<T> setter, T initialValue)
        {
            ID = id;
            Element = element;
            Setter = setter;
            Value = initialValue;
        }

        protected abstract void Free();
        void IStateControl.Free() => Free();
        
        public static implicit operator T(StateControl<T> control) => control.Value;

        protected static T1 GetFromPool<T1>() where T1 : StateControl<T>, new()
        {
            var type = typeof(T1);
            if (!Pools.TryGetValue(type, out var pool))
            {
                pool = new Pool<T1>();
                Pools.Add(type, pool);
            }
            
            return ((Pool<T1>)pool).GetNew();
        }

        protected static void ReturnToPool<T1>(T1 control) where T1 : StateControl<T>, new()
        {
            var type = typeof(T1);
            if (!Pools.TryGetValue(type, out var pool))
            {
                pool = new Pool<T1>();
                Pools.Add(type, pool);
            }
            
            ((Pool<T1>)pool).Return(control);
        }
        
        private interface IPool { }
        private class Pool<T1> : IPool where T1 : StateControl<T>, new()
        {
            private Stack<T1> Stack { get; } = new();
        
            public T1 GetNew() => Stack.TryPop(out var control) ? control : new T1();

            public void Return(T1 control) => Stack.Push(control);
        }
    }
}