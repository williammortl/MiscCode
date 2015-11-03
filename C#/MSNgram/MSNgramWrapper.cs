namespace MSNgram
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// This class wraps the Microsoft Web N-Gram Service API
    /// </summary>
    public sealed class MSNgramWrapper
    {

        /// <summary>
        /// The key for the App.config file in the appSettings section
        /// </summary>
        private const String KEY_NGRAM_TOKEN = "MSNGramToken";

        /// <summary>
        /// The key for the web service
        /// </summary>
        private static String NGRAM_TOKEN = ConfigurationManager.AppSettings[KEY_NGRAM_TOKEN].Trim();

        /// <summary>
        /// The web service client
        /// </summary>
        private WiabServiceClient client;

        /// <summary>
        /// Constructor
        /// </summary>
        public MSNgramWrapper()
        {
            this.client = new WiabServiceClient();
        }

        /// <summary>
        /// Gets the log(P) probability
        /// </summary>
        /// <param name="model">
        ///     the model to use, such as: urn:ngram:bing-body:2013-12:n - where n
        ///     is the order of the ngram, for example urn:ngram:bing-body:2013-12:3
        ///     represents a trigram
        /// </param>
        /// <param name="phrase">the phrase to test</param>
        /// <returns>the log(P) probability</returns>
        public float GetProbability(String model, String phrase)
        {
            return client.GetProbability(MSNgramWrapper.NGRAM_TOKEN, model, phrase);
        }

        /// <summary>
        /// Gets the log(P) probabilities
        /// </summary>
        /// <param name="model">
        ///     the model to use, such as: urn:ngram:bing-body:2013-12:n - where n
        ///     is the order of the ngram, for example urn:ngram:bing-body:2013-12:3
        ///     represents a trigram
        /// </param>
        /// <param name="phrases">array of phrases to test</param>
        /// <returns>the log(P) probability</returns>
        public float[] GetProbabilities(String model, String[] phrases)
        {
            return client.GetProbabilities(MSNgramWrapper.NGRAM_TOKEN, model, phrases);
        }


    }
}
