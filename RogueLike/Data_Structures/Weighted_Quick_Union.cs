
using System.Collections.Generic;
using Xerxes_Engine;

namespace Rogue_Like
{
    public class Weighted_Quick_Union
    {
        public struct WQU_Element
        {
            public int WQU_Element__PARENT_INDEX { get; }
            public int WQU_Element__TYPE { get; }

            public WQU_Element(int parent_index, int type=0)
            {
                WQU_Element__PARENT_INDEX = parent_index;
                WQU_Element__TYPE = type;
            }
        }

        public int Weighted_Quick_Union__COUNT { get; }
        public int Weighted_Quick_Union__TYPE_COUNT { get; }
        private WQU_Element[] Weighted_Quick_Union__ELEMENTS { get; }
        private int[] Weighted_Quick_Union__SIZES { get; }

        private Dictionary<int, List<int>> Weighted_Quick_Union__TYPE_LOOKUP { get; }

        public Weighted_Quick_Union(int count, int type_count=1)
        {
            Weighted_Quick_Union__ELEMENTS =
                new WQU_Element[count];
            Weighted_Quick_Union__SIZES =
                new int[count];

            Weighted_Quick_Union__COUNT = count;
            Weighted_Quick_Union__TYPE_LOOKUP =
                new Dictionary<int, List<int>>();

            for(int i=0;i<type_count;i++)
                Weighted_Quick_Union__TYPE_LOOKUP.Add(i, new List<int>());

            for(int v=0;v<Weighted_Quick_Union__COUNT;v++)
                Weighted_Quick_Union__ELEMENTS[v] = new WQU_Element(v);
        }

        public void Union__WQU(int p, int q)
        {
            bool invalid_p =
                Assert__Invalid_Index(this, p, this);
            bool invalid_q =
                Assert__Invalid_Index(this, q, this);

            bool invalid =
                invalid_p
                ||
                invalid_q;

            if(invalid)
                return;

            int root_p = Private_Root__WQU(p);
            int root_q = Private_Root__WQU(q);

            int compare_size =
                Weighted_Quick_Union__SIZES[root_p] 
                -
                Weighted_Quick_Union__SIZES[root_q];

            WQU_Element old_element;

            if (compare_size <= 0)
            {
                old_element = Weighted_Quick_Union__ELEMENTS[root_p];
                Weighted_Quick_Union__ELEMENTS[root_p] =
                    new WQU_Element(root_q, old_element.WQU_Element__TYPE);

                Weighted_Quick_Union__SIZES[root_q] +=
                    Weighted_Quick_Union__SIZES[root_p];

                return;
            }

            old_element = Weighted_Quick_Union__ELEMENTS[root_q];

            Weighted_Quick_Union__ELEMENTS[root_q] =
                new WQU_Element(root_p, old_element.WQU_Element__TYPE);

            Weighted_Quick_Union__SIZES[root_p] +=
                Weighted_Quick_Union__SIZES[root_q];
        }

        private void Disjoin__WQU(int p)
        {
            bool invalid =
                Assert__Invalid_Index(this, p, this);

            if(invalid)
                return;

            int new_parent_index =
                Weighted_Quick_Union__ELEMENTS[p].WQU_Element__PARENT_INDEX;

            for(int v=0;v<Weighted_Quick_Union__COUNT;v++)
            {
                if (Weighted_Quick_Union__ELEMENTS[v].WQU_Element__PARENT_INDEX == p)
                {
                    WQU_Element element = Weighted_Quick_Union__ELEMENTS[v];

                    Weighted_Quick_Union__ELEMENTS[v] =
                        new WQU_Element(new_parent_index, element.WQU_Element__TYPE);
                }
            }
                    

            Weighted_Quick_Union__ELEMENTS[p] = 
                new WQU_Element(p, Weighted_Quick_Union__ELEMENTS[p].WQU_Element__TYPE);
            Weighted_Quick_Union__SIZES[p] = 0;
        }

        public void Set__WQU(int p, int q)
        {
            Disjoin__WQU(p);
            Union__WQU(p,q);
        }

        private int Private_Root__WQU(int p)
        {
            int child_index = p;
            WQU_Element child  = Weighted_Quick_Union__ELEMENTS[p];
            WQU_Element parent = Weighted_Quick_Union__ELEMENTS[child.WQU_Element__PARENT_INDEX]; 

            while(parent.WQU_Element__PARENT_INDEX != child.WQU_Element__PARENT_INDEX)
            {
                child =
                    parent;
                child_index =
                    child.WQU_Element__PARENT_INDEX;
                parent =
                    Weighted_Quick_Union__ELEMENTS[parent.WQU_Element__PARENT_INDEX];
            }

            return child_index;
        }

        public IEnumerable<int> Query__By_Type__WQU(int type)
        {
            bool invalid =
                Assert__Invalid_Type(this, type, this);

            if (invalid)
                yield break;

            foreach(int v in Weighted_Quick_Union__TYPE_LOOKUP[type])
                yield return v;
        }

        public int this[int v]
        {
            get 
            {
                bool invalid =
                    Assert__Invalid_Index(this, v, this);

                if (invalid)
                    return -1;

                return Weighted_Quick_Union__ELEMENTS[v].WQU_Element__TYPE;
            }
            set
            {
                bool invalid =
                    Assert__Invalid_Index(this, v, this);

                if (invalid)
                    return;

                WQU_Element element = Weighted_Quick_Union__ELEMENTS[v];

                Weighted_Quick_Union__TYPE_LOOKUP[element.WQU_Element__TYPE]
                    .Remove(v);

                Weighted_Quick_Union__ELEMENTS[v] = new WQU_Element(v, value);

                Weighted_Quick_Union__TYPE_LOOKUP[value]
                    .Add(v);
            }
        }

        public static bool Assert__Invalid_Index
        (
            Weighted_Quick_Union wqu, 
            int index,
            object owner,
            string context=null
        )
        {
            if (index >= 0 && index < wqu.Weighted_Quick_Union__COUNT)
                return false;

            Private_Log__Error($"Index:{index} is out of range for weighted quick union!", owner, context);
            
            return true;
        }

        public static bool Assert__Invalid_Type
        (
            Weighted_Quick_Union wqu,
            int type,
            object owner,
            string context = null
        )
        {
            if (type >= 0 && type < wqu.Weighted_Quick_Union__TYPE_COUNT)
                return false;

            Private_Log__Error($"Type:{type} is not present in weighted quick union!", owner, context);

            return true;
        }

        private static void Private_Log__Error(string message, object owner, string context=null)
        {
            if (context != null)
                context = $" {context}";

            Log.Write__Error__Log
            (
                $"{message}!{context}!",
                owner
            );
        }
    }
}
