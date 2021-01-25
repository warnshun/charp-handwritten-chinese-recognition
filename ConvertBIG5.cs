using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job4
{
    class ConvertBIG5
    {
        // < 轉換
        public int BIG5toSEQ(int big5)
        {
            int hb, lb, seq;

            hb = big5 / 256;
            lb = big5 % 256;

            if ((lb >= 64) && (lb <= 126))
                seq = (hb - 164) * 157 + (lb - 64 + 1);
            else
                seq = (hb - 164) * 157 + (lb - 161 + 1) + 63;
            seq--;
            if ((seq < 0) || (seq > 5400))
                seq = -1;
            return (seq);
        }

        public int SEQtoBIG5(int a)
        {
            int v = 0, vh, vl;

            if (a < 5401)
            {
                v = a / 157;
                vh = 164 + v;
                v = a % 157;
                if (v <= 62)
                    vl = v + 64;
                else
                    vl = v + 98;
                v = vh * 256 + vl;
            }
            return v;
        }
        // > 轉換
    }
}
