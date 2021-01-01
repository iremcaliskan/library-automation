using System.Web;

namespace LibOtomasyonu.HelperClasses
{
    public class KullaniciIpAdres
    {
        public static string KullaniciIpBul() 
        {
            var ipAdress = string.Empty;
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                ipAdress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            else if (HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null && HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].Length !=0)
            {
                ipAdress = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"];
            }
            else if (HttpContext.Current.Request.UserHostAddress.Length !=0)
            {
                ipAdress = HttpContext.Current.Request.UserHostName;
            }
            return ipAdress;        
        }
    }
}