using UnityEngine;

public class ConditionalPropertyAttribute : PropertyAttribute
{
    public string Name;
    public bool Opposite;

    public ConditionalPropertyAttribute(string name, bool opposite = false)
    {
        Name = name;
        Opposite = opposite;
    }
}
