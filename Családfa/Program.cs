using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Családfa
{
    class Program
    {
        static Random r = new Random();
        static Kalap<Infok> lehet = new Kalap<Infok>();
        #region Beolvasáshoz használt függvények
        public static void Beolvasás(Kalap<string> kalap, string mit)
        {
            StreamReader r = new StreamReader(mit);
            while (!r.EndOfStream) { kalap.Push(r.ReadLine());}
        }
        #endregion

        public static void Párcsinálás(Infok alany, Kalap<string> V, Kalap<string> F, Kalap<string> L)
        {
            Infok tars = new Infok(V.Pop(), alany.Fe ? L.Pop() : F.Pop(), !alany.Fe);
            
            for (int i = 0; i < r.Next(1,3); i++)
            {
                if (alany.Fe)
                {
                    int nem = r.Next(2);
                    Infok gyerek = new Infok(alany.V, nem == 0 ? F.Pop() : L.Pop(), nem == 0);
                    Console.WriteLine($"\"{alany.V} {alany.N}\" -> \"{gyerek.V} {gyerek.N}\"");
                    Console.WriteLine($"\"{tars.V} {tars.N}\" -> \"{gyerek.V} {gyerek.N}\"");
                    lehet.Push(gyerek);
                }
                else
                {
                    int nem = r.Next(2);
                    Infok gyerek = new Infok(tars.V, nem == 0 ? F.Pop() : L.Pop(), nem == 0 );
                    Console.WriteLine($"\"{alany.V} {alany.N}\" -> \"{gyerek.V} {gyerek.N}\"");
                    Console.WriteLine($"\"{tars.V} {tars.N}\" -> \"{gyerek.V} {gyerek.N}\"");
                    lehet.Push(gyerek);
                }
            }
        }
        static void Main(string[] args)
        {
            #region Beolvasás
            Kalap<string> F = new Kalap<string>();
            Beolvasás(F, "sokfiunev.txt");
            Kalap<string> L = new Kalap<string>();
            Beolvasás(L, "soklanynev.txt");
            Kalap<string> V = new Kalap<string>();
            Beolvasás(V, "veznev.txt");
            #endregion

            int er = r.Next(2);
            Infok elso = new Infok(V.Pop(), er == 0 ? F.Pop() : L.Pop(), er==0);
            lehet.Push(elso);
            while (V.Count>0&&lehet.Count>0)
            {
                Párcsinálás(lehet.Pop(), V, F, L);
            }
            Console.WriteLine($"Vezeték nevek száma: {V.Count}, Lehetséges kapcsolatok száma: {lehet.Count}");
        }
    }
}
