using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOVEf_WPF
{
    class Item
    {
        private string name;
        private string path;
        private string extension;
        private string category;
        private string newPath;

        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
        public string Extension { get => extension; set => extension = value; }
        public string Category { get => category; set => category = value; }
        public string NewPath { get => newPath; set => newPath = value; }
    }
}
