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
            if (midiEvents == null) return;
            var playback = new MidiOutPlayback(midiEvents, midiOut);
            playback.Play();
        }

        public void Stop()
        {
            midiOut?.Reset();
        }

        public void Dispose()
        {
            midiOut?.Dispose();
        }
    }

    // Helper for simple playback (not ideal for real-time sync, for demo only)
    public class MidiOutPlayback
    {
        private MidiEventCollection events;
        private MidiOut midiOut;
        public MidiOutPlayback(MidiEventCollection events, MidiOut midiOut)
        {
            this.events = events;
            this.midiOut = midiOut;
        }
        public void Play()
        {
            // Naive sequential playback; use background thread/timer for better UX
            foreach (var track in events)
            {
                foreach (var midiEvent in track)
                {
                    if (midiEvent is NoteOnEvent noteOn)
                        midiOut.Send(MidiMessage.StartNote(noteOn.NoteNumber, noteOn.Velocity, noteOn.Channel).RawData);
                    else if (midiEvent is NoteEvent noteOff && noteOff.CommandCode == MidiCommandCode.NoteOff)
                        midiOut.Send(MidiMessage.StopNote(noteOff.NoteNumber, 0, noteOff.Channel).RawData);
                    System.Threading.Thread.Sleep(1); // Real code should honor event timing!
                }
            }
        }
    }
}
