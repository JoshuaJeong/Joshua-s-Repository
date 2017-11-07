using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RSM = Xave.Framework.Base;

namespace Xave.Framework.Base.Config
{
    public class BaseConfigCollection<TConfig> : ConfigurationElementCollection where TConfig : BaseConfigElement, new()
    {
        /// <summary>
        /// 순서 값에 의한 환경 구성 요소를 가져오거나 추가합니다.
        /// </summary>
        /// <param name="index">순서</param>
        /// <returns>환경 순서 요소(<c>TConfig</c> 파생 객체)</returns>
        public ConfigurationElement this[int index]
        {
            get { return BaseGet(index); }
            set
            {
                if (value is TConfig)
                {
                    if (BaseGet(index) != null)
                    {
                        BaseRemoveAt(index);
                    }

                    BaseAdd(index, value);
                }
            }
        }

        /// <summary>
        /// 키 값에 의한 환경 구성 요소를 가져옵니다.
        /// </summary>
        /// <param name="key">키</param>
        /// <returns>환경 순서 요소(<c>TConfig</c> 파생 객체)</returns>
        public new ConfigurationElement this[string key]
        {
            get { return BaseGet(key); }
        }

        /// <summary>
        /// 새 요소를 생성합니다.
        /// </summary>
        /// <returns>환경 순서 요소(<c>TConfig</c> 파생 객체)</returns>
        protected override ConfigurationElement CreateNewElement()
        {
            return new TConfig();
        }

        /// <summary>
        /// 새 요소를 생성합니다.
        /// </summary>
        /// <param name="elementName">새 요소의 이름(키)</param>
        /// <returns>환경 순서 요소(<c>TConfig</c> 파생 객체)</returns>
        protected override ConfigurationElement CreateNewElement(string elementName)
        {
            TConfig t = new TConfig();
            t.Name = elementName;

            return t;
        }

        /// <summary>
        /// 환경 구성 요소의 키값을 반환합니다.
        /// </summary>
        /// <param name="element">환경 구성 요소</param>
        /// <returns>키</returns>
        protected override object GetElementKey(ConfigurationElement element)
        {
            TConfig telem = element as TConfig;
            if (telem != null) return telem.Name;
            else throw new ArgumentException(RSM.GetError("ES_CNF_0001"));
        }
    }
}
