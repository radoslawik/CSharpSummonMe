﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SummonMe.Models;

namespace SummonMe.View
{
    /// <summary>
    /// Interaction logic for MatchHistory.xaml
    /// </summary>
    public partial class MatchHistory : Page
    {
        public MatchHistory(ViewManager viewManager, Dictionary<string, string> champNamesDict)
        {

            InitializeComponent();
            this.DataContext = viewManager;

            for(int i = 0; i < viewManager.MatchEntries.Count; i++)
            {
                int champId = viewManager.MatchlistEntry.Matches[i].Champion;
                string champName = champNamesDict[champId.ToString()];
                string posName = viewManager.MatchlistEntry.Matches[i].Lane;
                string roleName = viewManager.MatchlistEntry.Matches[i].Role;
                int queueId = viewManager.MatchlistEntry.Matches[i].Queue;
                Queues gameQueue = viewManager.AllQueuesList.Where(p => p.QueueId == queueId).FirstOrDefault();
                string mapName = gameQueue.Map;
                int hoursAgo = (int)(439858 - (viewManager.MatchlistEntry.Matches[i].Timestamp) / 3600000);
                string playedWhen = (Math.Round(hoursAgo / 24.0)).ToString() + " day(s) ago";

                long gameId = viewManager.MatchlistEntry.Matches[i].GameId;
                long gameDurationSec = viewManager.MatchEntries[i].GameDuration;
                int minutes = (int)(gameDurationSec / 60);
                int sec = (int)(gameDurationSec - 60 * minutes);
                string gameDuration = minutes.ToString() + "min " + sec.ToString() + "sec";

                ParticipantDto player = viewManager.MatchEntries[i].Participants.Where(p => p.ChampionId == champId).FirstOrDefault();
                TeamStatsDto team = viewManager.MatchEntries[i].Teams.Where(p => p.TeamId == player.TeamId).FirstOrDefault();
                string result = team.Win.ToUpper();
                string stats = player.Stats.Kills.ToString() + "/" + player.Stats.Deaths.ToString() + "/" + player.Stats.Assists.ToString();

                spMatchHistory.Children.Add(new MatchHistoryField(champName, posName, roleName, mapName, 
                    playedWhen, gameDuration, result, stats));
            }
        }
    }
}
