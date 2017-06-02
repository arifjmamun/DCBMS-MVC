using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using App.Core.Context;
using App.Core.Models.EntityModels;

namespace App.Web.Controllers
{
    public class TestsAPIController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/TestsAPI
        public IQueryable<Test> GetTests()
        {
            return db.Tests;
        }

        // GET: api/TestsAPI/5
        [ResponseType(typeof(Test))]
        public IHttpActionResult GetTest(int id)
        {
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return NotFound();
            }

            return Ok(test);
        }

        // PUT: api/TestsAPI/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTest(int id, Test test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != test.TestId)
            {
                return BadRequest();
            }

            db.Entry(test).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TestExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/TestsAPI
        [ResponseType(typeof(Test))]
        public IHttpActionResult PostTest(Test test)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tests.Add(test);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = test.TestId }, test);
        }

        // DELETE: api/TestsAPI/5
        [ResponseType(typeof(Test))]
        public IHttpActionResult DeleteTest(int id)
        {
            Test test = db.Tests.Find(id);
            if (test == null)
            {
                return NotFound();
            }

            db.Tests.Remove(test);
            db.SaveChanges();

            return Ok(test);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TestExists(int id)
        {
            return db.Tests.Count(e => e.TestId == id) > 0;
        }
    }
}