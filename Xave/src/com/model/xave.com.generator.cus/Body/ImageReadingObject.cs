using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace xave.com.generator.cus
{
    /// <summary>
    /// 영상 판독정보
    /// </summary>
    [DataContract]
    [System.SerializableAttribute()]
    [System.ServiceModel.XmlSerializerFormat]
    public class ImageReadingObject : ModelBase
    {
        private string performDate;
        private string readingDate;
        private string doctorName;
        private string conclusion;
        private string imageURL;
        private string testCode;
        private string testName;
        private string studyUID;
        private string seriesUID;
        private string sopUID;

        /// <summary>
        /// 영상 촬영일자
        /// </summary>
        [DataMember]
        public virtual string PerformDate
        {
            get { return performDate; }
            set { if (performDate != value) { performDate = value; OnPropertyChanged("PerformDate"); } }
        }

        public virtual string PerformDateValue
        {
            get { return string.Format("영상 촬영일 : {0}", ConvertToDateFormat(performDate)); }
            set { performDate = value; }
        }

        public string GetPerformDate() { return PerformDate; }
        public void SetPerformDate(string _PerformDate) { PerformDate = _PerformDate; }

        /// <summary>
        /// 판독일자
        /// </summary>
        [DataMember]
        public virtual string ReadingDate
        {
            get { return readingDate; }
            set { if (readingDate != value) { readingDate = value; OnPropertyChanged("ReadingDate"); } }
        }

        public virtual string ReadingDateValue
        {
            get { return string.Format("최종 판독일 : {0}", ConvertToDateFormat(readingDate)); }
            set { if (readingDate != value) { readingDate = value; } }
        }

        public string GetReadingDate() { return ReadingDate; }
        public void SetReadingDate(string _ReadingDate) { ReadingDate = _ReadingDate; }

        /// <summary>
        /// 판독의 성명
        /// </summary>
        [DataMember]
        public virtual string DoctorName
        {
            get { return doctorName; }
            set { if (doctorName != value) { doctorName = value; OnPropertyChanged("DoctorName"); } }
        }

        public virtual string DoctorNameValue
        {
            get { return string.Format("판독의 : {0}", ConvertToDateFormat(doctorName)); }
            set { if (doctorName != value) { doctorName = value; } }
        }

        public string GetDoctorName() { return DoctorName; }
        public void SetDoctorName(string _DoctorName) { DoctorName = _DoctorName; }

        /// <summary>
        /// 최종 판독결과
        /// </summary>
        [DataMember]
        public virtual string Conclusion
        {
            get { return conclusion; }
            set { if (conclusion != value) { conclusion = value; OnPropertyChanged("Conclusion"); } }
        }

        public virtual string ConclusionValue
        {
            get { return string.Format("판독결과 : {0}", conclusion); }
            set { if (conclusion != value) { conclusion = value; } }
        }

        public string GetConclusion() { return Conclusion; }
        public void SetConclusion(string _Conclusion) { Conclusion = _Conclusion; }

        /// <summary>
        /// 이미지 URL
        /// </summary>
        [DataMember]
        public virtual string ImageURL
        {
            get { return imageURL; }
            set { if (imageURL != value) { imageURL = value; OnPropertyChanged("ImageURL"); } }
        }

        public virtual string ImageURLValue
        {
            get { return string.Format("이미지 URL : {0}", imageURL); }
            set { if (imageURL != value) { imageURL = value; OnPropertyChanged("ImageURL"); } }
        }

        public string GetImageURL() { return ImageURL; }
        public void SetImageURL(string _ImageURL) { ImageURL = _ImageURL; }

        /// <summary>
        /// 영상촬영명 Code (CPT-4)
        /// </summary>
        [DataMember]
        public virtual string TestCode
        {
            get { return testCode; }
            set { if (testCode != value) { testCode = value; OnPropertyChanged("TestCode"); } }
        }

        public virtual string TestCodeValue
        {
            get { return string.Format("검사코드 : {0}", testCode); }
            set { if (testCode != value) { testCode = value; OnPropertyChanged("TestCode"); } }
        }

        public string GetTestCode() { return TestCode; }
        public void SetTestCode(string _TestCode) { TestCode = _TestCode; }

        /// <summary>
        /// 영상촬영명 Code 명칭
        /// </summary>
        [DataMember]
        public virtual string TestName
        {
            get { return testName; }
            set { if (testName != value) { testName = value; OnPropertyChanged("TestName"); } }
        }

        public virtual string TestNameValue
        {
            get { return string.Format("검사명 : {0}", testName); }
            set { if (testName != value) { testName = value; } }
        }

        public string GetTestName() { return TestName; }
        public void SetTestName(string _TestName) { TestName = _TestName; }

        /// <summary>
        /// Study Instance UID
        /// DICOM Tag : (0020, 000D)
        /// </summary>
        [DataMember]
        public virtual string StudyUID
        {
            get { return studyUID; ; }
            set { if (studyUID != value) { studyUID = value; OnPropertyChanged("StudyUID"); } }
        }

        public virtual string StudyUIDValue
        {
            get { return string.Format("Study Instance UID : {0}", studyUID); }
            set { if (studyUID != value) { studyUID = value; OnPropertyChanged("StudyUID"); } }
        }

        public string GetStudyUID() { return StudyUID; }
        public void SetStudyUID(string _StudyUID) { StudyUID = _StudyUID; }

        /// <summary>
        /// Series Instance UID 
        /// DICOM Tag : (0020, 000E)
        /// </summary>
        [DataMember]
        public virtual string SeriesUID
        {
            get { return seriesUID; }
            set { if (seriesUID != value) { seriesUID = value; OnPropertyChanged("SeriesUID"); } }
        }

        public virtual string SeriesUIDValue
        {
            get { return string.Format("Series Instance UID : {0}", seriesUID); }
            set { if (seriesUID != value) { seriesUID = value; OnPropertyChanged("SeriesUID"); } }
        }

        public string GetSeriesUID() { return SeriesUID; }
        public void SetSeriesUID(string _SeriesUID) { SeriesUID = _SeriesUID; }

        /// <summary>
        /// SOP Instance UID
        /// DICOM Tag : (0008,0018)
        /// </summary>
        [DataMember]
        public virtual string SopUID
        {
            get { return sopUID; }
            set { if (sopUID != value) { sopUID = value; OnPropertyChanged("SopUID"); } }
        }

        public virtual string SopUIDValue
        {
            get { return string.Format("SOP Instance UID : {0}", sopUID); }
            set { if (sopUID != value) { sopUID = value; OnPropertyChanged("SopUID"); } }
        }

        public string GetSopUID() { return SopUID; }
        public void SetSopUID(string _SopUID) { SopUID = _SopUID; }

        #region :: Method
        private string ConvertToDateFormat(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                string obj = System.Text.RegularExpressions.Regex.Replace(value.Trim(), @"[^0-9]", "");
                System.Globalization.CultureInfo info = System.Globalization.CultureInfo.CurrentCulture;
                DateTime temp = new DateTime();

                if (DateTime.TryParseExact(obj, "yyyyMMdd", info, System.Globalization.DateTimeStyles.None, out temp))
                {
                    string rtn = temp.ToString("yyyy-MM-dd");
                    return rtn;
                }
                else if (DateTime.TryParseExact(obj, "yyyyMMddHHmm", info, System.Globalization.DateTimeStyles.None, out temp))
                {
                    string rtn = temp.ToString("yyyy-MM-dd HH:mm");
                    return rtn;
                }
                else if (DateTime.TryParseExact(obj, "yyyyMMddHHmmss", info, System.Globalization.DateTimeStyles.None, out temp))
                {
                    string rtn = temp.ToString("yyyy-MM-dd HH:mm:ss");
                    return rtn;
                }
                else
                {
                    return value;
                }
            }
            else return string.Empty;
        }
        #endregion
    }
}
