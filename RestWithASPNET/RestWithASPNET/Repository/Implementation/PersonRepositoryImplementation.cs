﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using RestWithASPNET.Model;
using RestWithASPNET.Model.Context;

namespace RestWithASPNET.Repository.Implementation
{
    public class PersonRepositoryImplementation : IPersonRepository
    {
        private MySQLContext _context;
        public PersonRepositoryImplementation(MySQLContext context)
        {
            _context = context;
        }

        private int count;

        public Person Create(Person person)
        {
            try
            {
                _context.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public void Delete(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            try
            {
                if (result != null)
                    _context.Persons.Remove(result);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Person> FindAll()
        {
            return _context.Persons.ToList();
        }

        public Person FindById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person Update(Person person)
        {
            if (!Exist(person.Id))
                return null;

            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(person.Id));
            try
            {
                _context.Entry(result).CurrentValues.SetValues(person);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return person;
        }

        public bool Exist(long? id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }

        private long IndrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }
    }
}