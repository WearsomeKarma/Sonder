
using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public class Level_Hallway_Graph
    {
        private Integer_Vector_3_Graph Level_Hallway_Graph__VERTICES { get; }

        internal Level_Hallway_Graph
        (
            KDTree_R3 room_tree,
            List<Integer_Vector_3> hallway_exits
        )
        {
            int vertex_count =
                (room_tree.KDTree__Partition_Count + 2) * 4 + hallway_exits.Count;

            Level_Hallway_Graph__VERTICES =
                new Integer_Vector_3_Graph(vertex_count);

            int v = 0;

            HashSet<Integer_Vector_3> marked_points = new HashSet<Integer_Vector_3>();
        }

        private void Private_Record__XY_YZ__Level_Hallway_Graph
        (Rect_Prism space )
        {}

        private void Private_Record__XZ__Level_Hallway_Graph(Rect_Prism space, ref int v)
        {
            Rect_Prism rotated_xz = new Rect_Prism(space.Rect_Prism__MIN, space.Rect_Prism__MAX, Plane_Type.XZ);

            // <Link_Floor>
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MIN, 
                rotated_xz.Rect_Prism__MIN_PLANE.Plane__MIN_X_Y__MAX_Z,
                ref v
            );
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MIN, 
                rotated_xz.Rect_Prism__MIN_PLANE.Plane__MIN_Y_Z__MAX_X,
                ref v
            );
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MIN_PLANE.Plane__MIN_Y__MAX_X_Z, 
                rotated_xz.Rect_Prism__MIN_PLANE.Plane__MIN_X_Y__MAX_Z,
                ref v
            );
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MIN_PLANE.Plane__MIN_Y__MAX_X_Z, 
                rotated_xz.Rect_Prism__MIN_PLANE.Plane__MIN_Y_Z__MAX_X,
                ref v
            );
            //</Link_Floor>
            
            //<Link_Ceiling>
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MAX, 
                rotated_xz.Rect_Prism__MAX_PLANE.Plane__MIN_X__MAX_Y_Z,
                ref v
            );
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MAX, 
                rotated_xz.Rect_Prism__MAX_PLANE.Plane__MIN_Z__MAX_X_Y,
                ref v
            );
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MAX_PLANE.Plane__MIN_X_Z__MAX_Y, 
                rotated_xz.Rect_Prism__MAX_PLANE.Plane__MIN_X__MAX_Y_Z,
                ref v
            );
            Private_Link__Level_Hallway_Graph
            (
                rotated_xz.Rect_Prism__MAX_PLANE.Plane__MIN_X_Z__MAX_Y, 
                rotated_xz.Rect_Prism__MAX_PLANE.Plane__MIN_Z__MAX_X_Y,
                ref v
            );
            //</Link_Ceiling>
        }

        private void Private_Link__Level_Hallway_Graph(Integer_Vector_3 v1, Integer_Vector_3 v2, ref int v)
        {
            Level_Hallway_Graph__VERTICES
                .Define__Vertex__Graph(v, v1);
            Level_Hallway_Graph__VERTICES
                .Define__Vertex__Graph(v+1, v2);

            Level_Hallway_Graph__VERTICES
                .Define__Double_Adjacent__Graph(v, ++v);
        }
    }
}
