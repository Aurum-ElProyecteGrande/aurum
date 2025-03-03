using System.Text.Json;
using System.Text.Json.Serialization;

namespace Aurum.Models.CustomJsonConverter;

public class CaseInsensitiveEnumConverter<T> : JsonConverter<T> where T : struct, Enum
{
	public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		if (reader.TokenType != JsonTokenType.String)
			throw new JsonException($"Invalid value for enum {typeof(T).Name}");
		
		var value = reader.GetString();
		if (Enum.TryParse(value, true, out T result))
			return result;

		throw new JsonException($"Invalid value for enum {typeof(T).Name}");
	}

	public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
	{
		writer.WriteStringValue(value.ToString());
	}
}