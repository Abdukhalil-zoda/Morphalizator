using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using Wpf.Ui.Common.Interfaces;

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
        public void OnNavigatedTo() {
        }

        public void OnNavigatedFrom() {
        }



        [RelayCommand]
        private void Analyze() {
            var asoses = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory,"..", "..", "..", "Assets", "asos.txt"));
            var qushimchases = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "..", "..", "..", "Assets", "qushimcha.txt"));
            foreach (var line in asoses) {
                if (WordText.ToLower().StartsWith(line.Split()[0])) {
                    RootWord = line.Split()[0];
                    RootWordType = line.Split()[1];
                    return;
                }
            }

            foreach (var line in qushimchases) {
                var splited = line.Split(' ');
                if (WordText.ToLower().Contains(splited[0])) {

                }
            }


        }
    }
}
