namespace ReflectionExample
{
    public class DiPrototype
    {
        public object CreateObject(Type typeOfObject)
        {
            var constructor = typeOfObject.GetConstructors().First();

            var parametersInfo = constructor.GetParameters();

            var parameters = parametersInfo
                .Select(parameterInfo =>
                {
                    var parameterType = parameterInfo.ParameterType;
                    return CreateObject(parameterType);
                })
                .ToArray();

            var obj = constructor.Invoke(parameters);

            return obj;
        }
    }
}
