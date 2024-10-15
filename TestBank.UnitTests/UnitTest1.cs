using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using BankingApplication.UnitTests;
//using System;

namespace BankingApplication.UnitTests
{
    [TestFixture]
    public class ErrorViewModelTests
    {
        [Test]
        public void ShowRequestId_WhenRequestIdIsNull_ReturnsFalse()
        {
            // Arrange
            var model = new ErrorViewModel();

            // Act
            var result = model.ShowRequestId();

            // Assert
            Assert.IsFalse(result);
        }
    }
}
