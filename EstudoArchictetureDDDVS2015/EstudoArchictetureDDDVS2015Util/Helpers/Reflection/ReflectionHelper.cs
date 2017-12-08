using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EstudoArchictetureDDDVS2015Util.Helpers.Reflection
{
    public static class ReflectionHelper
    {
        public static TAttribute GetAttribute<TAttribute, TModel, TValue>(this Expression<Func<TModel, TValue>> expression) where TAttribute : Attribute
        {
            var memberExpression = expression.Body as MemberExpression;

            if (memberExpression == null) return null;

            return (TAttribute)Attribute.GetCustomAttribute(memberExpression.Member, typeof(TAttribute));
        }
    }
}
