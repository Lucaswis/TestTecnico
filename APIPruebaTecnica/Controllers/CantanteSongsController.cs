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
    public class CantanteSongsController : ApiController
    {
        private BDPruebaTecnicaEntities db = new BDPruebaTecnicaEntities();

        // GET: api/CantanteSongs
        public IQueryable<CantanteSongs> GetCantanteSongs()
        {
            return db.CantanteSongs;
        }

        public IEnumerable<CantanteSongs> GetCantantes()
        {
            //Select Cantante.name, Cantante.kindOfMusic, songs.song from CantanteSongs 
            //inner join Cantante on Cantante.id = CantanteSongs.idCantante inner join songs on CantanteSongs.idSong = songs.idSong
            //var listadoCantante = from cantantesong in db.CantanteSongs
            //                      join cantante in db.Cantante on cantantesong.idCantante equals cantante.id
            //                      join cancion in db.songs on cantantesong.idSong equals cancion.idSong
            //                      select new { db.CantanteSongs };
            //var listado = from cantantesongs in db.CantanteSongs
            //              select new { db.CantanteSongs };
            var cantante = db.CantanteSongs.ToList();
            return cantante;
        }

        // GET: api/CantanteSongs/5
        [ResponseType(typeof(CantanteSongs))]
        public IHttpActionResult GetCantanteSongs(int id)
        {
            CantanteSongs cantanteSongs = db.CantanteSongs.Find(id);
            if (cantanteSongs == null)
            {
                return NotFound();
            }

            return Ok(cantanteSongs);
        }

        // PUT: api/CantanteSongs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCantanteSongs(int id, CantanteSongs cantanteSongs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cantanteSongs.idInter)
            {
                return BadRequest();
            }

            db.Entry(cantanteSongs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CantanteSongsExists(id))
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

        // POST: api/CantanteSongs
        [ResponseType(typeof(CantanteSongs))]
        public IHttpActionResult PostCantanteSongs(CantanteSongs cantanteSongs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CantanteSongs.Add(cantanteSongs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = cantanteSongs.idInter }, cantanteSongs);
        }

        // DELETE: api/CantanteSongs/5
        [ResponseType(typeof(CantanteSongs))]
        public IHttpActionResult DeleteCantanteSongs(int id)
        {
            CantanteSongs cantanteSongs = db.CantanteSongs.Find(id);
            if (cantanteSongs == null)
            {
                return NotFound();
            }

            db.CantanteSongs.Remove(cantanteSongs);
            db.SaveChanges();

            return Ok(cantanteSongs);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CantanteSongsExists(int id)
        {
            return db.CantanteSongs.Count(e => e.idInter == id) > 0;
        }
    }
}