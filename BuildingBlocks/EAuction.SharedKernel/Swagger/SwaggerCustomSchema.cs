using System;
using System.Linq;

namespace Auction.SharedKernel.Swagger
{
    public static class SwaggerCustomSchema
    {
        public static string GetSchemaId(Type type)
        {
            Func<Type, string> getSchemaId = (t) =>
            {
                if (t.DeclaringType != null && t.DeclaringType.Name != t.Name)
                {
                    return t.DeclaringType.Name + t.Name;
                }
                return t.Name;
            };

            var schemaId = getSchemaId(type);
            if (type.IsGenericType)
            {
                var genericClassSplit = type.Name.Split('`');
                var genericClassName = genericClassSplit.FirstOrDefault();
                var genericType = type.GetGenericArguments().FirstOrDefault();

                schemaId = $"{genericClassName}{getSchemaId(genericType)}";
            }

            return schemaId;
        }
    }
}
