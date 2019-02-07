using NETCoreJsonMapper.Builders;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace NETCoreJsonMapper.Tests
{
    public class ExampleTest
    {
        [Theory]
        [ClassData(typeof(RunExamplesTestData))]
        public void RunExamples(string jsonDataSourceFileFullPath, Type jsonDataSourceType, string expectedJsonFileFullPath)
        {
            // Arrange
            string jsonDataSourceString = string.Empty;
            if (File.Exists(jsonDataSourceFileFullPath))
            {
                jsonDataSourceString = File.ReadAllText(jsonDataSourceFileFullPath);
            }
            string expectedJsonString = string.Empty;
            if (File.Exists(expectedJsonFileFullPath))
            {
                expectedJsonString = File.ReadAllText(expectedJsonFileFullPath);
            }

            // Act
            string outputJsonString = new JsonMapper(jsonDataSourceString)
                .InvokeJsonMapping(deserializationType: jsonDataSourceType);

            // Assert
            Assert.Equal(
                Regex.Replace(expectedJsonString, @"\s+", ""),
                Regex.Replace(outputJsonString, @"\s+", ""));
        }
    }
}