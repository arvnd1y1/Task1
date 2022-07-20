using Microsoft.AspNetCore.Mvc;
using Prophaze.Models;
using System.Diagnostics;
using System.Text;

namespace Prophaze.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            CipherModel cm=new CipherModel();
            return View(cm);
        }

        [HttpPost]
        public ActionResult Index(string Cipher)
        {
            CipherModel cm = new CipherModel();
            cm.PlainText = decode(Cipher);
            return View();
        }

        public string decode(string cipher)
        {
            string plainText="",text="";
            byte[] bytes = cipher.Split(' ').Select(s => Convert.ToByte(s, 16)).ToArray();
            string hexString = BitConverter.ToString(bytes);
            hexString = hexString.Replace("-", "");
            byte[] temp = new byte[hexString.Length / 2];
            for (int i = 0; i < temp.Length; i++)
            {
                temp[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }
            string ascii = Encoding.ASCII.GetString(temp);
            string[] words = ascii.Split(' ');
            foreach (string word in words)
            {
                text = text + char.ConvertFromUtf32(int.Parse(word));
            }
            byte[] data = Convert.FromBase64String(text);
            plainText = Encoding.UTF8.GetString(data);
            ViewData["Message"] = plainText;
            return plainText;
        }
    }
}