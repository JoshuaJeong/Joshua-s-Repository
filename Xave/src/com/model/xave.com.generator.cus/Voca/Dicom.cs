using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace xave.com.generator.cus.Voca
{
    [Serializable]
    public class DicomCode
    {
        public const string Findings = "121070";
        public const string Dicom_Object_Catalog = "121181";
    }

    [Serializable]
    public class DicomDisplayName 
    {
        public const string Findings = "Findings";
        public const string Dicom_Object_Catalog = "DICOM Object Catalog";
    }
}
