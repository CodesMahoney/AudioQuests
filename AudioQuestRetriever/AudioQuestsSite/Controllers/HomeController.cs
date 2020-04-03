using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AudioQuestsSite.Models;
using Newtonsoft.Json;

namespace AudioQuestsSite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(int questId = 0)
        {
            var ret = new IndexViewModel();
            var list = new List<AudioQuestItem>();

            var text = System.IO.File.ReadAllText("masterquestfile.json");
            JsonConvert.DeserializeObject<List<AudioQuestItem>>(text);
            list.AddRange(JsonConvert.DeserializeObject<List<AudioQuestItem>>(text));

            var item = list.SingleOrDefault(x => x.QuestId == questId);
            ret.Id = questId;
            ret.description = item.Description;
            ret.objective = item.Summary;

            return View(ret);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
