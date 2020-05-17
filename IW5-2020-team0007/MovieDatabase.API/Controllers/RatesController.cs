using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieDatabase.API.Services;
using MovieDatabase.Common.Extensions;
using MovieDatabase.Data.Models.Common;
using MovieDatabase.Data.Models.Ratings;
using NSwag.Annotations;
using System;
using System.Net;

namespace MovieDatabase.API.Controllers
{
    [ApiController]
    [Route("rates")]
    public class RatesController : Controller
    {
        private RatesService Service { get; }

        public RatesController(RatesService service)
        {
            Service = service;
        }

        /// <summary>
        /// Create rate.
        /// </summary>
        [HttpPost]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(RatesController) + "_" + nameof(CreateRate))]
        [ProducesResponseType(typeof(Rating), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult CreateRate([FromBody] CreateRateRequest request)
        {
            var currentUserID = HttpContext.User.GetUserID();
            var rating = Service.CreateRate(request, currentUserID);
            return Ok(rating);
        }

        /// <summary>
        /// Get all rates.
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [OpenApiOperation(nameof(RatesController) + "_" + nameof(GetRatingsList))]
        [ProducesResponseType(typeof(PaginatedData<Rating>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public IActionResult GetRatingsList([FromQuery] RatingSearchRequest request)
        {
            var ratings = Service.GetRateList(request);
            return Ok(ratings);
        }

        /// <summary>
        /// Change rate.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(RatesController) + "_" + nameof(UpdateRate))]
        [ProducesResponseType(typeof(Rating), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public IActionResult UpdateRate(long id, [FromBody] EditRatingRequest request)
        {
            try
            {
                var rating = Service.UpdateRate(id, request, HttpContext.User);

                if (rating == null)
                    return NotFound();

                return Ok(rating);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        /// <summary>
        /// Delete rate.
        /// </summary>
        /// <param name="id">Unique ID of rate.</param>
        [HttpDelete("{id}")]
        [Authorize(Roles = "User,ContentManager,Administrator")]
        [OpenApiOperation(nameof(RatesController) + "_" + nameof(DeleteRate))]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Forbidden)]
        public IActionResult DeleteRate(long id)
        {
            try
            {
                var success = Service.DeleteRate(id, HttpContext.User);

                if (!success)
                    return NotFound();

                return Ok();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                Service.Dispose();

            base.Dispose(disposing);
        }
    }
}