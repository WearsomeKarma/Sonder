using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Entity
    {
        public Integer_Vector_3 Entity__Position { get; internal set; }
        public Vertex_Object_Handle Entity__VO { get; internal set; }

        public Entity(Integer_Vector_3? position = null)
        {
            Entity__Position = position ?? new Integer_Vector_3();
        }
    }
}
