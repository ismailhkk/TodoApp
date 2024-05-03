namespace TodoApp
{
    public class Todo 
    {
        public string Task { get; set; } 
        public bool IsComplete { get; set; } 
        public DateTime Deadline { get; set; } 
    }

    internal class Program
    {
        static List<Todo> todos = new List<Todo>();

        static void Main(string[] args)
        {
            VerileriYukle();
            MenuyeGec();
        }

        static void MenuyeGec()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Todo Uygulaması");
                Console.WriteLine("****************************");
                Console.WriteLine("1. Yapılacak işleri listele");
                Console.WriteLine("2. Yeni iş ekle");
                Console.WriteLine("3. İş tamamlandı olarak işaretle");
                Console.WriteLine("4. İş düzenle");
                Console.WriteLine("5. İş sil");
                Console.WriteLine("6. İş listesini temizle");
                Console.WriteLine("7. Bugün bitirmeniz gereken işler");
                Console.WriteLine("8. İşleri ayrı ayrı listele");
                Console.WriteLine("9. Çıkış");

                Console.Write("\nSeçiminiz: ");
                char secim = Console.ReadKey().KeyChar;
                Console.WriteLine();

                switch (secim)
                {
                    case '1':
                        YapilacakIsleriListele();
                        break;
                    case '2':
                        YeniIsEkle();
                        break;
                    case '3':
                        IsTamamlandi();
                        break;
                    case '4':
                        IsDuzenle();
                        break;
                    case '5':
                        IsSil();
                        break;
                    case '6':
                        IsListesiniTemizle();
                        break;
                    case '7':
                        BugunBitirmenizGerekenIsler();
                        break;
                    case '8':
                        IsleriAyriAyriListele();
                        break;
                    case '9':
                        TxtKaydet();
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Geçersiz seçim!");
                        break;
                }

                Console.WriteLine("\nDevam etmek için bir tuşa basın...");
                Console.ReadKey();
            }



        }

        static void YapilacakIsleriListele()
        {
            Console.WriteLine("Yapılacak İşler");
            Console.WriteLine("****************************");
            foreach (var todo in todos)
            {
                Console.WriteLine($"{todo.Task} - {(todo.IsComplete ? "Tamamlandı" : "Tamamlanmadı")} - Bitiş Tarihi: {todo.Deadline.ToShortDateString()}");
            }
        }

        static void YeniIsEkle()
        {
            Console.Write("Yeni iş girin: ");
            string yeniIs = Console.ReadLine();
            Console.Write("Bitiş tarihini girin (GG/AA/YYYY): ");
            DateTime deadline;
            while (!DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out deadline))
            {
                Console.WriteLine("Geçersiz tarih.. Tekrar deneyin.");
                Console.Write("Bitiş tarihini girin (GG/AA/YYYY): ");
            }

            todos.Add(new Todo { Task = yeniIs, IsComplete = false, Deadline = deadline });
            TxtKaydet();
            Console.WriteLine("İş eklendi.");
        }

        static void IsTamamlandi()
        {
            Console.WriteLine("Tamamlandı olarak işaretlemek istediğiniz işin numarasını girin:");
            int index = int.Parse(Console.ReadLine());
            if (index >= 0 && index < todos.Count)
            {
                todos[index].IsComplete = true;
                Console.WriteLine("Tamamlandı olarak işaretlendi.");
            }
            else
            {
                Console.WriteLine("Geçersiz iş numarası.");
            }
        }

        static void IsDuzenle()
        {
            Console.WriteLine("Düzenlemek istediğiniz işin numarasını girin:");
            int index = int.Parse(Console.ReadLine());
            if (index >= 0 && index < todos.Count)
            {
                Console.Write("Yeni iş girin: ");
                todos[index].Task = Console.ReadLine();
                Console.WriteLine("İş düzenlendi.");
                TxtKaydet();
            }
            else
            {
                Console.WriteLine("Geçersiz iş numarası.");
            }
        }

        static void IsSil()
        {
            Console.WriteLine("Silmek istediğiniz işin numarasını girin:");
            int index = int.Parse(Console.ReadLine());
            if (index >= 0 && index < todos.Count)
            {
                todos.RemoveAt(index);
                TxtKaydet();
                Console.WriteLine("İş silindi.");
            }
            else
            {
                Console.WriteLine("Geçersiz iş numarası.");
            }
        }

        static void IsListesiniTemizle()
        {
            Console.WriteLine("İş listesi temizlendi.");
            todos.Clear();
        }

        static void BugunBitirmenizGerekenIsler()
        {
            Console.WriteLine("Bugün Bitirmeniz Gereken İşler");
            Console.WriteLine("****************************");
            foreach (var todo in todos)
            {
                if (todo.Deadline.Date == DateTime.Today && !todo.IsComplete)
                {
                    Console.WriteLine($"{todo.Task} - Bitiş Tarihi: {todo.Deadline.ToShortDateString()}");
                }
            }
        }

        static void IsleriAyriAyriListele()
        {
            Console.WriteLine("İşleri Ayırt Ayırt Listele");
            Console.WriteLine("****************************");
            Console.WriteLine("(T)üm işleri listele - (A)ktif işleri listele - (B)iten işleri listele - (M)enüye dön");
            Console.Write("Seçiminiz: ");
            char secim = Console.ReadKey().KeyChar;
            Console.WriteLine();

            switch (secim)
            {
                case 'T':
                    YapilacakIsleriListele();
                    break;
                case 'A':
                    AktifIsleriListele();
                    break;
                case 'B':
                    BitenIsleriListele();
                    break;
                case 'M':
                    return;
                default:
                    Console.WriteLine("Geçersiz seçim");
                    break;
            }
        }

        static void AktifIsleriListele()
        {
            Console.WriteLine("Aktif İşler");
            Console.WriteLine("****************************");
            foreach (var todo in todos.Where(t => !t.IsComplete))
            {
                Console.WriteLine($"{todo.Task} - Bitiş Tarihi: {todo.Deadline.ToShortDateString()}");
            }
        }

        static void BitenIsleriListele()
        {
            Console.WriteLine("Biten İşler");
            Console.WriteLine("****************************");
            foreach (var todo in todos.Where(t => t.IsComplete))
            {
                Console.WriteLine($"{todo.Task} - Bitiş Tarihi: {todo.Deadline.ToShortDateString()}");
            }
        }

        static void VerileriYukle()
        {
            
                using (StreamReader reader = new StreamReader("todos.txt"))
                {
                    string satirlar;
                    while (!reader.EndOfStream)
                    {
                        Console.WriteLine("satır:  "+reader.ReadLine());
                    }
                }
            
        }

        static void TxtKaydet()
        {
            using (StreamWriter writer = new StreamWriter("todos.txt"))
            {
                foreach (var todo in todos)
                {
                    writer.WriteLine($"{todo.Task} | {todo.IsComplete} | {todo.Deadline}");
                }
            }
        }
    }
}
