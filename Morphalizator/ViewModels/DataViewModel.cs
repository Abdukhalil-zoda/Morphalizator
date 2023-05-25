using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Morphalizator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Media;
using Microsoft.VisualBasic.FileIO;
using Wpf.Ui.Common.Interfaces;

namespace Morphalizator.ViewModels {
    public partial class DataViewModel : ObservableObject, INavigationAware {
        private bool _isInitialized = false;

        [ObservableProperty]
        private IEnumerable<DataColor> _colors;

        [ObservableProperty] 
        private string _text;
        [ObservableProperty]
        private List<Word> _words = new List<Word>();

        public void OnNavigatedTo()
        {
        }

        public void OnNavigatedFrom() {
        }


        AnalyzedWord Analyze(string word) {
            var Qushimchas = new List<Qushimcha>();
            var RootWord = "";
            var RootWordType = "";
            Qushimchas.Clear();
            var asoses = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Assets", "asos.txt"));
                                                          var qushimchas = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Assets", "qushimcha.txt"));
            foreach (var line in asoses) {
                if (word.ToLower().StartsWith(line.Split()[0])) {
                    RootWord = line.Split()[0];
                    RootWordType = line.Split()[1];
                    break;
                }
            }

            List<string> used = new List<string>();
            var other = word.ToLower().Substring(RootWord.Length);
            foreach (var line in qushimchas) {
                var splited = line.Split(' ');
                if (other.Contains(splited[0])) {
                    used.Add(line);
                    //Qushimchas.Add(new Qushimcha(){Value = splited[0], Type = splited[1].Replace('_', ' ') });
                }

            }
            used.Sort((x, y) => x.Split()[0].Length.CompareTo(y.Split()[0].Length));
            used.Reverse();

            foreach (var line in used) {
                var splited = line.Split(' ');
                if (other.StartsWith(splited[0])) {
                    other = other.Substring(splited[0].Length);
                    Qushimchas.Add(new Qushimcha() { Value = splited[0], Type = splited[1].Replace('_', ' ') });

                }
            }

            return new AnalyzedWord() { RootWord = RootWord, Type = RootWordType, Qushimchas = Qushimchas };
        }

        [RelayCommand]
        private void Analyze()
        {
            Words.Clear();
            foreach (var s in Text.Split())
            {
                Words.Add(new Word(){Value = s, Analyz = Analyze(s)});
            }
        }

        private void InitializeViewModel() {
            var random = new Random();
            var colorCollection = new List<DataColor>();

            for (int i = 0; i < 8192; i++)
                colorCollection.Add(new DataColor {
                    Color = new SolidColorBrush(Color.FromArgb(
                        (byte)200,
                        (byte)random.Next(0, 250),
                        (byte)random.Next(0, 250),
                        (byte)random.Next(0, 250)))
                });

            Colors = colorCollection;

            _isInitialized = true;
        }
    }
}
