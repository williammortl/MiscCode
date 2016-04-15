namespace SoundGenerator
{
    using System;
    using System.IO;
    using System.Media;

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
            int numBytes = numSamples * 4;
            int[] headerBytes = { 0X46464952, 36 + numBytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, numBytes };

            // generate sound
            using (MemoryStream ms = new MemoryStream(44 + numBytes))
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    for (int i = 0; i < headerBytes.Length; i++)
                    {
                        bw.Write(headerBytes[i]);
                    }
                    for (int T = 0; T < numSamples; T++)
                    {
                        short sampleData = System.Convert.ToInt16(actualAmplitude * Math.Sin(deltaFT * T));
                        bw.Write(sampleData);
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
    }
}
