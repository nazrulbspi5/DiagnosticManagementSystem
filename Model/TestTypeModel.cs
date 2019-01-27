using System;
using System.Collections.Generic;

namespace DiagnosticCenterBillMgtWebApp.Model
{
    [Serializable]
    public class TestTypeModel
    {
        public int TypeID { get; private set; }
        public string TypeName { get; private set; }
        public List<TestModel> Tests { get; private set; }

        public TestTypeModel(int typeId, string typeName)
        {
            TypeID = typeId;
            TypeName = typeName;
        }

        public TestTypeModel(int typeId, string typeName, List<TestModel> tests)
            : this(typeId, typeName)
        {
            Tests = tests;
        }
    }
}