using System;
using System.Globalization;
using System.Web;
using MyHealthLib;

namespace HealthWebApp
{
    public class Api : IHttpHandler
    {
        static double ParseNumber(string s)
        {
            if (s == null) return double.NaN;
            s = s.Trim().Replace(',', '.');
            double v;
            // Dùng InvariantCulture để chấp nhận dấu chấm
            if (double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v))
                return v;
            return double.NaN;
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "application/json";

            try
            {
                string sh = context.Request["h"];
                string sw = context.Request["w"];

                double h = ParseNumber(sh);
                double w = ParseNumber(sw);
                if (double.IsNaN(h) || double.IsNaN(w) || h <= 0 || w <= 0)
                {
                    context.Response.StatusCode = 400;
                    context.Response.Write("{\"ok\":false,\"msg\":\"Nhập số hợp lệ (>0).\"}");
                    return;
                }

                HealthChecker hc = new HealthChecker();
                hc.Signature = "Web API by Dinhdat2k4";
                hc.HeightCm = h;
                hc.WeightKg = w;

                int code = hc.Process();
                if (code < 0)
                {
                    string err = (hc.LastError == null ? "Lỗi" : hc.LastError)
                        .Replace("\\", "\\\\").Replace("\"", "\\\"");
                    context.Response.StatusCode = 500;
                    context.Response.Write("{\"ok\":false,\"msg\":\"" + err + "\"}");
                    return;
                }

                string report = (hc.Report == null ? "" : hc.Report)
                    .Replace("\\", "\\\\").Replace("\"", "\\\"")
                    .Replace("\r", "").Replace("\n", "\\n");

                context.Response.Write("{\"ok\":true,\"report\":\"" + report + "\"}");
            }
            catch (Exception ex)
            {
                string msg = (ex.Message == null ? "Lỗi" : ex.Message)
                    .Replace("\\", "\\\\").Replace("\"", "\\\"");
                context.Response.StatusCode = 500;
                context.Response.Write("{\"ok\":false,\"msg\":\"" + msg + "\"}");
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}