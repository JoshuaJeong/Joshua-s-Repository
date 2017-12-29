using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 환자교육내역
    /// </summary>    
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    internal class EducationCheckObject : ModelBase
    {
        /// <summary>
        /// 교육일자
        /// </summary>        
        [DataMember]
        internal string Date { get; set; }
        /// <summary>
        /// 교육명
        /// </summary>        
        [DataMember]
        internal string EducationName { get; set; }
        /// <summary>
        /// 의견 및 제언
        /// </summary>        
        [DataMember]
        internal string Text { get; set; }
        ///// <summary>
        ///// 교육종류
        ///// </summary>        
        //[DataMember]
        //internal EducationTypes EducationType { get; set; }
        ///// <summary>
        ///// 교육대상자
        ///// </summary>
        //[DataMember]
        //internal EducationSubjectTypes EducationSubject { get; set; }

        //internal EducationCheckObject()
        //{
        //    this.EducationType = EducationTypes.Individual_Education;
        //    this.EducationSubject = EducationSubjectTypes.Patient;
        //}
    }    

    ///// <summary>
    ///// 교육종류 유형
    ///// </summary>    
    //internal enum EducationTypes
    //{
    //    /// <summary>
    //    /// 집단교육
    //    /// </summary>
    //    [Description("집단교육")]
    //    Group_Education,
    //    /// <summary>
    //    /// 개별교육
    //    /// </summary>
    //    [Description("개별교육")]
    //    Individual_Education
    //}

    ///// <summary>
    ///// 교육대상 유형
    ///// </summary>    
    //internal enum EducationSubjectTypes
    //{
    //    /// <summary>
    //    /// 환자
    //    /// </summary>
    //    [Description("환자")]
    //    Patient,
    //    /// <summary>
    //    /// 배우자
    //    /// </summary>
    //    [Description("배우자")]
    //    Spouse,
    //    /// <summary>
    //    /// 자녀
    //    /// </summary>
    //    [Description("자녀")]
    //    Children,
    //    /// <summary>
    //    /// 며느리
    //    /// </summary>
    //    [Description("며느리")]
    //    Daughter_in_law,
    //    /// <summary>
    //    /// 기타
    //    /// </summary>
    //    [Description("기타")]
    //    Others
    //}
}
