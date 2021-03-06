﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using Trex2.Common.Models;
using Trex2.Services.Contracts;
using Trex2.Services.Data;

namespace Trex2.Services.Controllers
{
    public class DetailsController : IDetailsController
    {
        private readonly DetailsContext _context;

        public DetailsController(DetailsContext context)
        {           
            _context = context;
        }

        public async Task<List<Person>> Get()
        {
            var result = await _context.PersonDetails.ToListAsync();
            return result;
        }

        public Task<Person> Get(int PersonId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Person> Post(Person Person)
        {
            if (Person == null) return null;
            var result = _context.PersonDetails.Add(Person);
            _context.SaveChanges();
            return Task.FromResult(result);
        }

        public Task<bool> Put(int PersonId, Person Person)
        {
            if (!_context.PersonDetails.Any(x => x.Id == PersonId))
            {
                return Task.FromResult(false);
            }
            else
            {
                _context.PersonDetails.AddOrUpdate(Person);
                _context.SaveChanges();
            }
            return Task.FromResult(true);
        }

        public Task<bool> Delete(int PersonId)
        {
            if (!_context.PersonDetails.Any(x => x.Id == PersonId))
            {
                return Task.FromResult(false);
            }

            var removeItem = _context.PersonDetails.Find(PersonId);
            if (removeItem == null)
            {
                return Task.FromResult(false);
            }

            _context.PersonDetails.Remove(removeItem);
            _context.SaveChanges();

            return Task.FromResult(true);
        }
    }
}