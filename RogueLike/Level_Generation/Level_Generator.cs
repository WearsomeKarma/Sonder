
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Level_Generator :
        Xerxes_Object<Level_Generator>
    {
        private Dictionary<Rect_Prism, Room> Level_Generator__ROOM_LOOKUP  { get; }
        private Dictionary<Plane_R3, Room> Level_Generator__HALLWAY_LOOKUP { get; }

        private Dictionary<Rect_Prism, KDTree_R3> Level_Generator__SPLICED_LEVELS { get; }
        private Dictionary<Rect_Prism, KDTree_R2_Lattice> Level_Generator__LATTICES { get; }

        private Dictionary<Rect_Prism, Integer_Vector_3_Graph> Level_Generator__HALLWAY_ADJACENCY { get; }

        private Dictionary<Rect_Prism, Integer_Vector_3_Graph> Level_Generator__HALLWAY_ROUTES { get; }

        public Level_Generator()
        {
            Level_Generator__ROOM_LOOKUP =
                new Dictionary<Rect_Prism, Room>();
            Level_Generator__HALLWAY_LOOKUP =
                new Dictionary<Plane_R3, Room>();

            Level_Generator__SPLICED_LEVELS =
                new Dictionary<Rect_Prism, KDTree_R3>();

            Level_Generator__LATTICES = 
                new Dictionary<Rect_Prism, KDTree_R2_Lattice>();

            Declare__Streams()
                .Downstream.Receiving<SA__Level_Generation>
                (Private_Generate__Level__Level_Generator)
                .Downstream.Extending<SA__Level_Partitioning>()
                .Downstream.Extending<SA__Level_Hallway_Plotting>()
                .Downstream.Extending<SA__Level_Compositing>();
        }

        private void Private_Generate__Level__Level_Generator(SA__Level_Generation e)
        {
            Private_Pass__Splicing_Stage__Level_Generator();
            Private_Pass__Partitioning_Stage__Level_Generator();
            Private_Pass__Plotting_Stage__Level_Generator();
            Private_Pass__Linking_Stage__Level_Generator();
            Private_Pass__Compositing_Stage__Level_Generator();
        }

        private void Private_Pass__Splicing_Stage__Level_Generator()
        {
            //TODO: Implement later.
        }

        private void Private_Pass__Partitioning_Stage__Level_Generator()
        {
            foreach(KDTree_R3 space_partition_tree in Level_Generator__SPLICED_LEVELS.Values)
            {
                SA__Level_Partitioning e_partition =
                    new SA__Level_Partitioning
                    (
                        space_partition_tree,
                        Level_Generator__ROOM_LOOKUP
                    );

                Invoke__Descending(e_partition);

                Level_Generator__LATTICES
                    .Add(space_partition_tree.KDTree__SPACE, new KDTree_R2_Lattice(space_partition_tree));
            }
        }

        private void Private_Pass__Plotting_Stage__Level_Generator()
        {
            foreach(KDTree_R2_Lattice structure_lattice in Level_Generator__LATTICES.Values)
            {
                SA__Level_Hallway_Plotting e_plotting =
                    new SA__Level_Hallway_Plotting
                    (
                        structure_lattice,
                        Level_Generator__ROOM_LOOKUP
                    );

                Invoke__Descending(e_plotting);

                Integer_Vector_3_Graph hallway_graph =
                    new Integer_Vector_3_Graph(0);
                int v = 0;

                foreach(Plane_R3 sub_plane in structure_lattice.Get__Sub_Surfaces__KDTree_R2_Lattice())
                {
                    foreach(Integer_Vector_3 vertex_a in sub_plane.Get__Vertices__Plane_R3())
                    {
                        foreach(Integer_Vector_3 vertex_b in sub_plane.Get__Vertices__Plane_R3())
                        {
                            if (vertex_a == vertex_b)
                                continue;

                            bool contains_a = 
                                hallway_graph.Check_If__Contains_Vector__Graph(vertex_a);
                            bool contains_b =
                                hallway_graph.Check_If__Contains_Vector__Graph(vertex_b);

                            int index_a, index_b;

                            if (!contains_a)
                                hallway_graph.Define__Vertex__Graph(index_a = v++, vertex_a);
                            else
                                index_a = hallway_graph.Get__Vertex__Graph(vertex_a);
                            if (!contains_b)
                                hallway_graph.Define__Vertex__Graph(index_b = v++, vertex_b);
                            else
                                index_b = hallway_graph.Get__Vertex__Graph(vertex_b);

                            hallway_graph.Define__Double_Adjacent__Graph(index_a, index_b);
                        }
                    }
                }

                Level_Generator__HALLWAY_ADJACENCY[structure_lattice.KDTree_R2_Lattice__SPACE] =
                    hallway_graph;
            }
        }

        private void Private_Pass__Linking_Stage__Level_Generator()
        {
            foreach(KDTree_R2_Lattice structure_lattice in Level_Generator__LATTICES.Values)
            {
                IEnumerable<Integer_Vector_3> keys = structure_lattice.Get__Keys__KDTree_R2_Lattice();

                Integer_Vector_3_Graph network_graph =
                    Level_Generator__HALLWAY_ADJACENCY[structure_lattice.KDTree_R2_Lattice__SPACE];

                Integer_Vector_3_Graph route_graph =
                    new Integer_Vector_3_Graph(network_graph.Graph__VERTEX_COUNT);

                SA__Level_Hallway_Linking e_linking =
                    new SA__Level_Hallway_Linking
                    (
                        structure_lattice,
                        network_graph, 
                        route_graph, 
                        keys,
                        Level_Generator__HALLWAY_LOOKUP
                    );

                Invoke__Descending(e_linking);

                Level_Generator__HALLWAY_ROUTES[structure_lattice.KDTree_R2_Lattice__SPACE] =
                    route_graph;
            }
        }

        private void Private_Pass__Compositing_Stage__Level_Generator()
        {
            foreach(Rect_Prism space in Level_Generator__SPLICED_LEVELS.Keys)
            {
                KDTree_R3 room_tree =
                    Level_Generator__SPLICED_LEVELS[space];
                Integer_Vector_3_Graph hallway_routes =
                    Level_Generator__HALLWAY_ROUTES[space];

                SA__Level_Compositing e_compositing =
                    new SA__Level_Compositing
                    (
                        null, //TODO: Set as level
                        Level_Generator__ROOM_LOOKUP.Values,
                        Level_Generator__HALLWAY_LOOKUP.Values
                    );

                Invoke__Descending(e_compositing);
            }
        }
    }
}
