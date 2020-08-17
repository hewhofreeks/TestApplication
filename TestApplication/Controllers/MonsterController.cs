using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApplication.DataModel;
using TestApplication.DataModel.Models;
using TestApplication.Services;

namespace TestApplication.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MonsterController
    {
        private readonly MHContext _context;
        private readonly ICombatService _combatService;

        public MonsterController(MHContext mHContext, ICombatService combatService)
        {
            _context = mHContext;
            _combatService = combatService;
        }


        [HttpPost]
        public async Task<Monster> AddMonster([FromBody] Monster monster)
        {
            await _context.Monsters.AddAsync(monster);

            await _context.SaveChangesAsync();

            return monster;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<Monster> GetMonster([FromRoute] long id)
        {
            return await _context.Monsters.FirstOrDefaultAsync(m => m.ID == id);
        }

        [HttpGet]
        [Route("{id}/IsAlive")]
        public async Task<bool> IsAlive([FromRoute] long id)
        {
            return await _combatService.IsMonsterDead(id);
        }
    }
}
