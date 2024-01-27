using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infinity.Bnois
{
    public class TabModel
    {
        public int Id { get; }
        public string Title { get; }
        public string Template { get; }
        public string Icon { get; }
        public bool Disable { get; }

        public TabModel(int id, string title, string template, string icon = "", bool disable = false)
        {
            Id = id;
            Title = title;
            Template = template;
            Icon = icon;
            Disable = disable;
        }
    }
}
