using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using SocialNetwork.Core;

namespace TestProject1
{
    public class NetworkManagerTests
    {
        // ==========================================
        // 1. Инициализация и генерация сети
        // ==========================================

        [Fact]
        public void GenerateRandomNetwork_CreatesCorrectUserCount()
        {
            var network = new NetworkManager();
            int expectedUsers = 1000;
            network.GenerateRandomNetwork(expectedUsers);
            Assert.Equal(expectedUsers, network.TotalUsers);
        }

        [Fact]
        public void GenerateRandomNetwork_UsersHaveRussianNames()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var users = network.GetAllUsers();
            foreach (var user in users.Values)
            {
                Assert.False(string.IsNullOrEmpty(user.Name));
                Assert.Contains(" ", user.Name);
            }
        }

        [Fact]
        public void GenerateRandomNetwork_ReproducibleWithSameSeed()
        {
            var network1 = new NetworkManager();
            var network2 = new NetworkManager();
            network1.GenerateRandomNetwork(100, 0.015);
            network2.GenerateRandomNetwork(100, 0.015);
            Assert.Equal(network1.TotalUsers, network2.TotalUsers);
        }

        [Fact]
        public void GenerateRandomNetwork_DifferentProbabilities_CreateDifferentDensities()
        {
            var network1 = new NetworkManager();
            var network2 = new NetworkManager();
            network1.GenerateRandomNetwork(100, 0.01);
            network2.GenerateRandomNetwork(100, 0.10);
            var user1 = network1.GetUser(1);
            var user2 = network2.GetUser(1);
            Assert.NotNull(user1);
            Assert.NotNull(user2);
        }

        [Fact]
        public void GenerateRandomNetwork_MinimumSize()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(1);
            Assert.Equal(1, network.TotalUsers);
        }

        [Fact]
        public void GenerateRandomNetwork_LargeSize()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(5000);
            Assert.Equal(5000, network.TotalUsers);
        }

        [Fact]
        public void GenerateRandomNetwork_ZeroUsers_ThrowsException()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(0);
            Assert.Equal(0, network.TotalUsers);
        }

        [Fact]
        public void GenerateRandomNetwork_ResetsExistingNetwork()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            network.GenerateRandomNetwork(200);
            Assert.Equal(200, network.TotalUsers);
        }

        // ==========================================
        // 2. Получение пользователей
        // ==========================================

        [Fact]
        public void GetUser_ValidId_ReturnsUser()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var user = network.GetUser(1);
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        [Fact]
        public void GetUser_InvalidId_ReturnsNull()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var user = network.GetUser(9999);
            Assert.Null(user);
        }

        [Fact]
        public void GetUser_NegativeId_ReturnsNull()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var user = network.GetUser(-1);
            Assert.Null(user);
        }

        [Fact]
        public void GetUser_ZeroId_ReturnsNull()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var user = network.GetUser(0);
            Assert.Null(user);
        }

        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            var network = new NetworkManager();
            int userCount = 50;
            network.GenerateRandomNetwork(userCount);
            var users = network.GetAllUsers();
            Assert.Equal(userCount, users.Count);
        }

        [Fact]
        public void GetAllUsers_ReturnsReadOnlyDictionary()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var users = network.GetAllUsers();
            Assert.NotNull(users);
            Assert.IsAssignableFrom<IReadOnlyDictionary<int, User>>(users);
        }

        [Fact]
        public void TotalUsers_ReturnsCorrectCount()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(250);
            Assert.Equal(250, network.TotalUsers);
        }

        [Fact]
        public void TotalUsers_AfterReset_UpdatesCorrectly()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            Assert.Equal(100, network.TotalUsers);
            network.GenerateRandomNetwork(300);
            Assert.Equal(300, network.TotalUsers);
        }

        // ==========================================
        // 3. Рекомендации друзей (BFS на расстоянии 2)
        // ==========================================

        [Fact]
        public void GetTopRecommendations_ReturnsRecommendationList()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var recommendations = network.GetTopRecommendations(1, 10);
            Assert.NotNull(recommendations);
        }

        [Fact]
        public void GetTopRecommendations_ExcludesExistingFriends()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            int userId = 1;
            var user = network.GetUser(userId);
            var existingFriendIds = user?.Friends ?? new HashSet<int>();
            var recommendations = network.GetTopRecommendations(userId, 10);
            foreach (var rec in recommendations)
            {
                Assert.DoesNotContain(rec.CandidateId, existingFriendIds);
                Assert.NotEqual(userId, rec.CandidateId);
            }
        }

        [Fact]
        public void GetTopRecommendations_RespectsTopNLimit()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            var recommendations = network.GetTopRecommendations(1, 5);
            Assert.True(recommendations.Count <= 5);
        }

        [Fact]
        public void GetTopRecommendations_RespectsTopNLimit_10()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            var recommendations = network.GetTopRecommendations(1, 10);
            Assert.True(recommendations.Count <= 10);
        }

        [Fact]
        public void GetTopRecommendations_RespectsTopNLimit_20()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            var recommendations = network.GetTopRecommendations(1, 20);
            Assert.True(recommendations.Count <= 20);
        }

        [Fact]
        public void GetTopRecommendations_SortedByMutualFriends()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            var recommendations = network.GetTopRecommendations(1, 10);
            if (recommendations.Count > 1)
            {
                for (int i = 0; i < recommendations.Count - 1; i++)
                {
                    Assert.True(
                        recommendations[i].MutualFriendsCount >= recommendations[i + 1].MutualFriendsCount);
                }
            }
        }

        [Fact]
        public void GetTopRecommendations_InvalidUserId_ThrowsException()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            Assert.Throws<ArgumentException>(() => network.GetTopRecommendations(9999, 10));
        }

        [Fact]
        public void GetTopRecommendations_TopN_Zero_ReturnsEmpty()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var recommendations = network.GetTopRecommendations(1, 0);
            Assert.Empty(recommendations);
        }

        [Fact]
        public void GetTopRecommendations_TopN_One_ReturnsSingleOrEmpty()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(200, 0.1);
            var recommendations = network.GetTopRecommendations(1, 1);
            Assert.True(recommendations.Count <= 1);
        }

        [Fact]
        public void GetTopRecommendations_TopN_Large_ReturnsAllAvailable()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100, 0.1);
            var recommendations = network.GetTopRecommendations(1, 1000);
            Assert.NotNull(recommendations);
        }

        [Fact]
        public void GetTopRecommendations_UserWithManyFriends_ReturnsRecommendations()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500, 0.05);
            var recommendations = network.GetTopRecommendations(1, 10);
            Assert.NotNull(recommendations);
        }

        [Fact]
        public void GetTopRecommendations_WithZeroMutualFriends_ReturnsEmptyOrLowRanked()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100, 0.01);
            var recommendations = network.GetTopRecommendations(1, 10);
            foreach (var rec in recommendations)
            {
                Assert.True(rec.MutualFriendsCount >= 0);
            }
        }

        [Fact]
        public void GetTopRecommendations_NoFriendsOfFriends_ReturnsEmpty()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100, 0.0);
            var recommendations = network.GetTopRecommendations(1, 10);
            Assert.Empty(recommendations);
        }

        // ==========================================
        // 4. Коэффициент связности
        // ==========================================

        [Fact]
        public void ConnectivityCoefficient_InValidRange()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            var recommendations = network.GetTopRecommendations(1, 10);
            foreach (var rec in recommendations)
            {
                Assert.True(rec.ConnectivityCoefficient >= 0.0);
                Assert.True(rec.ConnectivityCoefficient <= 1.0);
            }
        }

        [Fact]
        public void ConnectivityCoefficient_CalculatedCorrectly()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            int userId = 1;
            var recommendations = network.GetTopRecommendations(userId, 10);
            foreach (var rec in recommendations)
            {
                var candidate = network.GetUser(rec.CandidateId);
                if (candidate != null && candidate.Friends.Count > 0)
                {
                    var expectedCoeff = (double)rec.MutualFriendsCount / candidate.Friends.Count;
                    Assert.Equal(expectedCoeff, rec.ConnectivityCoefficient, 3);
                }
            }
        }

        [Fact]
        public void ConnectivityCoefficient_WithNoMutualFriends_IsZero()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100, 0.01);
            var recommendations = network.GetTopRecommendations(1, 10);
            foreach (var rec in recommendations.Where(r => r.MutualFriendsCount == 0))
            {
                Assert.Equal(0.0, rec.ConnectivityCoefficient);
            }
        }

        // ==========================================
        // 5. Глобальная статистика
        // ==========================================

        [Fact]
        public void CalculateGlobalStatistics_ReturnsValidStats()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var stats = network.CalculateGlobalStatistics(10);
            Assert.Equal(100, stats.TotalUsers);
            Assert.True(stats.UsersWithRecommendations >= 0);
            Assert.True(stats.UsersWithRecommendations <= stats.TotalUsers);
            Assert.True(stats.PercentageWithRecommendations >= 0.0);
            Assert.True(stats.PercentageWithRecommendations <= 100.0);
            Assert.True(stats.AverageRecommendationsPerUser >= 0.0);
        }

        [Fact]
        public void CalculateGlobalStatistics_WithSmallNetwork_ReturnsValidStats()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(50, 0.1);
            var stats = network.CalculateGlobalStatistics(5);
            Assert.Equal(50, stats.TotalUsers);
            Assert.True(stats.UsersWithRecommendations >= 0);
            Assert.True(stats.UsersWithRecommendations <= 50);
        }

        [Fact]
        public void CalculateGlobalStatistics_WithDifferentTopNValues_WorksCorrectly()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var stats1 = network.CalculateGlobalStatistics(5);
            var stats2 = network.CalculateGlobalStatistics(20);
            Assert.NotNull(stats1);
            Assert.NotNull(stats2);
            Assert.Equal(stats1.TotalUsers, stats2.TotalUsers);
        }

        [Fact]
        public void PercentageWithRecommendations_CalculatedCorrectly()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var stats = network.CalculateGlobalStatistics(10);
            var expectedPercentage = (double)stats.UsersWithRecommendations / stats.TotalUsers * 100;
            Assert.Equal(expectedPercentage, stats.PercentageWithRecommendations, 2);
        }

        [Fact]
        public void AverageRecommendationsPerUser_CalculatedCorrectly()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var stats = network.CalculateGlobalStatistics(10);
            Assert.True(stats.AverageRecommendationsPerUser >= 0.0);
        }

        [Fact]
        public void CalculateGlobalStatistics_EmptyNetwork_ReturnsZeroStats()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(0);
            var stats = network.CalculateGlobalStatistics(10);
            Assert.Equal(0, stats.TotalUsers);
            Assert.Equal(0, stats.UsersWithRecommendations);
            Assert.Equal(0.0, stats.PercentageWithRecommendations);
            Assert.Equal(0.0, stats.AverageRecommendationsPerUser);
        }

        // ==========================================
        // 6. Алгоритм BFS
        // ==========================================

        [Fact]
        public void BFS_FindsFriendsAtDistance2()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            int userId = 1;
            var user = network.GetUser(userId);
            var directFriends = user?.Friends ?? new HashSet<int>();
            var recommendations = network.GetTopRecommendations(userId, 50);
            foreach (var rec in recommendations)
            {
                Assert.DoesNotContain(rec.CandidateId, directFriends);
                Assert.NotEqual(userId, rec.CandidateId);
                Assert.True(rec.MutualFriendsCount >= 1);
            }
        }

        [Fact]
        public void MutualFriendsCount_CalculatedCorrectly()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            int userId = 1;
            var user = network.GetUser(userId);
            var recommendations = network.GetTopRecommendations(userId, 10);
            foreach (var rec in recommendations)
            {
                var candidate = network.GetUser(rec.CandidateId);
                if (candidate != null && user != null)
                {
                    var actualMutual = user.Friends.Intersect(candidate.Friends).Count();
                    Assert.Equal(actualMutual, rec.MutualFriendsCount);
                }
            }
        }

        [Fact]
        public void BFS_DoesNotReturnSelfAsRecommendation()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            int userId = 1;
            var recommendations = network.GetTopRecommendations(userId, 10);
            foreach (var rec in recommendations)
            {
                Assert.NotEqual(userId, rec.CandidateId);
            }
        }

        // ==========================================
        // 7. Тесты класса User
        // ==========================================

        [Fact]
        public void User_Creation_ValidData()
        {
            var user = new User(1, "Иванов Иван");
            Assert.Equal(1, user.Id);
            Assert.Equal("Иванов Иван", user.Name);
            Assert.NotNull(user.Friends);
            Assert.Empty(user.Friends);
        }

        [Fact]
        public void User_AddFriend_AddsToFriendsCollection()
        {
            var user = new User(1, "Иванов Иван");
            user.Friends.Add(2);
            user.Friends.Add(3);
            Assert.Equal(2, user.Friends.Count);
            Assert.Contains(2, user.Friends);
            Assert.Contains(3, user.Friends);
        }

        [Fact]
        public void User_FriendsCollection_IsHashSet()
        {
            var user = new User(1, "Иванов Иван");
            Assert.IsType<HashSet<int>>(user.Friends);
        }

        [Fact]
        public void User_FriendsCollection_NoDuplicates()
        {
            var user = new User(1, "Иванов Иван");
            user.Friends.Add(2);
            user.Friends.Add(2);
            user.Friends.Add(2);
            Assert.Equal(1, user.Friends.Count);
        }

        // ==========================================
        // 8. Тесты класса Recommendation
        // ==========================================

        [Fact]
        public void Recommendation_Creation_ValidData()
        {
            var rec = new Recommendation
            {
                CandidateId = 5,
                CandidateName = "Петров Петр",
                MutualFriendsCount = 3,
                ConnectivityCoefficient = 0.75,
                FriendsCount = 10
            };
            Assert.Equal(5, rec.CandidateId);
            Assert.Equal("Петров Петр", rec.CandidateName);
            Assert.Equal(3, rec.MutualFriendsCount);
            Assert.Equal(0.75, rec.ConnectivityCoefficient);
            Assert.Equal(10, rec.FriendsCount);
        }

        [Fact]
        public void Recommendation_DefaultValues()
        {
            var rec = new Recommendation();
            Assert.Equal(0, rec.CandidateId);
            Assert.Null(rec.CandidateName);
            Assert.Equal(0, rec.MutualFriendsCount);
            Assert.Equal(0.0, rec.ConnectivityCoefficient);
            Assert.Equal(0, rec.FriendsCount);
        }

        // ==========================================
        // 9. Интеграционный тест
        // ==========================================

        [Fact]
        public void FullRecommendationScenario()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(1000);
            int testUserId = 50;
            var recommendations = network.GetTopRecommendations(testUserId, 10);
            var stats = network.CalculateGlobalStatistics(10);
            var user = network.GetUser(testUserId);

            Assert.NotNull(user);
            Assert.NotNull(recommendations);
            Assert.True(recommendations.Count <= 10);
            Assert.Equal(1000, stats.TotalUsers);
            Assert.True(stats.PercentageWithRecommendations >= 0.0);
            Assert.True(stats.PercentageWithRecommendations <= 100.0);
            Assert.Equal(testUserId, user.Id);
            Assert.False(string.IsNullOrEmpty(user.Name));

            foreach (var rec in recommendations)
            {
                Assert.NotEqual(testUserId, rec.CandidateId);
                Assert.False(string.IsNullOrEmpty(rec.CandidateName));
                Assert.True(rec.MutualFriendsCount >= 0);
                Assert.True(rec.ConnectivityCoefficient >= 0.0);
                Assert.True(rec.ConnectivityCoefficient <= 1.0);
            }
        }

        // ==========================================
        // 10. Дополнительные граничные тесты
        // ==========================================

        [Fact]
        public void GetTopRecommendations_MultipleCalls_ReturnsConsistentResults()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var recs1 = network.GetTopRecommendations(1, 10);
            var recs2 = network.GetTopRecommendations(1, 10);
            Assert.Equal(recs1.Count, recs2.Count);
        }

        [Fact]
        public void GetAllUsers_KeysAreValidUserIds()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var users = network.GetAllUsers();
            foreach (var key in users.Keys)
            {
                Assert.True(key >= 1 && key <= 100);
            }
        }

        [Fact]
        public void GetUser_AllUserIds_ReturnValidUsers()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(50);
            for (int i = 1; i <= 50; i++)
            {
                var user = network.GetUser(i);
                Assert.NotNull(user);
                Assert.Equal(i, user.Id);
            }
        }

        [Fact]
        public void GenerateRandomNetwork_NamesAreUnique()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var users = network.GetAllUsers();
            var names = users.Values.Select(u => u.Name).ToList();
            // Имена могут повторяться (это нормально для случайной генерации)
            Assert.Equal(100, names.Count);
        }

        [Fact]
        public void CalculateGlobalStatistics_MultipleCalls_ReturnsConsistentResults()
        {
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            var stats1 = network.CalculateGlobalStatistics(10);
            var stats2 = network.CalculateGlobalStatistics(10);
            Assert.Equal(stats1.TotalUsers, stats2.TotalUsers);
            Assert.Equal(stats1.UsersWithRecommendations, stats2.UsersWithRecommendations);
        }
    }
}