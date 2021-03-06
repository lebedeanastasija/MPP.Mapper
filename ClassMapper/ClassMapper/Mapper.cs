﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;

namespace ClassMapper
{
    public class Mapper: IMapper
    {
        private static readonly Mapper instance = new Mapper();

        private TypeComparator typeComparator = new TypeComparator();

        private Dictionary<MappingResultPair, Delegate> mappingHash = new Dictionary<MappingResultPair, Delegate>();

        public static Mapper Instance
        {
            get
            {
                return instance;
            }
        }


        public TDestination Map<TSource, TDestination>(TSource source) where TDestination : new()
        {
            Type sourseType = typeof(TSource);
            Type destType = typeof(TDestination);

            MappingResultPair tempPair = new MappingResultPair()
            {
                TSource = sourseType,
                TDestionation = destType
            };

            TDestination result = (TDestination)destType.GetConstructor(new Type[0]).Invoke(new Object[0]);

            if(mappingHash.ContainsKey(tempPair))
            {
                var lambda = mappingHash[tempPair];
                ((Action<TSource, TDestination>)lambda)(source, result);
            }
            else
            {
                Expression<Action<TSource, TDestination>> expression = BuildLambda<TSource, TDestination>();
                var lambda = expression.Compile();
                mappingHash[tempPair] = lambda;
                lambda(source, result);
            }
            
            return result;            
        }

        private Expression<Action<TSource, TDestination>> BuildLambda<TSource, TDestination>(){
            Type sourceType = typeof(TSource);
            Type destType = typeof(TDestination);

            List<Expression> expressionList = new List<Expression>();
            ParameterExpression sourceParameter = Expression.Parameter(typeof(TSource), "source");
            ParameterExpression destinationParameter = Expression.Parameter(typeof(TDestination), "destination");

            Expression assignPropertiesExpression = null;

            foreach(PropertyInfo destinationProperty in destType.GetProperties())
            {
                if (destinationProperty.CanWrite)
                {
                    foreach (PropertyInfo sourceProperty in sourceType.GetProperties())
                    {
                        if(typeComparator.IsSuitableTypes(sourceProperty.PropertyType, destinationProperty.PropertyType) &&
                           sourceProperty.Name == destinationProperty.Name)
                        { 
                            Expression sourcePropertyExpression = Expression.Property(sourceParameter, sourceProperty);
                            Expression destinationPropertyExpression = Expression.Property(destinationParameter, destinationProperty);

                            assignPropertiesExpression = Expression.Assign(destinationPropertyExpression, Expression.Convert(sourcePropertyExpression, destinationProperty.PropertyType));
                            expressionList.Add(assignPropertiesExpression);
                        }
                    }
                }
            }

            Expression body = null;
            if(expressionList.Count != 0)
            {
                body = Expression.Block(expressionList);
            }
            else
            {
                body = Expression.Empty();
            }
            Expression<Action<TSource, TDestination>> result = Expression.Lambda<Action<TSource, TDestination>>(body, sourceParameter, destinationParameter);
            return result;
        }
    }
}
