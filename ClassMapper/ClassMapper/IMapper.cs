using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassMapper
{
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource sourse) where TDestination : new();
    }
}
