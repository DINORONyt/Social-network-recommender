using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Core
{
    public class NetworkManager
    {
        private readonly Dictionary<int, User> _users = new();
        private readonly Random _random = new(42);

        // Список русских имён и фамилий
        private readonly string[] _firstNames = {
            "Александр", "Дмитрий", "Максим", "Сергей", "Андрей",
            "Алексей", "Артём", "Илья", "Кирилл", "Михаил",
            "Никита", "Матвей", "Роман", "Егор", "Арсений",
            "Иван", "Денис", "Евгений", "Владислав", "Павел",
            "Анна", "Мария", "Екатерина", "Анастасия", "Дарья",
            "Александра", "Полина", "София", "Виктория", "Елизавета",
            "Варвара", "Алиса", "Ксения", "Татьяна", "Юлия"
        };

        private readonly string[] _lastNames = {
            "Иванов", "Смирнов", "Кузнецов", "Попов", "Васильев",
            "Петров", "Соколов", "Михайлов", "Новиков", "Федоров",
            "Морозов", "Волков", "Алексеев", "Лебедев", "Семенов",
            "Егоров", "Павлов", "Козлов", "Степанов", "Николаев",
            "Соловьёва", "Васильева", "Зайцева", "Павлова", "Михайлова",
            "Новикова", "Федорова", "Морозова", "Волкова", "Алексеева"
        };

        public int TotalUsers => _users.Count;

        public void GenerateRandomNetwork(int userCount = 1000, double connectionProbability = 0.015)
        {
            _users.Clear();

            // Генерация пользователей с русскими именами
            for (int i = 1; i <= userCount; i++)
            {
                string firstName = _firstNames[_random.Next(_firstNames.Length)];
                string lastName = _lastNames[_random.Next(_lastNames.Length)];
                string fullName = $"{lastName} {firstName}";

                _users[i] = new User(i, fullName);
            }

            var ids = _users.Keys.ToList();
            for (int i = 0; i < ids.Count; i++)
            {
                for (int j = i + 1; j < ids.Count; j++)
                {
                    if (_random.NextDouble() < connectionProbability)
                    {
                        _users[ids[i]].Friends.Add(ids[j]);
                        _users[ids[j]].Friends.Add(ids[i]);
                    }
                }
            }
        }

        public List<Recommendation> GetTopRecommendations(int userId, int topN = 10)
        {
            if (!_users.ContainsKey(userId))
                throw new ArgumentException("Пользователь не найден.");

            var targetUser = _users[userId];
            var candidates = new HashSet<int>();
            var visited = new HashSet<int> { userId };
            var queue = new Queue<(int NodeId, int Distance)>();
            queue.Enqueue((userId, 0));

            while (queue.Count > 0)
            {
                var (current, dist) = queue.Dequeue();
                if (dist == 2)
                {
                    candidates.Add(current);
                    continue;
                }

                if (!_users.ContainsKey(current)) continue;

                foreach (var friendId in _users[current].Friends)
                {
                    if (!visited.Contains(friendId))
                    {
                        visited.Add(friendId);
                        queue.Enqueue((friendId, dist + 1));
                    }
                }
            }

            var recommendations = new List<Recommendation>();
            foreach (var candId in candidates)
            {
                if (candId == userId || targetUser.Friends.Contains(candId))
                    continue;

                var mutualCount = targetUser.Friends.Intersect(_users[candId].Friends).Count();
                var coeff = mutualCount / (double)Math.Max(1, _users[candId].Friends.Count);

                recommendations.Add(new Recommendation
                {
                    CandidateId = candId,
                    CandidateName = _users[candId].Name,
                    MutualFriendsCount = mutualCount,
                    ConnectivityCoefficient = coeff,
                    FriendsCount = _users[candId].Friends.Count
                });
            }

            return recommendations
                .OrderByDescending(r => r.MutualFriendsCount)
                .ThenByDescending(r => r.ConnectivityCoefficient)
                .ThenByDescending(r => r.FriendsCount)
                .Take(topN)
                .ToList();
        }

        public NetworkStatistics CalculateGlobalStatistics(int topN = 10)
        {
            int totalRecs = 0;
            int usersWithRecs = 0;

            foreach (var user in _users.Values)
            {
                var recs = GetTopRecommendations(user.Id, topN);
                if (recs.Count > 0)
                {
                    usersWithRecs++;
                    totalRecs += recs.Count;
                }
            }

            return new NetworkStatistics
            {
                TotalUsers = _users.Count,
                UsersWithRecommendations = usersWithRecs,
                PercentageWithRecommendations = _users.Count > 0
                    ? (double)usersWithRecs / _users.Count * 100 : 0,
                AverageRecommendationsPerUser = _users.Count > 0
                    ? (double)totalRecs / _users.Count : 0
            };
        }

        public User GetUser(int id) => _users.GetValueOrDefault(id);
        public IReadOnlyDictionary<int, User> GetAllUsers() => _users;
    }

    public class NetworkStatistics
    {
        public int TotalUsers { get; set; }
        public int UsersWithRecommendations { get; set; }
        public double PercentageWithRecommendations { get; set; }
        public double AverageRecommendationsPerUser { get; set; }
    }
}