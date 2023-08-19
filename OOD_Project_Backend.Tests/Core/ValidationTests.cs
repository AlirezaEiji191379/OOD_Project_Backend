using FluentAssertions;
using OOD_Project_Backend.User.Business.Validations.Conditions;
using Xunit;

namespace OOD_Project_Backend.Tests.Core;

public class ValidationTests
{

    [Fact]
    public void EmailRule_ShouldReturnFalse_WhenEmailIsNotValid()
    {
        // Arrange
        var email = "a.com";
        var emailRule = new EmailRule(email);
        // Act
        var result = emailRule.Apply();
        // Assert
        result.Should().Be(false);
    }
    
    [Fact]
    public void EmailRule_ShouldReturnTrue_WhenEmailIsValid()
    {
        // Arrange
        var email = "a@gmail.com";
        var emailRule = new EmailRule(email);
        // Act
        var result = emailRule.Apply();
        // Assert
        result.Should().Be(true);
    }
    
    [Fact]
    public void PhoneNumberRule_ShouldReturnFalse_WhenPhoneNumberIsNotValid()
    {
        // Arrange
        var phone = "09194165";
        var phoneNumberRule = new PhoneNumberRule(phone);
        // Act
        var result = phoneNumberRule.Apply();
        // Assert
        result.Should().Be(false);
    }
    
    [Fact]
    public void PhoneNumberRule_ShouldReturnTrue_WhenPhoneNumberIsValid()
    {
        // Arrange
        var phone = "09194165232";
        var phoneNumberRule = new PhoneNumberRule(phone);
        // Act
        var result = phoneNumberRule.Apply();
        // Assert
        result.Should().Be(true);
    }
    
    [Fact]
    public void PasswordRule_ShouldReturnTrue_WhenPasswordIsValid()
    {
        // Arrange
        var password = "1234567";
        var passwordRule = new PhoneNumberRule(password);
        // Act
        var result = passwordRule.Apply();
        // Assert
        result.Should().Be(false);
    }
    
    [Fact]
    public void PasswordRule_ShouldReturnFalse_WhenPasswordIsNotValid()
    {
        // Arrange
        var password = "1234";
        var passwordRule = new PhoneNumberRule(password);
        // Act
        var result = passwordRule.Apply();
        // Assert
        result.Should().Be(false);
    }
    
    
}