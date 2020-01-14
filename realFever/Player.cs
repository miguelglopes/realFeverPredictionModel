using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace realFever
{
    class Player
    {
        /* MUDAR ISTO!!!!! */
        private static int week = 26;
        public static int totalPlayers = 15;
        public static int maxSubstitutions = 2;
        public static double maxPrice = 106;
        /* MUDAR ISTO!!!!! */


        public Variable variable = null;
        private double homeFactor = 1;

        public String name = null;
        public String shortName = null;
        public double price = 0;
        public int totalPoints = 0;
        public Team team = 0;
        public Team opponentTeam = 0;
        public Position position = 0;
        public string Id = null;
        public double averagePoints = 0;
        public double averagePointsLast4 = 0;
        public int totalMinutes = 0;
        public double averageMinutes = 0;
        public double averageMinutesLast4 = 0;
        public int games = 0;
        public bool home = false;
        public bool isInjured = false;


        public double optimizationGoal
        {
            get
            {
                if (!home)
                    homeFactor = 0.93;

                double weights = (normalizedTotalPoints + averagePointsLast4 * 1.1 + normalizedTotalMinutes * 0.9+normalizedTeamDif * 0.8) * homeFactor;
                return weights;
            }
        }

        public double optimizationGoalNoTeamDif
        {
            get
            {
                if (!home)
                    homeFactor = 0.93;

                double weights = (normalizedTotalPoints + averagePointsLast4*1.1 + normalizedTotalMinutes*1) * homeFactor;
                return weights;
            }
        }

        public double normalizedTotalMinutes
        {
            get
            {
                return totalMinutes / week / 10;
            }
        }

        public double normalizedTeamDif
        {
            get
            {
                return (Teams.getAll()[team]- Teams.getAll()[opponentTeam])/6;
            }
        }

        public double normalizedTotalPoints
        {
            get
            {
                return totalPoints / week;
            }
        }

    }

    class PlayerList : List<Player>
    {
        internal PlayerList getPlayersFromTeam(Team team)
        {
            PlayerList pl = new PlayerList();
            foreach(Player p in this)
                if (p.team.Equals(team))
                    pl.Add(p);
            return pl;
        }

        internal PlayerList getPlayersFromPosicao(Position posicao)
        {
            PlayerList pl = new PlayerList();
            foreach (Player p in this)
                if (p.position.Equals(posicao))
                    pl.Add(p);
            return pl;
        }

        internal Player getPlayerByName(String name)
        {
            foreach (Player p in this)
            {
                if (p.name.Equals(name))
                    return p;
            }
            return null;
        }

        internal Player getPlayerByName(String name, Team team)
        {
            foreach (Player p in this)
            {
                if (p.name.Equals(name) && p.team.Equals(team))
                    return p;
            }
            return null;
        }

        internal PlayerList getPlayersFromTeams(Team team1, Team team2)
        {

            PlayerList pl = new PlayerList();               
            foreach (Player p in this)
                if (p.team.Equals(team1) || p.team.Equals(team2))
                    pl.Add(p);
            return pl;

        }

        internal Player getPlayerById(String id)
        {
            foreach (Player p in this)
            {
                if (p.Id.Equals(id))
                    return p;
            }
            return null;
        }

        internal static void printTestData()
        {

            PlayerList allPlayers = GetPlayers.getAllPlayers();
            Console.WriteLine();

            Excel.Application excel = new Excel.Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;
            Excel.Workbook wb = excel.Workbooks.Add(Type.Missing);
            Excel.Worksheet ws = (Excel.Worksheet) wb.ActiveSheet;
            Console.WriteLine("Writing data to Excel, please wait...");
            ws.Name = "PlayerData";
            ws.Cells[1, 1] = "Name";
            ws.Cells[1, 2] = "TotalPoints";
            ws.Cells[1, 3] = "NormalizedTotalPts";
            ws.Cells[1, 4] = "AverageLast4Pts";
            ws.Cells[1, 5] = "NormalizedMinutes";
            ws.Cells[1, 6] = "optimizationGoalNoTeamDif";
            ws.Cells[1, 7] = "Home";
            ws.Cells[1, 8] = "OptimizedGoal";
            ws.Cells[1, 9] = "price";
            ws.Cells[1, 10] = "team";
            ws.Cells[1, 11] = "position";
            ws.Cells[1, 12] = "averagePoints";
            ws.Cells[1, 13] = "totalMinutes";
            ws.Cells[1, 14] = "averageMinutes";
            ws.Cells[1, 15] = "averageMinutesLast4";
            ws.Cells[1, 16] = "games";
            ws.Cells[1, 17] = "normalizedTeamDif";
            int i = 2;
            foreach (Player p in allPlayers)
            {
                ws.Cells[i, 1] = p.shortName;
                ws.Cells[i, 2] = p.totalPoints;
                ws.Cells[i, 3] = p.normalizedTotalPoints;
                ws.Cells[i, 4] = p.averagePointsLast4;
                ws.Cells[i, 5] = p.normalizedTotalMinutes;
                ws.Cells[i, 6] = p.optimizationGoalNoTeamDif;
                ws.Cells[i, 7] = p.home;
                ws.Cells[i, 8] = p.optimizationGoal;
                ws.Cells[i, 9] = p.price;
                ws.Cells[i, 10] = p.team.ToString();
                ws.Cells[i, 11] = p.position.ToString();
                ws.Cells[i, 12] = p.averagePoints;
                ws.Cells[i, 13] = p.totalMinutes;
                ws.Cells[i, 14] = p.averageMinutes;
                ws.Cells[i, 15] = p.averageMinutesLast4;
                ws.Cells[i, 16] = p.games;
                ws.Cells[i, 17] = p.normalizedTeamDif;
                i++;
            }
            String fileName = GetPlayers.path + DateTime.Now.ToString("yyyyMMddHHmmssffff");
            wb.SaveAs(fileName);
            excel.Visible = true;
            excel.DisplayAlerts = true;
            Console.WriteLine("Data written to "+ fileName+".xlsx");
            Console.WriteLine("Press any key to quit...");
            Console.ReadLine();
        }
    }
}
