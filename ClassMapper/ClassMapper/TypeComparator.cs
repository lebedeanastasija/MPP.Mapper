using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{
    class TypeComparator
    {
        private static readonly TypeComparator instance = new TypeComparator();

        public TypeComparator Instance
        {
            get
            {
                return instance;
            }
        }

        private List<Type> TSigned = new List<Type>()
        {
            typeof(long),
            typeof(int),
            typeof(short),
            typeof(sbyte)
        };

        private List<Type> TUnsigned = new List<Type>()
        {
            typeof(ulong),
            typeof(uint),
            typeof(ushort),
            typeof(byte)
        };

        private List<Type> TReal = new List<Type>()
        {
            typeof(double),
            typeof(float)
        };

        public bool IsSuitableTypes(Type source, Type destination)
        {
            bool result = false;
            if (source.FullName == destination.FullName)
            {
                result = true;
            }
            else
            {
                if ((TSigned.Contains(source)) && (TSigned.Contains(destination)))
                {
                    if (TSigned.IndexOf(source) >= TSigned.IndexOf(destination))
                    {
                        result = true;
                    }
                }
                else if ((TUnsigned.Contains(source)) && (TUnsigned.Contains(destination)))
                {
                    if (TUnsigned.IndexOf(source) >= TUnsigned.IndexOf(destination))
                    {
                        result = true;
                    }
                }
                else if ((TReal.Contains(source)) && (TReal.Contains(destination)))
                {
                    if (TReal.IndexOf(source) >= TReal.IndexOf(destination))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }
    }
}
