using System;
using System.Collections.Generic;
using NAudio.Midi;
using TABFRET.Models;

namespace TABFRET.Services
{
    public class MidiPlaybackService : IDisposable
    {
        private MidiOut midiOut;
        private MidiEventCollection midiEvents;
        private int ticksPerQuarter;
        private bool isPlaying = false;

        public MidiPlaybackService()
        {
            midiOut = new MidiOut(0); // Use first MIDI output device
        }

        public void LoadMidiEvents(List<MidiNote> notes, int ticksPerQuarterNote)
        {
            ticksPerQuarter = ticksPerQuarterNote;
            midiEvents = new MidiEventCollection(1, ticksPerQuarter);

            var track = new List<MidiEvent>();
            foreach (var note in notes)
            {
                track.Add(new NoteOnEvent(note.StartTimeTicks, note.Channel, note.NoteNumber, note.Velocity, note.DurationTicks));
                track.Add(new NoteEvent(note.StartTimeTicks + note.DurationTicks, note.Channel, MidiCommandCode.NoteOff, note.NoteNumber, 0));
            }
            midiEvents.AddTrack(track);
        }

        public void Play()
        {
            if (midiEvents == null || isPlaying) return;

            isPlaying = true;
            System.Threading.Tasks.Task.Run(() =>
            {
                foreach (var track in midiEvents)
                {
                    foreach (var midiEvent in track)
                    {
                        if (!isPlaying) break;
                        if (midiEvent is NoteOnEvent noteOn)
                            midiOut.Send(MidiMessage.StartNote(noteOn.NoteNumber, noteOn.Velocity, noteOn.Channel).RawData);
                        else if (midiEvent is NoteEvent noteOff && noteOff.CommandCode == MidiCommandCode.NoteOff)
                            midiOut.Send(MidiMessage.StopNote(noteOff.NoteNumber, 0, noteOff.Channel).RawData);

                        System.Threading.Thread.Sleep(1); // Not accurate timing, but placeholder
                    }
                }
                isPlaying = false;
            });
        }

        public void Stop()
        {
            isPlaying = false;
            midiOut?.Reset();
        }

        public void Dispose()
        {
            midiOut?.Dispose();
        }
    }
}
