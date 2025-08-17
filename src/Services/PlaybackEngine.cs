using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TABFRET.Models;

namespace TABFRET.Services
{
    /// <summary>
    /// Provides real-time playback and visualization sync for MIDI notes and tab notes, as well as metronome events, scrubbing, and BPM.
    /// </summary>
    public class PlaybackEngine : IDisposable
    {
    private CancellationTokenSource _cts = new();
    public event Action<long> PlaybackTick = delegate { };
    public event Action PlaybackStopped = delegate { };
    public event Action<long> MetronomeTick = delegate { };

    private List<MidiNote> _notes = new();
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
            CurrentTick = 0;
        }

        public void SetBpm(double bpm)
        {
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

        public void SeekToTick(long tick)
        {
            CurrentTick = tick;
            PlaybackTick?.Invoke(CurrentTick);
        }

        private async Task PlaybackLoop(CancellationToken token)
            {
                try
                {
                    long maxTick = 0;
                    foreach (var n in _notes)
                        if (n.StartTimeTicks + n.DurationTicks > maxTick)
                            maxTick = n.StartTimeTicks + n.DurationTicks;

                    double msPerTick = (60000.0 / _bpm) / _ticksPerQuarter;
                    for (long tick = CurrentTick; tick <= maxTick; tick++)
                    {
                        if (token.IsCancellationRequested) break;
                        CurrentTick = tick;
                        PlaybackTick?.Invoke(tick);
                        if (tick % _ticksPerQuarter == 0) MetronomeTick?.Invoke(tick);
                        await Task.Delay((int)msPerTick, token);
                    }
                }
                catch (Exception)
                {
                    PlaybackStopped?.Invoke();
                }
                finally
                {
                    IsPlaying = false;
                    PlaybackStopped?.Invoke();
                }
            }

        public void Dispose()
        {
            Stop();
        }
    }
}
