namespace TABFRET.Models
{
    public class MidiNote
    {
        public int NoteNumber { get; set; }
        public long StartTimeTicks { get; set; }
        public long DurationTicks { get; set; }
        public int Velocity { get; set; }
        public int Channel { get; set; }    // <-- Add this line
    }
}
