using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    internal sealed class Entity_Manager<T> :
        Entity_Manager_Base
        where T : Entity, new()
    {
        private List<T> _Entity_Manager__ENTITIES { get; }
        private Level _Entity_Manager__Current_Level__REFERENCE { get; set; }

        public Entity_Manager()
        {
            _Entity_Manager__ENTITIES = new List<T>();

            Declare__Streams()
                .Downstream.Receiving<SA__Update>
                (
                    Private_Control__Entities__Entity_Manager
                )
                .Downstream.Receiving<SA__Render>
                (
                    Private_Draw__Entities__Entity_Manager
                )
                .Downstream.Receiving<SA__Level_Generation>
                (
                    Private_New__Level__Entity_Manager
                )
                .Downstream.Receiving<SA__Spawn_Entity<T>>
                (
                    Private_Spawn__Entity__Entity_Manager
                )
                .Upstream.Extending<SA__Draw_Entity>()
                .Downstream.Extending<SA__Control_Entity<T>>()
                .Upstream  .Receiving<SA__Move_Entity<T>>
                (
                    Private_Move__Entity__Entity_Manager
                );
        }

        private void Private_New__Level__Entity_Manager
        (SA__Level_Generation e)
        {
            _Entity_Manager__Current_Level__REFERENCE =
                e.Generate_Level__Level__Internal;
            _Entity_Manager__ENTITIES.Clear();
        }

        private void Private_Control__Entities__Entity_Manager
        (SA__Update e)
        {
            foreach(T entity in _Entity_Manager__ENTITIES)
            {
                SA__Control_Entity<T> e1 = 
                    new SA__Control_Entity<T>
                    (
                        e,
                        entity
                    );

                Invoke__Descending
                    (e1);
            }
        }

        private void Private_Draw__Entities__Entity_Manager
        (SA__Render e)
        {
            foreach(T entity in _Entity_Manager__ENTITIES)
            {
                SA__Draw_Entity e1 = 
                    new SA__Draw_Entity
                    (
                        e,
                        entity
                    );

                Invoke__Ascending
                    (e1);
            }
        }

        private void Private_Move__Entity__Entity_Manager
        (SA__Move_Entity<T> e)
        {
            if (!_Entity_Manager__ENTITIES.Contains(e.Move_Entity__ENTITY__Internal))
                return;
            if (_Entity_Manager__Current_Level__REFERENCE == null)
                return;

            bool invalidMove =
                _Entity_Manager__Current_Level__REFERENCE
                .Check_If__Not_Passable__Level(e.Move_Entity__POSITION__Internal);

            if (invalidMove)
                return;

            T entity = e.Move_Entity__ENTITY__Internal;

            entity.Entity__Position = e.Move_Entity__POSITION__Internal;
        }

        private void Private_Spawn__Entity__Entity_Manager
        (SA__Spawn_Entity<T> e)
        {
            T entity = new T();

            entity.Entity__Position = e.Spawn_Entity__Position;

            _Entity_Manager__ENTITIES
                .Add(entity);
        }
    }
}
