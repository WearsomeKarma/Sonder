
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class KDTree_R3
    {
        public class KDTree_R3_Node
        {
            public Integer_Vector_3 Node__KEY { get; }
            public int Node__DEPTH { get; }
            public Plane_Type Get__Axis_Bias__Node(int depth_offset = 0)
                => (Plane_Type)((Node__DEPTH+depth_offset) % 3);

            public KDTree_R3_Node Node__Left { get; set; }
            public KDTree_R3_Node Node__Right { get; set; }

            public Rect_Prism node__Partition_Left;
            public Rect_Prism node__Partition_Right;

            internal KDTree_R3_Node(Integer_Vector_3 key, int depth)
            {
                Node__KEY = key;
                Node__DEPTH = depth;
            }

            public int Compare__Key__Node(Integer_Vector_3 position, int depth_offset = 0)
            {
                Plane_Type axis_type = 
                    Get__Axis_Bias__Node(depth_offset);

                int val;

                switch(axis_type)
                {
                    default:
                    case Plane_Type.XY:
                        val = position.Z - Node__KEY.Z;
                        break;
                    case Plane_Type.XZ:
                        val = position.Y - Node__KEY.Y;
                        break;
                    case Plane_Type.YZ:
                        val = position.X - Node__KEY.X;
                        break;
                }

                if (val == 0 && depth_offset < 2)
                    val = Compare__Key__Node(position, depth_offset+1);

                return val;
            }
        }

        public Rect_Prism KDTree__SPACE { get; }
        private KDTree_R3_Node KDTree__Root { get; set; }

        public int KDTree__Partition_Count { get; set; }
        public int KDTree__Partition_Depth { get; set; }

        public KDTree_R3(Rect_Prism space)
        {
            KDTree__SPACE = space;
        }

        public bool Partition__KDTree(Integer_Vector_3 position)
        {
            bool invalid =
                Assert__Invalid_Position(this, position, this);

            if (invalid)
                return false;

            KDTree__Partition_Count++;

            if (KDTree__Root == null)
            {
                KDTree__Root = new KDTree_R3_Node(position, 0);

                Rect_Prism.Split
                (
                    KDTree__SPACE,
                    KDTree__Root.Node__KEY,
                    KDTree__Root.Get__Axis_Bias__Node(),
                    out KDTree__Root.node__Partition_Left,
                    out KDTree__Root.node__Partition_Right
                );

                return true;
            }

            bool is_Left_Or_Right;

            KDTree_R3_Node end_point = Traverse(position, out is_Left_Or_Right);

            if (end_point == null)
                return false;

            KDTree_R3_Node target;
            int depth = end_point.Node__DEPTH+1;

            if (is_Left_Or_Right)
            {
                end_point.Node__Left = new KDTree_R3_Node(position, depth);
                target = end_point.Node__Left;
            }
            else
            {
                end_point.Node__Right = new KDTree_R3_Node(position, depth);
                target = end_point.Node__Right;
            }

            Rect_Prism.Split
            (
                (is_Left_Or_Right) 
                    ? end_point.node__Partition_Left
                    : end_point.node__Partition_Right,
                target.Node__KEY,
                target.Get__Axis_Bias__Node(),
                out target.node__Partition_Left,
                out target.node__Partition_Right
            );

            if (target.Node__DEPTH > KDTree__Partition_Depth)
                KDTree__Partition_Depth = target.Node__DEPTH;

            return true;
        }

        private KDTree_R3_Node Traverse
        (
            Integer_Vector_3 position,
            out bool is_Left_Or_Right
        ) 
        {
            KDTree_R3_Node target = KDTree__Root;
            KDTree_R3_Node previous = KDTree__Root;

            is_Left_Or_Right = false;

            while(target != null)
            {
                int compare =
                    target.Compare__Key__Node(position);

                if (compare == 0)
                {
                    return null;
                }

                is_Left_Or_Right = compare < 0;

                previous = target;
                target = (is_Left_Or_Right) ? target.Node__Left : target.Node__Right;
            }

            return previous;
        }

        public IEnumerable<Rect_Prism> Get__Partitions__KDTree()
        {
            if(KDTree__Root == null)
                return new List<Rect_Prism> {KDTree__SPACE};

            List<Rect_Prism> rects = new List<Rect_Prism>();

            foreach(KDTree_R3_Node n in Traverse__In_Order__KDTree())
            {
                if (n.Node__Left == null)
                {
                    if (n.node__Partition_Left.Rect_Prism__VOLUME > 0)
                        rects.Add(n.node__Partition_Left);
                    if (n.Node__Right == null && n.node__Partition_Right.Rect_Prism__VOLUME > 0)
                        rects.Add(n.node__Partition_Right);
                }
            }

            return rects;
        }

        public IEnumerable<KDTree_R3_Node> Traverse__In_Order__KDTree()
        {
            IEnumerator<KDTree_R3_Node> recursive =
                Private_Traverse__In_Order__KDTree(KDTree__Root)
                .GetEnumerator();

            while(recursive.MoveNext())
                yield return recursive.Current;
        }

        private IEnumerable<KDTree_R3_Node> Private_Traverse__In_Order__KDTree
        (
            KDTree_R3_Node current
        )
        {
            if (current == null)
                yield break;

            if (current.Node__Left != null)
            {
                IEnumerator<KDTree_R3_Node> recursive_left =
                    Private_Traverse__In_Order__KDTree(current.Node__Left)
                    .GetEnumerator();

                while(recursive_left.MoveNext())
                    yield return recursive_left.Current;
            }

            yield return current;

            if (current.Node__Right != null)
            {
                IEnumerator<KDTree_R3_Node> recursive_right =
                    Private_Traverse__In_Order__KDTree(current.Node__Right)
                    .GetEnumerator();

                while(recursive_right.MoveNext())
                    yield return recursive_right.Current;
            }
        }

        public override string ToString()
        {
            string s = "";

            foreach(KDTree_R3_Node n in Traverse__In_Order__KDTree())
            {
                s += $"<LEFT:{n.node__Partition_Left} -- RIGHT:{n.node__Partition_Right}>";
            }

            return s;
        }

        public Rect_Prism? this[Integer_Vector_3 position]
        {
            get 
            {
                bool invalid = 
                    Assert__Invalid_Position(this, position, this);

                if (invalid)
                    return null;

                bool is_Left_Or_Right;
                KDTree_R3_Node target =
                    Traverse(position, out is_Left_Or_Right);

                return (is_Left_Or_Right) ? target?.node__Partition_Left : target?.node__Partition_Right;
            }
        }

        public static bool Assert__Invalid_Position
        (
            KDTree_R3 kdtree,
            Integer_Vector_3 position,
            object owner
        )
        {
            bool invalid =
                !kdtree.KDTree__SPACE.Contains__Point__Rect_Prism(position);

            if (invalid)
            {
                Log.Write__Error__Log
                (
                    $"Position: {position} is out of bounds for Space: {kdtree.KDTree__SPACE}!",
                    owner
                );
            }

            return invalid;
        }

    }
}
