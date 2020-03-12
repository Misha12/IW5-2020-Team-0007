﻿using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Models;
using MovieDatabase.API.Services;
using MovieDatabase.Domain.DTO;
using NSwag.Annotations;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("movies/{movieID}/rates")]
    public class RatesController : ControllerBase
    {
        private RatesService Service { get; }

        public RatesController(RatesService service)
        {
            Service = service;
        }

        /// <summary>
        /// Získání seznamu hodnocení na daný film.
        /// </summary>
        /// <param name="movieID">Jednoznačný identifikátor filmu.</param>
        [HttpGet]
        [ProducesResponseType(typeof(List<Rate>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieRates(long movieID)
        {
            var list = Service.GetRateList(movieID);

            if (list == null)
                return NotFound(new ErrorModel("Požadovaný film nebyl nalezen."));

            return Ok(list);
        }

        /// <summary>
        /// Získání hodnocení vč. základní informace o filmu.
        /// </summary>
        /// <param name="movieID"></param>
        /// <param name="rateId"></param>
        /// <returns></returns>
        [HttpGet("{rateId}")]
        [ProducesResponseType(typeof(RateDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult GetMovieRateByRateId(long movieID, long rateId)
        {
            var rate = Service.FindRateById(movieID, rateId);
            return rate == null ? (IActionResult)NotFound(new ErrorModel("Požadované hodnocení k filmu se nepodařilo najít.")) : Ok(rate);
        }

        /// <summary>
        /// Vytvoření hodnocení.
        /// </summary>
        /// <param name="movieID">Jednoznačný identifikátor filmu.</param>
        /// <param name="data"></param>
        [HttpPost]
        [ProducesResponseType(typeof(RateDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult CreateMovieRate(long movieID, [FromBody] RateInput data)
        {
            if (!data.IsValid())
                return BadRequest(new ErrorModel("Nebylo zadán popis hodnocení."));

            var rate = Service.CreateRate(movieID, data);
            return rate == null ? (IActionResult)NotFound(new ErrorModel("Hodnocení k filmu nelze přidat, protože film nebyl nalezen.")) : Ok(rate);
        }

        /// <summary>
        /// Úprava hodnocení.
        /// </summary>
        /// <param name="movieID">Jednoznačný identifikátor filmu.</param>
        /// <param name="rateId">Jednoznačný identifkátor hodnocení.</param>
        /// <param name="data"></param>
        [HttpPut("{rateId}")]
        [ProducesResponseType(typeof(RateDetail), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorModel), (int)HttpStatusCode.NotFound)]
        public IActionResult UpdateMovieRate(long movieID, long rateId, RateInput data)
        {
            var rate = Service.UpdateRate(movieID, rateId, data);
            return rate == null ? (IActionResult)NotFound(new ErrorModel("Nelze aktualizovat hodnocení, protože neexistuje.")) : Ok(rate);
        }

        /// <summary>
        /// Smazání hodnocení.
        /// </summary>
        /// <param name="movieID">Jedinečný identifikátor filmu.</param>
        /// <param name="rateId">Jedinečný identifikátor hodnocení.</param>
        [HttpDelete("{rateId}")]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(object), (int)HttpStatusCode.NotFound)]
        public IActionResult DeleteMovieName(long movieID, long rateId)
        {
            var success = Service.DeleteRate(movieID, rateId);
            return success ? Ok(null) : (IActionResult)NotFound(null);
        }

        /// <summary>
        /// Získání seznamu hodnocení na všechny filmy.
        /// </summary>
        /// <returns></returns>
        [HttpGet("/rates")]
        [ProducesResponseType(typeof(List<Rate>), (int)HttpStatusCode.OK)]
        public IActionResult GetAllRates()
        {
            return Ok(Service.GetRateList(null));
        }
    }
}