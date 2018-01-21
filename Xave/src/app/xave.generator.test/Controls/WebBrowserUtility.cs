using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Xsl;
using xave.com.helper;

namespace xave.generator.test.Controls
{
    public class WebBrowserUtility
    {
        #region Const Values
        /// <summary>
        /// Property: XSLPath
        /// 설명:     첨부파일(CDA, *.XML)에 대한 Style
        ///           - ONC 제공
        /// </summary>
        private static string CDAXSLPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CDAXSLPath"];
            }
        }

        private static string CDADIRXSLPath
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CDADIRXSLPath"];
            }
        }

        //private string SourceType;
        #endregion

        #region LinkSourceProperty
        public static readonly DependencyProperty LinkSourceProperty =
        DependencyProperty.RegisterAttached("LinkSource", typeof(string[]), typeof(WebBrowserUtility), new UIPropertyMetadata(null, LinkSourcePropertyChanged));

        public static string[] GetLinkSource(DependencyObject obj)
        {
            return (string[])obj.GetValue(LinkSourceProperty);
        }

        public static void SetLinkSource(DependencyObject obj, string[] value)
        {
            obj.SetValue(LinkSourceProperty, value);
        }

        // When link changed navigate to site.
        public static void LinkSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;

            MemoryStream outputStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(outputStream, System.Text.Encoding.UTF8);
            XmlDocument doc = new XmlDocument();
            XslCompiledTransform xslt = new XslCompiledTransform(false);
            XsltSettings set = new XsltSettings() { EnableDocumentFunction = true, EnableScript = true };

            if (browser != null)
            {
                try
                {
                    string[] LinkSource = e.NewValue as string[];

                    if (LinkSource == null)
                    {
                        browser.Source = null;
                        return;
                    }

                    string xsl = LinkSource[0];
                    string content = LinkSource[1];

                    #region XML
                    try
                    {
                        if (!string.IsNullOrEmpty(xsl))
                        {
                            doc.LoadXml(content);
                            xslt.Load(xsl, set, null);
                            xslt.Transform(doc, writer);
                            outputStream.Position = 0;
                            browser.NavigateToStream(outputStream);
                        }
                        else
                        {
                            System.Xml.Linq.XDocument loaded_xml = System.Xml.Linq.XDocument.Parse(content);
                            loaded_xml.Save(outputStream);
                            outputStream.Position = 0;
                            browser.NavigateToStream(outputStream);
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(MessageHandler.GetErrorMessage(ex));
                        browser.Source = null;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(MessageHandler.GetErrorMessage(ex));
                    browser.Source = null;
                }
            }
        }

        public static string StringToBase64Str(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            string returnValue = System.Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static byte[] StringToBase64(string toEncode)
        {
            byte[] toEncodeAsBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(toEncode);
            return toEncodeAsBytes;
        }

        public static string Base64ToString(string encodedData)
        {
            byte[] encodedDataAsBytes = System.Convert.FromBase64String(encodedData);
            string returnValue = System.Text.ASCIIEncoding.ASCII.GetString(encodedDataAsBytes);
            return returnValue;
        }

        #endregion

        #region LinkXML D.P

        public static readonly DependencyProperty LinkXmlProperty =
        DependencyProperty.RegisterAttached("LinkXml", typeof(string[]), typeof(WebBrowserUtility), new UIPropertyMetadata(null, LinkXmlPropertyChanged));

        public static string[] GetLinkXml(DependencyObject obj)
        {
            return (string[])obj.GetValue(LinkXmlProperty);
        }

        public static void SetLinkXml(DependencyObject obj, string[] value)
        {
            obj.SetValue(LinkXmlProperty, value);
        }

        private static void LinkXmlPropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            WebBrowser browser = o as WebBrowser;
            MemoryStream outputStream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(outputStream, System.Text.Encoding.UTF8);
            XmlDocument doc = new XmlDocument();

            if (browser != null)
            {
                try
                {
                    string[] LinkSource = e.NewValue as string[];

                    if (LinkSource == null)
                    {
                        browser.Source = null;
                        return;
                    }

                    string xsl = LinkSource[0];
                    string content = LinkSource[1];

                    #region XML
                    try
                    {
                        doc.LoadXml(content);
                        browser.NavigateToString(doc.InnerXml);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(MessageHandler.GetErrorMessage(ex));
                        browser.Source = null;
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(MessageHandler.GetErrorMessage(ex));
                    browser.Source = null;
                }
            }
        }
        #endregion
    }
}
