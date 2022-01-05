
using System.Collections.Generic;
using System.Linq;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class SA__Level_Hallway_Linking :
        Streamline_Argument
    {
        private KDTree_R2_Lattice Level_Hallway_Linking__STRUCTURE_LATTICE { get; }

        private Integer_Vector_3_Graph Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH { get; }
        private Integer_Vector_3_Graph Level_Hallway_Linking__HALLWAY_ROUTES_GRAPH { get; }

        private bool[] Level_Hallway_Linking__MARKED_VERTICIES { get; }

        public int Level_Hallway_Linking__VERTEX_COUNT
            => Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH.Graph__VERTEX_COUNT;
        public IEnumerable<int> Get__Adjacent_Vertices__Level_Hallway_Linking(int v)
            => Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH.Graph__ADJACENCY[v].ToArray();

        public IEnumerable<Integer_Vector_3> Level_Hallway_Linking__HALLWAY_EXITS { get; }
        
        private Dictionary<Plane_R3, Room> Level_Hallway_Linking__HALLWAY_LOOKUP { get; }

        internal SA__Level_Hallway_Linking
        (
            KDTree_R2_Lattice structure_lattice,
            Integer_Vector_3_Graph hallway_network_graph,
            Integer_Vector_3_Graph hallway_routes_graph,
            IEnumerable<Integer_Vector_3> keys,
            Dictionary<Plane_R3, Room> hallway_lookup
        )
        {
            Level_Hallway_Linking__STRUCTURE_LATTICE = structure_lattice;

            Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH = hallway_network_graph;
            Level_Hallway_Linking__HALLWAY_ROUTES_GRAPH = hallway_routes_graph;

            Level_Hallway_Linking__MARKED_VERTICIES =
                new bool[Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH.Graph__VERTEX_COUNT];

            Level_Hallway_Linking__HALLWAY_EXITS = keys;

            Level_Hallway_Linking__HALLWAY_LOOKUP = hallway_lookup;
        }

        public bool Declare__Hallway_Route__Level_Hallway_Linking(int vertex_a, int vertex_b)
        {
            bool invalid_a = 
                Integer_Vector_3_Graph
                .Assert__Invalid_Vertex
                (
                    Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH,
                    vertex_a,
                    this
                );

            bool invalid_b =
                Integer_Vector_3_Graph
                .Assert__Invalid_Vertex
                (
                    Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH,
                    vertex_b,
                    this
                );

            bool invalid =
                invalid_a
                ||
                invalid_b;

            if (invalid)
                return false;

            bool is_illegal = 
                !Level_Hallway_Linking__HALLWAY_NETWORK_GRAPH
                .Graph__ADJACENCY[vertex_a]
                .Contains(vertex_b);

            if (is_illegal)
                return false; //TODO: Log error!

            if (Level_Hallway_Linking__MARKED_VERTICIES[vertex_a])
                return false; //TODO: Log error!

            Level_Hallway_Linking__MARKED_VERTICIES[vertex_a] = true;

            Level_Hallway_Linking__HALLWAY_ROUTES_GRAPH
                .Define__Double_Adjacent__Graph(vertex_a, vertex_b);

            return true;
        }

        public bool Declare__Hallway__Level_Hallway_Linking(Integer_Vector_3 position, Room hallway)
        {
            Plane_R3? nullable_closest_partition =
                Level_Hallway_Linking__STRUCTURE_LATTICE
                .Get__Closest_Partition__KDTree_R2_Lattice(position);

            if (nullable_closest_partition == null)
                return false; //TODO: Log error.

            Plane_R3 closest_partition = 
                (Plane_R3)nullable_closest_partition;

            if (Level_Hallway_Linking__HALLWAY_LOOKUP.ContainsKey(closest_partition))
                return false; //TODO: Log error!

            Level_Hallway_Linking__HALLWAY_LOOKUP
                .Add(closest_partition, hallway);

            return true;
        }

        public Room Get__Hallway__Level_Hallway_Linking(Integer_Vector_3 position)
        {
            Plane_R3? nullable_closest_partition =
                Level_Hallway_Linking__STRUCTURE_LATTICE
                .Get__Closest_Partition__KDTree_R2_Lattice(position);

            if (nullable_closest_partition == null)
                return null;

            Plane_R3 closest_partition =
                (Plane_R3)nullable_closest_partition;

            if (!Level_Hallway_Linking__HALLWAY_LOOKUP.ContainsKey(closest_partition))
                return null;

            return Level_Hallway_Linking__HALLWAY_LOOKUP[closest_partition];
        }

        public bool Check_If__Contains_Hallway_Definition__Level_Hallway_Linking(Integer_Vector_3 position)
        {
            bool contains =
                Get__Hallway__Level_Hallway_Linking(position) != null;

            return contains;
        }
    }
}
