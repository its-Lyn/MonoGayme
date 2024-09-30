using System.Collections.Generic;
using MonoGayme.Components;

namespace MonoGayme.Controllers;

public class ComponentController
{
    public List<Component> Components { get; private set; } = [];

    public void AddComponent(Component component)
        => Components.Add(component);

    /// <summary>
    /// Get the first component with a matching type.
    /// </summary>
    public T? GetComponent<T>() where T : Component
        => (T?)Components.Find(c => c is T);

    /// <summary>
    /// Get a component based on it's name
    /// </summary>
    public T? GetComponent<T>(string name) where T : Component
        => (T?)Components.Find(c => c.Name == name);

    /// <summary>
    /// Remove a component.
    /// </summary>
    public void Remove<T>(T component) where T : Component
       => Components.Remove(component);

    /// <summary>
    /// Remove a component using it's name.
    /// </summary>
    public void Remove(string name)
    {
        Component? c = Components.Find(c => c.Name == name);
        if (c is null) return;

        Components.Remove(c);
    }
}
