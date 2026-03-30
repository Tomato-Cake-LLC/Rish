using System;
using System.Collections.Generic;
using Sappy;

namespace RishUI.StateControl
{
    public partial class Group
    {
        private Dictionary<int, int> Indices { get; } = new();
        private List<IStateControl> Controls { get; } = new();
        
        private IElement _element;
        private IElement Element
        {
            get => _element;
            set
            {
                if (_element == value) return;

                _element?.OnUnmounted.Remove(Sappy.OnUnmounted);
                _element = value;
                value?.OnUnmounted.Add(Sappy.OnUnmounted);
            }
        }
        
        internal void Setup(IElement element)
        {
            Element = element;
        }

        [SapTarget]
        private void OnUnmounted()
        {
            foreach (var control in Controls)
            {
                control.Free();
            }
            Indices.Clear();
            Controls.Clear();
            var id = Element.ID;
            Element = null;
            StateControls.ReturnToPool(id);
        }

        public void AddControl(int id, IStateControl control)
        {
            if (!Indices.TryAdd(id, Controls.Count)) throw new InvalidOperationException($"A State Control with ID {id} already exists.");
            Controls.Add(control);
        }
        internal void RemoveControl(IStateControl control)
        {
            var id = control.ID;
            if (Indices.Remove(id, out var index))
            {
                var lastIndex = Controls.Count - 1;
                if(index < lastIndex)
                {
                    var lastId = Controls[lastIndex].ID;
                    Indices[lastId] = index;
                    Controls.RemoveAtSwapBack(index);
                }
                control.Free();
            }
        }

        public bool TryGetControl<T>(int id, out T control) where T : IStateControl
        {
            if (Indices.TryGetValue(id, out var index) && Controls[index] is T result)
            {
                control = result;
                return true;
            }

            control = default;
            return false;
        }
    }
}