using System.Collections.Generic;

namespace AsmGrammar
{
    class State
    {
        public byte[] Reg;
        public Dictionary<string, int> Labels;
        public int Ip;

        public State()
        {
            Ip = 0;
            Reg = new byte[16];
            Labels = new Dictionary<string, int>();
        }
    }
}