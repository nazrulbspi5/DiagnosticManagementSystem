using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DiagnosticCenterBillMgtWebApp.Gateway
{
    public class PatientGateway : DatabaseGateway
    {
        public long AddPatient(PatientModel patient)
        {
            command.CommandText = "INSERT INTO PatientInfo (PatientName, DateOfBirth, Contact) OUTPUT INSERTED.PatientID VALUES (@PatientName, @DateOfBirth, @Contact)";
            command.Parameters.Clear();
            command.Parameters.Add("PatientName", SqlDbType.VarChar, 70).Value = patient.Name;
            command.Parameters.Add("DateOfBirth", SqlDbType.DateTime).Value = patient.DateOfBirth;
            command.Parameters.Add("Contact", SqlDbType.VarChar, 11).Value = patient.Contact;

            long insertedId = ExecuteNonQuery(true);
            int result = 0;

            if (insertedId > 0)
            {
                command.CommandText = "INSERT INTO PatientTestInfo VALUES (@PatientId, @TestId)";
                foreach (TestModel test in patient.Tests)
                {
                    command.Parameters.Clear();
                    command.Parameters.Add("PatientId", SqlDbType.BigInt).Value = insertedId;
                    command.Parameters.Add("TestId", SqlDbType.Int).Value = test.TestId;
                    result = ExecuteNonQuery();
                }
            }
            return insertedId;
        }

        public int DeletePatient(PatientModel patient)
        {
            return 0;
        }

        internal List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            command.CommandText = "UnpaidBillReport";
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
                    report.Add(new ReportModel(reader["BillNo"].ToString(),
                                                reader["PatientName"].ToString(),
                                                reader["Contact"].ToString(),
                                                (decimal)reader["TotalFees"],
                                                startDate, endDate));
                }
                return report;
            }
            return report;
        }

        public int UpdatePatient(PatientModel patient)
        {
            return 0;
        }

        public int UpdatePayment(long patientId)
        {
            command.CommandText = "UPDATE PatientInfo SET PaymentStatus = 1 WHERE PatientID = @PatientID";
            command.Parameters.Clear();
            command.Parameters.Add("PatientID", SqlDbType.BigInt).Value = patientId;
            return ExecuteNonQuery();
        }

        private List<PatientModel> AllPatients()
        {
            ExecuteQuery();
            if (reader.HasRows)
            {
                List<PatientModel> patients = new List<PatientModel>();
                while (reader.Read())
                {
                    patients.Add(new PatientModel(Convert.ToInt64(reader["PatientID"]),
                                        reader["PatientName"].ToString(),
                                        reader["Contact"].ToString(),
                                        (DateTime)reader["DateOfBirth"],
                                        reader["BillNo"].ToString(),
                                        Convert.ToInt32(reader["PaymentStatus"]),
                                        (DateTime)reader["TestDate"]));
                }
                return patients;
            }
            return null;
        }

        private PatientModel GetPatient()
        {
            ExecuteQuery();
            if (reader.HasRows)
            {
                reader.Read();
                return new PatientModel(Convert.ToInt64(reader["PatientID"]),
                                        reader["PatientName"].ToString(),
                                        reader["Contact"].ToString(),
                                        (DateTime)reader["DateOfBirth"],
                                        reader["BillNo"].ToString(),
                                        Convert.ToInt32(reader["PaymentStatus"]),
                                        (DateTime)reader["TestDate"]);
            }
            return null;
        }

        public List<PatientModel> GetPatients()
        {
            command.CommandText = "SELECT * FROM vPatientInfo";
            return AllPatients();
        }

        public List<PatientModel> GetPatientsByName(string name)
        {
            command.CommandText = "SELECT * FROM PatientInfo WHERE Patient = @Name";
            command.Parameters.Clear();
            command.Parameters.Add("Name", SqlDbType.VarChar, 70).Value = name;
            return AllPatients();
        }

        public PatientModel GetPatientsByContact(string contact)
        {
            command.CommandText = "SELECT * FROM vPatientInfo WHERE Contact = @Contact";
            command.Parameters.Clear();
            command.Parameters.Add("Contact", SqlDbType.VarChar, 11).Value = contact;
            return GetPatient();
        }

        public PatientModel GetPatientsByPatientID(long patientId)
        {
            command.CommandText = "SELECT * FROM vPatientInfo WHERE PatientID = @PatientID";
            command.Parameters.Clear();
            command.Parameters.Add("PatientID", SqlDbType.BigInt).Value = patientId;
            return GetPatient();
        }

        public DataSet GetPatientTestDetailsByPatientID(long patientId)
        {
            command.CommandText = "SELECT * FROM vPatientTestDetails WHERE PatientID = @PatientID";
            command.Parameters.Clear();
            command.Parameters.Add("PatientID", SqlDbType.BigInt).Value = patientId;
            ExecuteQuery();
            DataSet patientDetails = new DataSet();
            patientDetails.Tables.Add("PatientDetails");
            patientDetails.Tables[0].Load(reader);
            return patientDetails;
        }

        public PatientModel GetPatientsByBillNo(string billNo)
        {
            command.CommandText = "SELECT * FROM vPatientInfo WHERE BillNo = @BillNo";
            command.Parameters.Clear();
            command.Parameters.Add("BillNo", SqlDbType.VarChar).Value = billNo;
            return GetPatient();
        }
    }
}