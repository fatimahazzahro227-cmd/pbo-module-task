using System;
using System.Collections.Generic;

class Produk
{
    public string Nama { get; set; }
    public double Harga { get; set; }

    public Produk(string nama, double harga)
    {
        Nama = nama;
        Harga = harga;
    }

    public virtual void InfoProduk()
    {
        Console.WriteLine($"Nama    : {Nama}");
        Console.WriteLine($"Harga   : Rp {Harga:N0}");
        Console.WriteLine($"Kategori: {Kategori()}");
    }

    public virtual string Kategori() => "Produk Umum";
}

class Elektronik : Produk
{
    public int Garansi { get; set; }

    public Elektronik(string nama, double harga, int garansi)
        : base(nama, harga)
    {
        Garansi = garansi;
    }

    public void CekGaransi()
    {
        if (Garansi > 0)
            Console.WriteLine($"[Garansi] {Nama} memiliki garansi {Garansi} bulan.");
        else
            Console.WriteLine($"[Garansi] {Nama} sudah tidak bergaransi.");
    }

    public override string Kategori() => "Elektronik";
}

class Makanan : Produk
{
    public DateTime TanggalKadaluarsa { get; set; }

    public Makanan(string nama, double harga, DateTime tanggalKadaluarsa)
        : base(nama, harga)
    {
        TanggalKadaluarsa = tanggalKadaluarsa;
    }

    public void CekKadaluarsa()
    {
        if (DateTime.Now <= TanggalKadaluarsa)
            Console.WriteLine($"[Kadaluarsa] {Nama} masih layak dikonsumsi hingga {TanggalKadaluarsa:dd-MM-yyyy}.");
        else
            Console.WriteLine($"[Kadaluarsa] {Nama} sudah KADALUARSA sejak {TanggalKadaluarsa:dd-MM-yyyy}!");
    }

    public override string Kategori() => "Makanan";
}

class Laptop : Elektronik
{
    public Laptop(string nama, double harga, int garansi)
        : base(nama, harga, garansi) { }

    public void InstallSoftware(string software)
    {
        Console.WriteLine($"[Laptop] Menginstall '{software}' di {Nama}...");
    }

    public override string Kategori() => "Laptop";
}

class HP : Elektronik
{
    public HP(string nama, double harga, int garansi)
        : base(nama, harga, garansi) { }

    public void Telepon(string nomor)
    {
        Console.WriteLine($"[HP] {Nama} sedang menelepon {nomor}...");
    }

    public override string Kategori() => "HP / Smartphone";
}

class Snack : Makanan
{
    public Snack(string nama, double harga, DateTime kadaluarsa)
        : base(nama, harga, kadaluarsa) { }

    public void Makan()
    {
        Console.WriteLine($"[Snack] Sedang memakan {Nama}... Yum!");
    }

    public override string Kategori() => "Snack";
}

class Minuman : Makanan
{
    public Minuman(string nama, double harga, DateTime kadaluarsa)
        : base(nama, harga, kadaluarsa) { }

    public void Dinginkan()
    {
        Console.WriteLine($"[Minuman] {Nama} sedang didinginkan di kulkas.");
    }

    public override string Kategori() => "Minuman";
}

class Toko
{
    private List<Produk> daftarProduk = new List<Produk>();

    public void TambahProduk(Produk produk)
    {
        daftarProduk.Add(produk);
        Console.WriteLine($"[Toko] '{produk.Nama}' berhasil ditambahkan.");
    }

    public void DaftarProduk()
    {
        Console.WriteLine("\n========================================");
        Console.WriteLine("         DAFTAR PRODUK TOKO");
        Console.WriteLine("========================================");
        foreach (var p in daftarProduk)
        {
            p.InfoProduk();
            Console.WriteLine("----------------------------------------");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Toko toko = new Toko();

        Laptop laptop = new Laptop("ASUS VivoBook 15", 8_500_000, 24);
        HP hp = new HP("Samsung Galaxy A55", 4_200_000, 12);
        Snack snack = new Snack("Chitato Sapi Panggang", 10_000, new DateTime(2025, 12, 31));
        Minuman minum = new Minuman("Pocari Sweat 500ml", 7_000, new DateTime(2025, 6, 30));

        toko.TambahProduk(laptop);
        toko.TambahProduk(hp);
        toko.TambahProduk(snack);
        toko.TambahProduk(minum);

        toko.DaftarProduk();

        Console.WriteLine("\n=== Polymorphism ===");
        List<Produk> semuaProduk = new List<Produk> { laptop, hp, snack, minum };
        foreach (Produk p in semuaProduk)
            Console.WriteLine($"  {p.Nama} → Kategori: {p.Kategori()}");

        Console.WriteLine("\n=== Method Khusus ===");
        laptop.CekGaransi();
        laptop.InstallSoftware("Visual Studio Code");

        hp.CekGaransi();
        hp.Telepon("0812-3456-7890");

        snack.CekKadaluarsa();
        snack.Makan();

        minum.CekKadaluarsa();
        minum.Dinginkan();
    }
}