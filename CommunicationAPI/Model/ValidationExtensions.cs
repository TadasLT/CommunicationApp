using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CommunicationAPI.Model
{
    public static class ValidationExtensions
    {
        public static ValidationResult ValidateCustomer(this Customer customer)
        {
            var result = new ValidationResult();
            
            if (string.IsNullOrWhiteSpace(customer.Name))
            {
                result.Errors.Add("Customer name is required");
            }
            else if (customer.Name.Length > 100)
            {
                result.Errors.Add("Customer name cannot exceed 100 characters");
            }
            
            if (string.IsNullOrWhiteSpace(customer.Email))
            {
                result.Errors.Add("Customer email is required");
            }
            else if (!IsValidEmail(customer.Email))
            {
                result.Errors.Add("Customer email format is invalid");
            }
            
            result.IsValid = result.Errors.Count == 0;
            result.Message = result.IsValid ? "Customer data is valid" : "Customer validation failed";
            
            return result;
        }
        
        public static ValidationResult ValidateTemplate(this Template template)
        {
            var result = new ValidationResult();
            
            if (string.IsNullOrWhiteSpace(template.Name))
            {
                result.Errors.Add("Template name is required");
            }
            else if (template.Name.Length > 100)
            {
                result.Errors.Add("Template name cannot exceed 100 characters");
            }
            
            if (string.IsNullOrWhiteSpace(template.Subject))
            {
                result.Errors.Add("Template subject is required");
            }
            else if (template.Subject.Length > 200)
            {
                result.Errors.Add("Template subject cannot exceed 200 characters");
            }
            
            if (string.IsNullOrWhiteSpace(template.Body))
            {
                result.Errors.Add("Template body is required");
            }
            else if (template.Body.Length > 2000)
            {
                result.Errors.Add("Template body cannot exceed 2000 characters");
            }
            
            result.IsValid = result.Errors.Count == 0;
            result.Message = result.IsValid ? "Template data is valid" : "Template validation failed";
            
            return result;
        }
        
        public static ValidationResult ValidateId(this int id)
        {
            var result = new ValidationResult();
            
            if (id <= 0)
            {
                result.Errors.Add("ID must be greater than 0");
            }
            
            result.IsValid = result.Errors.Count == 0;
            result.Message = result.IsValid ? "ID is valid" : "ID validation failed";
            
            return result;
        }
        
        public static ValidationResult ValidateSendMessageRequest(this int customerId, int templateId)
        {
            var result = new ValidationResult();
            
            var customerIdValidation = customerId.ValidateId();
            if (!customerIdValidation.IsValid)
            {
                result.Errors.AddRange(customerIdValidation.Errors);
            }
            
            var templateIdValidation = templateId.ValidateId();
            if (!templateIdValidation.IsValid)
            {
                result.Errors.AddRange(templateIdValidation.Errors);
            }
            
            result.IsValid = result.Errors.Count == 0;
            result.Message = result.IsValid ? "Send message request is valid" : "Send message request validation failed";
            
            return result;
        }
        
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
} 