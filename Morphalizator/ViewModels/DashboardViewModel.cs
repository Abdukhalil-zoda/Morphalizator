using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.IO;
using Wpf.Ui.Common.Interfaces;
using Path = System.IO.Path;

namespace Morphalizator.ViewModels {
    public partial class DashboardViewModel : ObservableObject, INavigationAware {
        [ObservableProperty]
        private int _counter = 0;
        [ObservableProperty]
        public string _WordText = string.Empty;
        [ObservableProperty]
        public string _RootWord = string.Empty;
        [ObservableProperty]
        public string _RootWordType = string.Empty;
        [ObservableProperty]
        public List<Qushimcha> _Qushimchas = new List<Qushimcha>();
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
                    //Qushimchas.Add(new Qushimcha(){Value = splited[0], Type = splited[1].Replace('_', ' ') });
                }
                
            }
            used.Sort((x, y) => x.Split()[0].Length.CompareTo(y.Split()[0].Length));
            used.Reverse();

            foreach (var line in used)
            {
                var splited = line.Split(' ');
                if (other.StartsWith(splited[0]))
                {
                    other = other.Substring(splited[0].Length);
                    Qushimchas.Add(new Qushimcha() { Value = splited[0], Type = splited[1].Replace('_', ' ') });
                    
                }
            }

            int a = 0;
        }

        
    }
    /// <summary>
    /// 
    /// </summary>
    public class Qushimcha {
        public string? Value { get; set; }
        public string? Type { get; set; }
    }
}
