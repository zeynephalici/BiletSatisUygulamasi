using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiletSatisUygulamasi.Models
{
    public class Bilet
    {
        //Properties:
        public int BiletNo { get; set; }
        public string BuEc { get { return BuEcGetir(); } }
        public string DoBo { get { return DoBoGetir(); } }
        public string MusteriAdi { get; set; }
        public string MusteriSoyadi { get; set; }
        public string MusteriEPosta { get; set; }
        public string MusteriTelefon { get; set; }

        //Actions:
        public void bilet(ref Bilet[] _biletler)
        {
            //kullanicidan Bus. / Econ. ayrimi isteniyor
            Console.WriteLine("Bilet kategorisini giriniz(B / E):");
            string BE = Console.ReadLine();

            //girilen deger kontrol ediliyor
            string bosKoltuklar = "";
            switch (BE)
            {
                case "B":
                case "E":
                    //bos koltuklar listeleniyor
                    bosKoltuklar = BosKoltuklariGetir(_biletler, BE);
                    Console.WriteLine(BE + " kategorisindeki boş koltuklar: " + bosKoltuklar);

                    //kullanicidan koltuk no isteniyor
                    Console.WriteLine("Hangi koltuk ile devam edilsin?");
                    string istenenKoltukNo = Console.ReadLine();

                    //girilen koltuk no bos mu
                    if (BosKoltuklariGetir(_biletler, BE, istenenKoltukNo) == true)
                    {
                        //musteri bilgileri aliniyor
                        Console.WriteLine("Müşterinin adını giriniz:");
                        string Ad = Console.ReadLine();
                        Console.WriteLine("Müşterinin soyadını giriniz:");
                        string Soyad = Console.ReadLine();
                        Console.WriteLine("Müşterinin e-postasını giriniz:");
                        string EPosta = Console.ReadLine();
                        Console.WriteLine("Müşterinin telefonunu giriniz:");
                        string Telefon = Console.ReadLine();

                        //kaydet
                        bool kaydetSonucu = BiletKaydet(istenenKoltukNo, Ad, Soyad, EPosta, Telefon, ref _biletler);
                        if (kaydetSonucu == true)
                        {
                            Console.WriteLine(istenenKoltukNo + " numaralı koltuk rezerve edilmiştir.");
                        }
                        else
                        {
                            Console.WriteLine(istenenKoltukNo + " numaralı koltuk rezerve edilirken hata oluştu.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Sadece listelenen koltuklar içinden seçim yapabilirsiniz!");
                    }
                    break;
                default:
                    Console.WriteLine("Tanımlanmayan kategori girdiniz!");
                    break;
            }
        }

        public void rapor(Bilet[] _biletler)
        {
            for (int i = 0; i < _biletler.Length; i++)
            {
                Bilet blt = new Bilet();
                blt = _biletler[i];

                if (blt != null && blt.BiletNo > 0)
                    BiletiEkranaTekSatirdaYaz(blt);
                else
                    Console.WriteLine((i + 1) + " > " + ((i < 8) ? "B" : "E") + " - Boş - ");
            }
        }

        public bool cikis()
        {
            return false;
        }

        public void arama(Bilet[] _biletler)
        {
            Console.WriteLine("Aranacak değeri giriniz:");
            string aranacakDeger = Console.ReadLine();

            bool kayitVarMi = false;
            for (int i = 0; i < _biletler.Length; i++)
            {
                Bilet blt = new Bilet();
                blt = _biletler[i];

                if (blt != null)
                {
                    if (blt.BiletNo.ToString() == aranacakDeger || blt.MusteriAdi.Contains(aranacakDeger) == true || blt.MusteriSoyadi.Contains(aranacakDeger) == true || blt.MusteriEPosta.Contains(aranacakDeger) == true || blt.MusteriTelefon.Contains(aranacakDeger) == true || blt.DoBo.ToUpper().Contains(aranacakDeger.ToUpper()) == true || blt.BuEc == aranacakDeger)
                    {
                        BiletiEkranaTekSatirdaYaz(blt);
                        kayitVarMi = true;
                    }
                }
            }

            if (kayitVarMi == false) Console.WriteLine("Kayıt bulunamadı!");
        }

        public void sil(ref Bilet[] _biletler)
        {
            //kullanidan silinecek koltuk isteniyor
            Console.WriteLine("Hangi koltuk silinsin?");
            string silinecekKoltukNo = Console.ReadLine();

            //bilet bilgileri ekrana yazdiriliyor
            Bilet blt = new Bilet();
            blt = _biletler[int.Parse(silinecekKoltukNo) - 1];
            if (blt == null)
            {
                Console.WriteLine("Bilet bulunamadı!");
            }
            else
            {
                BiletiEkranaTekSatirdaYaz(blt);

                //silme oncesi kullaniciya son uyari yapiliyor
                Console.WriteLine("Bu bilet silinecek, emin misiniz? (E/H)");
                char EH = Console.ReadKey().KeyChar;

                //silme veya vazgecme islemleri
                switch (EH)
                {
                    case 'E':
                        _biletler[int.Parse(silinecekKoltukNo) - 1] = null;
                        Console.WriteLine(silinecekKoltukNo + " numaralı bilet başarıyla silindi!");
                        break;
                    case 'H':
                        Console.WriteLine(silinecekKoltukNo + " numaralı bilet korundu!");
                        break;
                    default:
                        Console.WriteLine("Tanımlanmayan değer girildi!");
                        break;
                }
            }
        }

        public void duzenle(ref Bilet[] _biletler)
        {
            //kullanidan duzenlenecek koltuk isteniyor
            Console.WriteLine("Hangi koltuk bilgileri değiştirilecek?");
            string duzenlenecekKoltukNo = Console.ReadLine();

            //bilet bilgileri ekrana yazdiriliyor
            Bilet blt = new Bilet();
            blt = _biletler[int.Parse(duzenlenecekKoltukNo) - 1];
            if (blt == null)
            {
                Console.WriteLine("Bilet bulunamadı!");
            }
            else
            {
                BiletiEkranaTekSatirdaYaz(blt);

                //kullanidan teyit aliniyor
                Console.WriteLine("Bu bilet ile devam edilsin mi? (E/H)");
                char EH = Console.ReadKey().KeyChar;

                //ilgili bilet duzenleniyor veya korunuyor
                switch (EH)
                {
                    case 'E':
                        //musteri bilgileri aliniyor
                        Console.WriteLine("Müşterinin adını giriniz:");
                        string Ad = Console.ReadLine();
                        Console.WriteLine("Müşterinin soyadını giriniz:");
                        string Soyad = Console.ReadLine();
                        Console.WriteLine("Müşterinin e-postasını giriniz:");
                        string EPosta = Console.ReadLine();
                        Console.WriteLine("Müşterinin telefonunu giriniz:");
                        string Telefon = Console.ReadLine();

                        //dizinin ilgili rafi eziliyor
                        if (Ad != "") _biletler[int.Parse(duzenlenecekKoltukNo) - 1].MusteriAdi = Ad;
                        if (Soyad != "") _biletler[int.Parse(duzenlenecekKoltukNo) - 1].MusteriSoyadi = Soyad;
                        if (EPosta != "") _biletler[int.Parse(duzenlenecekKoltukNo) - 1].MusteriEPosta = EPosta;
                        if (Telefon != "") _biletler[int.Parse(duzenlenecekKoltukNo) - 1].MusteriTelefon = Telefon;

                        //kullanici bilgilendiriliyor
                        Console.WriteLine(duzenlenecekKoltukNo + " numaralı bilet başarıyla güncellendi!");
                        break;
                    case 'H':
                        Console.WriteLine(duzenlenecekKoltukNo + " numaralı bilet korundu!");
                        break;
                    default:
                        Console.WriteLine("Tanımlanmayan değer girildi!");
                        break;
                }
            }
        }

        public void BiletiEkranaTekSatirdaYaz(Bilet blt)
        {
            if (blt != null)
            {
                Console.WriteLine(blt.BiletNo + " > " + blt.BuEc + " - " + blt.DoBo + " - " + (blt.MusteriAdi + " " + blt.MusteriSoyadi) + " (" + blt.MusteriEPosta + " / " + blt.MusteriTelefon + ")");
            }
        }

        private string BuEcGetir()
        {
            string result = null;

            if (BiletNo > 0 && BiletNo < 9)
            {
                result = "B";
            }
            else if (BiletNo >= 9 && BiletNo <= 30)
            {
                result = "E";
            }

            return result;
        }

        private string DoBoGetir()
        {
            string result = null;

            if (BiletNo > 0 && BiletNo <= 30)
            {
                result = "Dolu";
            }
            else
            {
                result = "Boş";
            }

            return result;
        }

        public bool BosKoltuklariGetir(Bilet[] _biletler, string _BE, string _istenenKoltukNo)
        {
            bool result = false;

            string bosKoltuklar = BosKoltuklariGetir(_biletler, _BE);
            if (bosKoltuklar.Contains(" " + _istenenKoltukNo + " ") == true)
            {
                //girilen koltuk no bos oldugu icin true yapildi
                result = true;
            }

            return result;
        }

        public string BosKoltuklariGetir(Bilet[] _biletler, string _BE)
        {
            string result = " ";

            for (int i = ((_BE == "B") ? 0 : 8); i < ((_BE == "B") ? 8 : 30); i++)
            {
                if (_biletler[i] == null)
                {
                    result += (i + 1) + " ";
                }
            }

            return result;
        }

        private bool BiletKaydet(string _istenenKoltukNo, string _ad, string _soyad, string _ePosta, string _telefon, ref Bilet[] _biletler)
        {
            bool result = false;

            try
            {
                Bilet _blt = new Bilet();
                _blt.BiletNo = int.Parse(_istenenKoltukNo);
                _blt.MusteriAdi = _ad;
                _blt.MusteriSoyadi = _soyad;
                _blt.MusteriEPosta = _ePosta;
                _blt.MusteriTelefon = _telefon;

                _biletler[int.Parse(_istenenKoltukNo) - 1] = _blt;

                result = true;
            }
            catch
            {
                result = false;
            }

            return result;
        }
    }
}
