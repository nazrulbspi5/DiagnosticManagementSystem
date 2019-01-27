using DiagnosticCenterBillMgtWebApp.Gateway;
using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;

namespace DiagnosticCenterBillMgtWebApp.Manager
{
    public class TestTypeManager
    {
        public int AddTestType(TestTypeModel testType)
        {
            return new TestTypeGateway().AddTestType(testType);
        }

        public int DeleteTestType(TestTypeModel testType)
        {
            return new TestTypeGateway().DeleteTestType(testType);
        }

        public int UpdateTestType(TestTypeModel testType)
        {
            return new TestTypeGateway().UpdateTestType(testType);
        }

        public List<TestTypeModel> GetTestTypes()
        {
            return new TestTypeGateway().GetTestTypes();
        }

        public List<TestTypeModel> GetTestTypesByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetTestTypes();
            return new TestTypeGateway().GetTestTypesByName(name);
        }

        public int IsExistsByName(string name)
        {
            return new TestTypeGateway().IsExistsByName(name);
        }

        public List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            return new TestTypeGateway().GetReport(startDate, endDate);
        }
    }
}