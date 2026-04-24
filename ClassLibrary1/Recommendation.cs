namespace SocialNetwork.Core
{
    public class Recommendation
    {
        public int CandidateId { get; set; }
        public string CandidateName { get; set; }
        public int MutualFriendsCount { get; set; }
        public double ConnectivityCoefficient { get; set; }
        public int FriendsCount { get; set; }  // Новое поле
    }
}