using System.Text.Json;
using System.Text.Json.Serialization;

namespace IndiaLivingsAPI
{

    public static class clsHelper
    {

        public static string RemoveEmptyProps(object _lstObj)
        {
            string strJson = "";
            var options = new JsonSerializerOptions { WriteIndented = true, DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
            strJson = System.Text.Json.JsonSerializer.Serialize(_lstObj, options);
            if (string.IsNullOrEmpty(strJson) == true)
            {
                strJson = "Json Empty Props Removal Method Failure";
            }
            return strJson;
        }

    }
}
