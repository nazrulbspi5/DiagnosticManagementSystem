using System;

namespace DiagnosticCenterBillMgtWebApp.Model
{
    [Serializable]
    public class TestModel : TestTypeModel
    {
        public int TestId { get; private set; }
        public string TestName { get; private set; }
        public decimal TestFee { get; private set; }

        public TestModel(int testId, string testName, decimal testFee,
                        int typeId, string typeName)
            : base(typeId, typeName)
        {
            TestId = testId;
            TestName = testName;
            TestFee = testFee;
        }

        public TestModel(int testId, string testName, decimal testFee)
            : this(testId, testName, testFee, 0, null) { }
    }
}