using System.Collections.Generic;

namespace MustacheGrammar
{
    class State
    {
        public Dictionary<string, string> Labels;
        public int Ip;

        public State()
        {
            Ip = 0;
            Labels = new Dictionary<string, string>();
        }
    }
}