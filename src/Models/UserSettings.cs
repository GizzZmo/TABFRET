using System.Collections.Generic;

namespace TABFRET.Models
{
    public class UserSettings
    {
        public int NumberOfStrings { get; set; } = 6;
        public List<int> Tuning { get; set; } = new() { 64, 59, 55, 50, 45, 40 }; // EADGBE by default
        public string Theme { get; set; } = "Default";
    }
}
