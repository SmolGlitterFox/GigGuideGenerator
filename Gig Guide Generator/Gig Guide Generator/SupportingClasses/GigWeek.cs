using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Gig_Guide_Generator.SupportingClasses
{
    class GigWeek
    {
        public List<Gig> Gigs { get; set; }

        public string PrintGigs(DateTime startDate, DateTime endDate)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("*");
            if (startDate.Month != endDate.Month)
            {
                builder.Append(startDate.ToString("dd MMMM"));
            }
            else
            {
                builder.Append(startDate.ToString("dd"));
            }

            builder.Append(string.Format(" - {0}*\n\n", endDate.ToString("dd MMMM yyyy")));

            foreach (IGrouping<DateTime, Gig> GigGroup in Gigs.GroupBy(g => g.StartTime).OrderBy(g => g.Key))
            {
                builder.AppendLine(string.Format("*{1} - {0}*", GigGroup.Key.ToString("dd MMMM yyyy"), GigGroup.Key.DayOfWeek));
                builder.AppendLine();

                foreach (Gig gig in GigGroup)
                {
                    builder.AppendLine(gig.PrintGig());
                    builder.AppendLine();
                }
            }
            return builder.ToString();
        }

        public async void GenerateGigs(DateTime startDate, DateTime endDate, List<string> venues, System.Windows.Forms.RichTextBox rtbOutput)
        {
            var secrets = new ClientInformation();
            using (System.IO.StreamReader r = new System.IO.StreamReader(@"C:\FacebookSecrets.json"))
            {
                string json = r.ReadToEnd();
                secrets = JsonConvert.DeserializeObject<ClientInformation>(json);
            }

            var fb = new Facebook.FacebookClient();
            dynamic result = fb.Get("oauth/access_token", new
            {
                client_id = secrets.ClientID,
                client_secret = secrets.ClientSecret,
                grant_type = "client_credentials"
            });
            fb.AccessToken = result.access_token;

            Gigs = new List<Gig>();

            foreach (string venue in venues)
            {
                var venueDetails = await fb.GetTaskAsync(string.Format("/{0}/events", venue));
                var extractedGigs = JsonConvert.DeserializeObject<List<Gig>>(JObject.Parse(venueDetails.ToString()).SelectToken("data").ToString());

                foreach (var gig in extractedGigs.Where(g => g.StartTime >= startDate && g.StartTime < endDate))
                {
                    Gigs.Add(gig);
                }
            }

            rtbOutput.Text = PrintGigs(startDate, endDate);
        }
    }
}
