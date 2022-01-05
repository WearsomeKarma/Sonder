
namespace Rogue_Like 
{
    public enum Axis_Type
    {
        X = 1,
        Y = 2,
        Z = 4
    }

    public static class AXIS_TYPE
    {
        public static Axis_Type Get__Other(Axis_Type axis_type, Plane_Type plane_type)
        {
            switch(plane_type)
            {
                default:
                case Plane_Type.XY:
                    return (axis_type == Axis_Type.X)
                        ? Axis_Type.Y
                        : Axis_Type.X;
                case Plane_Type.XZ:
                    return (axis_type == Axis_Type.X)
                        ? Axis_Type.Z
                        : Axis_Type.X;
                case Plane_Type.YZ:
                    return (axis_type == Axis_Type.Y)
                        ? Axis_Type.Z
                        : Axis_Type.Y;
            }
        }
    }
}
