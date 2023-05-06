using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Api
{
    public const string Host = "http://192.168.152.115:5000";
    public static string Token { get; set; }

    public static readonly HttpClient Client = new()
    {
        BaseAddress = new Uri(Host)
    };
}