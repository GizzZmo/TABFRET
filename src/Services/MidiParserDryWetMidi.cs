using System.Collections.Generic;
using System.Threading.Tasks;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using TABFRET.Models;

public class MidiParserDryWetMidi : IMidiParser
{
    public async Task<IList<MidiNote>> ParseMidiFileAsync(string path)
    {
        return await Task.Run(() =>
        {
            var midiFile = MidiFile.Read(path);
            var notes = midiFile.GetNotes();
            var result = new List<MidiNote>();
            foreach (var note in notes)
            {
                result.Add(new MidiNote
                {
                    NoteNumber = note.NoteNumber,
                    StartTimeTicks = (long)note.Time,
                    DurationTicks = (long)note.Length,
                    Velocity = note.Velocity
                });
            }
            return result;
        });
    }
}
