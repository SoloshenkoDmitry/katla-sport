using System;
using System.Web.Http;
using System.Web.Http.Cors;
using KatlaSport.Services.SupplierNoteManagement;
using KatlaSport.WebApi.CustomFilters;
using Microsoft.Web.Http;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;

namespace KatlaSport.WebApi.Controllers
{
    [ApiVersion("1.0")]
    [RoutePrefix("api/notes")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [CustomExceptionFilter]
    [SwaggerResponseRemoveDefaults]
    public class SupplierNotesController : ApiController
    {
        private readonly ISupplierNoteService _supplierNoteService;

        public SupplierNotesController(ISupplierNoteService supplierNoteService)
        {
            _supplierNoteService = supplierNoteService ?? throw new ArgumentNullException(nameof(supplierNoteService));
        }

        [HttpGet]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of supplier notes.", Type = typeof(SupplierNoteListItem[]))]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetSupplierNotesAsync()
        {
            var notes = await _supplierNoteService.GetSupplierNotesAsync();
            return Ok(notes);
        }
        
        [HttpGet]
        [Route("{noteId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.OK, Description = "Returns a list of supplier note.", Type = typeof(SupplierNote))]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> GetSupplierNoteAsync(int noteId)
        {
            var note = await _supplierNoteService.GetSupplierNoteAsync(noteId);
//            var note = await _supplierNoteService.GetSupplierNotesAsync(noteId);
            return Ok(note);
        }
        
        [HttpPost]
        [Route("")]
        [SwaggerResponse(HttpStatusCode.Created, Description = "Creates a new note.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> AddHiveSection([FromBody] UpdateSupplierNoteRequest createRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var supplierNote = await _supplierNoteService.CreateSupplierNoteAsync(createRequest);
            var location = $"/api/notes/{supplierNote.Id}";
            return Created<SupplierNote>(location, supplierNote);
        }
        
        [HttpPut]
        [Route("{supplierNoteId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Updates an existed supplier's note.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> UpdateHiveSection([FromUri] int supplierNoteId, [FromBody] UpdateSupplierNoteRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _supplierNoteService.UpdateSupplierNoteAsync(supplierNoteId, updateRequest);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
        
        [HttpPut]
        [Route("{supplierNoteId:int:min(1)}/notes/{deletedStatus:bool}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Sets deleted status for an existed supplier's note.")]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> SetStatus([FromUri] int supplierNoteId, [FromUri] bool deletedStatus)
        {
            await _supplierNoteService.SetStatusAsync(supplierNoteId, deletedStatus);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
        
        [HttpDelete]
        [Route("{supplierNoteId:int:min(1)}")]
        [SwaggerResponse(HttpStatusCode.NoContent, Description = "Deletes an existed supplier's note.")]
        [SwaggerResponse(HttpStatusCode.BadRequest)]
        [SwaggerResponse(HttpStatusCode.Conflict)]
        [SwaggerResponse(HttpStatusCode.NotFound)]
        [SwaggerResponse(HttpStatusCode.InternalServerError)]
        public async Task<IHttpActionResult> DeleteHiveSection([FromUri] int supplierNoteId)
        {
            if (supplierNoteId < 1)
            {
                return BadRequest($"Invalid {nameof(supplierNoteId)}");
            }

            await _supplierNoteService.DeleteSupplierNoteAsync(supplierNoteId);
            return ResponseMessage(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}