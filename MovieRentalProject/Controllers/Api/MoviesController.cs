﻿using MovieRentalProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Data.Entity;
using System.Web.Http;
using MovieRentalProject.Dtos;
using AutoMapper;

namespace MovieRentalProject.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET api/movies
        public IEnumerable<MovieDto> GetMovies()
        {
            var movieDtos = _context.Movies.Include(m => m.Genre).ToList().Select(Mapper.Map<Movie, MovieDto>);
            return movieDtos;
        }

        //GET /api/movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(movie));
        }

        //POST /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _context.Movies.Add(movie);
            _context.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT api/movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }
            var movieInDb = _context.Movies.SingleOrDefault(c => c.Id == id);
            if(movieInDb == null)
            {
                return NotFound();
            }
            Mapper.Map(movieDto, movieInDb);
            _context.SaveChanges();
            return Ok();
        }
        //DELETE api/movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if(movieInDb==null)
            {
                return NotFound();
            }
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
