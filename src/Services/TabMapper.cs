using System.Collections.Generic;
using TABFRET.Models;

namespace TABFRET.Services
{
    public class TabMapper
    {
        private static readonly int[] StringMidiRoots = { 40, 45, 50, 55, 59, 64 };

        public List<TabNote> MapMidiNotesToTab(List<MidiNote> midiNotes)
        {
            var tabNotes = new List<TabNote>();

            foreach (var note in midiNotes)
            {
                int bestString = -1;
                int bestFret = -1;
                int minFret = int.MaxValue;

                for (int stringIdx = 0; stringIdx < StringMidiRoots.Length; stringIdx++)
                {
                    int fret = note.NoteNumber - StringMidiRoots[stringIdx];
                    if (fret >= 0 && fret <= 20 && fret < minFret)
                    {
                        bestString = stringIdx + 1; // 1=high E, 6=low E
                        bestFret = fret;
                        minFret = fret;
                    }
                }

                if (bestString != -1)
                {
                    tabNotes.Add(new TabNote
                    {
                        StringNumber = bestString,
                        FretNumber = bestFret,
                        StartTimeTicks = note.StartTimeTicks,
                        DurationTicks = note.DurationTicks,
                        OriginalMidiNoteNumber = note.NoteNumber
                    });
                }
            }
            return tabNotes;
        }
    }
}
