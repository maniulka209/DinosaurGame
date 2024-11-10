
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinousaurGame.Entities
{
    public class EntityManager
    {
        public IEnumerable<IGameEntity> Entity()
        {
            return new ReadOnlyCollection<IGameEntity>(_entities);
        }
        public void Update(GameTime gameTime) 
        {
            foreach(IGameEntity entity in _entities)
            {
                entity.Update(gameTime);
            }

            foreach (IGameEntity entity in _entitiesToAdd)
            {
                _entities.Add(entity);
            }
            foreach (IGameEntity entity in _entitiesToRemove)
            {
                _entities.Remove(entity);
            }
            _entitiesToAdd.Clear();
            _entitiesToRemove.Clear();
        }

        public void Draw(GameTime gameTime , Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (IGameEntity entity in _entities.OrderBy(e => e.DrawOrder))
            {
                entity.Draw(gameTime,spriteBatch);
            }

        }

        public void AddEntity(IGameEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentNullException(nameof(entity) , " null cannot be added as an entity");
            }
            _entitiesToAdd.Add(entity);
        }
        public void RemoveEntity(IGameEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), " null is not a entity");
            }
            _entitiesToRemove.Add(entity);
        }
        public void Clear()
        {
            _entitiesToRemove.AddRange(_entities);
        }

        private readonly List<IGameEntity> _entities = new List<IGameEntity>(); 
        private readonly List<IGameEntity> _entitiesToAdd = new List<IGameEntity>(); 
        private readonly List<IGameEntity> _entitiesToRemove = new List<IGameEntity>(); 
    }
}
