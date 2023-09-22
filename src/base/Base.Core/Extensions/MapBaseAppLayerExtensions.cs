using Mapster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Base.Core.Extensions
{
    public static class MapBaseAppLayerExtensions
    {
        public static IEnumerable<TDestination> ProjectToType<TIn,TDestination>(this IEnumerable<TIn> entities)
        {
            return entities.Where(x=> x!=null).Select(x => x.Adapt<TDestination>() );
        }
    }
}
