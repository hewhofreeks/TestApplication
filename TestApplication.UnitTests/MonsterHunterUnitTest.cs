using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CallCenterService.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestApplication.DataModel.Models;
using TestApplication.Services;

namespace TestApplication.UnitTests
{
    [TestClass]
    public class MonsterHunterUnitTest
    {

        [TestMethod]
        public async Task Test_HunterAttacksMonster_Deals100Dmg()
        {
            // Arrange
            var mockHunterRepository = new Moq.Mock<IRepository<Hunter>>();
            mockHunterRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Hunter, bool>>>()))
                .Returns(() =>
                {
                    var hunter = new Hunter { ID = 1, Name = "tester" };
                    return Task.FromResult(hunter);
                });

            var mockMonsterRepository = new Moq.Mock<IRepository<Monster>>();
            mockMonsterRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Monster, bool>>>()))
                .Returns(() =>
                {
                    var monster = new Monster { ID = 1, Name = "test monster", Health = 300 };
                    return Task.FromResult(monster);
                }
                );

            var mockWeaponRepository = new Moq.Mock<IRepository<Weapon>>();
            mockWeaponRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Weapon, bool>>>()))
                .Returns(() =>
                {
                    var weapon = new Weapon { Name = "Sword & Shield", DamagePerHit = 100 };
                    return Task.FromResult(weapon);
                }
                );

            CombatService combatService = new CombatService(mockHunterRepository.Object, mockMonsterRepository.Object, mockWeaponRepository.Object);


            // Act
            var monsterHealth = await combatService.HitAMonster(1, 1);

            // Assert
            Assert.AreEqual(monsterHealth, 200, "Monster health should be 200 after being attacked by a weapon that does 100 damage.");
        }

        [TestMethod]
        public async Task Test_MonsterIsDeadAfterBeingAttackTooMuch()
        {
            // Arrange
            var mockHunterRepository = new Moq.Mock<IRepository<Hunter>>();
            mockHunterRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Hunter, bool>>>()))
                .Returns(() =>
                {
                    var hunter = new Hunter { ID = 1, Name = "tester" };
                    return Task.FromResult(hunter);
                });

            var mockMonsterRepository = new Moq.Mock<IRepository<Monster>>();
            mockMonsterRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Monster, bool>>>()))
                .Returns(() =>
                {
                    var monster = new Monster { ID = 1, Name = "test monster", Health = -20 };
                    return Task.FromResult(monster);
                }
                );

            var mockWeaponRepository = new Moq.Mock<IRepository<Weapon>>();
            mockWeaponRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Weapon, bool>>>()))
                .Returns(() =>
                {
                    var weapon = new Weapon { Name = "Sword & Shield", DamagePerHit = 75 };
                    return Task.FromResult(weapon);
                }
                );

            CombatService combatService = new CombatService(mockHunterRepository.Object, mockMonsterRepository.Object, mockWeaponRepository.Object);


            // Act
            var isAlive = await combatService.IsMonsterDead(1);

            // Assert
            Assert.IsFalse(isAlive, "When monster health is below zero they should be dead.");
        }

        [TestMethod]
        public async Task Test_MonsterIsAliveWHenTheyHaveHealth()
        {
            // Arrange
            var mockHunterRepository = new Moq.Mock<IRepository<Hunter>>();
            mockHunterRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Hunter, bool>>>()))
                .Returns(() =>
                {
                    var hunter = new Hunter { ID = 1, Name = "tester" };
                    return Task.FromResult(hunter);
                });

            var mockMonsterRepository = new Moq.Mock<IRepository<Monster>>();
            mockMonsterRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Monster, bool>>>()))
                .Returns(() =>
                {
                    var monster = new Monster { ID = 1, Name = "test monster", Health = 20 };
                    return Task.FromResult(monster);
                }
                );

            var mockWeaponRepository = new Moq.Mock<IRepository<Weapon>>();
            mockWeaponRepository.Setup(m => m.FirstOrDefault(It.IsAny<Expression<Func<Weapon, bool>>>()))
                .Returns(() =>
                {
                    var weapon = new Weapon { Name = "Sword & Shield", DamagePerHit = 75 };
                    return Task.FromResult(weapon);
                }
                );

            CombatService combatService = new CombatService(mockHunterRepository.Object, mockMonsterRepository.Object, mockWeaponRepository.Object);


            // Act
            var isAlive = await combatService.IsMonsterDead(1);

            // Assert
            Assert.IsTrue(isAlive, "When monster health is above zero they should be alive.");
        }
    }
}
