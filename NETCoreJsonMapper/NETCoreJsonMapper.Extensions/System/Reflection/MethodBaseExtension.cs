using System;
using System.Linq;
using System.Reflection;

namespace NETCoreJsonMapper.Extensions.System.Reflection
{
    public static class MethodBaseExtension
    {
        public static bool HasMethodOneParameterOfType(this MethodBase method, Type parameterType)
        {
            return HasMethodParameterOfTypes(method: method,
                parameterTypes: parameterType);
        }

        public static bool HasMethodParameterOfTypes(this MethodBase method, params Type[] parameterTypes)
        {
            ParameterInfo[] methodParameters = method.GetParameters();
            int parameterArrayLength = parameterTypes.Length;
            bool isValidMethod = methodParameters.Count().Equals(parameterArrayLength);

            for (int idx = 0; isValidMethod && idx < parameterArrayLength; idx += 1)
            {
                isValidMethod &= methodParameters[idx].ParameterType
                    .IsAssignableFrom(parameterTypes[idx]);
            }

            return isValidMethod;
        }
    }
}