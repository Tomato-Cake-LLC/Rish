using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Sappy;

namespace RishUI.StateControl
{
    public static class StateControls
    {
        private static Dictionary<int, Group> Groups { get; } = new();
        
        private static Stack<Group> GroupsPool { get; } = new();
        
        public static MutableState<T> MutableState<T>(this IElement element, T initialValue = default, [CallerLineNumber] int id = 0) where T : IEquatable<T>
        {
            var group = GetGroupOrNew(element);
            if (group.TryGetControl<MutableState<T>>(id, out var control)) return control;
            
            control = StateControl.MutableState<T>.Create(id, element, initialValue);
            group.AddControl(id, control);

            return control;
        }
        
        public static StateControl<T> SapState<T>(this IElement element, SapTargets<T> onUpdate, Action<T> setter = null, T initialValue = default, [CallerLineNumber] int id = 0) where T : IEquatable<T>
        {
            var group = GetGroupOrNew(element);
            if (group.TryGetControl<SapState<T>>(id, out var control))
            {
                control.Setup(onUpdate);
                return control;
            }
            
            control = StateControl.SapState<T>.Create(id, element, onUpdate, setter, initialValue);
            group.AddControl(id, control);

            return control;
        }

        public static void RemoveStateControl(this IElement element, IStateControl stateControl)
        {
            if (TryGetGroup(element, out var group))
            {
                group.RemoveControl(stateControl);
            }
        }

        public static Group GetGroupOrNew(IElement element) => GetGroupOrNew(element.ID, element);
        public static Group GetGroupOrNew(int id, IElement element)
        {
            if (!TryGetGroup(id, out var group))
            {
                group = GetFromPool();
                Groups.Add(id, group);
            }

            group.Setup(element);
            
            return group;
        }
        
        public static bool TryGetGroup(IElement element, out Group group) => TryGetGroup(element.ID, out group);
        public static bool TryGetGroup(int id, out Group group) => Groups.TryGetValue(id, out group);

        private static Group GetFromPool() => GroupsPool.TryPop(out var group) ? group : new Group();
        internal static void ReturnToPool(int id)
        {
            if (Groups.Remove(id, out var group))
            {
                GroupsPool.Push(group);
            }
        }
    }
}