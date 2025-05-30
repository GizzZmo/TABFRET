namespace TABFRET.Utils
{
    public static class FretboardHelper
    {
        // Standard tuning lowest MIDI note for each string (6=E2, 1=E4)
        public static readonly int[] StringRoots = { 40, 45, 50, 55, 59, 64 };

        // Returns (string, fret) tuple for a MIDI note, or (-1,-1) if out of range
        public static (int stringNum, int fret) GetBestPosition(int midiNote)
        {
            int bestString = -1, bestFret = int.MaxValue;
            for (int i = 0; i < StringRoots.Length; i++)
            {
                int fret = midiNote - StringRoots[i];
                if (fret >= 0 && fret <= 20 && fret < bestFret)
                {
                    bestString = i + 1;
                    bestFret = fret;
                }
            }
            return (bestString == -1) ? (-1, -1) : (bestString, bestFret);
        }

        // Transpose MIDI notes by semitones
        public static int Transpose(int midiNote, int semitones)
        {
            return midiNote + semitones;
        }
    }
}
