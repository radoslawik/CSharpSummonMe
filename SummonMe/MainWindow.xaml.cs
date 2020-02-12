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
using SummonMe.API;
using SummonMe.Models;
using SummonMe.View;

namespace SummonMe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ViewManager viewProfile;
        public MainWindow()
        {
            
            viewProfile = new ViewManager();
            InitializeComponent();

            this.DataContext = viewProfile;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(viewProfile.SummonerName) || string.IsNullOrEmpty(viewProfile.Region))
            {
                viewProfile.LeagueEntry = null;
                Console.WriteLine("Provide correct summoner name and/or region");
                return;
            }

            SummonerHandler summoner_handler = new SummonerHandler(viewProfile.Region);
            SummonerDTO summoner = await summoner_handler.GetSummoner(viewProfile.SummonerName);
            if(summoner == null)
            {
                return;
            }
            viewProfile.SummonerEntry = summoner;

            LeagueEntryHandler league_entry_handler = new LeagueEntryHandler(viewProfile.Region);
            List<LeagueEntryDTO> league_entries = await league_entry_handler.GetLeagueEntry(summoner.Id);
            if (league_entries == null)
            {
                return;
            }
            LeagueEntryDTO league_entry = league_entries.Where(p => p.QueueType.Equals("RANKED_SOLO_5x5")).FirstOrDefault();
            viewProfile.LeagueEntry = league_entry;
            viewProfile.EmblemPath = "pack://application:,,,/Assets/Emblem/Emblem_" + league_entry.Tier + ".png";

            ChampionMasteryHandler champ_mastery_handler = new ChampionMasteryHandler(viewProfile.Region);
            List<ChampionMasteryDTO> champ_masteries = await champ_mastery_handler.GetChampionMasteries(summoner.Id);
            if (champ_masteries == null)
            {
                return;
            }
            ChampionMasteryDTO most_points_champ = champ_masteries.First();
            viewProfile.ChampionMasteryEntry = most_points_champ;
            // viewProfile.ChampIconPath = "http://ddragon.leagueoflegends.com/cdn/10.3.1/img/champion/" + most_points_champ.ChampionId + ".png";
            // championID is a number, we need string, API is not useful here :(
            viewProfile.ChampIconPath = "http://ddragon.leagueoflegends.com/cdn/10.3.1/img/champion/" + "Nidalee" + ".png";

            Console.WriteLine("summoner name, region");
            Console.WriteLine(viewProfile.SummonerName);
            Console.WriteLine(viewProfile.Region);
            
            Console.WriteLine("name, level, revisiondate(seconds), special id");
            Console.WriteLine(summoner.Name);
            Console.WriteLine(summoner.SummonerLevel);
            Console.WriteLine(summoner.RevisionDate);
            Console.WriteLine(summoner.Puuid);
            Console.WriteLine(summoner.Id);

            Console.WriteLine("wins, loses");
            Console.WriteLine(league_entry.Wins);
            Console.WriteLine(league_entry.Losses);
            Console.WriteLine(league_entry.Tier);


        }
    }
}
