using System;
using System.IO;
using System.Net;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace realFever
{
    public class GetPlayers
    {

        private static int NAME = 5;
        private static int POSITION = 6;
        private static int TEAMNAME = 7;
        private static int TEAM = 8;
        private static int PRICE = 9;
        private static int ID = 11;
        private static int JERSEY = 15;
        private static int OPPONENTNAME = 16;
        private static int OPPONENTDATE = 17;
        private static int TOTALPOINTS = 18;
        private static int AVERAGEPOINTS = 19;
        private static int AVERAGEPOINTSLAST4 = 20;
        private static int TOTALMINUTES = 21;
        private static int AVERAGEMINUTES = 22;
        private static int AVERAGEMINUTESLAST4 = 23;
        private static int GAMES = 24;
        private static int GOALS = 25;
        private static int ASSISTS = 26;
        private static int CLEANSHEETS = 27;
        private static int NEXTMATCHDATE = 30;
        private static int NEXTMATCHTIME = 31;
        private static int LEDGERID = 32;
        private static int NEXTMATCHHOME = 33;
        private static int SHORTNAME = 34;
        private static int ISINJURED = 35;

        internal static Dictionary<Team, Team> teamPairs = new Dictionary<Team, Team>();

        //private static String jsonFile = @"C:\Users\864498\Desktop\realfvr\playerData.json";
        public static String path = @"C:\Dropbox\projetos\realFever\";
        private static String jsonName = "playerData.json";

        internal static PlayerList getCurrentPlayers(PlayerList allPlayers)
        {
            PlayerList pl = new PlayerList();

            String[] myPlayerList = { "Paulinho", "Hélder Guedes", "William Oliveira", "Lucas Evangelista", "Fabrício", "Tozé",
                               "Yacine Brahimi", "Paulo Machado", "Sebastián Coates", "Bebeto Machado", "Ricardo Esgaio", "Felipe",
                               "Florent Hanin","Vagner", "Rui Patrício" };

            //String[] myPlayerList = { "Moussa Marega", "Jonas", "Lucas Evangelista", "Davidson", "Raphinha", "Shoya Nakajima", "Raul Silva", "Sebastián Coates", "Iván Marcano", "Ricardo Esgaio", "Rui Patrício", "Quim", "Alex Telles", "Murilo Freitas","Emmanuel Hackman","Bruno Fernandes"};

            foreach (String name in myPlayerList)
                pl.Add(allPlayers.getPlayerByName(name));
            return pl;

        }

        internal static PlayerList getAllPlayers(Boolean refreshData = false)
        {
            if (refreshData)
                refreshPlayerData();
            Dictionary<string, string> teamList = new Dictionary<string, string>();
            String playerData = File.ReadAllText(path + jsonName);
            JObject json = JObject.Parse(playerData); // parse as array  
            JArray jsonData = (JArray)json["aaData"];

            PlayerList pl = new PlayerList();
            foreach (JArray p in jsonData)
            {
                Player player = new Player();
                player.name = p[NAME].ToString().Trim();
                Position pos;
                if (Enum.TryParse<Position>(p[POSITION].ToString(), out pos))
                    player.position = pos;
                Team team;
                if (Enum.TryParse<Team>(p[TEAM].ToString(), out team))
                    player.team = team;
                Team opponentTeam;
                if (Enum.TryParse<Team>(p[OPPONENTNAME].ToString(), out opponentTeam))
                    player.opponentTeam = opponentTeam;
                player.price = Convert.ToDouble(p[PRICE]);
                player.Id = "_"+p[ID].ToString();
                player.totalPoints = Convert.ToInt32(p[TOTALPOINTS]);
                player.averagePoints = Convert.ToDouble(p[AVERAGEPOINTS]);
                player.averagePointsLast4 = Convert.ToDouble(p[AVERAGEPOINTSLAST4]);
                player.totalMinutes = Convert.ToInt32(p[TOTALMINUTES]);
                player.averageMinutes = Convert.ToDouble(p[AVERAGEMINUTES]);
                player.averageMinutesLast4 = Convert.ToDouble(p[AVERAGEMINUTESLAST4]);
                player.games = Convert.ToInt32(p[GAMES]);
                player.home = Convert.ToBoolean(p[NEXTMATCHHOME]);
                player.shortName = p[SHORTNAME].ToString();

                if(!p[ISINJURED].ToString().Equals(""))
                    player.isInjured = true;


                pl.Add(player);

                if(!teamPairs.ContainsKey(player.team) && !teamPairs.ContainsKey(player.opponentTeam) && !teamPairs.ContainsValue(player.team) & !teamPairs.ContainsValue(player.opponentTeam))
                    teamPairs.Add(player.team, player.opponentTeam);
            }
            return pl;
        }

        private static void refreshPlayerData()
        {
            String url = "https://fantasy.realfevr.com/sc/available_players?sEcho=2&iColumns=4&sColumns=&iDisplayStart=0&iDisplayLength=800&mDataProp_0=0&mDataProp_1=1&mDataProp_2=2&mDataProp_3=3&sSearch=&bRegex=false&sSearch_0=&bRegex_0=false&bSearchable_0=true&sSearch_1=&bRegex_1=false&bSearchable_1=true&sSearch_2=&bRegex_2=false&bSearchable_2=true&sSearch_3=&bRegex_3=false&bSearchable_3=true&iSortingCols=0&bSortable_0=true&bSortable_1=true&bSortable_2=true&bSortable_3=true&season_id=59650ca2f7502d6bd6000001&_=1509227315190";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Headers["X-NewRelic-ID"] = "VQYBVVBaDhAIV1NWAAMA";
            request.Headers["X-CSRF-Token"] = "yIGt72hrIocqM91ZdBhDB/zrrluQmbOhaiTzEXk3s50=";
            request.Headers["X-Requested-With"] = "XMLHttpRequest";
            request.Referer = "https://fantasy.realfevr.com/sc/teams/599d7bf6bd99d32221000001/transfers";
            request.Headers["Cookie"] = "__cfduid=d40d933e0f740cd1f0bde2ab100eef0bb1519330258; locale=en; time_zone=Europe/London; remember_user_token=BAhbB1sGaQL7BEkiIiQyYSQxMCRMMkI1VTdOcmlEb09wOVduaWxLWk5lBjoGRVQ%3D--541a32ab3f2e8aa9babcf45a40fa23cad92e1364; _fantasy-revolution_session=BAh7DUkiE3VzZXJfcmV0dXJuX3RvBjoGRVQiJy9zYy90ZWFtcy81OTlkN2JmNmJkOTlkMzIyMjEwMDAwMDFJIg9zZXNzaW9uX2lkBjsAVEkiJTg0OTczODQyNzk5ZTNjOGI3OWY3MjJmMDA3N2Q2MGMzBjsAVEkiEW9yaWdpbmFsX3VyaQY7AEZJIi9odHRwczovL2ZhbnRhc3kucmVhbGZldnIuY29tL3VzZXJzL3NpZ25faW4GOwBUSSIZcmVxdWVzdF9jb3VudHJ5X2NvZGUGOwBGSSIHUFQGOwBUSSIZd2FyZGVuLnVzZXIudXNlci5rZXkGOwBUWwdbBmkC%2BwRJIiIkMmEkMTAkTDJCNVU3TnJpRG9PcDlXbmlsS1pOZQY7AFRJIh13YXJkZW4udXNlci51c2VyLnNlc3Npb24GOwBUewZJIhRsYXN0X3JlcXVlc3RfYXQGOwBUbCsHGM6rWkkiEHBlcm1pc3Npb25zBjsARnsASSIQX2NzcmZfdG9rZW4GOwBGSSIxeUlHdDcyaHJJb2NxTTkxWmRCaERCL3pycmx1UW1iT2hhaVR6RVhrM3M1MD0GOwBG--0f611a94d4fa0ee22f1810b1d2ab020a1dbd4326";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            String responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            File.WriteAllText(path + jsonName, responseString);
        }
    }
}
