using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Avalonia.Media;
using ReactiveUI;

namespace Theseus.Avalonia.ViewModels
{
    public class Modpack
    {
        public string Id { get; set; }
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

        public async Task LoadOnline()
        {
            var result = await Modrinth.SearchAsync(limit: 32, facets: new[] { "project_type:modpack" });
            if (result == null)
            {
                Console.WriteLine("Failed to load modpacks");
                return;
            }

            List<Modpack> modpacks = new();
            var random = new Random();
            foreach (var pack in result.hits)
            {
                var color = new byte[3];
                random.NextBytes(color);
                for (var i = 0; i < color.Length; i++)
                {
                    color[i] = (byte)(color[i] * 3 / 8 + 255 * 5 / 8);
                }

                modpacks.Add(new Modpack
                {
                    Name = pack.title,
                    Desc = pack.description,
                    Id = pack.project_id,
                    Color = new SolidColorBrush(new Color(0xff, color[0], color[1], color[2]))
                });
            }

            Modpacks = modpacks;
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
                var slug = m.Id;

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