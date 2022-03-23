using BiletSatisUygulamasi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiletSatisUygulamasi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //biletler dizisi olusturuluyor
            Bilet[] biletler = new Bilet[30];

            //listener (dinleyici)
            bool whileKontrol = true;
            while (whileKontrol == true)
            {
                //kullanici bilgi
                Console.WriteLine("bilet - rapor - temizle - arama - sil - duzenle - cikis:");

                //komut okunuyor
                string komut = Console.ReadLine();

                //komuta gore switch - case
                Bilet blt = new Bilet();
                switch (komut)
                {
                    case "bilet":
                        blt.bilet(ref biletler);
                        break;
                    case "rapor":
                        blt.rapor(biletler);
                        break;
                    case "temizle":
                        Console.Clear();
                        break;
                    case "arama":
                        blt.arama(biletler);
                        break;
                    case "sil":
                        blt.sil(ref biletler);
                        break;
                    case "duzenle":
                        blt.duzenle(ref biletler);
                        break;
                    case "cikis":
                        whileKontrol = blt.cikis();
                        //Environment.Exit(0);
                        //Console.Clear();
                        break;
                    default:
                        Console.WriteLine("Hatalı komut girdiniz!");
                        break;
                }
            }
        }
    }
}
