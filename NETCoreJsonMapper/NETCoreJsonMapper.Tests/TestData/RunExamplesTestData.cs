using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace NETCoreJsonMapper.Tests
{
    internal class RunExamplesTestData : IEnumerable<object[]>
    {
        private static string AssemblyDirectory {
            get {
                string codeBase = Assembly.GetExecutingAssembly().CodeBase;
                UriBuilder uri = new UriBuilder(codeBase);
                string path = Uri.UnescapeDataString(uri.Path);
                return Path.GetDirectoryName(path);
            }
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            // Inputs: string jsonDataSourceFileFullPath, Type jsonDataSourceType
            // Outputs: string expectedJsonFileFullPath
            yield return new object[] {
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\..\Example1\JsonDataSource\Example.json"),
                typeof(Example1.Mappings.JsonDataSource),
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\JsonDataTarget\Example1\Example.json")
            };
            yield return new object[] {
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\..\Example2\JsonDataSource\Example.json"),
                typeof(Example2.Mappings.JsonDataSource),
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\JsonDataTarget\Example2\Example.json")
            };
            yield return new object[] {
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\..\Example3\JsonDataSource\Example.json"),
                typeof(Example3.Mappings.JsonDataSource),
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\JsonDataTarget\Example3\Example.json")
            };
            yield return new object[] {
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\..\Example4\JsonDataSource\Example.json"),
                typeof(Example4.Mappings.JsonDataSource),
                Path.Combine(AssemblyDirectory,
                    @"..\..\..\JsonDataTarget\Example4\Example.json")
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}