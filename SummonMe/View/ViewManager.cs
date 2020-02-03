﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SummonMe.Models;

namespace SummonMe.View
{
    public class ViewManager : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
  
        }

        string region;
        public string Region
        {
            get { return region; }
            set { region = value; NotifyPropertyChanged("Region"); }
        }

        string summonerName;
        public string SummonerName
        {
            get { return summonerName; }
            set { summonerName = value; NotifyPropertyChanged("SummonerName"); }
        }

        private LeagueEntryDTO leagueEntry;
        public LeagueEntryDTO LeagueEntry
        {
            get { return leagueEntry; }
            set { leagueEntry = value; NotifyPropertyChanged("LeagueEntry"); }
        }

    }

}