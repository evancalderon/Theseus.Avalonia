using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.RegularExpressions;
using Avalonia.Media;
using ReactiveUI;
using SkiaSharp;

namespace Theseus.Avalonia.ViewModels
{
    public class Modpack
    {
        public string Slug { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public SolidColorBrush Color { get; set; }

        public int Score { get; set; } = 0;
    }

    public class MainWindowViewModel : ViewModelBase
    {
        private List<Modpack> _searchResult = new();

        public List<Modpack> SearchResult
        {
            get => _searchResult;
            set => this.RaiseAndSetIfChanged(ref _searchResult, value);
        }

        private List<Modpack> _modpacks = new();

        public List<Modpack> Modpacks
        {
            get => _modpacks;
            set => this.RaiseAndSetIfChanged(ref _modpacks, value);
        }

        public MainWindowViewModel()
        {
            var alphabet = "1234567890qwertyuiopasdfghjklzxcvbnmPOIUYTREWQLKJHGFDSAMNBVCXZ";
            var random = new Random();
            for (var i = 0; i < 30; i++)
            {
                var buffer = new byte[3];
                new Random().NextBytes(buffer);
                for (var j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = (byte)(buffer[j] * 3 / 8 + 255 * 5 / 8);
                }

                Modpacks.Add(new Modpack
                {
                    Slug = new string(Enumerable.Repeat(alphabet, 8).Select(s => s[random.Next(s.Length)]).ToArray()),
                    Name = "Modpack",
                    Desc = "Description",
                    Color = new SolidColorBrush(new Color(0xff, buffer[0], buffer[1], buffer[2])),
                });
            }

            {
                var buffer = new byte[3];
                new Random().NextBytes(buffer);
                for (var j = 0; j < buffer.Length; j++)
                {
                    buffer[j] = (byte)(buffer[j] * 3 / 8 + 255 * 5 / 8);
                }

                Modpacks.Add(new Modpack
                {
                    Slug = new string(Enumerable.Repeat(alphabet, 8).Select(s => s[random.Next(s.Length)]).ToArray()),
                    Name = "Example with a really long name that's honestly a little " +
                           "too long for this menu and stuff like that",
                    Desc = "Description",
                    Color = new SolidColorBrush(new Color(0xff, buffer[0], buffer[1], buffer[2])),
                });
            }
        }

        public void DoSearch(string searchFor)
        {
            var keywords =
                searchFor.Split(" ",
                    StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            var lckeywords = (
                from kw in keywords
                select Regex.Replace(kw.ToLower(), @"[^\w\s]", "")).ToList();

            Dictionary<Modpack, int> scores = new();
            foreach (var m in Modpacks)
            {
                var name = m.Name;
                var namel = Regex.Replace(name.ToLower(), @"[^\w\s]", "");
                var desc = m.Desc;
                var descl = Regex.Replace(desc.ToLower(), @"[^\w\s]", "");
                var slug = m.Slug;

                var score = 0;
                foreach (var kw in keywords)
                {
                    if (name == kw) score += 10;
                    if (name.Contains(kw)) score += 9;
                    if (desc.Contains(kw)) score += 2;
                    if (slug == kw) score += 1000;
                }

                foreach (var kw in lckeywords)
                {
                    if (namel == kw) score += 5;
                    if (namel.Contains(kw)) score += 4;
                    if (descl.Contains(kw)) score++;
                }

                m.Score = score;
                scores[m] = score;
            }

            var gt0 = scores.Where(kp => kp.Value > 0).ToList();
            gt0.Sort((a, b) => b.Value - a.Value);
            
            SearchResult = gt0.Select(kp => kp.Key).ToList();
        }
    }
}