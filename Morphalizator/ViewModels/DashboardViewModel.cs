using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Wpf.Ui.Common.Interfaces;
using Path = System.IO.Path;
using Morphalizator.Models;

namespace Morphalizator.ViewModels {
    public partial class DashboardViewModel : ObservableObject, INavigationAware {
        [ObservableProperty]
        private int _counter = 0;
        [ObservableProperty]
        private string _WordText = string.Empty;
        [ObservableProperty]
        private string _RootWord = string.Empty;
        [ObservableProperty]
        private string _RootWordType = string.Empty;
        [ObservableProperty]
        private List<Qushimcha> _qushimchas = new List<Qushimcha>();
        public void OnNavigatedTo() {
        }

        public void OnNavigatedFrom() {
        }



        [RelayCommand]
        private void Analyze() {
            Qushimchas.Clear();
            var asoses = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory,"..", "..", "..", "Assets", "asos.txt"));
            var qushimchas = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Assets", "qushimcha.txt"));
            foreach (var line in asoses) {
                if (WordText.ToLower().StartsWith(line.Split()[0])) {
                    RootWord = line.Split()[0];
                    RootWordType = line.Split()[1];
                    break;
                }
            }

            List<string> used = new List<string>();
            var other = WordText.ToLower().Substring(RootWord.Length);
            foreach (var line in qushimchas) {
                var splited = line.Split(' ');
                if (other.Contains(splited[0]))
                {
                    used.Add(line);
                }
                
            }

            used.Sort((x, y) => x.Split()[0].Length.CompareTo(y.Split()[0].Length));
            used.Reverse();
            int i = 0;
            while (!string.IsNullOrWhiteSpace(other) && i <= used.Count)
            {
                i++;
                foreach (var line in used)
                {
                    var splited = line.Split(' ');
                    if (other.StartsWith(splited[0]))
                    {
                        other = other.Substring(splited[0].Length);
                        Qushimchas.Add(new Qushimcha() { Value = splited[0], Type = splited[1].Replace('_', ' ') });
                        break;
                    }
                }
            }

            
        }

        
    }
    
}
