using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_automapper
{
    class AudioAnalyzer
    {
        private NAudio.Wave.WaveStream pcm;

        //TODO: Add various beatmap settings to constructor
        public AudioAnalyzer(string filename, NAudio.Gui.WaveViewer waveViewer)
        {
            waveViewer.SamplesPerPixel = 450;
            waveViewer.WaveStream = new NAudio.Wave.Mp3FileReader(filename);   
            
            //TODO: (1) Retrieve data from the wave viewer.
            //      (2) How do we know at what ms the data will play?
        }

        //For calculating peaks. 

        /*
        double sum = 0;
        for (var i = 0; i < _buffer.length; i = i + 2)
        {
            double sample = BitConverter.ToInt16(_buffer, i) / 32768.0;
            sum += (sample * sample);
        }
        double rms = Math.Sqrt(sum / (_buffer.length / 2));
        var decibel = 20 * Math.Log10(rms);
         */
    }
}
