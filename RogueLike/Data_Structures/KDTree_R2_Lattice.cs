
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class KDTree_R2_Lattice
    {
        private KDTree_R3 KDTree_R2_Lattice__KDTREE_R3__Reference { get; }
        public Rect_Prism KDTree_R2_Lattice__SPACE
            => KDTree_R2_Lattice__KDTREE_R3__Reference.KDTree__SPACE;
        private Dictionary<Plane_R3, KDTree_R2> KDTree_R2_Lattice__LOOKUP { get; }

        public KDTree_R2_Lattice(KDTree_R3 kdtree_r3)
        {
            KDTree_R2_Lattice__KDTREE_R3__Reference =
                kdtree_r3;

            KDTree_R2_Lattice__LOOKUP = 
                new Dictionary<Plane_R3, KDTree_R2>();
        }

        public Plane_R3? Get__Closest_Plane__KDTree_R2_Lattice(Integer_Vector_3 position)
        {
            Rect_Prism? nullable_space =
                KDTree_R2_Lattice__KDTREE_R3__Reference[position];

            if (nullable_space == null)
                return null;

            Rect_Prism space = (Rect_Prism)nullable_space;

            Plane_R3 closest_plane =
                Rect_Prism.Get__Closest_Plane(space, position);

            return closest_plane;
        }

        public Plane_R3? Get__Closest_Partition__KDTree_R2_Lattice(Integer_Vector_3 position)
        {
            Plane_R3? nullable_closest_plane = 
                Get__Closest_Plane__KDTree_R2_Lattice(position);

            if (nullable_closest_plane == null)
                return null;

            Plane_R3 closest_plane = 
                (Plane_R3)nullable_closest_plane;

            KDTree_R2 kdtree_r2 =
                KDTree_R2_Lattice__LOOKUP[closest_plane];

            Plane_R3? nullable_bounding_plane = 
                kdtree_r2[position];

            if (nullable_closest_plane == null)
                return null;

            return (Plane_R3)nullable_closest_plane;
        }

        public bool Partition__KDTree_R2_Lattice(Integer_Vector_3 position)
        {
            Plane_R3? nullable_closest_plane = 
                Get__Closest_Plane__KDTree_R2_Lattice(position);

            if (nullable_closest_plane == null)
                return false;

            Plane_R3 closest_plane = 
                (Plane_R3)nullable_closest_plane;

            if (!KDTree_R2_Lattice__LOOKUP.ContainsKey(closest_plane))
                KDTree_R2_Lattice__LOOKUP.Add(closest_plane, new KDTree_R2(closest_plane));

            KDTree_R2 kdtree_r2 =
                KDTree_R2_Lattice__LOOKUP[closest_plane];

            return kdtree_r2.Partition__KDTree(position);
        }

        public IEnumerable<Integer_Vector_3> Get__Keys__KDTree_R2_Lattice()
        {
            foreach(KDTree_R2 kdtree_r2 in KDTree_R2_Lattice__LOOKUP.Values)
            {
                foreach(KDTree_R2.KDTree_R2_Node node in kdtree_r2.Traverse__In_Order__KDTree())
                {
                    if (node.Node__Left == null || node.Node__Right == null)
                        yield return node.Node__KEY;
                }
            }
        }

        public IEnumerable<Plane_R3> Get__Sub_Surfaces__KDTree_R2_Lattice()
        {
            foreach(KDTree_R2 kdtree in KDTree_R2_Lattice__LOOKUP.Values)
            {
                foreach(KDTree_R2.KDTree_R2_Node node in kdtree.Traverse__In_Order__KDTree())
                {
                    if (node.Node__Left == null)
                        yield return node.node__Partition_Left;

                    if (node.Node__Right == null)
                        yield return node.node__Partition_Right;
                }
            }
        }
    }
}
