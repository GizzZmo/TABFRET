using System.Collections.Generic;
using System.Threading.Tasks;
using TABFRET.Models;

public interface IMidiParser
{
    Task<IList<MidiNote>> ParseMidiFileAsync(string path);
}
