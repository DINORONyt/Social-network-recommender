using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetwork.Core
{
    public class NetworkManager
    {
        private readonly Dictionary<int, User> _users = new();
        private readonly Random _random = new(42); // Фиксированный seed для воспроизводимости

        public int TotalUsers => _users.Count;

        // Генерация тестовой сети (1000 пользователей, случайные связи)
        public void GenerateRandomNetwork(int userCount = 1000, double connectionProbability = 0.015)
        {
            _users.Clear();
            for (int i = 1; i <= userCount; i++)
                _users[i] = new User(i, $"User_{i}");

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

        // Основная логика: поиск рекомендаций через BFS на расстоянии 2
        public List<Recommendation> GetTopRecommendations(int userId, int topN = 10)
        {
            if (!_users.ContainsKey(userId))
                throw new ArgumentException("Пользователь не найден.");

            var targetUser = _users[userId];
            var candidates = new HashSet<int>();
            var visited = new HashSet<int> { userId };
            var queue = new Queue<(int NodeId, int Distance)>();
            queue.Enqueue((userId, 0));

            // BFS для поиска пользователей на расстоянии ровно 2 (друзья друзей)
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

            // Оценка кандидатов
            var recommendations = new List<Recommendation>();
            foreach (var candId in candidates)
            {
                // Фильтрация: исключаем уже существующих друзей и самого пользователя
                if (candId == userId || targetUser.Friends.Contains(candId))
                    continue;

                var mutualCount = targetUser.Friends.Intersect(_users[candId].Friends).Count();
                // Коэффициент связности: доля общих друзей от общего числа друзей кандидата
                var coeff = mutualCount / (double)Math.Max(1, _users[candId].Friends.Count);

                recommendations.Add(new Recommendation
                {
                    CandidateId = candId,
                    CandidateName = _users[candId].Name,
                    MutualFriendsCount = mutualCount,
                    ConnectivityCoefficient = coeff
                });
            }

            // Ранжирование и возврат топ-N
            return recommendations
                .OrderByDescending(r => r.MutualFriendsCount)
                .ThenByDescending(r => r.ConnectivityCoefficient)
                .Take(topN)
                .ToList();
        }

        // Статистика по всей сети
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

        // Вспомогательные методы для визуализации
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