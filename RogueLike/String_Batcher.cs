using System.Collections.Generic;
using Xerxes_Engine.Export_OpenTK;

namespace Rogue_Like
{
    public static class String_Batcher
    {
        public const int CHAR_PIXEL_WIDTH = 18;
        public const int CHAR_PIXEL_HEIGHT = 28;
        private const string ALPHABET =
            ",gjpqyABCDEFGHIJKLMNOPQRSTUVWXYZabcdefhiklmnorstuvwxz1234567890.?!/-+@#$%^&+()_=[]\\[]|:;\"'<>`~";

        public static SA__Declare_Vertex_Object Batch(string s, Texture_R2 font)
        {
            List<Integer_Vector_2> uvs = new List<Integer_Vector_2>();
            List<Integer_Vector_2> positions = new List<Integer_Vector_2>();

            int x=0, y=0;
            foreach(char c in s)
            {
                if (c == '\n')
                {
                    x=0; y++;
                    continue;
                }
                uvs.Add(new Integer_Vector_2(ALPHABET.IndexOf(c),0));
                positions.Add(new Integer_Vector_2(x,y));
                x++;
            }

            SA__Declare_Vertex_Object e = 
                new SA__Declare_Vertex_Object
                (
                    font,
                    CHAR_PIXEL_WIDTH,
                    CHAR_PIXEL_HEIGHT,
                    uvs.ToArray(),
                    positions.ToArray()
                );

            return e;
        }

        public static SA__Declare_Sprite Sheet(string s)
        {
            return null;
        }
    }
}
