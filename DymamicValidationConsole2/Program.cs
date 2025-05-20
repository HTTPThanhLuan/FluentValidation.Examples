// See https://aka.ms/new-console-template for more information
using DynamicValidation;
using DynamicValidation.Model;
using DynamicValidation.Validator;

Console.WriteLine("Hello, World!");



var myType = DynamicClassBuilder.CreateDynamicClass("DynamicClass", FieldsProvider.GetFields().ToList());
var myInstance = Activator.CreateInstance(myType);
//How set value for property?
var propertyInfo = myType.GetProperty("ISBN");
propertyInfo.SetValue(myInstance, "1234567890");

var propertyFitness = myType.GetProperty("Fineness");
propertyFitness.SetValue(myInstance, 9);

ObjectValidator objectValidator = new ObjectValidator();

var validationResult = objectValidator.Validate(myInstance, FieldsProvider.GetFields().ToList());

Console.WriteLine(myType.Name);