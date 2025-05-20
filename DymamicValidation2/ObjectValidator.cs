using DynamicValidation.Model;
using RulesEngine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicValidation.Validator
{
    public class ObjectValidator
    {
        public async Task<ValidationResult> Validate(object obj , IList<Field> fields)
        {

            if (obj == null)
            {
                var error = new ValidationResult();
                error.Errors.Add("Object is null", new List<string> { "Object cannot be null" });
                return error;
            }

            var type = obj.GetType();
            var properties = type.GetProperties();

            var workflows = new List<Workflow>();
            Workflow exampleWorkflow = new Workflow();
            exampleWorkflow.WorkflowName = "Validation Workflow";
            List<Rule> rules = new List<Rule>();
            foreach (var property in properties) {

                var field = fields.FirstOrDefault(f => f.Name == property.Name);
                if (field != null)
                {
                    var value = property.GetValue(obj);

                    rules.AddRange(field.ValidationRules);
                    
                }


            }

            exampleWorkflow.Rules = rules;
            workflows.Add(exampleWorkflow);
            var bre = new RulesEngine.RulesEngine(workflows.ToArray());
            var result = await bre.ExecuteAllRulesAsync(exampleWorkflow.WorkflowName, obj);


            var validationResult = new ValidationResult();
            foreach (var ruleResult in result)
            {
                if (ruleResult.IsSuccess == false)
                {
                    validationResult.Errors.Add(ruleResult.Rule.RuleName, new List<string> { ruleResult.ExceptionMessage });
                   
                }
            }
            return validationResult;
        }
    }
    public class ValidationResult
    {
        public ValidationResult()
        {
         
            Errors = new Dictionary<string, IList<string>>();
        }
        public bool IsValid => !Errors.Any();
        public Dictionary<string, IList<string>> Errors { get; }
    }
}
