using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DiagnosticCenterBillMgtWebApp.Gateway
{
    public class TestGateway : DatabaseGateway
    {
        public int AddTest(TestModel test)
        {
            command.CommandText = "INSERT INTO Tests VALUES(@TestName, @TestFee, @TypeID)";
            command.Parameters.Clear();
            command.Parameters.Add("TestName", SqlDbType.VarChar, 300).Value = test.TestName;
            command.Parameters.Add("TestFee", SqlDbType.Money).Value = test.TestFee;
            command.Parameters.Add("TypeID", SqlDbType.Int).Value = test.TypeID;

            return ExecuteNonQuery();
        }

        public int DeleteTest(TestModel test)
        {
            return 0;
        }

        public int UpdateTest(TestModel test)
        {
            return 0;
        }

        private List<TestModel> AllTests()
        {
            if (reader.HasRows)
            {
                List<TestModel> tests = new List<TestModel>();
                while (reader.Read())
                {
                    tests.Add(new TestModel((int)reader["TestID"],
                                        reader["TestName"].ToString(),
                                        (decimal)reader["TestFees"],
                                        (int)reader["TypeID"],
                                        reader["TypeName"].ToString()));
                }
                return tests;
            }
            return null;
        }

        internal List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            command.CommandText = "TestWiseReport";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Clear();
            command.Parameters.Add("@startDate", SqlDbType.Date).Value = startDate;
            command.Parameters.Add("@endDate", SqlDbType.Date).Value = endDate;
            ExecuteQuery();
            List<ReportModel> report = null;
            if (reader.HasRows)
            {
                report = new List<ReportModel>();
                while (reader.Read())
                {
                    report.Add(new ReportModel(reader["TestName"].ToString(),
                                                (int)reader["TotalTests"],
                                                (decimal)reader["TotalFees"],
                                                startDate, endDate));
                }
                return report;
            }
            return report;
        }

        private TestModel GetTest()
        {
            if (reader.HasRows)
            {
                reader.Read();
                return new TestModel((int)reader["TestID"],
                                    reader["TestName"].ToString(),
                                    (decimal)reader["TestFees"],
                                    (int)reader["TypeID"],
                                    reader["TypeName"].ToString());
            }
            return null;
        }

        public List<TestModel> GetTests()
        {
            command.CommandText = "SELECT * FROM vTestDetails ORDER BY TestName ASC";
            ExecuteQuery();
            return AllTests();
        }

        public List<TestModel> GetTestsByName(string name, bool wildcard = false)
        {
            if (!wildcard)
                command.CommandText = "SELECT * FROM vTestDetails WHERE TestName = @Name";
            else
                command.CommandText = "SELECT * FROM vTestDetails WHERE TestName LIKE '%@Name%'";
            command.Parameters.Clear();
            command.Parameters.Add("Name", SqlDbType.VarChar, 50).Value = name;
            ExecuteQuery();
            return AllTests();
        }

        public List<TestModel> GetTestsByType(int typeId)
        {
            command.CommandText = "SELECT * FROM vTestDetails WHERE TypeID = @TypeID";
            command.Parameters.Clear();
            command.Parameters.Add("TypeID", SqlDbType.Int).Value = typeId;
            ExecuteQuery();
            return AllTests();
        }

        public TestModel GetTestById(int testId)
        {
            command.CommandText = "SELECT * FROM vTestDetails WHERE TestID = @TestID";
            command.Parameters.Clear();
            command.Parameters.Add("TestID", SqlDbType.Int).Value = testId;
            ExecuteQuery();
            return GetTest();
        }

        public List<TestModel> GetTestsByPatient(long patientId)
        {
            command.CommandText = "SELECT * FROM vPatientTestDetails WHERE PatientID = @PatientID";
            command.Parameters.Clear();
            command.Parameters.Add("PatientID", SqlDbType.BigInt).Value = patientId;
            ExecuteQuery();
            return AllTests();
        }

        public int IsExistsByName(string name)
        {
            if (GetTestsByName(name) != null)
                return 1;
            return 0;
        }
    }
}