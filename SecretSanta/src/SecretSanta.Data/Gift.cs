using System;

namespace SecretSanta.Data
{
    public class Gift : FingerPrintEntityBase
    {
        private string _Title = string.Empty;
        public string Title {
            get => _Title;
            set => _Title = value ?? throw new ArgumentNullException(nameof(Title));
        }

        private string _Description = string.Empty;
        public string Description {
            get => _Description;
            set => _Description = value ?? throw new ArgumentNullException(nameof(Description));
        }

        private string _Url = string.Empty;
        public string Url {
            get => _Url;
            set => _Url = value ?? throw new ArgumentNullException(nameof(Url));
        }

#nullable disable
        public User User { get; set; }
#nullable enable
    }
}
