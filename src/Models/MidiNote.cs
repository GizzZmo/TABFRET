namespace TABFRET.Models
{
    /// <summary>
    /// Represents a MIDI note event, suitable for mapping to tablature.
    /// </summary>
    public class MidiNote
    {
        public int NoteNumber { get; set; }
        public long StartTimeTicks { get; set; }
        public long DurationTicks { get; set; }
        public int Channel { get; set; }
        public int Velocity { get; set; }
    }
}
