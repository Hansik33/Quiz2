namespace Quiz2.Models
{
    internal class InfoWindowModel
    {
        private string _website = "https://www.hansik.pl";

        public string Website
        {
            get => _website;
            set => _website = value;
        }

        private string _informations = " Quiz: Wersja 2.0"
                                     + "\n Twórca: Adam Franz"
                                     + "\n Pytania: Adam Franz"
                                     + "\n Strona WWW: ";

        public string Informations
        {
            get => _informations;
            set => _informations = value;
        }
    }
}