using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestApplication.DataModel;
using TestApplication.DataModel.Models;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HunterController : ControllerBase
    {
        private readonly MHContext _context;
        private readonly ICombatService _combatService;

        public HunterController(MHContext context, ICombatService combatService)
        {
            _context = context;
            _combatService = combatService;
        }

        [HttpGet()]
        [Route("{id}")]
        public async Task<Hunter> GetHunter([FromRoute] long id)
        {
            var hunter = await _context.Hunters.Include(w => w.EquippedWeapon).FirstOrDefaultAsync(a => a.ID == id);

            return hunter;
        }

        [HttpPost]
        public async Task<Hunter> AddHunter([FromBody] Hunter hunter)
        {
            await _context.Hunters.AddAsync(hunter);

            await _context.SaveChangesAsync();

            return hunter;
        }

        [HttpPost]
        [Route("{id}/Weapon")]
        public async Task<Hunter> EquipHunter([FromRoute] long id, [FromBody] Weapon weapon)
        {
            var hunter = await _context.Hunters.FirstOrDefaultAsync(c => c.ID == id);
            
            await _context.Weapons.AddAsync(weapon);

            hunter.EquippedWeapon = weapon;

            await _context.SaveChangesAsync();

            return hunter;
        }

        [HttpPut]
        [Route("{hunterID}/Attacking/{monsterID}")]
        public async Task AttackMonster([FromRoute] long hunterID, [FromRoute] long monsterID)
        {
            await _combatService.HitAMonster(hunterID, monsterID);
        }
    }
}
