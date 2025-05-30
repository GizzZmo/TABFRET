using System.Collections.Generic;
using TABFRET.Models;

namespace TABFRET.Services
{
    /// <summary>
    /// Maps MIDI notes to guitar tab positions (string, fret).
    /// </summary>
    public class TabMapper
    {
        // Standard tuning MIDI numbers for open strings (E2, A2, D3, G3, B3, E4)
        private static readonly int[] StringMidiRoots = { 40, 45, 50, 55, 59, 64 };

        /// <summary>
        /// Maps a list of MIDI notes to their best guitar tab positions.
        /// </summary>
        public List<TabNote> MapMidiNotesToTab(List<MidiNote> midiNotes)
        {
            var tabNotes = new List<TabNote>();

            foreach (var note in midiNotes)
            {
                // Find possible string/fret combos for the note (within 0-20 frets)
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
