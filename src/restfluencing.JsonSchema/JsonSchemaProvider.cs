namespace RestFluencing.JsonSchema
{
    using System;
    using System.Collections.Concurrent;
    using Newtonsoft.Json.Schema;
    using Newtonsoft.Json.Schema.Generation;

    internal class JsonSchemaProvider
    {
        private static readonly object _lock = new object();
        private readonly ConcurrentDictionary<Type, JSchema> _cache = new ConcurrentDictionary<Type, JSchema>();
        private readonly JSchemaGenerator _jSchemaGenerator = new JSchemaGenerator();

        public static JsonSchemaProvider Instance { get; } = new JsonSchemaProvider();

        public JSchema GetSchema<T>()
        {
            var type = typeof(T);

            if (_cache.TryGetValue(type, out var schema))
                return schema;

            lock (_lock)
            {
                if (_cache.TryGetValue(type, out schema))
                    return schema;

                schema = _jSchemaGenerator.Generate(type);
                _cache.TryAdd(type, schema);

                return schema;
            }
        }
    }
}