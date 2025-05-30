using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TABFRET.Models;

namespace TABFRET.Services
{
    /// <summary>
    /// Provides real-time playback and visualization sync for MIDI notes and tab notes.
    /// </summary>
    public class PlaybackEngine : IDisposable
    {
        private CancellationTokenSource _cts;
        public event Action<long> PlaybackTick; // Notifies UI of the current playback position (in ticks)
        public event Action PlaybackStopped;

        private List<MidiNote> _notes;
        private int _ticksPerQuarter;
        private double _bpm;

        public bool IsPlaying { get; private set; }
        public long CurrentTick { get; private set; }

        public PlaybackEngine()
        {
            _bpm = 120;
        }

        public void Load(List<MidiNote> notes, int ticksPerQuarter, double bpm = 120)
        {
            _notes = notes;
            _ticksPerQuarter = ticksPerQuarter;
            _bpm = bpm;
        }

        public void Play()
        {
            if (_notes == null || _notes.Count == 0) return;
            Stop();
            IsPlaying = true;
            _cts = new CancellationTokenSource();
            Task.Run(() => PlaybackLoop(_cts.Token));
        }

        public void Stop()
        {
            IsPlaying = false;
            _cts?.Cancel();
            PlaybackStopped?.Invoke();
        }

        private async Task PlaybackLoop(CancellationToken token)
        {
            long maxTick = 0;
            foreach (var n in _notes)
                if (n.StartTimeTicks + n.DurationTicks > maxTick)
                    maxTick = n.StartTimeTicks + n.DurationTicks;

            // Calculate ms per tick
            double msPerTick = (60000.0 / _bpm) / _ticksPerQuarter;

            for (long tick = 0; tick <= maxTick; tick++)
            {
                if (token.IsCancellationRequested) break;
                CurrentTick = tick;
                PlaybackTick?.Invoke(tick);
                await Task.Delay((int)msPerTick, token);
            }
            IsPlaying = false;
            PlaybackStopped?.Invoke();
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
