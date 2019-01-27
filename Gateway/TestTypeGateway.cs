using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DiagnosticCenterBillMgtWebApp.Gateway
{
    public class TestTypeGateway : DatabaseGateway
    {
        public int AddTestType(TestTypeModel testType)
        {
            command.CommandText = "INSERT INTO TestTypes VALUES (@TypeName)";
            command.Parameters.Clear();
            command.Parameters.Add("TypeName", SqlDbType.VarChar, 50).Value = testType.TypeName;
            return ExecuteNonQuery();
        }

        public int DeleteTestType(TestTypeModel testType)
        {
            return 0;
        }

        public int UpdateTestType(TestTypeModel testType)
        {
            return 0;
        }

        public List<TestTypeModel> GetTestTypes()
        {
            command.CommandText = "SELECT * FROM TestTypes ORDER BY TypeName ASC";
            ExecuteQuery();
            List<TestTypeModel> testTypes = null;
            if (reader.HasRows)
            {
                testTypes = new List<TestTypeModel>();
                while (reader.Read())
                {
                    testTypes.Add(new TestTypeModel((int)reader["TypeID"], reader["TypeName"].ToString()));
                }
            }
            return testTypes;
        }

        internal List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            command.CommandText = "TypeWiseReport";
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
                    report.Add(new ReportModel(reader["TypeName"].ToString(),
                                                (int)reader["TotalTests"],
                                                (decimal)reader["TotalFees"],
                                                startDate, endDate));
                }
                return report;
            }
            return report;
        }

        public List<TestTypeModel> GetTestTypesByName(string name)
        {
            return null;
        }

        internal int IsExistsByName(string name)
        {
            command.CommandText = "SELECT * FROM TestTypes WHERE typeName = @Name";
            command.Parameters.Clear();
            command.Parameters.Add("Name", SqlDbType.VarChar, 50).Value = name;
            ExecuteQuery();
            if (reader.HasRows)
                return 1;
            return 0;
        }
    }
}