﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VectorWars
{
    public class Players
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private int score;

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

    }
    public class HighScoreWindowViewModel
    {
        private IList<Players> _players { get; set; }
        public IOrderedEnumerable<Players> _orderedPlayers { get; set; }

        public HighScoreWindowViewModel()
        {
            _players = new List<Players>();
            if(!IsInDesignMode)
            {
                string[] line = new string[2];
                StreamReader reader = new StreamReader("highscores.txt");
                while (!reader.EndOfStream)
                {
                    line = reader.ReadLine().Split(';');
                    _players.Add(new Players() { Name = line[0], Score = Convert.ToInt32(line[1]) });
                };
                reader.Close();
            }
            _orderedPlayers = _players.OrderByDescending(a => a.Score);
        }

        static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

    }
}
