using System;
using System.Collections.Generic;
using RishUI.Events;
using RishUI.MemoryManagement;
using Sappy;
using UnityEngine.UIElements;

namespace RishUI
{
    internal partial class Manipulable : IManipulable
    {
        private IBridge Bridge { get; }
#if UNITY_6000_0_OR_NEWER
        private UpdateBinding UpdateBinding { get; }
#endif
        private VisualElement Element { get; }
        private PickingManager PickingManager { get; }
        
        private Node Node { get; set; }
        
        int IManipulable.ID => Node.ID;

        VisualElement IManipulable.element => Element;

        string IManipulable.name
        {
            get => Element.name;
            set
            {
                if (IsBubblingUp) return;
                Element.name = value;
            }
        }
        
        [RequiresManagedContext]
        private ClassName CloneClassName() => new(Bridge.ClassName);
        ClassName IManipulable.CloneClassName() => CloneClassName();
        private bool AddClassName(string className)
        {
            if (Bridge.ClassName.Contains(className)) return false;
            using (ManagedContext.New())
            {
                var result = CloneClassName();
                result.Add(className);
                SetClassName(result);
            }

            return true;
        }
        bool IManipulable.AddClassName(string className) => AddClassName(className);
        bool IManipulable.AddClassName(RishString className) => AddClassName(className);
        int IManipulable.AddClassName(ClassName className)
        {
            var count = 0;
            using (ManagedContext.New())
            {
                var result = CloneClassName();
                foreach (var cn in className)
                {
                    if (result.Contains(cn)) continue;
                    result.Add(cn);
                    count++;
                }
                if(count > 0)
                {
                    SetClassName(result);
                }
            }
            return count;
        }
        private bool RemoveClassName(string className)
        {
            if (Bridge.ClassName.Contains(className)) return false;
            using (ManagedContext.New())
            {
                var result = new ClassName();
                foreach (var cn in Bridge.ClassName)
                {
                    if (cn == className) continue;
                    result.Add(cn);
                }
                SetClassName(result);
            }

            return true;
        }
        bool IManipulable.RemoveClassName(string className) => RemoveClassName(className);
        bool IManipulable.RemoveClassName(RishString className) => RemoveClassName(className);
        int IManipulable.RemoveClassName(ClassName className)
        {
            var count = 0;
            using (ManagedContext.New())
            {
                var result = new ClassName();
                foreach (var cn in Bridge.ClassName)
                {
                    if (className.Contains(cn))
                    {
                        count++;
                        continue;
                    }
                    result.Add(cn);
                }
                if(count > 0)
                {
                    SetClassName(result);
                }
            }
            return count;
        }
        private void SetClassName(ClassName className)
        {
            if (IsBubblingUp) return;
            Bridge.ClassName = className;
        }
        void IManipulable.SetClassName(ClassName className) => SetClassName(className);

        private ManipulableStyle _style;
        private ManipulableStyle Style
        {
            get
            {
                if (_style == null)
                {
                    var value = new ManipulableStyle(Element);
                    value.Reset(Bridge.Style, PickingManager.PointerDetection);
                    _style = value;
                }
                return _style;
            }
        }
        ManipulableStyle IManipulable.style => Style;

        private List<VisualManipulator> Manipulators { get; } = new();

        private bool _subscribedToVisualChanges;
        private bool SubscribedToVisualChanges
        {
            get => _subscribedToVisualChanges;
            set
            {
                if (_subscribedToVisualChanges == value) return;
                _subscribedToVisualChanges = value;
                if (value)
                {
#if UNITY_6000_0_OR_NEWER
                    UpdateBinding.OnUpdated.Add(Sappy.ReportBubbleUp);
#else
                    Element.schedule.Execute(Sappy.ReportBubbleUp.Callback).Until(Sappy.ShouldStopReporting);
#endif
                    Element.RegisterCallback(Sappy.OnVisualChange.Callback);
                }
                else
                {
#if UNITY_6000_0_OR_NEWER
                    UpdateBinding.OnUpdated.Remove(Sappy.ReportBubbleUp);
#endif
                    Element.UnregisterCallback(Sappy.OnVisualChange.Callback);
                }
            }
        }
        
        private bool IsBubblingUp { get; set; }
        bool IManipulable.IsBubblingUp => IsBubblingUp;

#if UNITY_6000_0_OR_NEWER
        public Manipulable(IBridge bridge, UpdateBinding updateBinding)
#else
        public Manipulable(IBridge bridge)
#endif
        {
            Bridge = bridge;
#if UNITY_6000_0_OR_NEWER
            UpdateBinding = updateBinding;
#endif
            var visualElement = bridge.Element;
            Element = visualElement;
            PickingManager = visualElement is ICustomPicking customPicking ? customPicking.Manager : null;
        }

        public void Setup(Node node)
        {
            Node = node;
        }

        public void Reset()
        {
            foreach (var manipulator in Manipulators)
            {
                manipulator.Remove(this);
            }
            Manipulators.Clear();
            IsBubblingUp = false;
            SubscribedToVisualChanges = false;
        }

        public void BubbleUp()
        {
            Manipulators.Clear();
            var manipulator = Node.VisualManipulator;
            while (manipulator != null)
            {
                if (manipulator.Evaluate(this))
                {
                    Manipulators.Add(manipulator);
                }
                manipulator = manipulator.Parent;
            }
            
            if (Manipulators.Count > 0)
            {
                _style?.Reset(Bridge.Style, PickingManager.PointerDetection);
                SubscribedToVisualChanges = true;
                IsBubblingUp = true;
            }
            else
            {
                IsBubblingUp = false;
                SubscribedToVisualChanges = false;
            }
        }

        [SapTarget(typeof(Func<bool>))]
        private bool ShouldStopReporting() => !SubscribedToVisualChanges;

        [SapTarget(typeof(EventCallback<VisualChangeEvent>))]
        private void OnVisualChange(VisualChangeEvent evt) => ReportBubbleUp();
        
        [SapTarget]
        private void ReportBubbleUp()
        {
            if (IsBubblingUp)
            {
                IsBubblingUp = false;
                
                foreach (var manipulator in Manipulators)
                {
                    manipulator.BubbleUp(this);
                }
                
                ApplyInlineStyle();
            }
        }

        private void ApplyInlineStyle()
        {
            if (Style.ProcessFinalStyle(out var style))
            {
                Bridge.Style = style;
            }
        }
        void IManipulable.ApplyInlineStyle() => ApplyInlineStyle();
    }
}