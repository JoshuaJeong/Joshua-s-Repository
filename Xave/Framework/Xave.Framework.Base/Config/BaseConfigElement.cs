using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using RSM = Xave.Framework.Base;

namespace Xave.Framework.Base.Config
{
    public abstract class BaseConfigElement : ConfigurationElement
    {
        #region Constants

        /// <summary>
        /// KEY_NAME == name
        /// </summary>
        private const string KEY_NAME = "name";

        #endregion

        #region Attributes & Properties

        /// <summary>
        /// 요소의 이름을 가져오거나 설정합니다.
        /// </summary>
        [ConfigurationProperty(KEY_NAME, IsKey = true)]
        public string Name
        {
            get { return (string)this[KEY_NAME]; }
            set { this[KEY_NAME] = value; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// 기본 생성자입니다.
        /// </summary>
        protected BaseConfigElement()
        {
        }

        /// <summary>
        /// 주어진 <c>name</c>을 키로 하는 요소를 생성합니다.
        /// </summary>
        /// <param name="name">
        /// 키, 키는 <c>BaseConfigElement</c> 개체를 둘러싸는 콜렉션 내부에서 유일해야 합니다.
        /// </param>
        protected BaseConfigElement(string name)
        {
            Name = name;
        }

        #endregion
    }
}
