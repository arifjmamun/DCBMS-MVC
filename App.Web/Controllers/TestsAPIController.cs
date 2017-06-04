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
using App.Core.Models.ApiModels;
using App.Core.Models.EntityModels;

namespace App.Web.Controllers
{
    public class TestsAPIController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        // GET: api/TestsAPI
        public IQueryable<TestDTO> GetTests()
        {
            var tests = db.Tests.Join(db.TestTypes, t => t.TestTypeId, tt => tt.TestTypeId, (t, tt) => new TestDTO
            {
                TestId = t.TestId,
                TestName = t.TestName,
                Fee = t.Fee,
                TestTypeName = tt.TestTypeName,
                TestTypeId = tt.TestTypeId
            });
            return tests;
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
            if (test == null) return BadRequest(ModelState);
            if (test.TestTypeId <= 0) ModelState.AddModelError("TestTypeId", "The field cannot be empty!");
            if (Math.Abs(test.Fee) <= 0) ModelState.AddModelError("Fee", "The field cannot be empty!");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tests.Add(test);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = test.TestId }, db.Tests.Join(db.TestTypes, t => t.TestTypeId, tt => tt.TestTypeId, (t, tt) => new { t, tt })
                    .Where(x => x.t.TestId == test.TestId).Select(x => new TestDTO
                    {
                        TestId = x.t.TestId,
                        TestName = x.t.TestName,
                        Fee = x.t.Fee,
                        TestTypeName = x.tt.TestTypeName,
                        TestTypeId = x.tt.TestTypeId
                    }).SingleOrDefault());
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