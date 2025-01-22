using System.Text.Json;
using Aurum.Models.CustomJsonConverter;
using Aurum.Models.RegularityEnum;
using Aurum.Models.RegularExpenseDto;
using Aurum.Models.RegularityEnum;

namespace AurumTest.ModelTests;

public class CaseInsensitiveEnumConverterTest
{
	private JsonSerializerOptions _options;

	[SetUp]
	public void SetUp()
	{
		_options = new JsonSerializerOptions
		{
			Converters = { new CaseInsensitiveEnumConverter<Regularity>() }
		};
	}

	[Test]
	public void Read_ShouldConvertValid()
	{
		// Arrange
		var json = "\"daily\"";

		// Act
		var result = JsonSerializer.Deserialize<Regularity>(json, _options);

		// Assert
		Assert.That(result, Is.EqualTo(Regularity.Daily));
	}

	[Test]
	public void Read_InvalidValue_ShouldThrowJsonException()
	{
		// Arrange
		var json = "\"invalid_value\"";

		// Act & Assert
		var ex = Assert.Throws<JsonException>(() => JsonSerializer.Deserialize<Regularity>(json, _options));
		Assert.That(ex.Message, Is.EqualTo("Invalid value for enum Regularity"));
	}

	[Test]
	public void Write_ShouldConvert()
	{
		// Arrange
		var value = Regularity.Monthly;

		// Act
		var json = JsonSerializer.Serialize(value, _options);

		// Assert
		Assert.That(json, Is.EqualTo("\"Monthly\""));
	}
	
}