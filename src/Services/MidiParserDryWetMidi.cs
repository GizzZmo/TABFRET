using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using TABFRET.Models;

namespace TABFRET.Services
{
    public class MidiParserDryWetMidi : IMidiParser
    {
        public async Task<IList<MidiNote>> ParseMidiFileAsync(string path)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var midiFile = MidiFile.Read(path);
                    var notes = midiFile.GetNotes();
                    var noteList = notes as IList<Note> ?? new List<Note>(notes);
                    var result = new List<MidiNote>(noteList.Count);
                    result.AddRange(noteList.Select(note => new MidiNote
                    {
                        NoteNumber = note.NoteNumber,
                        StartTimeTicks = (long)note.Time,
                        DurationTicks = (long)note.Length,
                        Velocity = note.Velocity
                    }));
                    return result;
                });
            }
            catch (Exception)
            {
                // Optionally log exception for diagnostics
                return new List<MidiNote>();
            }
        }
    }
}
