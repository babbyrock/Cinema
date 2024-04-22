using System;
using System.Collections.Generic;
using System.Linq;
using Xunit.Abstractions;
using Xunit.Extensions.Ordering;
using Xunit.Sdk;

public class PriorityOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        where TTestCase : ITestCase
    {
        var sortedMethods = testCases
            .Select(tc => new { tc, attr = tc.TestMethod.Method.GetCustomAttributes(typeof(OrderAttribute).AssemblyQualifiedName).FirstOrDefault() })
            .OrderBy(tc =>
            {
                if (tc.attr == null)
                {
                    return Int32.MaxValue;
                }
                else
                {
                    var orderValue = tc.attr.GetNamedArgument<Int32>("Order");
                    return orderValue != null ? orderValue : Int32.MaxValue;
                }
            })
            .Select(tc => tc.tc);

        return sortedMethods;
    }
}
