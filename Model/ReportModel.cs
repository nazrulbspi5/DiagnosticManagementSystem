using System;

namespace DiagnosticCenterBillMgtWebApp.Model
{
    [Serializable]
    public class ReportModel
    {
        public string Name { get; private set; }
        public int TotalCount { get; private set; }
        public decimal TotalAmount { get; private set; }

        public string BillNo { get; private set; }
        public string Contact { get; private set; }

        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public ReportModel(string name, int totalCount, decimal totalAmount, DateTime startDate, DateTime endDate)
        {
            Name = name;
            TotalCount = totalCount;
            TotalAmount = Math.Round(totalAmount, 2);

            SetDates(startDate, endDate);
        }

        public ReportModel(string billNo, string patientName, string contact, decimal billAmount, DateTime startDate, DateTime endDate)
        {
            BillNo = billNo;
            Name = patientName;
            Contact = contact;
            TotalAmount = Math.Round(billAmount, 2);

            SetDates(startDate, endDate);
        }

        private void SetDates(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}