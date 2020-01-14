using Microsoft.SolverFoundation.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace realFever.Solvers
{
    class MicrosoftSolver : GeneralSolver
    {

        public override void run()
        {
            PlayerList allPlayers = GetPlayers.getAllPlayers();
            int allSize = allPlayers.Count;

            SolverContext context = SolverContext.GetContext();
            Model model = context.CreateModel();
            model.Name = "realFever";

            #region DECISIONS
            Decision[] dl = new Decision[allSize];
            int i = 0;
            foreach (Player p in allPlayers)
            {
                dl[i] = new Decision(Domain.IntegerRange(0, 1), p.Id);
                i++;
            }
            model.AddDecisions(dl);
            #endregion

            #region CONSTRAINTS
            String constraint = "";

            //price
            constraint = "";
            foreach (Player p in allPlayers)
                constraint = constraint + " + " + p.price.ToString().Replace(",", ".") + " * " + p.Id;
            constraint = constraint.Substring(3, constraint.Length - 3) + " <= 100.5";
            model.AddConstraint("price", constraint);
            //teams
            foreach (Team team in Enum.GetValues(typeof(Team)))
            {
                PlayerList plt = allPlayers.getPlayersFromTeam(team);
                constraint = "";
                if (plt.Count > 0)
                {
                    foreach (Player p in plt)
                        constraint = constraint + " + " + p.Id;
                    constraint = constraint.Substring(3, constraint.Length - 3) + " <= 3";
                    model.AddConstraint(team.ToString(), constraint);
                }
            }
            //posicao
            Positions posicoes = Positions.getAll();
            foreach (KeyValuePair<Position, int> posicao in posicoes)
            {
                PlayerList plp = allPlayers.getPlayersFromPosicao(posicao.Key);
                constraint = "";
                if (plp.Count > 0)
                {
                    foreach (Player p in plp)
                        constraint = constraint + " + " + p.Id;
                    constraint = constraint.Substring(3, constraint.Length - 3) + " == " + posicao.Value;
                    model.AddConstraint(posicao.Key.ToString(), constraint);
                }
            }
            //substitutions
            PlayerList currentPlayers = GetPlayers.getCurrentPlayers(allPlayers);
            constraint = "";
            foreach (Player p in currentPlayers)
                constraint = constraint + " + " + p.Id;
            constraint = constraint.Substring(3, constraint.Length - 3) + " >= " + (Player.totalPlayers - Player.maxSubstitutions);
            model.AddConstraint("substitutions", constraint);


            #endregion

            #region GOAL
            constraint = "";
            foreach (Player p in allPlayers)
                constraint = constraint + " + " + p.optimizationGoal.ToString().Replace(",", ".") + " * " + p.Id;
            constraint = constraint.Substring(3, constraint.Length - 3);
            model.AddGoal("points", GoalKind.Maximize, constraint);
            #endregion

            #region REPORT
            Solution solution = context.Solve();
            Report report = solution.GetReport(ReportVerbosity.Decisions);

            String[] merda = report.ToString().Split('\n');
            String[] results = Array.FindAll(merda, s => s.StartsWith("_"));

            PlayerList resultsList = new PlayerList();
            foreach (String s in results)
            {
                String[] splitted = s.Split(':');
                if (splitted[1].Trim().Equals("1"))
                    resultsList.Add(allPlayers.getPlayerById(splitted[0]));
            }
            Console.WriteLine("Status: " + solution.Quality);
            foreach (Goal g in solution.Goals)
                Console.WriteLine("Total Points: " + g);
            foreach (Player pl in resultsList)
                Console.WriteLine(pl.name + " | " + pl.team + " | " + pl.position + " | " + pl.optimizationGoal);

            Console.ReadLine();

            #endregion


        }
    }
}
