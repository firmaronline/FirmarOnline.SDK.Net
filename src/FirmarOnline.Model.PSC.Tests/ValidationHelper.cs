using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FirmarOnline.Model.PSC.Tests
{
    /// <summary>
    /// Utilidades de validación basadas en <see cref="System.ComponentModel.DataAnnotations"/>
    /// que recorren recursivamente un grafo de objetos para recopilar todos los errores
    /// de validación, incluidos los de colecciones y propiedades anidadas.
    /// </summary>
    internal static class ValidationHelper
    {
        /// <summary>
        /// Ejecuta la validación de anotaciones de datos sobre una instancia y todo su grafo
        /// de objetos accesible a través de propiedades públicas legibles y elementos de colección.
        /// </summary>
        public static IList<ValidationResult> Validate(object instance)
        {
            var results = new List<ValidationResult>();
            var visited = new HashSet<object>(ReferenceEqualityComparer.Instance);

            void Recurse(object? obj)
            {
                if (obj is null) return;
                if (obj is string) { /* leaf */ return; }
                if (!visited.Add(obj)) return; // avoid cycles

                // Validate this object
                var ctx = new ValidationContext(obj, serviceProvider: null, items: null);
                Validator.TryValidateObject(obj, ctx, results, validateAllProperties: true);

                // If it's a collection, validate the collection object itself (class-level validators)
                // and then each element.
                if (obj is IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                        Recurse(item);
                    return;
                }

                // Recurse into public instance properties (no indexers)
                var props = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(p => p.CanRead && p.GetIndexParameters().Length == 0);
                foreach (var p in props)
                    Recurse(p.GetValue(obj));
            }

            Recurse(instance);
            return results;
        }

        /// <summary>
        /// Indica si una instancia y cumplen todas las anotaciones de datos sin producir resultados de validación.
        /// </summary>
        /// <param name="instance">Instancia raíz a validar.</param>
        /// <returns><see langword="true"/> si no hay errores; en caso contrario, <see langword="false"/>.</returns>
        public static bool IsValid(object instance) => Validate(instance).Count == 0;

        /// <summary>
        /// Comparador de igualdad por referencia para usar en estructuras que
        /// requieren identificar objetos sin depender de <c>Equals</c>/<c>GetHashCode</c> de usuario.
        /// </summary>
        private sealed class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            public static readonly ReferenceEqualityComparer Instance = new();
            public new bool Equals(object? x, object? y) => ReferenceEquals(x, y);
            public int GetHashCode(object obj) => System.Runtime.CompilerServices.RuntimeHelpers.GetHashCode(obj);
        }
    }
}
