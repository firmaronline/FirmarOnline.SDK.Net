using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Xunit;

namespace FirmarOnline.Model.PSC.Tests.Recipients
{
    /// <summary>
    /// Pruebas de los valores numéricos y metadatos (<see cref="DisplayAttribute"/>) de las
    /// entradas de enumeración añadidas para la funcionalidad "Liveness con Documento".
    /// </summary>
    public class LivenessAndCardIdTests
    {
        #region Valores numéricos

        [Fact]
        public void RecipientAuthenticationType_LivenessAndCardId_Has_Value_71()
        {
            Assert.Equal(71, (int)RecipientAuthenticationType.LivenessAndCardId);
        }

        [Fact]
        public void AuditEventType_AuthenticationLivenessAndCardIdValidated_Has_Value_331()
        {
            Assert.Equal(331, (int)AuditEventType.AuthenticationLivenessAndCardIdValidated);
        }

        [Fact]
        public void AuditEventType_AuthenticationLivenessAndCardIdNotValidated_Has_Value_332()
        {
            Assert.Equal(332, (int)AuditEventType.AuthenticationLivenessAndCardIdNotValidated);
        }

        [Fact]
        public void ErrorType_ValidationLivenessAndCardId_Has_Value_14()
        {
            Assert.Equal(14, (int)ErrorType.ValidationLivenessAndCardId);
        }

        #endregion Valores numéricos

        #region Metadatos (DisplayAttribute)

        [Fact]
        public void RecipientAuthenticationType_LivenessAndCardId_Has_Expected_DisplayName()
        {
            var displayName = GetDisplayName(RecipientAuthenticationType.LivenessAndCardId);
            Assert.Equal("Liveness con Documento", displayName);
        }

        [Fact]
        public void AuditEventType_AuthenticationLivenessAndCardIdValidated_Has_Expected_DisplayName()
        {
            var displayName = GetDisplayName(AuditEventType.AuthenticationLivenessAndCardIdValidated);
            Assert.Equal("Autenticación por Liveness con Documento válida", displayName);
        }

        [Fact]
        public void AuditEventType_AuthenticationLivenessAndCardIdNotValidated_Has_Expected_DisplayName()
        {
            var displayName = GetDisplayName(AuditEventType.AuthenticationLivenessAndCardIdNotValidated);
            Assert.Equal("Autenticación por Liveness con Documento inválida", displayName);
        }

        #endregion Metadatos (DisplayAttribute)

        #region Unicidad de valores

        [Fact]
        public void RecipientAuthenticationType_Values_Are_Unique()
        {
            AssertEnumValuesAreUnique<RecipientAuthenticationType>();
        }

        [Fact]
        public void AuditEventType_Values_Are_Unique()
        {
            AssertEnumValuesAreUnique<AuditEventType>();
        }

        [Fact]
        public void ErrorType_Values_Are_Unique()
        {
            AssertEnumValuesAreUnique<ErrorType>();
        }

        #endregion Unicidad de valores

        #region Helpers

        /// <summary>
        /// Obtiene el nombre del <see cref="DisplayAttribute"/> aplicado a un miembro de enumeración
        /// mediante reflexión.
        /// </summary>
        private static string? GetDisplayName<TEnum>(TEnum value) where TEnum : struct, Enum
        {
            var field = typeof(TEnum).GetField(value.ToString());
            Assert.NotNull(field);
            var display = field!.GetCustomAttribute<DisplayAttribute>();
            Assert.NotNull(display);
            return display!.Name;
        }

        /// <summary>
        /// Verifica que todos los miembros de la enumeración tienen valores numéricos únicos,
        /// detectando alias introducidos por error (que en C# no producen error de compilación).
        /// </summary>
        private static void AssertEnumValuesAreUnique<TEnum>() where TEnum : struct, Enum
        {
            // Se recorren los nombres (no los valores) para detectar alias: dos miembros
            // con nombres distintos que comparten el mismo valor numérico, lo cual en C#
            // no produce error de compilación pero sí representa una colisión no deseada.
            var names = Enum.GetNames(typeof(TEnum));
            var underlyingValues = names
                .Select(name => Convert.ToInt64((TEnum)Enum.Parse(typeof(TEnum), name)))
                .ToArray();
            var distinctCount = underlyingValues.Distinct().Count();

            Assert.Equal(names.Length, distinctCount);
        }

        #endregion Helpers
    }
}