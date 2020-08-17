using CallCenterService.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.DataModel;
using TestApplication.DataModel.Models;

namespace TestApplication.Services
{
    public interface ICombatService
    {
        Task<int> HitAMonster(long hunterID, long monsterID);

        Task<bool> IsMonsterDead(long monsterID);
    }

    public class CombatService: ICombatService
    {
        private readonly IRepository<Hunter> _hunterRepository;
        private readonly IRepository<Monster> _monsterRepository;
        private readonly IRepository<Weapon> _weaponRepository;

        public CombatService(IRepository<Hunter> hunterRepository, IRepository<Monster> monsterRepository, IRepository<Weapon> weaponRepository)
        {
            _hunterRepository = hunterRepository;
            _weaponRepository = weaponRepository;
            _monsterRepository = monsterRepository;
        }

        public async Task<int> HitAMonster(long hunterID, long monsterID)
        {
            var hunter = await _hunterRepository.FirstOrDefault(c => c.ID == hunterID);
            var weapon = await _weaponRepository.FirstOrDefault(w => w.ID == hunter.EquippedWeaponID);
            var monster = await _monsterRepository.FirstOrDefault(m => m.ID == monsterID);

            monster.Health = monster.Health - weapon.DamagePerHit;

            await _monsterRepository.SaveChangesAsync();

            return monster.Health;
        }

        public async Task<bool> IsMonsterDead(long monsterID)
        {
            var monster = await _monsterRepository.FirstOrDefault(m => m.ID == monsterID);

            return monster.Health > 0;
        }
    }
}
