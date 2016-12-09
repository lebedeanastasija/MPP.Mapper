using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{
    class MappingResultPair
    {
        public Type TSource { get; set; }
        public Type TDestionation { get; set; }

        public override Int32 GetHashCode()
        {
            return TSource.GetHashCode() * TDestionation.GetHashCode();
        }

        public override bool Equals(object mapResultObject)
        {
            bool result = false;
            MappingResultPair pair = mapResultObject as MappingResultPair;
            if((pair.TSource.FullName == TSource.FullName) &&
                (pair.TDestionation.FullName == TDestionation.FullName))
            {
                result = true;
            }
            return result;
        }
    }
}
