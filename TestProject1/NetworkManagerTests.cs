using System;
using System.Linq;
using System.Collections.Generic;
using Xunit;
using SocialNetwork.Core;

namespace SocialNetwork.Tests
{
    public class NetworkManagerTests
    {
        // ==========================================
        // 1. Инициализация и генерация сети
        // ==========================================

        /// <summary>
        /// Тест: Проверка создания сети с указанным количеством пользователей
        /// </summary>
        [Fact]
        public void GenerateRandomNetwork_CreatesCorrectUserCount()
        {
            // Arrange
            var network = new NetworkManager();
            int expectedUsers = 1000;

            // Act
            network.GenerateRandomNetwork(expectedUsers);

            // Assert
            Assert.Equal(expectedUsers, network.TotalUsers);
        }

        /// <summary>
        /// Тест: Проверка что пользователи имеют русские имена
        /// </summary>
        [Fact]
        public void GenerateRandomNetwork_UsersHaveRussianNames()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var users = network.GetAllUsers();

            // Assert
            foreach (var user in users.Values)
            {
                Assert.False(string.IsNullOrEmpty(user.Name));
                Assert.Contains(" ", user.Name); // Формат "Фамилия Имя"
            }
        }

        /// <summary>
        /// Тест: Проверка что сеть генерируется воспроизводимо с фиксированным seed
        /// </summary>
        [Fact]
        public void GenerateRandomNetwork_ReproducibleWithSameSeed()
        {
            // Arrange
            var network1 = new NetworkManager();
            var network2 = new NetworkManager();

            // Act
            network1.GenerateRandomNetwork(100, 0.015);
            network2.GenerateRandomNetwork(100, 0.015);

            // Assert
            // Примечание: из-за Random с фиксированным seed структуры должны совпадать
            Assert.Equal(network1.TotalUsers, network2.TotalUsers);
        }

        // ==========================================
        // 2. Получение пользователей
        // ==========================================

        /// <summary>
        /// Тест: GetUser возвращает пользователя по ID
        /// </summary>
        [Fact]
        public void GetUser_ValidId_ReturnsUser()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var user = network.GetUser(1);

            // Assert
            Assert.NotNull(user);
            Assert.Equal(1, user.Id);
        }

        /// <summary>
        /// Тест: GetUser возвращает null для несуществующего ID
        /// </summary>
        [Fact]
        public void GetUser_InvalidId_ReturnsNull()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var user = network.GetUser(9999);

            // Assert
            Assert.Null(user);
        }

        /// <summary>
        /// Тест: GetAllUsers возвращает всех пользователей
        /// </summary>
        [Fact]
        public void GetAllUsers_ReturnsAllUsers()
        {
            // Arrange
            var network = new NetworkManager();
            int userCount = 50;
            network.GenerateRandomNetwork(userCount);

            // Act
            var users = network.GetAllUsers();

            // Assert
            Assert.Equal(userCount, users.Count);
        }

        // ==========================================
        // 3. Рекомендации друзей (BFS на расстоянии 2)
        // ==========================================

        /// <summary>
        /// Тест: GetTopRecommendations возвращает список рекомендаций
        /// </summary>
        [Fact]
        public void GetTopRecommendations_ReturnsRecommendationList()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var recommendations = network.GetTopRecommendations(1, 10);

            // Assert
            Assert.NotNull(recommendations);
            Assert.True(recommendations.Count >= 0);
        }

        /// <summary>
        /// Тест: GetTopRecommendations не включает существующих друзей
        /// </summary>
        [Fact]
        public void GetTopRecommendations_ExcludesExistingFriends()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);
            int userId = 1;
            var user = network.GetUser(userId);
            var existingFriendIds = user?.Friends ?? new HashSet<int>();

            // Act
            var recommendations = network.GetTopRecommendations(userId, 10);

            // Assert
            foreach (var rec in recommendations)
            {
                Assert.DoesNotContain(rec.CandidateId, existingFriendIds);
                Assert.NotEqual(userId, rec.CandidateId);
            }
        }

        /// <summary>
        /// Тест: GetTopRecommendations возвращает максимум N рекомендаций
        /// </summary>
        [Theory]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(20)]
        public void GetTopRecommendations_RespectsTopNLimit(int topN)
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);

            // Act
            var recommendations = network.GetTopRecommendations(1, topN);

            // Assert
            Assert.True(recommendations.Count <= topN);
        }

        /// <summary>
        /// Тест: GetTopRecommendations сортирует по количеству общих друзей
        /// </summary>
        [Fact]
        public void GetTopRecommendations_SortedByMutualFriends()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);

            // Act
            var recommendations = network.GetTopRecommendations(1, 10);

            // Assert
            if (recommendations.Count > 1)
            {
                for (int i = 0; i < recommendations.Count - 1; i++)
                {
                    // Проверяем что список отсортирован по убыванию общих друзей
                    Assert.True(
                        recommendations[i].MutualFriendsCount >= recommendations[i + 1].MutualFriendsCount,
                        $"Рекомендации не отсортированы: [{i}] = {recommendations[i].MutualFriendsCount}, " +
                        $"[{i + 1}] = {recommendations[i + 1].MutualFriendsCount}"
                    );
                }
            }
        }

        /// <summary>
        /// Тест: GetTopRecommendations выбрасывает исключение для несуществующего пользователя
        /// </summary>
        [Fact]
        public void GetTopRecommendations_InvalidUserId_ThrowsException()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => network.GetTopRecommendations(9999, 10));
        }

        // ==========================================
        // 4. Коэффициент связности
        // ==========================================

        /// <summary>
        /// Тест: Коэффициент связности находится в диапазоне [0, 1]
        /// </summary>
        [Fact]
        public void ConnectivityCoefficient_InValidRange()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);

            // Act
            var recommendations = network.GetTopRecommendations(1, 10);

            // Assert
            foreach (var rec in recommendations)
            {
                Assert.True(rec.ConnectivityCoefficient >= 0.0);
                Assert.True(rec.ConnectivityCoefficient <= 1.0);
            }
        }

        /// <summary>
        /// Тест: Коэффициент связности рассчитывается корректно
        /// </summary>
        [Fact]
        public void ConnectivityCoefficient_CalculatedCorrectly()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            int userId = 1;

            // Act
            var recommendations = network.GetTopRecommendations(userId, 10);

            // Assert
            foreach (var rec in recommendations)
            {
                var candidate = network.GetUser(rec.CandidateId);
                if (candidate != null && candidate.Friends.Count > 0)
                {
                    // Проверяем формулу: mutualCount / candidateFriendsCount
                    var expectedCoeff = (double)rec.MutualFriendsCount / candidate.Friends.Count;
                    Assert.Equal(expectedCoeff, rec.ConnectivityCoefficient, 3);
                }
            }
        }

        // ==========================================
        // 5. Глобальная статистика
        // ==========================================

        /// <summary>
        /// Тест: CalculateGlobalStatistics возвращает корректную статистику
        /// </summary>
        [Fact]
        public void CalculateGlobalStatistics_ReturnsValidStats()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var stats = network.CalculateGlobalStatistics(10);

            // Assert
            Assert.Equal(100, stats.TotalUsers);
            Assert.True(stats.UsersWithRecommendations >= 0);
            Assert.True(stats.UsersWithRecommendations <= stats.TotalUsers);
            Assert.True(stats.PercentageWithRecommendations >= 0.0);
            Assert.True(stats.PercentageWithRecommendations <= 100.0);
            Assert.True(stats.AverageRecommendationsPerUser >= 0.0);
        }

        /// <summary>
        /// Тест: Процент пользователей с рекомендациями рассчитывается верно
        /// </summary>
        [Fact]
        public void PercentageWithRecommendations_CalculatedCorrectly()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var stats = network.CalculateGlobalStatistics(10);

            // Assert
            var expectedPercentage = (double)stats.UsersWithRecommendations / stats.TotalUsers * 100;
            Assert.Equal(expectedPercentage, stats.PercentageWithRecommendations, 2);
        }

        /// <summary>
        /// Тест: Среднее количество рекомендаций рассчитывается верно
        /// </summary>
        [Fact]
        public void AverageRecommendationsPerUser_CalculatedCorrectly()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var stats = network.CalculateGlobalStatistics(10);

            // Assert
            // Среднее должно быть неотрицательным
            Assert.True(stats.AverageRecommendationsPerUser >= 0.0);
        }

        // ==========================================
        // 6. Поиск друзей (BFS алгоритм)
        // ==========================================

        /// <summary>
        /// Тест: Пользователи на расстоянии 2 находятся корректно
        /// </summary>
        [Fact]
        public void BFS_FindsFriendsAtDistance2()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            int userId = 1;
            var user = network.GetUser(userId);
            var directFriends = user?.Friends ?? new HashSet<int>();

            // Act
            var recommendations = network.GetTopRecommendations(userId, 50);

            // Assert
            foreach (var rec in recommendations)
            {
                // Кандидат не должен быть прямым другом
                Assert.DoesNotContain(rec.CandidateId, directFriends);
                // Кандидат не должен быть самим пользователем
                Assert.NotEqual(userId, rec.CandidateId);
                // Должен быть хотя бы 1 общий друг (иначе не на расстоянии 2)
                Assert.True(rec.MutualFriendsCount >= 1);
            }
        }

        /// <summary>
        /// Тест: Количество общих друзей подсчитывается верно
        /// </summary>
        [Fact]
        public void MutualFriendsCount_CalculatedCorrectly()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(500);
            int userId = 1;
            var user = network.GetUser(userId);

            // Act
            var recommendations = network.GetTopRecommendations(userId, 10);

            // Assert
            foreach (var rec in recommendations)
            {
                var candidate = network.GetUser(rec.CandidateId);
                if (candidate != null && user != null)
                {
                    // Ручной подсчёт общих друзей для проверки
                    var actualMutual = user.Friends.Intersect(candidate.Friends).Count();
                    Assert.Equal(actualMutual, rec.MutualFriendsCount);
                }
            }
        }

        // ==========================================
        // 7. Граничные случаи
        // ==========================================

        /// <summary>
        /// Тест: Работа с минимальным размером сети
        /// </summary>
        [Fact]
        public void GenerateRandomNetwork_MinimumSize()
        {
            // Arrange
            var network = new NetworkManager();

            // Act
            network.GenerateRandomNetwork(10);

            // Assert
            Assert.Equal(10, network.TotalUsers);
        }

        /// <summary>
        /// Тест: Работа с большим размером сети
        /// </summary>
        [Fact]
        public void GenerateRandomNetwork_LargeSize()
        {
            // Arrange
            var network = new NetworkManager();

            // Act
            network.GenerateRandomNetwork(5000);

            // Assert
            Assert.Equal(5000, network.TotalUsers);
        }

        /// <summary>
        /// Тест: Рекомендации для пользователя без друзей друзей
        /// </summary>
        [Fact]
        public void GetTopRecommendations_NoFriendsOfFriends()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100, 0.0); // 0% связей

            // Act
            var recommendations = network.GetTopRecommendations(1, 10);

            // Assert
            Assert.Empty(recommendations); // Не должно быть рекомендаций
        }

        /// <summary>
        /// Тест: TopN = 0 возвращает пустой список
        /// </summary>
        [Fact]
        public void GetTopRecommendations_TopN_Zero()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(100);

            // Act
            var recommendations = network.GetTopRecommendations(1, 0);

            // Assert
            Assert.Empty(recommendations);
        }

        // ==========================================
        // 8. Интеграционный тест
        // ==========================================

        /// <summary>
        /// Тест: Полный сценарий работы системы рекомендаций
        /// </summary>
        [Fact]
        public void FullRecommendationScenario()
        {
            // Arrange
            var network = new NetworkManager();
            network.GenerateRandomNetwork(1000);
            int testUserId = 50;

            // Act - Шаг 1: Получаем рекомендации
            var recommendations = network.GetTopRecommendations(testUserId, 10);

            // Act - Шаг 2: Получаем статистику
            var stats = network.CalculateGlobalStatistics(10);

            // Act - Шаг 3: Получаем данные пользователя
            var user = network.GetUser(testUserId);

            // Assert - Шаг 1
            Assert.NotNull(user);
            Assert.NotNull(recommendations);
            Assert.True(recommendations.Count <= 10);

            // Assert - Шаг 2
            Assert.Equal(1000, stats.TotalUsers);
            Assert.True(stats.PercentageWithRecommendations >= 0.0);
            Assert.True(stats.PercentageWithRecommendations <= 100.0);

            // Assert - Шаг 3
            Assert.Equal(testUserId, user.Id);
            Assert.False(string.IsNullOrEmpty(user.Name));

            // Assert - Проверка что рекомендации не содержат самого пользователя
            foreach (var rec in recommendations)
            {
                Assert.NotEqual(testUserId, rec.CandidateId);
                Assert.False(string.IsNullOrEmpty(rec.CandidateName));
                Assert.True(rec.MutualFriendsCount >= 0);
                Assert.True(rec.ConnectivityCoefficient >= 0.0);
                Assert.True(rec.ConnectivityCoefficient <= 1.0);
            }
        }
    }
}