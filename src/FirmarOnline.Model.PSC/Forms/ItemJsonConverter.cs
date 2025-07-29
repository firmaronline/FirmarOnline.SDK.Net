#if NET6_0_OR_GREATER

using System;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FirmarOnline.Model.PSC.Forms
{
    /// <summary>
    /// Convierte un <see cref="ItemBase"/> en JSON o desde JSON.
    /// </summary>
    public class ItemJsonConverter : JsonConverter<ItemBase>
    {
        /// <summary>
        /// Lee y convierte el JSON en un <see cref="ItemBase"/>
        /// Dependiendo de los valores especificados en las propiedades devolverá:
        /// - <see cref="Image"/>
        /// - <see cref="PageBreak"/>
        /// - <see cref="BarBreak"/>
        /// - <see cref="LineBreak"/>
        /// - <see cref="HeaderText"/>
        /// - <see cref="ParagraphText"/>
        /// - <see cref="ContinuousText"/>
        /// - <see cref="TextField"/>
        /// - <see cref="NumberField"/>
        /// - <see cref="DateField"/>
        /// - <see cref="EmailField"/>
        /// - <see cref="RadioButton"/>
        /// - <see cref="DropDownField"/>
        /// - <see cref="Table"/>
        /// </summary>
        /// <param name="reader">Lector.</param>
        /// <param name="typeToConvert">Tipo que se va a convertir.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        /// <returns>Objeto convertido.</returns>
        /// <exception cref="JsonException"></exception>
        public override ItemBase Read(ref Utf8JsonReader reader, System.Type typeToConvert, JsonSerializerOptions options)
        {
            using JsonDocument doc = JsonDocument.ParseValue(ref reader);        
            var itemTypeProp = doc.RootElement.EnumerateObject().FirstOrDefault(i => string.Compare(i.Name, "type", StringComparison.OrdinalIgnoreCase) == 0).Value;

            if (Enum.TryParse(itemTypeProp.ToString(), true, out ItemType itemType))
            {
                return itemType switch
                {
                    ItemType.image => JsonSerializer.Deserialize<Image>(doc.RootElement.GetRawText(), options),

                    ItemType.pageBreak => JsonSerializer.Deserialize<PageBreak>(doc.RootElement.GetRawText(), options),
                    ItemType.barBreak => JsonSerializer.Deserialize<BarBreak>(doc.RootElement.GetRawText(), options),
                    ItemType.lineBreak => JsonSerializer.Deserialize<LineBreak>(doc.RootElement.GetRawText(), options),

                    ItemType.headerText => JsonSerializer.Deserialize<HeaderText>(doc.RootElement.GetRawText(), options),
                    ItemType.paragraphText => JsonSerializer.Deserialize<ParagraphText>(doc.RootElement.GetRawText(), options),
                    ItemType.continuousText => JsonSerializer.Deserialize<ContinuousText>(doc.RootElement.GetRawText(), options),

                    ItemType.textField => JsonSerializer.Deserialize<TextField>(doc.RootElement.GetRawText(), options),
                    ItemType.numberField => JsonSerializer.Deserialize<NumberField>(doc.RootElement.GetRawText(), options),
                    ItemType.dateField => JsonSerializer.Deserialize<DateField>(doc.RootElement.GetRawText(), options),
                    ItemType.emailField => JsonSerializer.Deserialize<EmailField>(doc.RootElement.GetRawText(), options),
                    ItemType.radioButton => JsonSerializer.Deserialize<RadioButton>(doc.RootElement.GetRawText(), options),
                    ItemType.dropDownField => JsonSerializer.Deserialize<DropDownField>(doc.RootElement.GetRawText(), options),

                    ItemType.table => JsonSerializer.Deserialize<Table>(doc.RootElement.GetRawText(), options),
                    _ => throw new NotImplementedException(),
                };
            }
            else
            {
                throw new JsonException($"Unknown json item type {itemTypeProp}.");
            }
        }

        /// <summary>
        /// Escribe el <see cref="ItemBase"/> especificado como JSON.
        /// </summary>
        /// <param name="writer">El sistema de escritura en el que se va a escribir.</param>
        /// <param name="value">Valor que se va a convertir en JSON.</param>
        /// <param name="options">Objeto que especifica las opciones de serialización que se van a utilizar.</param>
        /// <exception cref="JsonException"></exception>
        public override void Write(Utf8JsonWriter writer, ItemBase value, JsonSerializerOptions options)
        {
            switch (value)
            {
                case Image image:
                    JsonSerializer.Serialize(writer, image, options);
                    break;

                case PageBreak pageBreak:
                    JsonSerializer.Serialize(writer, pageBreak, options);
                    break;

                case BarBreak barBreak:
                    JsonSerializer.Serialize(writer, barBreak, options);
                    break;

                case LineBreak lineBreak:
                    JsonSerializer.Serialize(writer, lineBreak, options);
                    break;

                case HeaderText headerText:
                    JsonSerializer.Serialize(writer, headerText, options);
                    break;

                case ParagraphText paragraphText:
                    JsonSerializer.Serialize(writer, paragraphText, options);
                    break;

                case ContinuousText continuousText:
                    JsonSerializer.Serialize(writer, continuousText, options);
                    break;

                case TextField textField:
                    JsonSerializer.Serialize(writer, textField, options);
                    break;

                case NumberField numberField:
                    JsonSerializer.Serialize(writer, numberField, options);
                    break;

                case DateField dateField:
                    JsonSerializer.Serialize(writer, dateField, options);
                    break;

                case EmailField emailField:
                    JsonSerializer.Serialize(writer, emailField, options);
                    break;

                case RadioButton radioButton:
                    JsonSerializer.Serialize(writer, radioButton, options);
                    break;

                case Table table:
                    JsonSerializer.Serialize(writer, table, options);
                    break;

                case DropDownField dropDownField:
                    JsonSerializer.Serialize(writer, dropDownField, options);
                    break;

                default:
                    throw new JsonException("Unknown type");
            }
        }
    }
}
#endif