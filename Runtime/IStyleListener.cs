namespace RishUI
{
    /// <summary>
    /// Visual style attribute event listener.
    /// </summary>
    public interface IStyleListener
    {
        /// <summary>
        /// Gets called right after a VisualElement gets its inline style set.
        /// </summary>
        void StyleSet(Style style);
    }
}