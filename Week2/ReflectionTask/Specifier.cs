using System.Reflection;

namespace Week2.ReflectionTask
{
    public class Specifier<T> : ISpecifier
    {
        public string GetApiDescription()
        {
            var type = typeof(T);
            var result = type
                .GetCustomAttributes()
                .OfType<ApiDescriptionAttribute>()
                .FirstOrDefault();

            return result?.Description;
        }


        public string[] GetApiMethodNames()
        {
            var type = typeof(T);
            var result = new List<string>();

            foreach (var item in type.GetMethods())
            {
                if (item
                    .GetCustomAttributes()
                    .OfType<ApiMethodAttribute>()
                    .Any())
                {
                    result.Add(item.Name);
                }
            }

            return result.ToArray();
        }

        public string GetApiMethodDescription(string methodName)
        {
            var type = typeof(T);

            foreach (var item in type.GetMethods())
            {
                if (item
                    .GetCustomAttributes()
                    .OfType<ApiMethodAttribute>()
                    .Any())
                {
                    if (item.Name == methodName)
                    {
                        return item
                            .GetCustomAttributes()
                            .OfType<ApiDescriptionAttribute>()
                            .FirstOrDefault().Description;
                    }
                }
            }

            return null;
        }

        public string[] GetApiMethodParamNames(string methodName)
        {
            var type = typeof(T);
            var result = new List<string>();
            var nameOfMethod = type
                .GetMethods()
                .FirstOrDefault(m => m.Name == methodName);

            if (nameOfMethod
                .GetCustomAttributes()
                .OfType<ApiMethodAttribute>()
                .Any())
            {
                foreach (var item in nameOfMethod.GetParameters())
                {
                    result.Add(item.Name);
                }
            }

            return result.ToArray();
        }

        public string GetApiMethodParamDescription(string methodName, string paramName)
        {
            var type = typeof(T);
            var nameOfMethod = type
                .GetMethods()
                .FirstOrDefault(m => m.Name == methodName);

            if (nameOfMethod == null)
            {
                return null;
            }

            if (nameOfMethod
                .GetCustomAttributes()
                .OfType<ApiMethodAttribute>()
                .Any())
            {
                var nameOFParameter = nameOfMethod
                    .GetParameters()
                    .FirstOrDefault(p => p.Name == paramName);

                if (nameOFParameter == null)
                {
                    return null;
                }

                var result = nameOFParameter
                    .GetCustomAttributes()
                    .OfType<ApiDescriptionAttribute>();

                if (result.Any())
                {
                    return result
                        .FirstOrDefault()
                        .Description;
                }
            }

            return null;
        }

        public ApiParamDescription GetApiMethodParamFullDescription(string methodName, string paramName)
        {
            var type = typeof(T);
            var result = new ApiParamDescription
            {
                ParamDescription = new CommonDescription(paramName),
                MaxValue = null,
                MinValue = null,
                Required = false
            };
            var nameOfMethod = type
                .GetMethods()
                .FirstOrDefault(m => m.Name == methodName);

            if (nameOfMethod == null)
            {
                return result;
            }

            if (nameOfMethod
                .GetCustomAttributes()
                .OfType<ApiMethodAttribute>()
                .Any())
            {
                var parameter = nameOfMethod
                    .GetParameters()
                    .FirstOrDefault(p => p.Name == paramName);

                if (parameter == null)
                {
                    return result;
                }

                if (parameter
                    .GetCustomAttributes()
                    .OfType<ApiDescriptionAttribute>()
                    .Any())
                {
                    result.ParamDescription.Description = parameter
                        .GetCustomAttributes()
                        .OfType<ApiDescriptionAttribute>()
                        .First()
                        .Description;
                }

                if (parameter.GetCustomAttributes().OfType<ApiRequiredAttribute>().Any())
                {
                    result.Required = parameter
                        .GetCustomAttributes()
                        .OfType<ApiRequiredAttribute>()
                        .First()
                        .Required;
                }

                if (parameter.GetCustomAttributes().OfType<ApiIntValidationAttribute>().Any())
                {
                    result.MinValue = parameter
                        .GetCustomAttributes()
                        .OfType<ApiIntValidationAttribute>()
                        .First()
                        .MinValue;
                    result.MaxValue = parameter
                        .GetCustomAttributes()
                        .OfType<ApiIntValidationAttribute>()
                        .First()
                        .MaxValue;
                }
            }

            return result;
        }

        public ApiMethodDescription GetApiMethodFullDescription(string methodName)
        {
            var type = typeof(T);
            var nameOfMethod = type
                .GetMethods()
                .FirstOrDefault(m => m.Name == methodName);

            if (nameOfMethod == null)
            {
                return null;
            }

            if (nameOfMethod
                .GetCustomAttributes()
                .OfType<ApiMethodAttribute>()
                .Any())
            {
                var parameters = nameOfMethod.GetParameters();
                var result = new ApiMethodDescription
                {
                    MethodDescription = new CommonDescription(methodName, GetApiMethodDescription(methodName)),
                    ParamDescriptions = new ApiParamDescription[parameters.Length]
                };

                for (int i = 0; i < parameters.Length; i++)
                {
                    result.ParamDescriptions[i] = GetApiMethodParamFullDescription(nameOfMethod.Name, parameters[i].Name);
                }

                var returnAttributes = nameOfMethod
                    .ReturnTypeCustomAttributes
                    .GetCustomAttributes(true);

                if (returnAttributes.Any())
                {
                    result.ReturnDescription = new ApiParamDescription
                    {
                        ParamDescription = new CommonDescription(),
                        Required = false,
                        MaxValue = null,
                        MinValue = null
                    };

                    if (returnAttributes.OfType<ApiRequiredAttribute>().Any())
                    {
                        result.ReturnDescription.Required = returnAttributes
                            .OfType<ApiRequiredAttribute>()
                            .First()
                            .Required;
                    }

                    if (returnAttributes.OfType<ApiIntValidationAttribute>().Any())
                    {
                        result.ReturnDescription.MinValue = returnAttributes
                            .OfType<ApiIntValidationAttribute>()
                            .First()
                            .MinValue;
                        result.ReturnDescription.MaxValue = returnAttributes
                            .OfType<ApiIntValidationAttribute>()
                            .First()
                            .MaxValue;
                    }
                }

                return result;
            }

            return null;
        }
    }
}
