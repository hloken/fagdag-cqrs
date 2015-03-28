using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TechTalk.SpecFlow;

namespace FagdagCqrs.Specs.Arguments
{
    public static class TableTransformationValidator
    {
        public static void ValidateTransformation<T>(this Table table)
        {
            var outputType = typeof (T);
            var nonMatchingHeaders = new List<string>();

            foreach (var header in table.Header)
            {
                var foundProperty = FindProperty(header,
                    outputType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));
                var foundField = FindField(header,
                    outputType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic));

                if (!foundProperty && !foundField)
                    nonMatchingHeaders.Add(header);
            }
            if (nonMatchingHeaders.Count > 0)
            {
                var parameterNames = string.Format(@"""{0}""", string.Join(@""", """, nonMatchingHeaders));
                throw new ArgumentException(
                    string.Format(@"Can not find property or field mapping to table {0} {1}, inspecting type: ""{2}""",
                        nonMatchingHeaders.Count == 1 ? "column" : "columns", parameterNames, outputType.Name));
            }
        }

        private static bool FindField(IEnumerable<char> header, IEnumerable<FieldInfo> typeFields)
        {
            return typeFields.FirstOrDefault(field => Match(header, field.Name)) != null;
        }

        private static bool FindProperty(IEnumerable<char> header, IEnumerable<PropertyInfo> typeProperties)
        {
            return typeProperties.FirstOrDefault(property => Match(header, property.Name)) != null;
        }

        private static bool Match(IEnumerable<char> header, string propertyOrFieldName)
        {
            int propertyIndex = 0;
            foreach (var character in header)
            {
                if (character != ' ')
                {
                    if (propertyIndex == propertyOrFieldName.Length)
                        return false;

                    if (character != propertyOrFieldName[propertyIndex])
                    {
                        if (Char.ToLower(character) != Char.ToLower(propertyOrFieldName[propertyIndex]))
                            return false;
                    }
                    propertyIndex++;
                }
            }

            return propertyIndex == propertyOrFieldName.Length;
        }
    }
}
