using System.ComponentModel.DataAnnotations;

namespace LivrariaAPI.Services.ValidationObject
{
    public class DataValidation
    {
        public string messageResult { set; get; }
        private List<ValidationResult> _validationResults { get; set; }
        private ValidationContext _validationContext { get; set; }

        public bool Validate(object instance)
        {
            this._validationContext = new ValidationContext(instance);
            this._validationResults = new List<ValidationResult>();
            this.messageResult = string.Empty;

            bool isValid = Validator.TryValidateObject(instance, this._validationContext, this._validationResults);

            if (!isValid)
                foreach (var errorItem in this._validationResults)
                    this.messageResult += $"{errorItem.ErrorMessage}\n";
            return isValid;
        }


    }
}
