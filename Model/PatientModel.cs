using System;
using System.Collections.Generic;

namespace DiagnosticCenterBillMgtWebApp.Model
{
    [Serializable]
    public class PatientModel
    {
        public long ID { get; private set; }
        public string Name { get; private set; }
        public DateTime? DateOfBirth { get; private set; }
        public string Contact { get; private set; }
        public int PaymentStatus { get; private set; }
        public DateTime? TestDate { get; private set; }
        public string BillNo { get; private set; }

        public List<TestModel> Tests { get; private set; }

        public PatientModel(long id, string name, string contact,
                            DateTime? dateOfBirth = null,
                            string billNo = null,
                            int paymentStatus = 0,
                            DateTime? testDate = null)
        {
            ID = id;
            Name = name;
            Contact = contact;
            DateOfBirth = dateOfBirth;
            PaymentStatus = paymentStatus;
            TestDate = testDate;
            BillNo = billNo;
        }

        public PatientModel(long id, string name, string contact,
                            DateTime? dateOfBirth = null,
                            string billNo = null,
                            int paymentStatus = 0,
                            DateTime? testDate = null,
                            List<TestModel> tests = null)
            : this(id, name, contact, dateOfBirth, billNo, paymentStatus, testDate)
        {
            Tests = tests;
        }

        /// <summary>
        /// Constructor to initialize PatientModel to add
        /// patient test info in database.
        /// </summary>
        /// <param name="name">String, Patient name.</param>
        /// <param name="contact">String, Patient contact/mobile no.</param>
        /// <param name="dateOfBirth">DateTime, Patient date of birth.</param>
        /// <param name="tests">List<TestModel>, List of tests selected.</param>
        public PatientModel(string name, string contact, DateTime? dateOfBirth = null,
                            List<TestModel> tests = null)
            : this(0, name, contact, dateOfBirth, null, 0, null, tests)
        { }
    }
}