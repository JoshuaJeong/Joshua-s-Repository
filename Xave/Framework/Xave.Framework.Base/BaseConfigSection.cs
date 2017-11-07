using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Xave.Framework.Base
{
    public class BaseConfigSection : ConfigurationSection
    {
        /// <summary>
        /// TAB_CHAR == \t
        /// </summary>
        private const char TAB_CHAR = '\t';

        #region Constructors

        /// <summary>
        /// 기본 생성자입니다.
        /// </summary>
        public BaseConfigSection()
        {
        }

        #endregion

        #region METHOD: Save & Load xml

        /// <summary>
        /// 환경 설정 정보를 Xml 파일로 작성합니다.
        /// </summary>
        /// <param name="writer">XmlTextWriter 객체</param>
        /// <param name="rootElementName">
        /// 환경 정보를 감싸는 Xml요소입니다.
        /// <c>rootElementName</c>이 null이거나 빈 문자열이라면 클래스이름을 요소 이름으로 사용합니다.
        /// </param>
        public void SaveXml(XmlTextWriter writer, string rootElementName)
        {
            if (writer == null)
                throw new ArgumentNullException(
                    System.Reflection.MethodInfo.GetCurrentMethod().GetParameters()[0].Name);

            writer.Formatting = Formatting.Indented;
            writer.Indentation = 1;
            writer.IndentChar = TAB_CHAR;

            if (writer.WriteState == WriteState.Start)
                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\""); // Xml, version="1.0" encoding="utf-8"
            base.SerializeToXmlElement(writer, string.IsNullOrEmpty(rootElementName) ? this.GetType().Name : rootElementName);
        }

        /// <summary>
        /// 미구현
        /// </summary>
        /// <param name="reader">XmlTextReader 객체</param>
        /// <param name="rootElementName">
        /// 환경 정보를 감싸는 Xml요소입니다.
        /// </param>
        public void LoadXml(XmlTextReader reader, string rootElementName)
        {
            if (reader == null)
                throw new ArgumentNullException(
                    System.Reflection.MethodInfo.GetCurrentMethod().GetParameters()[0].Name);

            while (reader.MoveToElement())
            {
                if (string.Compare(reader.Name, rootElementName, true, System.Globalization.CultureInfo.InvariantCulture) == 0)
                {
                    //base.DeserializeSection(reader);
                    base.DeserializeElement(reader, false);
                    break;
                }
            }
        }

        #endregion
    }
}
