using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Domain.Models;
using CommunicationAPI.Model;

namespace CommunicationAPI.Middleware
{
    public class ValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ValidationMiddleware> _logger;
        private readonly JsonSerializerOptions _jsonOptions;

        public ValidationMiddleware(RequestDelegate next, ILogger<ValidationMiddleware> logger)
        {
            _next = next;
            _logger = logger;
            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                _logger.LogInformation("Validation middleware: Processing request {Method} {Path}", 
                    context.Request.Method, context.Request.Path);

                var validationResult = ValidateRequest(context);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Validation middleware: Request validation failed - {Errors}", 
                        string.Join(", ", validationResult.Errors));
                    
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";
                    
                    var response = new
                    {
                        message = "Request validation failed",
                        errors = validationResult.Errors,
                        details = validationResult.Message
                    };
                    
                    await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    return;
                }

                if (context.Request.Method == "POST" || context.Request.Method == "PUT")
                {
                    var bodyValidationResult = await ValidateRequestBody(context);
                    if (!bodyValidationResult.IsValid)
                    {
                        _logger.LogWarning("Validation middleware: Request body validation failed - {Errors}", 
                            string.Join(", ", bodyValidationResult.Errors));
                        
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        
                        var response = new
                        {
                            message = "Request body validation failed",
                            errors = bodyValidationResult.Errors,
                            details = bodyValidationResult.Message
                        };
                        
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                        return;
                    }
                }

                _logger.LogInformation("Validation middleware: Request validation passed");
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validation middleware: Error processing request");
                throw;
            }
        }

        private ValidationResult ValidateRequest(HttpContext context)
        {
            var result = new ValidationResult();

            if (context.Request.RouteValues.ContainsKey("id"))
            {
                if (int.TryParse(context.Request.RouteValues["id"]?.ToString(), out int id))
                {
                    var idValidation = id.ValidateId();
                    if (!idValidation.IsValid)
                    {
                        result.Errors.AddRange(idValidation.Errors);
                    }
                }
                else
                {
                    result.Errors.Add("Invalid ID format in path");
                }
            }

            if (context.Request.Path.Value?.Contains("/SendMessage") == true)
            {
                if (context.Request.Query.ContainsKey("customerId") && context.Request.Query.ContainsKey("templateId"))
                {
                    if (int.TryParse(context.Request.Query["customerId"], out int customerId) &&
                        int.TryParse(context.Request.Query["templateId"], out int templateId))
                    {
                        var sendMessageValidation = ValidateSendMessageRequest(customerId, templateId);
                        if (!sendMessageValidation.IsValid)
                        {
                            result.Errors.AddRange(sendMessageValidation.Errors);
                        }
                    }
                    else
                    {
                        result.Errors.Add("Invalid customerId or templateId format");
                    }
                }
                else
                {
                    result.Errors.Add("customerId and templateId are required for SendMessage");
                }
            }

            result.IsValid = result.Errors.Count == 0;
            result.Message = result.IsValid ? "Request validation passed" : "Request validation failed";

            return result;
        }

        private async Task<ValidationResult> ValidateRequestBody(HttpContext context)
        {
            var result = new ValidationResult();

            try
            {
                if (context.Request.Body.CanRead && context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();
                    context.Request.Body.Position = 0;
                    
                    using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
                    var body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    if (!string.IsNullOrEmpty(body))
                    {
                        if (context.Request.Path.Value?.Contains("/Customer/") == true)
                        {
                            try
                            {
                                var customer = JsonSerializer.Deserialize<Customer>(body, _jsonOptions);
                                if (customer != null)
                                {
                                    var validation = customer.ValidateCustomer();
                                    if (!validation.IsValid)
                                    {
                                        result.Errors.AddRange(validation.Errors);
                                    }
                                }
                                else
                                {
                                    result.Errors.Add("Invalid Customer data format");
                                }
                            }
                            catch (JsonException)
                            {
                                result.Errors.Add("Invalid JSON format for Customer");
                            }
                        }
                        else if (context.Request.Path.Value?.Contains("/Template/") == true)
                        {
                            try
                            {
                                var template = JsonSerializer.Deserialize<Template>(body, _jsonOptions);
                                if (template != null)
                                {
                                    var validation = template.ValidateTemplate();
                                    if (!validation.IsValid)
                                    {
                                        result.Errors.AddRange(validation.Errors);
                                    }
                                }
                                else
                                {
                                    result.Errors.Add("Invalid Template data format");
                                }
                            }
                            catch (JsonException)
                            {
                                result.Errors.Add("Invalid JSON format for Template");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Validation middleware: Error reading request body");
                result.Errors.Add("Error reading request body");
            }

            result.IsValid = result.Errors.Count == 0;
            result.Message = result.IsValid ? "Request body validation passed" : "Request body validation failed";

            return result;
        }

        private ValidationResult ValidateSendMessageRequest(int customerId, int templateId)
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
    }

    public static class ValidationMiddlewareExtensions
    {
        public static IApplicationBuilder UseValidationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ValidationMiddleware>();
        }
    }
} 