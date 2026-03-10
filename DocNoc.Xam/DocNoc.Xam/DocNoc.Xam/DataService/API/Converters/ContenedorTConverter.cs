using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PPS.Estandar
{

    public class ContenedorTConverter : JsonConverterFactory
    {
        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }

            if (typeToConvert.GetGenericTypeDefinition() != typeof(Contenedor<>))
            {
                return false;
            }

            return true;
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            Type tipoObjeto = type.GetGenericArguments()[0];

            JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(ContenedorTConverterInner<>).MakeGenericType(
                    new Type[] { tipoObjeto }),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: new object[] { options },
                culture: null);

            return converter;
        }

        private class ContenedorTConverterInner<Tipo> : JsonConverter<Contenedor<Tipo>>
        {
            private readonly JsonConverter<Tipo> _convertidorTipo;
            private Type _tipo;

            private Tipo _contenido { get; set; }
            private bool? _error { get; set; }
            private List<Mensaje> _mensajes { get; set; }

            private bool _hayError = false;
            private bool _hayMensajes = false;

            public ContenedorTConverterInner(JsonSerializerOptions options)
            {
                // Cache the key and value types.
                _tipo = typeof(Tipo);

                // For performance, use the existing converter if available.
                _convertidorTipo = (JsonConverter<Tipo>)options
                    .GetConverter(typeof(Tipo));
            }

            public override Contenedor<Tipo> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                if (reader.TokenType != JsonTokenType.StartObject)
                {
                    throw new JsonException();
                }

                while (reader.Read())
                {
                    if (reader.TokenType == JsonTokenType.EndObject)
                    {
                        if (!_hayError)
                            throw new Exception("Respuesta de API no compatible con la aplicación. Falta propiedad Error.");

                        if (!_hayMensajes)
                            throw new Exception("Respuesta de API no compatible con la aplicación. Falta propiedad Mensajes.");

                        Contenedor<Tipo> contenedor = new Contenedor<Tipo>();

                        if (_contenido != null)
                            contenedor.CargarContenido(_contenido);

                        contenedor.ImportarMensajes(new Entidad(_mensajes));

                        return contenedor;
                    }

                    // Get the key.
                    if (reader.TokenType != JsonTokenType.PropertyName)
                    {
                        throw new JsonException();
                    }

                    string propertyName = reader.GetString();

                    if (options.PropertyNameCaseInsensitive)
                        propertyName = propertyName.ToUpper();

                    switch (propertyName)
                    {
                        case "contenido":
                            reader.Read();
                            _contenido = JsonSerializer.Deserialize<Tipo>(ref reader, options);
                            break;
                        case "CONTENIDO":
                            reader.Read();
                            _contenido = JsonSerializer.Deserialize<Tipo>(ref reader, options);
                            break;
                        case "error":
                            _hayError = true;
                            reader.Read();
                            _error = JsonSerializer.Deserialize<bool>(ref reader, options);
                            break;
                        case "ERROR":
                            _hayError = true;
                            reader.Read();
                            _error = JsonSerializer.Deserialize<bool>(ref reader, options);
                            break;
                        case "mensajes":
                            _hayMensajes = true;
                            reader.Read();
                            _mensajes = JsonSerializer.Deserialize<List<Mensaje>>(ref reader, options);
                            break;
                        case "MENSAJES":
                            _hayMensajes = true;
                            reader.Read();
                            _mensajes = JsonSerializer.Deserialize<List<Mensaje>>(ref reader, options);
                            break;
                        default:
                            reader.Read();
                            break;
                    }
                }

                throw new JsonException();
            }

            public override void Write(Utf8JsonWriter writer, Contenedor<Tipo> contenedor, JsonSerializerOptions options)
            {
                writer.WriteStartObject();

                writer.WritePropertyName("contenido");
                JsonSerializer.Serialize(writer, contenedor.Contenido, options);

                writer.WritePropertyName("error");
                JsonSerializer.Serialize(writer, contenedor.Error, options);


                writer.WritePropertyName("mensajes");
                JsonSerializer.Serialize(writer, contenedor.Mensajes, options);

                writer.WriteEndObject();
            }
        }
    }
}

