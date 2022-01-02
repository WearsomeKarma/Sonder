
using System.Collections.Generic;
using System.Linq;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Integer_Vector_3_Graph
    {
        public int Graph__VERTEX_COUNT { get; }
        public int Graph__Edge_Count { get; }

        internal HashSet<int>[] Graph__ADJACENCY { get; }
        private Integer_Vector_3[] Graph__VECTORS { get; }

        private Dictionary<Integer_Vector_3, int> Graph__VERTEX_LOOKUP { get; }

        public Integer_Vector_3_Graph(int vertex_count)
        {
            Graph__VERTEX_COUNT = vertex_count;

            Graph__ADJACENCY = new HashSet<int>[vertex_count];
            Graph__VECTORS = new Integer_Vector_3[vertex_count];

            Graph__VERTEX_LOOKUP = new Dictionary<Integer_Vector_3, int>();
        }

        public Integer_Vector_3_Graph(Integer_Vector_3_Graph copy)
        {
            Graph__VERTEX_COUNT = copy.Graph__VERTEX_COUNT;

            Graph__ADJACENCY = new HashSet<int>[Graph__VERTEX_COUNT];

            for(int v=0;v<Graph__VERTEX_COUNT;v++)
                if (copy.Graph__ADJACENCY[v] != null)
                    Graph__ADJACENCY[v] = new HashSet<int>(copy.Graph__ADJACENCY[v]);

            Graph__VECTORS = copy.Graph__VECTORS.ToArray();

            Graph__VERTEX_LOOKUP = new Dictionary<Integer_Vector_3, int>(copy.Graph__VERTEX_LOOKUP);
        }

        public void Define__Vertex__Graph(int vertex, Integer_Vector_3 vector)
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
            {
                Graph__VERTEX_LOOKUP[vector] = vertex;
                return;
            }

            Graph__VERTEX_LOOKUP.Add(vector, vertex);
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

            if (Graph__ADJACENCY[vertex_a].Contains(vertex_b))
                return;

            Graph__ADJACENCY[vertex_a].Add(vertex_b);
        }

        public void Define__Double_Adjacent__Graph(int vertex_a, int vertex_b)
        {
            Define__Adjacent__Graph(vertex_a, vertex_b);
            Define__Adjacent__Graph(vertex_b, vertex_a);
        }

        public int Get__Vertex__Graph(Integer_Vector_3 vector)
        {
            bool invalid =
                Assert__Missing_Vector
                (
                    this,
                    vector,
                    this
                );

            if (invalid)
                return -1;

            return Graph__VERTEX_LOOKUP[vector];
        }

        public Integer_Vector_3 this[int vertex]
        {
            get
            {
                bool invalid =
                    Assert__Invalid_Vertex(this, vertex, this);

                if(invalid)
                    return new Integer_Vector_3(-1,-1,-1);

                return Graph__VECTORS[vertex];
            }
            set
            {
                Define__Vertex__Graph(vertex, value);
            }
        }

        public bool Check_If__Has_Adjacents__Graph(int vertex)
        {
            bool invalid =
                Assert__Invalid_Vertex(this, vertex, this);

            if (invalid)
                return false;

            return Graph__ADJACENCY[vertex] != null;
        }

        public bool Check_If__Contains_Vector__Graph(Integer_Vector_3 vector)
            => Graph__VERTEX_LOOKUP.ContainsKey(vector);

        public IEnumerable<int> Search__Breadth_First__Graph
        (
            int source
        )
        {
            bool invalid =
                Assert__Invalid_Vertex
                (
                    this,
                    source,
                    this
                );

            if (invalid)
                yield break;

            Queue<int> bfs_queue = new Queue<int>();
            bool[] marked = new bool[Graph__VERTEX_COUNT];

            bfs_queue.Enqueue(source);

            while(bfs_queue.Count > 0)
            {
                int v = bfs_queue.Dequeue();

                marked[v] = true;

                yield return v;
                
                if (Graph__ADJACENCY[v] == null)
                    continue;

                foreach(int adj in Graph__ADJACENCY[v])
                    if (!marked[adj])
                        bfs_queue.Enqueue(adj);
            }
        }

        public IEnumerable<int> Search__Depth_First__Graph
        (
            int source
        )
        {
            bool invalid =
                Assert__Invalid_Vertex
                (
                    this,
                    source,
                    this
                );

            if (invalid)
                yield break;

            bool[] marked = new bool[Graph__VERTEX_COUNT];

            foreach(int v in Private_Recursive_Search__Depth_First__Graph(marked, source))
                yield return v;
        }

        private IEnumerable<int> Private_Recursive_Search__Depth_First__Graph
        (
            bool[] marked,
            int vertex
        )
        {
            marked[vertex] = true;

            yield return vertex;

            if (Graph__ADJACENCY[vertex] == null)
                yield break;

            foreach(int adj in Graph__ADJACENCY[vertex])
            {
                IEnumerator<int> recursive_enumator =
                    Private_Recursive_Search__Depth_First__Graph(marked, adj)
                    .GetEnumerator();

                while(recursive_enumator.MoveNext())
                    yield return recursive_enumator.Current;
            }
        }
        
        public static bool Assert__Invalid_Vertex
        (
            Integer_Vector_3_Graph graph,
            int v,
            object owner,
            string context = null
        )
        {
            if (v < 0 || v > graph.Graph__VERTEX_COUNT)
            {
                Private_Log_Error__Graph
                (
                    $"Vertex:{v} is out of range [0:{graph.Graph__VERTEX_COUNT-1}]!",
                    owner,
                    context
                );
                return true;
            }
            return false;
        }

        public static bool Assert__Duplicate_Vector
        (
            Integer_Vector_3_Graph graph,
            Integer_Vector_3 vector,
            object owner,
            string context = null
        )
        {
            bool invalid = graph.Check_If__Contains_Vector__Graph(vector);

            if (invalid)
            {
                Private_Log_Error__Graph
                (
                    $"Vector {vector} is already present in the graph!",
                    owner,
                    context
                );
                return true;
            }

            return false;
        }

        public static bool Assert__Missing_Vector
        (
            Integer_Vector_3_Graph graph,
            Integer_Vector_3 vector,
            object owner,
            string context = null
        )
        {
            bool invalid = !graph.Check_If__Contains_Vector__Graph(vector);

            if (invalid)
            {
                Private_Log_Error__Graph
                (
                    $"Vector {vector} is not present in the graph!",
                    owner,
                    context
                );
                return true;
            }

            return false;
        }

        private static void Private_Log_Error__Graph
        (
            string message,
            object owner,
            string context
        )
        {
            if (context != null)
                context = $" {context}";
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
