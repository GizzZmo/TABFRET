using System.Collections.Generic;
using System.Threading.Tasks;
using TABFRET.Models;

namespace TABFRET.Services
{
    public interface IMidiParser
    {
        Task<IList<MidiNote>> ParseMidiFileAsync(string path);
    }
}
