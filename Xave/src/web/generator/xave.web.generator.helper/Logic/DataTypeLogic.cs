using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using xave.com.generator.cus;

namespace xave.web.generator.helper.Logic
{
    /// <summary>
    /// HL7 V3 Type 생성 클래스
    /// </summary>
    [Serializable]
    public abstract class DataTypeLogic
    {
        #region :  Member
        #endregion

        #region :  Properties
        #endregion

        #region :  Methods

        #region :  II Type
        /// <summary>
        /// Id Element를 생성
        /// </summary>
        /// <returns>II( root = GUID)</returns>
        internal static II GetII()
        {
            return new II() { root = Guid.NewGuid().ToString().ToUpper() };
        }

        /// <summary>
        /// Id Element를 생성
        /// </summary>
        /// <param name="extensionValue">extension</param>
        /// <param name="rootValue">root</param>
        /// <returns>II</returns>
        internal II GetII(string extensionValue, string rootValue)
        {
            return new II() { root = !string.IsNullOrEmpty(rootValue) ? rootValue : Guid.NewGuid().ToString().ToUpper(), extension = string.IsNullOrEmpty(extensionValue) ? null : extensionValue };
        }

        /// <summary>
        /// ID Element 생성( root = GUID )
        /// </summary>
        /// <returns></returns>
        internal II[] GetIIArray()
        {
            return new II[] { GetII() };
        }

        /// <summary>
        /// ID Element 생성
        /// </summary>
        /// <param name="extensionValue"></param>
        /// <param name="rootValue"></param>
        /// <returns>II Array</returns>
        internal II[] GetIIArray(string extension, string rootValue)
        {
            return string.IsNullOrEmpty(extension) && string.IsNullOrEmpty(rootValue) ? null : new II[] { GetII(extension, rootValue) };
        }
        #endregion

        #region :  II Type(template ID)
        internal static II[] GetTemplateId(string templateId)
        {
            return GetTemplateId(templateId, null);
        }
        //11.27 추가
        internal static II[] GetTemplateId(string templateId, string extensionValue)
        {
            //return new II[] { new II() { root = templateId, extension = extensionValue } };
            return new II[] { new II() { root = string.IsNullOrEmpty(templateId) ? null : templateId, extension = string.IsNullOrEmpty(extensionValue) ? null : extensionValue } };
        }
        internal II[] GetTemplateId(List<DocumentInformationObject.IdObject> list)
        {
            return list != null && list.Any() ? list.Select(s => new II() { root = s.root, extension = s.extension }).ToArray() : null;

        }

        #endregion

        #region :  Code Type
        /// <summary>
        /// Code Element 생성(CE Type)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="displayName"></param>
        /// <param name="codeSystemName"></param>
        /// <param name="codeSystem"></param>
        /// <returns>CE</returns>
        internal static CE GetCE(string code, string displayName, string codeSystemName, string codeSystem)
        {
            codeSystemName = codeSystemName == string.Empty ? null : codeSystemName;
            displayName = displayName == string.Empty ? null : displayName;
            return GetCE(code, displayName, codeSystemName, codeSystem, null);
        }
        internal static CE GetCE(string codeValue, string displayNameValue, string codeSystemNameValue, string codeSystemValue, string valueSetOIDValue)
        {
            if (string.IsNullOrEmpty(codeValue) && string.IsNullOrEmpty(displayNameValue))
                return new CE() { nullFlavor = "NI" };
            else if (string.IsNullOrEmpty(codeValue))
                return new CE() { nullFlavor = "NI", displayName = displayNameValue, codeSystemName = codeSystemNameValue, codeSystem = codeSystemValue, valueSet = valueSetOIDValue };
            else
                return new CE() { code = codeValue, displayName = displayNameValue, codeSystemName = codeSystemNameValue, codeSystem = codeSystemValue, valueSet = valueSetOIDValue };

        }

        /// <summary>
        /// Code Element 생성(CD Type)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="displayName"></param>
        /// <param name="codeSystemName"></param>
        /// <param name="codeSystem"></param>
        /// <returns>CD</returns>
        internal static CD GetCD(string code, string displayName, string codeSystemName, string codeSystem)
        {
            codeSystemName = codeSystemName == string.Empty ? null : codeSystemName;
            displayName = displayName == string.Empty ? null : displayName;
            return GetCD(code, displayName, codeSystemName, codeSystem, null);
        }
        internal static CD GetCD(string codeValue, string displayNameValue, string codeSystemNameValue, string codeSystemValue, string valueSetOIDValue)
        {
            if (string.IsNullOrEmpty(codeValue) && string.IsNullOrEmpty(displayNameValue))
                return new CD() { nullFlavor = "NI" };
            else if (string.IsNullOrEmpty(codeValue))
                return new CD() { nullFlavor = "NI", displayName = displayNameValue, codeSystemName = codeSystemNameValue, codeSystem = codeSystemValue, valueSet = valueSetOIDValue };
            else
                return new CD() { code = codeValue, displayName = displayNameValue, codeSystemName = codeSystemNameValue, codeSystem = codeSystemValue, valueSet = valueSetOIDValue };
        }


        /// <summary>
        /// Code Element 생성(CS Type)
        /// </summary>
        /// <param name="code"></param>
        /// <param name="codeSystem"></param>
        /// <param name="displayName"></param>
        /// <param name="codeSystemName"></param>
        /// <returns>CS</returns>
        internal CS GetCS(string code, string codeSystem, string displayName, string codeSystemName)
        {
            return new CS() { code = code, displayName = displayName, codeSystem = codeSystem, codeSystemName = codeSystemName };
        }
        internal CS[] GetCSArray(string code, string displayName = null, string codeSystem = null, string codeSystemName = null)
        {
            return new CS[] { GetCS(code, codeSystem, displayName, codeSystem) };
        }
        #endregion

        #region :  ST Type
        /// <summary>
        /// Title Element 생성(ST Type)
        /// </summary>
        /// <param name="param">title</param>
        /// <returns>ST</returns>
        internal static ST GetST(string param)
        {
            return !string.IsNullOrEmpty(param) ? new ST() { Text = new string[] { param } } : null;
        }
        #endregion

        #region :  TS Type
        /// <summary>
        /// effectiveTime Element 생성
        /// </summary>
        /// <param name="param">time value</param>
        /// <returns>TS</returns>
        internal TS GetTS(string param)
        {
            return !string.IsNullOrEmpty(param) ? new TS() { value = param } : null;
        }
        #endregion

        #region :  AD Type
        internal AD GetAD(string country, string city, string state, string additional, string street, string postal)
        {
            AD address = null;
            //국가
            adxpcountry countryAddress = string.IsNullOrEmpty(country) ? null : new adxpcountry() { Text = new string[] { country } };
            //주, 도
            adxpstate stateAddress = string.IsNullOrEmpty(state) ? null : new adxpstate() { Text = new string[] { state } };
            //도시명
            adxpcity cityAddress = string.IsNullOrEmpty(city) ? null : new adxpcity() { Text = new string[] { city } };
            //세부주소
            adxpstreetAddressLine streetAddress = string.IsNullOrEmpty(street) ? null : new adxpstreetAddressLine() { Text = new string[] { street } };
            //기본주소
            adxpadditionalLocator additionalLocator = string.IsNullOrEmpty(additional) ? null : new adxpadditionalLocator() { Text = new string[] { additional } };
            //우편번호
            adxppostalCode postalCode = string.IsNullOrEmpty(postal) ? null : new adxppostalCode() { Text = new string[] { postal } };

            var adxp = new ADXP[] { countryAddress, stateAddress, cityAddress, additionalLocator, streetAddress, postalCode };

            address = new AD() { Items = adxp.Where(p => p != null && !string.IsNullOrEmpty(p.Text[0])).ToArray() };
            return address;
        }

        internal AD[] GetADArray(string country, string city, string state, string additional, string street, string postal)
        {
            return new AD[] { GetAD(country, city, state, additional, street, postal) };
        }
        #endregion

        #region :  PN Type
        internal PN GetPN(string nameVal)
        {
            if (!string.IsNullOrEmpty(nameVal))
            {
                enfamily familyName = null;
                engiven givenName = null;
                ENXP[] nameArrange = null;

                if (!string.IsNullOrEmpty(nameVal))
                {
                    if (char.GetUnicodeCategory(nameVal, 0) == System.Globalization.UnicodeCategory.OtherLetter)
                    {
                        familyName = new enfamily() { Text = new string[] { nameVal[0].ToString() } };
                        givenName = new engiven() { Text = new string[] { nameVal.Substring(1) } };
                    }
                    else
                    {
                        givenName = new engiven() { Text = new string[] { nameVal.Split(' ')[0].ToString() } };
                        familyName = new enfamily() { Text = new string[] { nameVal.Substring(givenName.Text[0].Length).Trim() } };
                    }
                }
                nameArrange = new ENXP[] { givenName, familyName };
                PN returnObj = new PN() { Items = nameArrange.Where(p => !string.IsNullOrEmpty(p.Text[0])).ToArray() };

                return returnObj;
            }
            else return null;
        }
        internal PN[] GetPNArray(List<string> nameList)
        {
            return nameList != null && nameList.Any() ? nameList.Select(s => GetPN(s)).ToArray() : null;
        }
        #endregion

        #region :  TEL Type
        /// <summary>
        /// TEL Element 생성
        /// </summary>
        /// <param name="param">value(string)</param>
        /// <returns>TEL</returns>
        internal TEL GetTEL(string param)
        {
            return !string.IsNullOrEmpty(param) ? new TEL() { value = string.Format("tel:{0}", param) } : null;
        }
        /// <summary>
        /// Telecom Element 생성(array)
        /// </summary>
        /// <param name="telecomValue">telecom</param>
        /// <returns>TEL array</returns>
        internal TEL[] GetTELArray(List<string> list)
        {
            return list != null && list.Any() ? list.Select(s => GetTEL(s)).ToArray() : null;
        }
        #endregion

        #region :  ON Type
        /// <summary>
        /// On Element 생성
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>ON</returns>
        internal ON GetON(string name)
        {
            return !string.IsNullOrEmpty(name) ? new ON() { Text = new string[] { name } } : null;
        }
        /// <summary>
        /// ON Type 정의 (Array)
        /// </summary>
        /// <param name="nameList"></param>
        /// <returns></returns>
        internal ON[] GetONArray(List<string> nameList)
        {
            return nameList != null && nameList.Any() ? nameList.Select(s => GetON(s)).ToArray() : null;
        }
        #endregion

        #region :  IVL_TS Type
        /// <summary>
        /// effectiveTime(Data type : IVL_TS)의 값을 설정합니다
        /// </summary>
        /// <param name="timeValue">Time value</param>
        /// <returns>effectiveTime(IVL_TS)</returns>
        protected IVL_TS GetIVL_TS(string timeValue)
        {
            return !string.IsNullOrEmpty(timeValue) ? new IVL_TS() { value = timeValue } : new IVL_TS() { nullFlavor = "NI" };
        }

        /// <summary>
        /// effectiveTime(Data type : IVL_TS)의 값을 설정합니다
        /// </summary>
        /// <param name="lowTime">최초 시간</param>
        /// <param name="highTime">완료 시간</param>
        /// <returns>effecitiveTime(IVL_TS)</returns>
        protected IVL_TS GetIVL_TS(string lowTime, string highTime)
        {
            lowTime = string.IsNullOrEmpty(lowTime) ? null : lowTime;
            highTime = string.IsNullOrEmpty(highTime) ? null : highTime;

            if (string.IsNullOrEmpty(lowTime) && string.IsNullOrEmpty(highTime))
                return new IVL_TS() { nullFlavor = "UNK" };
            else if (!string.IsNullOrEmpty(lowTime) && !string.IsNullOrEmpty(highTime))
            {
                ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[2] { ItemsChoiceType2.low, ItemsChoiceType2.high };
                IVXB_TS lowEffectiveTime = new IVXB_TS() { value = lowTime };
                IVXB_TS highEffectiveTime = new IVXB_TS() { value = highTime };
                QTY[] qtys = new QTY[2] { lowEffectiveTime, highEffectiveTime };

                return new IVL_TS() { ItemsElementName = itemChoice, Items = qtys };
            }
            else if (!string.IsNullOrEmpty(lowTime) && string.IsNullOrEmpty(highTime))
            {
                ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[1] { ItemsChoiceType2.low };
                IVXB_TS lowEffectiveTime = new IVXB_TS() { value = lowTime };
                QTY[] qtys = new QTY[1] { lowEffectiveTime };
                return new IVL_TS() { ItemsElementName = itemChoice, Items = qtys };
            }
            else
            {
                ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[1] { ItemsChoiceType2.high };
                IVXB_TS highEffectiveTime = new IVXB_TS() { value = highTime };
                QTY[] qtys = new QTY[1] { highEffectiveTime };
                return new IVL_TS() { ItemsElementName = itemChoice, Items = qtys };
            }
        }
        #endregion

        #region :  IVL_PQ Type
        /// <summary>
        /// doseQuantity의 값을 설정합니다
        /// </summary>
        /// <param name="dosequantity">doseQuantity value</param>
        /// <returns> doseQuantity</returns>
        protected IVL_PQ GetDoseQuantity(string dosequantity)
        {
            if (!string.IsNullOrEmpty(dosequantity))
            {
                PQ[] pqArray = new PQ[] { new PQ() { value = dosequantity } };
                return new IVL_PQ() { ItemsElementName = new ItemsChoiceType[1] { ItemsChoiceType.center }, Items = pqArray };
            }
            else return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lowValue">최소값</param>
        /// <param name="highValue">최대값</param>
        /// <param name="unitValue">단위</param>
        /// <returns></returns>
        protected IVL_PQ GetIVL_PQ(string lowValue, string highValue, string unitValue)
        {
            if (string.IsNullOrEmpty(lowValue) && string.IsNullOrEmpty(highValue))
                return new IVL_PQ() { nullFlavor = "UNK" };
            else if (!string.IsNullOrEmpty(lowValue) && !string.IsNullOrEmpty(highValue))
            {
                ItemsChoiceType[] itemChoice = new ItemsChoiceType[2] { ItemsChoiceType.low, ItemsChoiceType.high };
                IVXB_PQ lowRate = new IVXB_PQ() { value = lowValue, unit = unitValue };
                IVXB_PQ highRate = new IVXB_PQ() { value = highValue, unit = unitValue };
                PQ[] pqs = new PQ[2] { lowRate, highRate };
                return new IVL_PQ() { ItemsElementName = itemChoice, Items = pqs };
            }
            else if (!string.IsNullOrEmpty(lowValue) && string.IsNullOrEmpty(highValue))
            {
                ItemsChoiceType[] itemChoice = new ItemsChoiceType[1] { ItemsChoiceType.low };
                IVXB_PQ lowRate = new IVXB_PQ() { value = lowValue, unit = unitValue };
                PQ[] pqs = new PQ[1] { lowRate };
                return new IVL_PQ() { ItemsElementName = itemChoice, Items = pqs };
            }
            else
            {
                ItemsChoiceType[] itemChoice = new ItemsChoiceType[1] { ItemsChoiceType.high };
                IVXB_PQ highRate = new IVXB_PQ() { value = lowValue, unit = unitValue };
                PQ[] pqs = new PQ[1] { highRate };
                return new IVL_PQ() { ItemsElementName = itemChoice, Items = pqs };
            }
        }
        #endregion

        #region :  SXCM_TS Type
        /// <summary>
        /// effectiveTime(Medication)의 값을 설정합니다
        /// </summary>
        /// <param name="lowValue">최초시간</param>
        /// <param name="highValue">종료(최종/최근)시간</param>
        /// <param name="timeOfPeriod">투여기간</param>
        /// <param name="periodUnit">투여기간 단위(d : day, h : hour, m : minute)</param>
        /// <returns>effectiveTime(Array)</returns>
        internal SXCM_TS[] GetSXCM_TS(string lowValue, string highValue, string timeOfPeriod, string periodUnit)
        {
            switch (timeOfPeriod)
            {
                case null:
                    SXCM_TS[] sxcm = new SXCM_TS[1];
                    if (string.IsNullOrEmpty(lowValue) && string.IsNullOrEmpty(highValue))
                    {
                        IVL_TS effective = new IVL_TS() { nullFlavor = "UNK" };
                        sxcm[0] = effective;
                    }
                    else if (!string.IsNullOrEmpty(lowValue) && !string.IsNullOrEmpty(highValue))
                    {
                        ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[2] { ItemsChoiceType2.low, ItemsChoiceType2.high };

                        IVXB_TS lowEffectiveTime = new IVXB_TS() { value = lowValue };
                        IVXB_TS highEffectiveTime = new IVXB_TS() { value = highValue };
                        QTY[] qtys = new QTY[2] { lowEffectiveTime, highEffectiveTime };
                        IVL_TS effectiveTime = new IVL_TS() { ItemsElementName = itemChoice, Items = qtys, @operator = SetOperator.A };

                        sxcm[0] = effectiveTime;
                    }
                    else if (!string.IsNullOrEmpty(lowValue) && string.IsNullOrEmpty(highValue))
                    {
                        ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[1] { ItemsChoiceType2.low };
                        IVXB_TS lowEffectiveTime = new IVXB_TS() { value = lowValue };
                        QTY[] qtys = new QTY[1] { lowEffectiveTime };

                        IVL_TS effectiveTime = new IVL_TS() { ItemsElementName = itemChoice, Items = qtys, @operator = SetOperator.A };

                        sxcm[0] = effectiveTime;
                    }
                    else
                    {
                        ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[1] { ItemsChoiceType2.high };
                        IVXB_TS highEffectiveTime = new IVXB_TS() { value = highValue };
                        QTY[] qtys = new QTY[1] { highEffectiveTime };
                        IVL_TS effectiveTime = new IVL_TS() { ItemsElementName = itemChoice, Items = qtys, @operator = SetOperator.A };

                        sxcm[0] = effectiveTime;
                    }
                    return sxcm;

                default:
                    SXCM_TS[] sxcms = new SXCM_TS[2];
                    {
                        ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[2] { ItemsChoiceType2.low, ItemsChoiceType2.high };
                        IVXB_TS lowEffectiveTime = !string.IsNullOrEmpty(lowValue) ? new IVXB_TS() { value = lowValue } : new IVXB_TS() { nullFlavor = "UNK" };
                        //IVXB_TS highEffectiveTime = !string.IsNullOrEmpty(highValue) ? new IVXB_TS() { value = highValue } : new IVXB_TS() { nullFlavor = "UNK" };
                        IVXB_TS highEffectiveTime = !string.IsNullOrEmpty(highValue) ? new IVXB_TS() { value = highValue } : null;
                        QTY[] qtys = new QTY[2] { lowEffectiveTime, highEffectiveTime };
                        IVL_TS effectiveTime = new IVL_TS() { ItemsElementName = itemChoice, Items = qtys, /*   @operator = SetOperator.A   */ };
                        PIVL_TS Period = new PIVL_TS()
                        {
                            @operator = SetOperator.A,
                            /*  institutionSpecified1 = true,  */
                            period = new PQ()
                            {
                                value = string.IsNullOrEmpty(timeOfPeriod) ? null : Regex.Replace(timeOfPeriod, @"\D", ""),
                                unit = string.IsNullOrEmpty(periodUnit) ? null : periodUnit
                            }
                        };

                        sxcms[0] = effectiveTime;
                        sxcms[1] = Period;
                    }
                    return sxcms;
            }
        }
        internal SXCM_TS[] GetSXCM_TS(string value, string timeOfPeriod, string periodUnit)
        {
            switch (timeOfPeriod)
            {
                case null:
                    return new SXCM_TS[1] { new IVL_TS() { value = value } };

                default:
                    SXCM_TS[] sxcms = new SXCM_TS[2];
                    ItemsChoiceType2[] itemChoice = new ItemsChoiceType2[2] { ItemsChoiceType2.low, ItemsChoiceType2.high };
                    IVXB_TS et = !string.IsNullOrEmpty(value) ? new IVXB_TS() { value = value } : new IVXB_TS() { nullFlavor = "UNK" };
                    QTY[] qtys = new QTY[1] { et };

                    sxcms[0] = new IVL_TS() { ItemsElementName = itemChoice, Items = qtys, @operator = SetOperator.A };
                    sxcms[1] = new PIVL_TS()
                    {
                        @operator = SetOperator.A,
                        institutionSpecified1 = true,
                        period = new PQ()
                        {
                            value = string.IsNullOrEmpty(timeOfPeriod) ? null : Regex.Replace(timeOfPeriod, @"\D", ""),
                            unit = string.IsNullOrEmpty(periodUnit) ? null : periodUnit
                        }
                    };
                    return sxcms;
            }
        }
        #endregion

        #region :  ANY Type
        /// <summary>
        /// Create Element(ANY Date type)
        /// </summary>
        /// <param name="item">object(all data type)</param>
        /// <returns>ANY[] array</returns>
        protected static ANY[] GetANY(ANY item)
        {
            return item == null ? null : new ANY[1] { item };
        }
        #endregion

        #region :  PQ Type
        /// <summary>
        /// PQ Data Type 값 설정
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="unit">unit</param>
        /// <returns>PQ</returns>
        protected PQ GetPq(string value, string unit)
        {
            return !string.IsNullOrEmpty(value) ? new PQ() { value = value, unit = string.IsNullOrEmpty(unit) ? null : unit } : null;
        }
        #endregion

        #region :  EN Type
        /// <summary>
        /// EN Type 정의
        /// </summary>
        /// <param name="name">name</param>
        /// <returns>EN</returns>
        internal static EN GetEN(string name)
        {
            return !string.IsNullOrEmpty(name) ? new EN() { Text = new string[] { name } } : null;
        }
        #endregion

        #region :  ED Type
        /// <summary>
        /// ED Type 정의
        /// </summary>
        /// <param name="item">TEL Type</param>
        /// <returns>ED</returns>
        protected ED GetED(object item)
        {

            if (item != null)
                return new ED() { reference = item as TEL };
            else return null;
        }
        #endregion

        #endregion
    }
}
