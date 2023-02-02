using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace Studentenbeheer.Controllers
{
    public class HalloIedereenController : Controller
    {
        public string Index()
        {
            return "dis is het startpagina ";
        }
        public string Welkom(string voornaam, string naam)
        {
            return HtmlEncoder.Default.Encode($"Hallo {voornaam} {naam}");
        }
    }
}
