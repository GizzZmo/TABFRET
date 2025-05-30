namespace TABFRET.Models
{
    public struct TabNote
    {
        public int StringNumber { get; set; }
        public int FretNumber { get; set; }
        public long StartTimeTicks { get; set; }
        public long DurationTicks { get; set; }
        public int OriginalMidiNoteNumber { get; set; }

        public override string ToString()
        {
            return $"String: {StringNumber}, Fret: {FretNumber}, Start: {StartTimeTicks}";
        }
    }
}
