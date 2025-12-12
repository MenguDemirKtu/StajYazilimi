namespace UniStaj
{
    public class YetkiYokHatasi : Exception
    {
        public YetkiYokHatasi() : base("Yetkiniz yok.") { }
        public YetkiYokHatasi(string message) : base(message) { }
        public YetkiYokHatasi(string message, Exception inner) : base(message, inner) { }

        public YetkiYokHatasi(Exception ex)
      : base("Bu alana erişme yetkiniz yok", ex) { }
    }

}
