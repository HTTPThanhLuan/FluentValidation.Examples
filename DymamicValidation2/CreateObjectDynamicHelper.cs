using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DynamicValidation.Model
{
    public class DynamicClassBuilder
    {
        public static Type CreateDynamicClass(string className, List<Field> fields)
        {
            var assemblyName = new AssemblyName("DynamicAssembly");
            var assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(assemblyName, AssemblyBuilderAccess.Run);
            var moduleBuilder = assemblyBuilder.DefineDynamicModule("MainModule");

            var typeBuilder = moduleBuilder.DefineType(className, TypeAttributes.Public);

            // Define fields
            foreach (var field in fields)
            {
              //  typeBuilder.DefineField(field.Name, field.Type, FieldAttributes.Public);
                typeBuilder.DefinePropertyWithAutoBacking(field.Name, field.Type);
            }

            // Create the type
            return typeBuilder.CreateType();
        }
    }

    /// <summary>
    /// Extension to define auto-implemented properties (simple syntactic sugar).
    /// </summary>
    public static class TypeBuilderExtensions
    {
        public static void DefinePropertyWithAutoBacking(this TypeBuilder typeBuilder, string propertyName, Type propertyType)
        {
            var fieldBuilder = typeBuilder.DefineField($"_{propertyName}", propertyType, FieldAttributes.Private);

            var propBuilder = typeBuilder.DefineProperty(propertyName, PropertyAttributes.HasDefault, propertyType, null);

            // Getter
            var getMethod = typeBuilder.DefineMethod($"get_{propertyName}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, propertyType, Type.EmptyTypes);
            var getIL = getMethod.GetILGenerator();
            getIL.Emit(OpCodes.Ldarg_0);
            getIL.Emit(OpCodes.Ldfld, fieldBuilder);
            getIL.Emit(OpCodes.Ret);

            // Setter
            var setMethod = typeBuilder.DefineMethod($"set_{propertyName}", MethodAttributes.Public | MethodAttributes.SpecialName | MethodAttributes.HideBySig, null, new[] { propertyType });
            var setIL = setMethod.GetILGenerator();
            setIL.Emit(OpCodes.Ldarg_0);
            setIL.Emit(OpCodes.Ldarg_1);
            setIL.Emit(OpCodes.Stfld, fieldBuilder);
            setIL.Emit(OpCodes.Ret);

            propBuilder.SetGetMethod(getMethod);
            propBuilder.SetSetMethod(setMethod);
        }
    }
}
