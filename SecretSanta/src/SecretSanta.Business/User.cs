using System.Collections.ObjectModel;

namespace SecretSanta.Business
{
    public class User
    {
        private readonly int _Id;
        private string _FirstName;
        private string _LastName;
        private Collection<Gift> _Gifts;
    }
}