using DiagnosticCenterBillMgtWebApp.Gateway;
using DiagnosticCenterBillMgtWebApp.Model;
using System;
using System.Collections.Generic;
using System.Data;

namespace DiagnosticCenterBillMgtWebApp.Manager
{
    public class PatientManager
    {
        public long AddPatient(PatientModel patient)
        {
            return new PatientGateway().AddPatient(patient);
        }

        public int DeletePatient(PatientModel patient)
        {
            return new PatientGateway().DeletePatient(patient);
        }

        public int UpdatePatient(PatientModel patient)
        {
            return new PatientGateway().UpdatePatient(patient);
        }

        public int UpdatePayment(long patientId)
        {
            return new PatientGateway().UpdatePayment(patientId);
        }

        public List<PatientModel> GetPatients()
        {
            return new PatientGateway().GetPatients();
        }

        public List<PatientModel> GetPatientsByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return GetPatients();
            return new PatientGateway().GetPatientsByName(name);
        }

        internal List<ReportModel> GetReport(DateTime startDate, DateTime endDate)
        {
            return new PatientGateway().GetReport(startDate, endDate);
        }

        public PatientModel GetPatientsByContact(string contact)
        {
            PatientModel patient = new PatientGateway().GetPatientsByContact(contact);
            if (patient != null)
            {
                List<TestModel> tests = new TestManager().GetTestsByPatient(patient.ID);
                return new PatientModel(patient.ID, patient.Name, patient.Contact, patient.DateOfBirth,
                                        patient.BillNo, patient.PaymentStatus, patient.TestDate, tests);
            }
            return null;
        }

        public PatientModel GetPatientsByPatientID(long patientId)
        {
            PatientModel patient = new PatientGateway().GetPatientsByPatientID(patientId);
            if (patient != null)
            {
                List<TestModel> tests = new TestManager().GetTestsByPatient(patientId);
                return new PatientModel(patient.ID, patient.Name, patient.Contact, patient.DateOfBirth,
                                        patient.BillNo, patient.PaymentStatus, patient.TestDate, tests);
            }
            return null;
        }

        public DataSet GetPatientTestDetailsByPatientID(long patientId)
        {
            return new PatientGateway().GetPatientTestDetailsByPatientID(patientId);
        }

        public PatientModel GetPatientsByBillNo(string billNo)
        {
            PatientModel patient = new PatientGateway().GetPatientsByBillNo(billNo);
            if (patient != null)
            {
                List<TestModel> tests = new TestManager().GetTestsByPatient(patient.ID);
                return new PatientModel(patient.ID, patient.Name, patient.Contact, patient.DateOfBirth,
                                        patient.BillNo, patient.PaymentStatus, patient.TestDate, tests);
            }
            return null;
        }
    }
}