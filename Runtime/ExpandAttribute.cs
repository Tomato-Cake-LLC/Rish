using System;
using UnityEngine.Scripting;

namespace RishUI
{
    /// <summary>
    /// Tells Rishenerator to expand this property field in factory methods. 
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class ExpandAttribute : PreserveAttribute { }
}
