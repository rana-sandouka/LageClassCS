    public class JsonPipeDelimitedArrayConverterFactory : JsonConverterFactory
    {
        /// <inheritdoc />
        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        /// <inheritdoc />
        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var structType = typeToConvert.GetElementType() ?? typeToConvert.GenericTypeArguments[0];
            return (JsonConverter)Activator.CreateInstance(typeof(JsonPipeDelimitedArrayConverter<>).MakeGenericType(structType));
        }
    }