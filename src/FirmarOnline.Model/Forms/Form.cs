#if NET6_0_OR_GREATER

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FirmarOnline.Model.Forms
{
    /// <summary>
    /// Formulario de WebForms.
    /// </summary>
    [CustomValidation(typeof(Form), nameof(ValidateInputIds),
    ErrorMessage = "The Form is not valid.")]
    public class Form
    {
        /// <summary>
        /// Estilos generales.
        /// </summary>
        public Css GeneralStyles { get; set; }

        /// <summary>
        /// Páginas.
        /// </summary>
        public List<Page> Pages { get; set; }

        /// <summary>
        /// Opciones de las listas desplegables
        /// </summary>
        public List<DataSetOption> DataSets { get; set; }

        internal const string ERROR_THERE_ARE_AT_LEAST_TWO_ITEMS_WITH_THE_SAME_ID =
            "There are at least two Items with the same Id";

        internal const string ERROR_ON_ID_PATTERN =
            "The following ids do not follow the accepted name pattern for ids based on letters, numbers and dashes, always starting with a letter";

        internal const string ERROR_LABEL_MAX_LENGHT =
            "There are fields of type label ({0}) with more than 255 characters.";

        internal const string ERROR_THERE_IS_AT_LEAST_ONE_FIELD_WITHOUT_ID =
            "There is at least one field that does not have an Id";

        /// <summary>
        /// Validación de Form.
        /// Se valida que no haya dos Items con Id igual.
        /// </summary>
        /// <param name="form">Formulario.</param>
        /// <returns>Error con lista de ids repetidos.</returns>
        public static ValidationResult ValidateInputIds(Form form)
        {
            // Buscamos los items con id repetido. Excluimos los que tiene Id == null.
            if (form != null)
            {
                var duplicatedIds = form.Pages.SelectMany(p => p.Items)
                    .OfType<ItemBase>().Where(i => i.Id != null)
                    .GroupBy(i => i.Id).Where(g => g.Count() > 1)
                    .Select(g => g.Key);

                if (duplicatedIds.Count() > 0)
                {
                    return new ValidationResult(ERROR_THERE_ARE_AT_LEAST_TWO_ITEMS_WITH_THE_SAME_ID + ": " + string.Join(", ", duplicatedIds));
                }

                var pattern = new Regex(@"^[a-zA-Z][a-zA-Z0-9_-]*$");
                var invalidIds = form.Pages.SelectMany(p => p.Items)
                    .OfType<ItemBase>().Where(i => i.Id != null && !pattern.IsMatch(i.Id))
                    .Select(i => i.Id);
                if (invalidIds.Count() > 0)
                {
                    return new ValidationResult(ERROR_ON_ID_PATTERN + ": " + string.Join(", ", invalidIds));
                }

                var labelsMaxLenght = form.Pages.SelectMany(p => p.Items)
                   .OfType<TextBase>()
                   .Where(i => i.Id != null && i.Label.Length > 255)
                   .Select(i => i.Id);

                if (labelsMaxLenght.Count() > 0)
                {
                    return new ValidationResult(ERROR_LABEL_MAX_LENGHT + ": " + string.Join(", ", labelsMaxLenght));
                }

                var numberFields = form.Pages.SelectMany(p => p.Items).OfType<NumberField>().Where(i => i.Id == null);
                var dateFields = form.Pages.SelectMany(p => p.Items).OfType<DateField>().Where(i => i.Id == null);
                var stringFields = form.Pages.SelectMany(p => p.Items).OfType<StringField>().Where(i => i.Id == null);
                var dropDownFields = form.Pages.SelectMany(p => p.Items).OfType<DropDownField>().Where(i => i.Id == null);
                var itemBase = ((IEnumerable<ItemBase>)numberFields).Concat(dateFields).Concat(stringFields).Concat(dropDownFields);
                if (itemBase.Any())
                {
                    var errorFieldsWithLabel = numberFields.Where(i => !string.IsNullOrWhiteSpace(i.Label)).Select(o => o.Label);
                    errorFieldsWithLabel = errorFieldsWithLabel.Concat(dateFields.Where(i => !string.IsNullOrWhiteSpace(i.Label)).Select(o => o.Label));
                    errorFieldsWithLabel = errorFieldsWithLabel.Concat(stringFields.Where(i => !string.IsNullOrWhiteSpace(i.Label)).Select(o => o.Label));
                    errorFieldsWithLabel = errorFieldsWithLabel.Concat(dropDownFields.Where(i => !string.IsNullOrWhiteSpace(i.Label)).Select(o => o.Label));
                    return new ValidationResult(ERROR_THERE_IS_AT_LEAST_ONE_FIELD_WITHOUT_ID + ": " + string.Join(", ", errorFieldsWithLabel));
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Obtener los campos del formulario
        /// </summary>
        /// <returns>Lista con los campos del formulario</returns>
        public List<FormValuesInfo> GetFormValuesInfo()
        {
            var formValues = new List<FormValuesInfo>();

            // Recorremos los campos de tipo Texto.
            foreach (StringField stringField in Pages.SelectMany(p => p.Items).OfType<StringField>())
            {
                var formValuesInfo = new FormValuesInfo
                {
                    FieldId = stringField.Id,
                    FieldType = FieldType.Text
                };
                if (stringField.Value != null) formValuesInfo.FieldValue = stringField.Value;
                formValues.Add(formValuesInfo);
            }

            // Recorremos los campos de tipo Number.
            foreach (NumberField numberField in Pages.SelectMany(p => p.Items).OfType<NumberField>())
            {
                var formValuesDto = new FormValuesInfo
                {
                    FieldId = numberField.Id,
                    FieldType = FieldType.Number
                };
                if (numberField.Value != null) formValuesDto.FieldValue = Convert.ToString(numberField.Value, CultureInfo.InvariantCulture);
                formValues.Add(formValuesDto);
            }

            // Recorremos los campos de tipo Date.
            foreach (DateField dateField in Pages.SelectMany(p => p.Items).OfType<DateField>())
            {
                var formValuesDto = new FormValuesInfo
                {
                    FieldId = dateField.Id,
                    FieldType = FieldType.Date
                };
                if (dateField.Value != null) formValuesDto.FieldValue = ((DateTime)dateField.Value).ToString("yyyy-MM-ddTHH:mm:ss.fffZ");
                formValues.Add(formValuesDto);
            }

            // Recorremos los campos de tipo Texto.
            foreach (TextBase textField in Pages.SelectMany(p => p.Items).OfType<TextBase>().Where(p => p.Id != null))
            {
                var formValuesDto = new FormValuesInfo
                {
                    FieldId = textField.Id,
                    FieldType = FieldType.Label
                };
                if (textField.Label != null) formValuesDto.FieldValue = textField.Label;
                formValues.Add(formValuesDto);
            }

            // Recorremos los campos de Dropdown.
            foreach (DropDownField stringField in Pages.SelectMany(p => p.Items).OfType<DropDownField>())
            {
                var formValuesInfo = new FormValuesInfo
                {
                    FieldId = stringField.Id,
                    FieldType = FieldType.Dropdown
                };
                if (stringField.Checked != null) formValuesInfo.FieldValue = stringField.Checked;
                formValues.Add(formValuesInfo);
            }

            return formValues;
        }

        /// <summary>
        /// Asignar valores a los campos del formulario
        /// </summary>
        /// <param name="formValues">Lista de campos con los nuevos valores</param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public void SetFormValuesInfo(IEnumerable<FormValue> formValues)
        {
            // Recorremos los campos de tipo Texto.
            foreach (StringField stringField in Pages.SelectMany(p => p.Items).OfType<StringField>())
            {
                // Buscamos un formValue cuyo id se corresponda con el de algún field.
                var formValue = formValues.FirstOrDefault(v => v.Id == stringField.Id);
                if (formValue != null)
                {
                    stringField.Value = formValue.Value.ToString();

                    // Si el tipo de control es un RadioButton, además deasignar el valor a la
                    // opción seleccionada, tenemos marcarlo con "checked".
                    if (stringField is RadioButton)
                    {
                        var radioButton = stringField as RadioButton;
                        var checkedOption = radioButton.Options.FirstOrDefault(o => string.Equals(o.Value, stringField.Value, StringComparison.OrdinalIgnoreCase));
                        if (checkedOption != null)
                        {
                            checkedOption.Checked = "checked";
                        }
                    }
                }
            }

            // Recorremos los campos de tipo Number.
            foreach (NumberField numberField in Pages.SelectMany(p => p.Items).OfType<NumberField>())
            {
                // Buscamos un formValue cuyo id se corresponda con el de algún field.
                var formValue = formValues.FirstOrDefault(v => v.Id == numberField.Id);
                if (formValue != null)
                {
                    if (!decimal.TryParse(formValue.Value.ToString(), NumberStyles.Any,
                        CultureInfo.InvariantCulture, out decimal decimalFormValue))
                    {
                        throw new FormatException($"The input string '{formValue.Value}' in the field with id '{numberField.Id}' was not in a correct format.");
                    }

                    numberField.Value = decimalFormValue;
                }
            }

            // Recorremos los campos de tipo Date.
            foreach (DateField dateField in Pages.SelectMany(p => p.Items).OfType<DateField>())
            {
                // Buscamos un formValue cuyo id se corresponda con el de algún field.
                var formValue = formValues.FirstOrDefault(v => v.Id == dateField.Id);
                if (formValue != null)
                {
                    if (!DateTime.TryParseExact(formValue.Value.ToString(), "yyyy-MM-ddTHH:mm:ss.fffZ",
                        CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime dateFormValue))
                    {
                        throw new ArgumentException($"The value '{formValue.Value}' in the field with id '{dateField.Id}' cannot be converted to ISO 8601 datetime.");
                    }

                    dateField.Value = dateFormValue;
                }
            }

            // Recorremos los campos de tipo Text.
            foreach (TextBase textField in Pages.SelectMany(p => p.Items).OfType<TextBase>().Where(p => p.Id != null))
            {
                // Buscamos un formValue cuyo id se corresponda con el de algún field.
                var formValue = formValues.FirstOrDefault(v => v.Id == textField.Id);
                if (formValue != null)
                {
                    textField.Label = formValue.Value.ToString();
                }
            }

            // Recorremos los campos de tipo DropDown.
            foreach (DropDownField dropDownField in Pages.SelectMany(p => p.Items).OfType<DropDownField>())
            {
                // Buscamos un formValue cuyo id se corresponda con el de algún field.
                var formValue = formValues.FirstOrDefault(v => v.Id == dropDownField.Id);
                if (formValue != null)
                {
                    dropDownField.Checked = formValue.Value.ToString();
                }
            }
        }
    }

    /// <summary>
    /// Información de los campos de un formulario
    /// </summary>
    public class FormValuesInfo
    {
        /// <summary>
        /// Nombre del campo.
        /// </summary>
        public string FieldId { get; set; }

        /// <summary>
        /// Tipo del campo.
        /// </summary>
        public FieldType FieldType { get; set; }

        /// <summary>
        /// Valor en InvariantCulture.
        /// Un número decimal se guardará como 12345.678
        /// Una dateFormValue se guardará como 2023-11-21
        /// </summary>
        public string FieldValue { get; set; }
    }

    /// <summary>
    /// Tipos de campos de un formulario.
    /// </summary>
    public enum FieldType : ushort
    {
        /// <summary>
        /// Texto.
        /// </summary>
        Text = 0,

        /// <summary>
        /// Numérico.
        /// </summary>
        Number = 1,

        /// <summary>
        /// Fecha.
        /// </summary>
        Date = 2,

        /// <summary>
        /// Textos que se muestran al firmante y se guardan, pero no los complementa el firmante.
        /// </summary>
        Label = 3,

        /// <summary>
        /// Lista desplegable
        /// </summary>
        Dropdown = 4
    }
}
#endif