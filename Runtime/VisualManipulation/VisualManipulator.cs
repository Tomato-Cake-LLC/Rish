namespace RishUI
{
    internal class VisualManipulator
    {
        private IVisualManipulator Element { get; }
        
        public VisualManipulator Parent { get; private set; }
        
        private IndexedList<int, IManipulable> Manipulables { get; } = new();

        public VisualManipulator(IVisualManipulator element)
        {
            Element = element;
        }
        
        public void SetParent(VisualManipulator parent)
        {
            Manipulables.Clear();
            Parent = parent;
        }

        public bool Evaluate(IManipulable manipulable)
        {
            if (Element.Evaluate(manipulable.element))
            {
                Manipulables.Set(manipulable.ID, manipulable);
                return true;
            }

            Remove(manipulable);
            return false;
        }
        
        public void Remove(IManipulable manipulable) => Manipulables.Remove(manipulable.ID);

        public void BubbleUp(IManipulable manipulable) => Element.Manipulate(VisualManipulationPhase.BubbleUp, manipulable);
        
        public void TrickleDown()
        {
            foreach (var manipulable in Manipulables)
            {
                if (manipulable.IsBubblingUp) continue;
                Element.Manipulate(VisualManipulationPhase.TrickleDown, manipulable);
                manipulable.ApplyInlineStyle();
            }
        }
    }
}