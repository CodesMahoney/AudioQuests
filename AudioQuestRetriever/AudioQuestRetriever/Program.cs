using System.IO;
using System.Linq;
using System;
using System.Collections.Generic;
using Google.Cloud.TextToSpeech.V1;
using System.Net.Http;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Newtonsoft.Json;

namespace AudioQuestRetriever
{
    class Program
    {
        public string path = @"C:\Users\dylro\source\repos\AudioQuest\AudioQuestRetriever";

        public static async Task Main(string[] args)
        {
            //GetQuestTexts(0, 60);

            //await CreateMasterText(0, 60);
            /// await GetAllSpeechFiles();
            //Console.WriteLine(temp.Sum(x => x.Description.Count()));
            //Console.WriteLine();
            //Console.WriteLine(temp.Count());

            Console.ReadKey();
        }

        /// <summary>
        /// this combs through the masterquestfile created in <see cref="CreateMasterText"/> and feeds each line one by one to
        /// the google cloud text to speech api.
        /// </summary>
        /// <returns></returns>
        public async Task GetAllSpeechFiles() {
            var list = new List<AudioQuestItem>();

            var text = File.ReadAllText($@"{path}\masterquestfile.json");
            JsonConvert.DeserializeObject<List<AudioQuestItem>>(text);
            list.AddRange(JsonConvert.DeserializeObject<List<AudioQuestItem>>(text));

            var temp = list.Where(x => x.Summary != "Error" && x.Description != "Error");

            foreach (var obj in temp)
            {
                try
                {
                    await GetSpeechFile(obj.QuestId, obj.Description);
                }
                catch (Exception)
                {
                    Console.WriteLine("go fix " + obj.QuestId);
                }
            }
        }

        /// <summary>
        ///  this calls out to google cloud text to speech api and creates an audio file from text
        ///  NOTE: this only works if you have set up a key on ur machine, i beleive the docs are here.
        ///  https://cloud.google.com/text-to-speech/docs/quickstart-protocol
        /// </summary>
        /// <param name="id"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task GetSpeechFile(int id, string text)
        {
            // Instantiate a client
            TextToSpeechClient client = TextToSpeechClient.Create();

            // Set the text input to be synthesized.
            SynthesisInput input = new SynthesisInput
            {
                Text = text
            };

            // Build the voice request, select the language code ("en-US"),
            // and the SSML voice gender ("neutral").
            VoiceSelectionParams voice = new VoiceSelectionParams
            {
                LanguageCode = "en-US",
                SsmlGender = SsmlVoiceGender.Neutral,
                Name = "en-US-Wavenet-D"
            };

            // Select the type of audio file you want returned.
            AudioConfig config = new AudioConfig
            {
                AudioEncoding = AudioEncoding.Mp3
                
            };

            // Perform the Text-to-Speech request, passing the text input
            // with the selected voice parameters and audio file type
            var response = client.SynthesizeSpeech(new SynthesizeSpeechRequest
            {
                Input = input,
                Voice = voice,
                AudioConfig = config
            });

            // Write the binary AudioContent of the response to an MP3 file.
            using (Stream output = File.Create(@"C:\Users\dylro\source\repos\QuestSpeak\QuestSpeak\audio\" + id + ".mp3"))
            {
                response.AudioContent.WriteTo(output);
                Console.WriteLine(id);
            }
        }

        /// <summary>
        /// this loops over the quest docs created in <see cref="GetQuestTexts"/> and cleans them up with <see cref="ManipulateString(string)"/>
        /// and then slaps em in one master document.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task CreateMasterText(int start, int end)
        {
            var list = new List<AudioQuestItem>();

            for (var y = start; y < end; y++)
            {
                var text = File.ReadAllText($@"{path}\questdocs\quests" + y + ".txt");

                JsonConvert.DeserializeObject<List<AudioQuestItem>>(text);

                list.AddRange(JsonConvert.DeserializeObject<List<AudioQuestItem>>(text));
            }

            list.ForEach(x =>
            {
                x.Description = ManipulateString(x.Description);
                x.Summary = ManipulateString(x.Summary);
            });

            File.WriteAllText($@"{path}\masterquestfile.json", JsonConvert.SerializeObject(list));
        }
       
        /// <summary>
        /// This cleans up the data so that it will not have random wildcards and goofy characters when we feed it into our text to voice.       
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string ManipulateString(string text)
        {
            var ret = text.Replace("&#39;", "'")
             .Replace("&lt;name&gt;", "Adventurer").Replace("&lt;Name&gt;", "Adventurer")
             .Replace("&lt;class&gt;", "Adventurer").Replace("&lt;Class&gt;", "Adventurer")
             .Replace("&lt;race&gt;", "Adventurer").Replace("&lt;Race&gt;", "Adventurer")
             .Replace("&amp;", "&")
             .Replace("&lt;", "<")
             .Replace("&gt;", ">")
             .Replace("&quot;", "\"");

            return ret;
        }

        /// <summary>
        /// Hits wowdb website and scrapes the site to get quest summary and description text.
        /// I created an outer loop (the y variable) so that I could do them 1k quests at a time.
        /// and breaks them into 1k chunk files, just in case crap goes haywire.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task GetQuestTexts(int start, int end)
        {
            var list = new List<AudioQuestItem>();

            var client = new HttpClient();

            for (var y = 52; y < 60; y++)
            {
                for (var i = 0; i < 1000; i++)
                {
                    var id = y * 1000 + i;

                    var request = new HttpRequestMessage(HttpMethod.Get, @"https://www.wowdb.com/quests/" + id);

                    var response = await client.SendAsync(request);

                    var ret = await response.Content.ReadAsStringAsync();

                    var doc = new HtmlDocument();

                    doc.LoadHtml(ret);

                    var node = doc.DocumentNode.Descendants("p").ToList();

                    try
                    {
                        var summ = node.Where(x => x.Attributes.Count > 0 && x.Attributes["class"].Value == "quest-summary").SingleOrDefault();
                        var desc = node.Where(x => x.Attributes.Count > 0 && x.Attributes["class"].Value == "quest-description").SingleOrDefault();

                        list.Add(new AudioQuestItem
                        {
                            QuestId = id,
                            Summary = summ.InnerText,
                            Description = desc.InnerText
                        });
                    }
                    catch (Exception)
                    {
                        list.Add(new AudioQuestItem
                        {
                            QuestId = id,
                            Summary = "Error",
                            Description = "Error"
                        });
                    }

                    Console.Clear();
                    Console.WriteLine(id);
                }

                var json = JsonConvert.SerializeObject(list);

                File.WriteAllText(@"C:\Users\dylro\source\repos\QuestSpeak\QuestSpeak\quests" + y + ".txt", json);

                list.Clear();
            }
        }
    }
}
