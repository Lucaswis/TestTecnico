using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using APIPruebaTecnica.Models;

namespace APIPruebaTecnica.Controllers
{
    [EnableCors(origins: "http://localhost:4200", headers: "*", methods: "*")]
    public class songsController : ApiController
    {
        private BDPruebaTecnicaEntities db = new BDPruebaTecnicaEntities();

        // GET: api/songs
        public IQueryable<songs> Getsongs()
        {
            return db.songs;
        }

        // GET: api/songs/5
        [ResponseType(typeof(songs))]
        public IHttpActionResult Getsongs(int id)
        {
            songs songs = db.songs.Find(id);
            if (songs == null)
            {
                return NotFound();
            }

            return Ok(songs);
        }

        // PUT: api/songs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putsongs(int id, songs songs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != songs.idSong)
            {
                return BadRequest();
            }

            db.Entry(songs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!songsExists(id))
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

        // POST: api/songs
        [ResponseType(typeof(songs))]
        public IHttpActionResult Postsongs(songs songs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.songs.Add(songs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = songs.idSong }, songs);
        }

        // DELETE: api/songs/5
        [ResponseType(typeof(songs))]
        public IHttpActionResult Deletesongs(int id)
        {
            songs songs = db.songs.Find(id);
            if (songs == null)
            {
                return NotFound();
            }

            db.songs.Remove(songs);
            db.SaveChanges();

            return Ok(songs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool songsExists(int id)
        {
            return db.songs.Count(e => e.idSong == id) > 0;
        }
    }
}