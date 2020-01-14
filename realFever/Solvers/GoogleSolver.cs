using Google.OrTools.LinearSolver;
using System;
using System.Collections.Generic;

namespace realFever.Solvers
{
    public class GoogleSolver : GeneralSolver
    {
        bool refresh = false;
        public GoogleSolver(bool refresh = false)
        {
            this.refresh = refresh;
            run();
        }

        public override void run()
        {
            PlayerList allPlayers = GetPlayers.getAllPlayers(this.refresh);
            int allSize = allPlayers.Count;
            int i = 0;

            //Solver solver = Solver.CreateSolver("realFever", "GLOP_LINEAR_PROGRAMMING");
            Solver solver = Solver.CreateSolver("realFever", "CBC_MIXED_INTEGER_PROGRAMMING");

            #region DECISIONS
            foreach (Player p in allPlayers)
                p.variable = solver.MakeBoolVar(p.Id);
            #endregion

            #region CONSTRAINTS
            List<Constraint> constraintList = new List<Constraint>();

            //price
            Constraint price = solver.MakeConstraint(0, Player.maxPrice, "price");
            foreach (Player p in allPlayers)
                price.SetCoefficient(p.variable, p.price);
            constraintList.Add(price);

            //teams
            Teams teams = Teams.getAll();
            i = 0;
            foreach (KeyValuePair<Team, int> item in teams)
            {
                Team team = item.Key;
                PlayerList plt = allPlayers.getPlayersFromTeam(team);
                if (plt.Count > 0)
                {
                    double max = 3;
                    if (team.Equals(Team.BEN))
                        max = 0;
                    Constraint c = solver.MakeConstraint(0, max, team.ToString());

                    foreach (Player p in plt)
                        c.SetCoefficient(p.variable, 1);
                    constraintList.Add(c);
                }
                i++;
            }
            //positions
            Positions positions = Positions.getAll();
            Constraint[] positionsConstraints = new Constraint[positions.Count];
            i = 0;
            foreach(KeyValuePair<Position, int> position in positions)
            {
                PlayerList plp = allPlayers.getPlayersFromPosicao(position.Key);
                if (plp.Count > 0)
                {
                    Constraint c = solver.MakeConstraint(position.Value, position.Value, position.Key.ToString());
                    foreach (Player p in plp)
                        c.SetCoefficient(p.variable, 1);
                    constraintList.Add(c);
                }
                i++;
            }

            //substitutions
            PlayerList currentPlayers = GetPlayers.getCurrentPlayers(allPlayers);
            Constraint substitutions = solver.MakeConstraint((Player.totalPlayers - Player.maxSubstitutions), double.PositiveInfinity, "noTransf");
            foreach (Player p in currentPlayers)
                substitutions.SetCoefficient(p.variable, 1);
            constraintList.Add(substitutions);

            ////1 player per game minimum
            //foreach (KeyValuePair<Team, Team> game in GetPlayers.teamPairs)
            //{
            //    PlayerList plp = allPlayers.getPlayersFromTeams(game.Key, game.Value);
            //    if (plp.Count > 0)
            //    {
            //        Constraint c = solver.MakeConstraint(1, double.PositiveInfinity, game.Key + "_" + game.Value);
            //        foreach (Player p in plp)
            //            c.SetCoefficient(p.variable, 1);
            //        constraintList.Add(c);
            //    }
            //    i++;
            //}


            //remove/dont allow injured
            foreach (Player p in allPlayers)
            {
                if (p.isInjured)
                {
                    Constraint fixPlayer = solver.MakeConstraint(0, 0, "fix" + p.shortName);
                    fixPlayer.SetCoefficient(p.variable, 1);
                    constraintList.Add(fixPlayer);
                }
            }

            //fix player
            //Constraint fixPlayer2 = solver.MakeConstraint(0, 0, "fixVa");
            //fixPlayer2.SetCoefficient(allPlayers.getPlayerByName("Vagner").variable, 1);
            //constraintList.Add(fixPlayer2);


            #endregion

            #region GOALS
            Objective objective = solver.Objective();
            foreach (Player p in allPlayers)
                objective.SetCoefficient(p.variable, p.optimizationGoal);
            objective.SetMaximization();
            #endregion

            #region REPORT
            String contains = "";
            Console.WriteLine("Number of variables = " + solver.NumVariables());
            Console.WriteLine("Number of constraints = " + solver.NumConstraints());
            int result = solver.Solve();
            bool optimal = solver.VerifySolution(1e-5, true);
            if (optimal)
            {
                Console.WriteLine("Problem solved in " + solver.WallTime() + " milliseconds");
                Console.WriteLine("Problem solved in " + solver.Iterations() + " iterations");

                Console.WriteLine("Solution:");
                Console.WriteLine("Optimal objective value = " +
                        solver.Objective().Value());

                Console.WriteLine("Decisions:");
                foreach (Player p in allPlayers)
                    if (Math.Round(p.variable.SolutionValue(), 0) == 1)
                    {
                        contains = currentPlayers.Contains(p) ? "" : " \t *";
                        Console.WriteLine(p.shortName.PadRight(15) + " \t " + Math.Round(p.variable.SolutionValue(),0) +
                                          " \t " + p.team + " \t " + p.position + " \t " + Math.Round(p.optimizationGoal, 2) +
                                          //" \t " + Math.Round(p.variable.ReducedCost(), 3) + contains);
                                          " \t " + contains);
                    }

                Console.WriteLine("Constraints:");
                MpDoubleVector constraintActivities = solver.ComputeConstraintActivities();
                i = 0;
                foreach (Double value in constraintActivities)
                {
                    Console.WriteLine(constraintList[i].Name().PadRight(10) + " \t " + value + " \t " + constraintList[i].Lb() + " \t " + constraintList[i].Ub());
                    i++;
                }
            }
            else
            {
                Console.WriteLine("Infeasible...");
            }


            #endregion

            Console.ReadLine();
        }

    }
}
