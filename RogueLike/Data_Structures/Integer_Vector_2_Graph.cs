
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Integer_Vector_2_Graph 
    {
        public delegate bool Graph_Searcher(int target, Integer_Vector_2 vector);

        public int Graph__VERTEX_COUNT { get; }
        public int Graph__Edge_Count { get; private set; }

        internal HashSet<int>[] Graph__ADJACENCY { get; }
        private Integer_Vector_2[] Graph__VECTORS { get; }

        internal Dictionary<Integer_Vector_2, int> Graph__VERTEX_LOOKUP { get; }

        public Integer_Vector_2_Graph(int vertex_count)
        {
            Graph__VERTEX_COUNT = vertex_count;

            Graph__ADJACENCY = new HashSet<int>[vertex_count];
            Graph__VECTORS = new Integer_Vector_2[vertex_count];
            Graph__VERTEX_LOOKUP = new Dictionary<Integer_Vector_2, int>();
        }

        public void Define__Vertex__Graph(int vertex, Integer_Vector_2 vector)
        {
            bool invalid_vertex = 
                Assert__Invalid_Vertex(this, vertex, this);

            bool invalid_vector =
                Assert__Duplicate_Vector(this, vector, this);

            bool invalid =
                invalid_vertex
                ||
                invalid_vector;

            if (invalid)
                return;

            Graph__VECTORS[vertex] = vector;
            if (Graph__VERTEX_LOOKUP.ContainsKey(vector))
                Graph__VERTEX_LOOKUP[vector] = vertex;
            else
                Graph__VERTEX_LOOKUP.Add(vector, vertex);
        }

        public void Define__Double_Adjacent__Graph(int vertex_a, int vertex_b)
        {
            Define__Adjacent__Graph(vertex_a, vertex_b);
            Define__Adjacent__Graph(vertex_b, vertex_a);
        }

        public void Define__Adjacent__Graph(int vertex_a, int vertex_b)
        {
            bool invalid_a = 
                Assert__Invalid_Vertex(this, vertex_a, this);
            bool invalid_b =
                Assert__Invalid_Vertex(this, vertex_b, this);

            bool invalid =
                invalid_a
                ||
                invalid_b;

            if (invalid)
                return;

            if (Graph__ADJACENCY[vertex_a] == null)
                Graph__ADJACENCY[vertex_a] = new HashSet<int>();
            if (!Graph__ADJACENCY[vertex_a].Contains(vertex_b))
                Graph__ADJACENCY[vertex_a].Add(vertex_b);

            Graph__Edge_Count++;
        }

        public Integer_Vector_2_Graph Copy__Graph()
        {
            Integer_Vector_2_Graph copy = 
                new Integer_Vector_2_Graph(Graph__VERTEX_COUNT);

            for(int v=0;v<Graph__VERTEX_COUNT;v++)
            {
                foreach(int adj_v in Graph__ADJACENCY[v])
                    copy.Define__Adjacent__Graph(v, adj_v);
                copy.Define__Vertex__Graph(v, Graph__VECTORS[v]);
            }

            return copy;
        }

        public bool Check_If__Has_Adjacents__Graph(int vertex)
        {
            bool invalid = 
                Assert__Invalid_Vertex
                (
                    this,
                    vertex,
                    this
                );

            if (invalid)
                return false;

            return Graph__ADJACENCY[vertex] != null && Graph__ADJACENCY[vertex].Count > 0;
        }

        public void Search__Breadth_First__Graph
        (
            Graph_Searcher search_terminator,
            int source
        )
        {
            bool invalid =
                Assert__Invalid_Search_Operation
                (
                    this,
                    search_terminator, 
                    source,
                    this
                );

            if (invalid)
                return;

            Queue<int> bfs_queue = new Queue<int>();
            bool[] marked = new bool[Graph__VERTEX_COUNT];

            bfs_queue.Enqueue(source);

            while(bfs_queue.Count > 0)
            {
                int v = bfs_queue.Dequeue();
                marked[v] = true;

                bool terminate =
                    search_terminator(v, this[v]);

                if (terminate)
                    return;

                foreach(int adj in Graph__ADJACENCY[v])
                    if(!marked[adj])
                        bfs_queue.Enqueue(adj);
            }
        }

        public void Search__Depth_First__Graph
        (
            Graph_Searcher search_terminator,
            int source
        )
        {
            bool invalid =
                Assert__Invalid_Search_Operation
                (
                    this,
                    search_terminator,
                    source,
                    this
                );

            if (invalid)
                return;

            
        }

        private void Private_Search__Depth_First__Graph
        (
            Graph_Searcher search_terminator,
            bool[] marked,
            int target,
            out bool terminate
        )
        {
            marked[target] = true;

            terminate =
                search_terminator(target, this[target]);

            foreach(int adj in Graph__ADJACENCY[target])
            {
                if (terminate)
                    return;
                if (marked[adj])
                    continue;

                Private_Search__Depth_First__Graph
                (
                    search_terminator,
                    marked,
                    adj,
                    out terminate
                );
            }
        }

        public override string ToString()
        {
            string s = "";
            for(int v=0;v<Graph__VERTEX_COUNT;v++)
            {
                string s_adj = "";
                foreach(int adj in Graph__ADJACENCY[v])
                    s_adj += $"({adj}), ";
                s_adj = s_adj.Substring(0, s_adj.Length-2);
                string s_v = $"[v:{v} -> {s_adj}], ";
                s+= s_v;
            }
            return s.Substring(0, s.Length-2);
        }

        public int Get__Vertex__Graph(Integer_Vector_2 vector)
        {
            bool invalid =
                Assert__Invalid_Vector(this, vector, this);

            if (invalid)
                return -1;

            return Graph__VERTEX_LOOKUP[vector];
        }

        public bool Check_If__Contains_Vector__Graph(Integer_Vector_2 vector)
            => Graph__VERTEX_LOOKUP.ContainsKey(vector);

        public Integer_Vector_2 this[int vertex]
        {
            get 
            {
                bool invalid =
                    Assert__Invalid_Vertex
                    (
                        this,
                        vertex,
                        this
                    );

                if (invalid)
                    return new Integer_Vector_2(-1,-1);

                return Graph__VECTORS[vertex];
            }
            set 
            {
                bool invalid =
                    Assert__Invalid_Vertex
                    (
                        this,
                        vertex,
                        this
                    );

                if (invalid)
                    return;

                Graph__VECTORS[vertex] = value;
            }
        }

        public static bool Assert__Invalid_Vertex
        (
            Integer_Vector_2_Graph graph, 
            int v, 
            object owner,
            string context = null
        )
        {
            if (context != null)
                context = " " + context;
            if (v < 0 || v >= graph.Graph__VERTEX_COUNT)
            {
                Private_Log_Error__Graph
                (
                    $"Vertex {v} is out of bounds of [0:{graph.Graph__VERTEX_COUNT-1}]!",
                    owner,
                    context
                );
                return true;
            }   

            return false;
        }

        public static bool Assert__Invalid_Vector
        (
            Integer_Vector_2_Graph graph,
            Integer_Vector_2 vector,
            object owner,
            string context =null
        )
        {
            bool invalid = !graph.Check_If__Contains_Vector__Graph(vector);

            if (invalid)
            {
                Private_Log_Error__Graph
                (
                    $"Vector {vector} is not present!",
                    owner,
                    context
                );

                return true;
            }

            return false;
        }

        public static bool Assert__Duplicate_Vector
        (
            Integer_Vector_2_Graph graph,
            Integer_Vector_2 vector,
            object owner,
            string context = null,
            bool check_if_exists_or_unique = true
        )
        {
            bool invalid = graph.Check_If__Contains_Vector__Graph(vector);

            if (invalid)
            {
                Private_Log_Error__Graph
                (
                    $"Vector: {vector} is not unique!",
                    owner,
                    context
                );

                return true;
            }

            return false;
        }

        public static bool Assert__Invalid_Searcher
        (
            Graph_Searcher searcher,
            object owner,
            string context = null
        )
        {
            if (searcher == null)
            {
                Private_Log_Error__Graph
                (
                    "Searcher was null!",
                    owner,
                    context
                );
                return true;
            }

            return false;
        }

        public static bool Assert__Invalid_Search_Operation
        (
            Integer_Vector_2_Graph graph,
            Graph_Searcher search_terminator,
            int source,
            object owner,
            string context_searcher = null,
            string context_source = null
        )
        {
            bool invalid_vertex = 
                Assert__Invalid_Vertex(graph, source, owner, context_source);
            bool invalid_searcher =
                Assert__Invalid_Searcher(search_terminator, owner, context_searcher);

            bool invalid =
                invalid_vertex
                ||
                invalid_searcher;

            return invalid;
        }

        private static void Private_Log_Error__Graph
        (
            string message,
            object owner,
            string context = null
        )
        {
            if (context != null)
                context = " " + context;
            Log.Write__Error__Log
            (
                $"{message}{{0}}",
                owner,
                Log_Message_Type.Error__System,
                context
            );
        }
    }
}
