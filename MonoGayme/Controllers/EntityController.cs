using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGayme.Components;
using MonoGayme.Entities;

namespace MonoGayme.Controllers;

public class EntityController : Component
{
    public List<Entity> Entities { get; } = [];
    public Action<GraphicsDevice, GameTime, Entity>? OnEntityUpdate;

    private bool _sort;
    private readonly HashSet<Entity> _toRemove = [];

    /// <summary>
    /// Add an entity to the controller, and begin sorting by ZIndex.
    /// </summary>
    public void AddEntity<T>(T entity) where T : Entity
    {
        entity.LoadContent();
        Entities.Add(entity);

        _sort = true;
    }

    /// <summary>
    /// Get the first entity with a matching type.
    /// </summary>
    public T? GetFirst<T>() where T : Entity
        => (T?)Entities.Find(e => e is T);

    /// <summary>
    /// Queue entity for removal the next frame.
    /// </summary>
    public void QueueRemove<T>(T entity) where T : Entity
    {
        _toRemove.Add(entity);
    }

    /// <summary>
    /// Updates each entity, then removes any queried entities. 
    /// </summary>
    public void UpdateEntities(GraphicsDevice device, GameTime gameTime)
    {
        if (_sort)
        {
            Entities.Sort((e1, e2) => e1.ZIndex.CompareTo(e2.ZIndex));
            _sort = false;
        }

        foreach (Entity entity in Entities)
        {
            entity.Update(gameTime);
            OnEntityUpdate?.Invoke(device, gameTime, entity);
        }

        if (_toRemove.Count > 0)
        {
            Entities.RemoveAll(_toRemove.Contains);
            _toRemove.Clear();
        }
    }

    /// <summary>
    /// Draw each entity to the screen. 
    /// </summary>
    public void DrawEntities(SpriteBatch batch, GameTime gameTime)
    {
        foreach (Entity entity in Entities)
            entity.Draw(batch, gameTime);
    }
}
