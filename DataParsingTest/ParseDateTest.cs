using System;
using DeskReservationAPI.Helpers;
using Xunit;

namespace DataParsingTest
{
    public class ParseDateTest
    {
        [Fact]
        public void ParseDateValidation()
        {
            var dateString = "01-05-2022";
            bool expected = true;
            var actual = DateParser.BeAValidDate(dateString);
            
            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ParseDateValidationFormatting()
        {
            var dateString = "01/05/2022";
            bool expected = true;
            var actual = DateParser.BeAValidDate(dateString);

            Assert.Equal(expected, actual);
        }
        [Fact]
        public void ParseDateValidationWrongInput()
        {
            var dateString = "01052022";
            bool expected = false;
            var actual = DateParser.BeAValidDate(dateString);

            Assert.Equal(expected, actual);
        }
    }
}
