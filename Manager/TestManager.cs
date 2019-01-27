using DiagnosticCenterBillMgtWebApp.Gateway;
using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;

namespace DiagnosticCenterBillMgtWebApp.Manager
{
    public class TestManager
    {
        public int AddTest(TestModel test)
        {
            return new TestGateway().AddTest(test);
        }

        public int DeleteTest(TestModel test)
        {
            return new TestGateway().DeleteTest(test);
        }

        public int UpdateTest(TestModel test)
        {
            return new TestGateway().UpdateTest(test);
        }

        public List<TestModel> GetTests()
        {
            return new TestGateway().GetTests();
        }

        public List<TestModel> GetTestsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetTests();
            return new TestGateway().GetTestsByName(name);
        }

        public List<TestModel> GetTestsByType(int typeId = 0)
        {
            if (typeId == 0)
                return GetTests();
            return new TestGateway().GetTestsByType(typeId);
        }

        public List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            return new TestGateway().GetReport(startDate, endDate);
        }

        public TestModel GetTestById(int testId)
        {
            return new TestGateway().GetTestById(testId);
        }

        public List<TestModel> GetTestsByPatient(long patientId = 0)
        {
            if (patientId == 0)
                return GetTests();
            return new TestGateway().GetTestsByPatient(patientId);
        }

        public int IsExistsByName(string name)
        {
            return new TestGateway().IsExistsByName(name);
        }
    }
}