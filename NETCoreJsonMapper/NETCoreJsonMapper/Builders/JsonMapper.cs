using Newtonsoft.Json;
using System;
using System.Linq;
using System.Reflection;

namespace NETCoreJsonMapper.Builders
{
    public class JsonMapper
    {
        private const string DESERIALIZE_OBJECT_METHOD_NAME = "DeserializeObject";
        private const string GET_RESULT_METHOD_NAME = "GetResult";
        private const string IS_VALID_METHOD_NAME = "IsValid";
        private const string SERIALIZE_OBJECT_METHOD_NAME = "SerializeObject";

        private static readonly MethodInfo JsonConvertDeserializeMethodInfo = typeof(JsonConvert)
            .GetMethods(
                BindingFlags.Public
                    | BindingFlags.Static
                    | BindingFlags.OptionalParamBinding
                    | BindingFlags.InvokeMethod)
            .Where(
                m => m.IsGenericMethod
                && m.ContainsGenericParameters
                && m.Name == DESERIALIZE_OBJECT_METHOD_NAME)
            .FirstOrDefault();

        private static readonly MethodInfo JsonConvertSerializeMethodInfo = typeof(JsonConvert)
            .GetMethod(SERIALIZE_OBJECT_METHOD_NAME,
                BindingFlags.Public
                    | BindingFlags.Static
                    | BindingFlags.OptionalParamBinding
                    | BindingFlags.InvokeMethod,
                Type.DefaultBinder,
                new[] {
                            typeof(string)
                },
                null);

        private readonly string jsonString;

        public JsonMapper(string jsonString)
        {
            this.jsonString = jsonString;
        }

        public string InvokeJsonMapping(Type deserializationType)
        {
            object deserializedJsonObject = GetDeserializationResult(deserializationType: deserializationType);
            return JsonConvertSerializeMethodInfo
                .Invoke(null, new object[] {
                    deserializedJsonObject
                }).ToString();
        }

        public bool IsJsonMatchType(Type deserializationType)
        {
            MethodInfo isValidMethod = deserializationType.GetMethod(IS_VALID_METHOD_NAME,
               BindingFlags.Instance | BindingFlags.Public);
            return (bool)isValidMethod.Invoke(Activator.CreateInstance(deserializationType),
                new object[] { jsonString });
        }

        private object GetDeserializationResult(Type deserializationType)
        {
            object deserializedJsonObjectWrapper = InvokeDeserialize(deserializationType: deserializationType);
            return deserializedJsonObjectWrapper.GetType().GetMethod(GET_RESULT_METHOD_NAME,
                BindingFlags.Instance | BindingFlags.NonPublic)
                .Invoke(deserializedJsonObjectWrapper, null);
        }

        private object InvokeDeserialize(Type deserializationType) => JsonConvertDeserializeMethodInfo
            .MakeGenericMethod(deserializationType)
            .Invoke(null, new object[] { jsonString });
    }
}