namespace TABFRET.Models
{
    /// <summary>
    /// Represents a note on guitar tablature.
    /// </summary>
    public struct TabNote
    {
        public int StringNumber { get; set; } // 1 (High E) to 6 (Low E)
        public int FretNumber { get; set; }   // 0 = open string
        public long StartTimeTicks { get; set; }
        public long DurationTicks { get; set; }
        public int OriginalMidiNoteNumber { get; set; }

        public override string ToString()
        {
            return $"String: {StringNumber}, Fret: {FretNumber}, Start: {StartTimeTicks}";
        }
    }
}
