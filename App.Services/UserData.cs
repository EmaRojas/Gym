using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;

namespace App.Services
{
    public static class UserData
    {
        public static string UserId
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current.Request.Cookies["gym"] != null)
                {
                    byte[] decryted = Convert.FromBase64String(HttpContext.Current.Request.Cookies["gym"].Value);
                    System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                    string deserialized = System.Text.Encoding.Unicode.GetString(decryted);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(deserialized);
                    result = serializeModel.Id.ToString();
                }
                return result;
            }
        }

        public static string FullName
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current.Request.Cookies["gym"] != null)
                {
                    byte[] decryted = Convert.FromBase64String(HttpContext.Current.Request.Cookies["gym"].Value);
                    System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                    string deserialized = System.Text.Encoding.Unicode.GetString(decryted);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(deserialized);
                    result = serializeModel.FullName;
                }
                return result;
            }
        }

        public static string Email
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current.Request.Cookies["gym"] != null)
                {
                    byte[] decryted = Convert.FromBase64String(HttpContext.Current.Request.Cookies["gym"].Value);
                    System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                    string deserialized = System.Text.Encoding.Unicode.GetString(decryted);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(deserialized);
                    result = serializeModel.Email;
                }
                return result;
            }
        }

        public static string CompanyName
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current.Request.Cookies["gym"] != null)
                {
                    byte[] decryted = Convert.FromBase64String(HttpContext.Current.Request.Cookies["gym"].Value);
                    System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                    string deserialized = System.Text.Encoding.Unicode.GetString(decryted);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(deserialized);
                    result = serializeModel.CompanyName;
                }
                return result;
            }
        }

        public static string ExpirationDate
        {
            get
            {
                string result = string.Empty;
                if (HttpContext.Current.Request.Cookies["gym"] != null)
                {
                    byte[] decryted = Convert.FromBase64String(HttpContext.Current.Request.Cookies["gym"].Value);
                    System.Text.Encoding.Unicode.GetString(decryted, 0, decryted.ToArray().Length);
                    string deserialized = System.Text.Encoding.Unicode.GetString(decryted);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    CustomPrincipalSerializeModel serializeModel = serializer.Deserialize<CustomPrincipalSerializeModel>(deserialized);
                    result = serializeModel.ExpirationDate;
                }
                return result;
            }
        }

    }

}
