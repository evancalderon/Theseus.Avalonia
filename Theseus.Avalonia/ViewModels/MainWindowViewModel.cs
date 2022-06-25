using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Avalonia.Media;

namespace Theseus.Avalonia.ViewModels
{
    public struct Modpack
    {
        public string slug { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public SolidColorBrush color { get; set; }
    }

    [DataContract]
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Modpack> _modpacks = new();
        [DataMember] public List<Modpack> modpacks => _modpacks;

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
                    buffer[j] = (byte) (buffer[j] * 3 / 8 + 255 * 5 / 8);
                }
                
                modpacks.Add(new Modpack
                {
                    slug = new string(Enumerable.Repeat(alphabet, 8).Select(s => s[random.Next(s.Length)]).ToArray()),
                    name = "Modpack",
                    desc = "Modpack Description",
                    color = new SolidColorBrush(new Color(0xff, buffer[0], buffer[1], buffer[2])),
                });
            }
        }
    }
}