namespace SoundGenerator
{
    using System;
    using System.IO;
    using System.Media;
    using System.Text;
    using System.Threading;

    /// <summary>
    /// The class for console application
    /// </summary>
    class Program
    {

        /// <summary>
        /// The main entry point for the console application
        /// </summary>
        /// <param name="args">command line args</param>
        static void Main(string[] args)
        {
            Console.WriteLine("Testing sound...");
            Random r = new Random();
            for (int i = 0; i <= 1000; i++)
            {
                int n = r.Next(100, 4000);
                Program.PlaySound(1000, n, 500);
            }
            Console.WriteLine("Testing complete.");
        }

        /// <summary>
        /// Creates and plays audio
        /// </summary>
        /// <param name="amplitude">amplitude of the audio</param>
        /// <param name="frequency">sound frequency</param>
        /// <param name="milliseconds">milliseconds to play</param>
        public static void PlaySound(int amplitude, int frequency, int milliseconds)
        {

            // var init
            double actualAmplitude = ((amplitude * (System.Math.Pow(2, 15))) / 1000) - 1;
            double deltaFT = 2 * Math.PI * frequency / 44100.0;
            int numSamples = 441 * milliseconds / 10;
            int numSamplesWhite = 441 * 20 / 10;
            int numBytes = numSamples * 4;

            // generate sound
            using (MemoryStream ms = new MemoryStream(44 + numBytes))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    WriteWavHeader(ms, false, 1, 16, 44100, 1 * (numSamples + numSamplesWhite));
                    for (int j = 0; j < numSamples; j++)
                    {
                        double t = (double)j / (double)44100;
                        short s = (short)(amplitude * (Math.Sin(t * frequency * 2.0 * Math.PI)));
                        bw.Write(s);
                    }
                    for (int j = 0; j < numSamplesWhite; j++)
                    {
                        short sampleData = 0;
                        bw.Write(sampleData);
                    }
                    bw.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    // actually play the sound
                    using (SoundPlayer sp = new SoundPlayer(ms))
                    {
                        sp.PlaySync();
                    }
                }
            }
        }

        private static void WriteWavHeader(MemoryStream stream, bool isFloatingPoint, ushort channelCount, ushort bitDepth, int sampleRate, int totalSampleCount)
        {
            stream.Position = 0;

            // RIFF header.
            // Chunk ID.
            stream.Write(Encoding.ASCII.GetBytes("RIFF"), 0, 4);

            // Chunk size.
            stream.Write(BitConverter.GetBytes(((bitDepth / 8) * totalSampleCount) + 36), 0, 4);

            // Format.
            stream.Write(Encoding.ASCII.GetBytes("WAVE"), 0, 4);



            // Sub-chunk 1.
            // Sub-chunk 1 ID.
            stream.Write(Encoding.ASCII.GetBytes("fmt "), 0, 4);

            // Sub-chunk 1 size.
            stream.Write(BitConverter.GetBytes(16), 0, 4);

            // Audio format (floating point (3) or PCM (1)). Any other format indicates compression.
            stream.Write(BitConverter.GetBytes((ushort)(isFloatingPoint ? 3 : 1)), 0, 2);

            // Channels.
            stream.Write(BitConverter.GetBytes(channelCount), 0, 2);

            // Sample rate.
            stream.Write(BitConverter.GetBytes(sampleRate), 0, 4);

            // Bytes rate.
            stream.Write(BitConverter.GetBytes(sampleRate * channelCount * (bitDepth / 8)), 0, 4);

            // Block align.
            stream.Write(BitConverter.GetBytes((ushort)channelCount * (bitDepth / 8)), 0, 2);

            // Bits per sample.
            stream.Write(BitConverter.GetBytes(bitDepth), 0, 2);



            // Sub-chunk 2.
            // Sub-chunk 2 ID.
            stream.Write(Encoding.ASCII.GetBytes("data"), 0, 4);

            // Sub-chunk 2 size.
            stream.Write(BitConverter.GetBytes((bitDepth / 8) * totalSampleCount), 0, 4);
        }

    }
}
