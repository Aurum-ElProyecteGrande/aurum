using System.Text.Json;
using System.Text.Json.Serialization;
using Aurum.Models.CategoryDtos;

namespace Aurum.Models.CustomJsonConverter;

public class CategoryDictionaryConverter : JsonConverter<Dictionary<CategoryDto, List<SubCategoryDto>>>
{
	public override Dictionary<CategoryDto, List<SubCategoryDto>> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}

	public override void Write(Utf8JsonWriter writer, Dictionary<CategoryDto, List<SubCategoryDto>> value, JsonSerializerOptions options)
	{
		writer.WriteStartArray();

		foreach (var category in value)
		{
			writer.WriteStartObject();

			//Write category details
			writer.WriteString("name", category.Key.Name);
			writer.WriteNumber("categoryId", category.Key.CategoryId);

			//Write subcategories directly under the category
			writer.WriteStartArray("subCategories");
			foreach (var subCategory in category.Value)
			{
				writer.WriteStartObject();
				writer.WriteString("name", subCategory.Name);
				writer.WriteNumber("subCategoryId", subCategory.SubCategoryId);
				writer.WriteNumber("categoryId", subCategory.CategoryId);
				writer.WriteEndObject();
			}
			writer.WriteEndArray();

			writer.WriteEndObject();
		}

		writer.WriteEndArray();
	}
}