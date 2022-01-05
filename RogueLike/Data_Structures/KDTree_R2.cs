
using System.Collections.Generic;
using Xerxes_Engine;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class KDTree_R2
    {
        public class KDTree_R2_Node
        {
            public Integer_Vector_3 Node__KEY { get; }
            public int Node__DEPTH { get; }
            public Plane_Type Node__PLANE_TYPE { get; }
            public Axis_Type Node__AXIS_BIAS { get; }

            public KDTree_R2_Node Node__Left { get; set; }
            public KDTree_R2_Node Node__Right { get; set; }

            public Plane_R3 node__Partition_Left;
            public Plane_R3 node__Partition_Right;

            internal KDTree_R2_Node(Integer_Vector_3 key, Plane_Type plane_type, int depth)
            {
                Node__KEY = key;
                Node__DEPTH = depth;

                Node__PLANE_TYPE = plane_type;

                bool is_biased_on_primary_axis = depth % 2 == 0;

                switch(plane_type)
                {
                    default:
                    case Plane_Type.XY:
                        Node__AXIS_BIAS = 
                            (is_biased_on_primary_axis)
                            ? Axis_Type.X
                            : Axis_Type.Y;
                        break;
                    case Plane_Type.XZ:
                        Node__AXIS_BIAS = 
                            (is_biased_on_primary_axis)
                            ? Axis_Type.X
                            : Axis_Type.Z;

                        break;
                    case Plane_Type.YZ:
                        Node__AXIS_BIAS = 
                            (is_biased_on_primary_axis)
                            ? Axis_Type.Y
                            : Axis_Type.Z;

                        break;
                }
            }

            public int Compare__Key__Node(Integer_Vector_3 position)
            {
                int compare_primary =
                    Private_Compare__Key_Axis__Node(position, Node__AXIS_BIAS);

                if (compare_primary != 0)
                    return compare_primary;

                Axis_Type other = AXIS_TYPE.Get__Other(Node__AXIS_BIAS, Node__PLANE_TYPE);

                int compare_other =
                    Private_Compare__Key_Axis__Node(position, other);

                return compare_other;
            }

            private int Private_Compare__Key_Axis__Node(Integer_Vector_3 point, Axis_Type axis_type)
            {
                switch(Node__PLANE_TYPE)
                {
                    default:
                    case Plane_Type.XY:
                        if (axis_type == Axis_Type.X)
                            return point.X - Node__KEY.X;
                        return point.Y - Node__KEY.Y;

                    case Plane_Type.XZ:
                        if (axis_type == Axis_Type.X)
                            return point.X - Node__KEY.X;
                        return point.Z - Node__KEY.Z;

                    case Plane_Type.YZ:
                        if (axis_type == Axis_Type.Y)
                            return point.Y - Node__KEY.Y;
                        return point.Z - Node__KEY.Z;
                }
            }
        }

        public Plane_R3 KDTree__SPACE { get; }
        public Plane_Type KDTree__SPACE_TYPE
            => KDTree__SPACE.Plane__PLANE_TYPE;
        private KDTree_R2_Node KDTree__Root { get; set; }

        public int KDTree__Partition_Count { get; private set; }
        public int KDTree__Partition_Depth { get; private set; }

        public KDTree_R2(Plane_R3 space)
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
                KDTree__Root = 
                    new KDTree_R2_Node(position, KDTree__SPACE_TYPE, 0);

                Plane_R3.Split
                (
                    KDTree__SPACE,
                    KDTree__Root.Node__KEY,
                    KDTree__Root.Node__AXIS_BIAS,
                    out KDTree__Root.node__Partition_Left,
                    out KDTree__Root.node__Partition_Right
                );

                return true;
            }

            bool is_Left_Or_Right;

            KDTree_R2_Node end_point = Traverse(position, out is_Left_Or_Right);

            if (end_point == null)
                return false;

            KDTree_R2_Node target;
            int depth = end_point.Node__DEPTH+1;

            if (is_Left_Or_Right)
            {
                end_point.Node__Left = new KDTree_R2_Node(position, KDTree__SPACE_TYPE, depth);
                target = end_point.Node__Left;
            }
            else
            {
                end_point.Node__Right = new KDTree_R2_Node(position, KDTree__SPACE_TYPE, depth);
                target = end_point.Node__Right;
            }

            Plane_R3.Split
            (
                (is_Left_Or_Right)
                    ? end_point.node__Partition_Left
                    : end_point.node__Partition_Right,
                target.Node__KEY,
                target.Node__AXIS_BIAS,
                out target.node__Partition_Left,
                out target.node__Partition_Right
            );

            if (target.Node__DEPTH > KDTree__Partition_Depth)
                KDTree__Partition_Depth = target.Node__DEPTH;

            return true;
        }

        private KDTree_R2_Node Traverse
        (
            Integer_Vector_3 position,
            out bool is_Left_Or_Right
        )
        {
            KDTree_R2_Node target = KDTree__Root;
            KDTree_R2_Node previous = KDTree__Root;

            is_Left_Or_Right = false;

            while(target != null)
            {
                int compare = target.Compare__Key__Node(position);

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

        public IEnumerable<KDTree_R2_Node> Traverse__In_Order__KDTree()
        {
            IEnumerator<KDTree_R2_Node> recursive = 
                Private_Traverse__In_Order__KDTree(KDTree__Root)
                .GetEnumerator();

            while(recursive.MoveNext())
                yield return recursive.Current;
        }

        private IEnumerable<KDTree_R2_Node> Private_Traverse__In_Order__KDTree
        (
            KDTree_R2_Node current
        )
        {
            if (current == null)
                yield break;

            if (current.Node__Left != null)
            {
                IEnumerator<KDTree_R2_Node> recursive_left =
                    Private_Traverse__In_Order__KDTree(current.Node__Left)
                    .GetEnumerator();

                while(recursive_left.MoveNext())
                    yield return recursive_left.Current;
            }

            yield return current;

            if (current.Node__Right != null)
            {
                IEnumerator<KDTree_R2_Node> recursive_right =
                    Private_Traverse__In_Order__KDTree(current.Node__Right)
                    .GetEnumerator();

                while(recursive_right.MoveNext())
                    yield return recursive_right.Current;
            }
        }

        public override string ToString()
        {
            string s = "";

            foreach(KDTree_R2_Node n in Traverse__In_Order__KDTree())
            {
                s += $"<LEFT:{n.node__Partition_Left} -- RIGHT:{n.node__Partition_Right}>";
            }

            return s;
        }

        public Plane_R3? this[Integer_Vector_3 position]
        {
            get
            {
                bool invalid =
                    Assert__Invalid_Position(this, position, this);

                if (invalid)
                    return null;

                bool is_Left_Or_Right;
                KDTree_R2_Node target = 
                    Traverse(position, out is_Left_Or_Right);

                return (is_Left_Or_Right) ? target?.node__Partition_Left : target?.node__Partition_Right;
            }
        }

        public static bool Assert__Invalid_Position
        (
            KDTree_R2 kdtree,
            Integer_Vector_3 position,
            object owner
        )
        {
            bool invalid =
                !kdtree.KDTree__SPACE.Contains__Point__Plane_3(position);

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
