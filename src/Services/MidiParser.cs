using System;
using System.Collections.Generic;
using NAudio.Midi;
using TABFRET.Models;

namespace TABFRET.Services
{
    public class MidiParser
    {
        public List<MidiNote> Notes { get; private set; } = new List<MidiNote>();
        public int TicksPerQuarterNote { get; private set; } = 480;
        public string? ErrorMessage { get; private set; }

        public bool LoadMidiFile(string filePath)
        {
            Notes.Clear();
            ErrorMessage = null;

            try
            {
                var midiFile = new MidiFile(filePath, false);
                TicksPerQuarterNote = midiFile.DeltaTicksPerQuarterNote;

                var noteOnTimes = new Dictionary<int, List<long>>();
                long absoluteTime = 0;

                foreach (var track in midiFile.Events)
                {
                    absoluteTime = 0;
                    foreach (var midiEvent in track)
                    {
                        absoluteTime += midiEvent.DeltaTime;

                        if (midiEvent is NoteOnEvent noteOn && noteOn.Velocity > 0)
                        {
                            if (!noteOnTimes.ContainsKey(noteOn.NoteNumber))
                                noteOnTimes[noteOn.NoteNumber] = new List<long>();
                            noteOnTimes[noteOn.NoteNumber].Add(absoluteTime);
                        }
                        else if (midiEvent is NoteEvent noteEvent && noteEvent.CommandCode == MidiCommandCode.NoteOff)
                        {
                            if (noteOnTimes.ContainsKey(noteEvent.NoteNumber) && noteOnTimes[noteEvent.NoteNumber].Count > 0)
                            {
                                var startTime = noteOnTimes[noteEvent.NoteNumber][0];
                                noteOnTimes[noteEvent.NoteNumber].RemoveAt(0);

                                Notes.Add(new MidiNote
                                {
                                    NoteNumber = noteEvent.NoteNumber,
                                    StartTimeTicks = startTime,
                                    DurationTicks = absoluteTime - startTime,
                                    Channel = noteEvent.Channel,
                                    Velocity = noteEvent.Velocity
                                });
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                ErrorMessage = "Failed to load MIDI: " + ex.Message;
                return false;
            }
        }
    }
}
