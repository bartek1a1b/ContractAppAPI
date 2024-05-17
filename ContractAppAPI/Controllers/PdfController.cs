using ContractAppAPI.Data;
using ContractAppAPI.Dto;
using ContractAppAPI.Interfaces;
using ContractAppAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;

namespace ContractAppAPI.Controllers
{
    [Route("api/[controller]")]
    //[Route("file")]
    [ApiController]
    public class PdfController : Controller
    {
        private readonly DataContext _context;
        public PdfController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-pdfById")]
        public async Task<ActionResult> GetPdf([FromQuery] int pdfId)
        {
            // Znajdź plik PDF w bazie danych na podstawie jego ID
            var pdf = await _context.Pdfs.FindAsync(pdfId);

            // Sprawdź, czy plik PDF został znaleziony
            if (pdf == null)
            {
                return NotFound();
            }

            // Skonstruuj pełną ścieżkę do pliku na podstawie jego nazwy
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var filePath = Path.Combine(folderPath, pdf.FilePath);

            // Sprawdź, czy plik fizycznie istnieje
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            // Pobierz typ zawartości (MIME type) pliku
            var contentProvier = new FileExtensionContentTypeProvider();
            contentProvier.TryGetContentType(pdf.FilePath, out string contentType);

            // Odczytaj zawartość pliku PDF
            var fileContents = System.IO.File.ReadAllBytes(filePath);

            // Zwróć plik PDF jako odpowiedź HTTP
            return File(fileContents, contentType, pdf.FilePath);
        }

        [HttpGet("get-pdf")]
        public ActionResult GetPdf([FromQuery] string fileName)
        {
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

            var filePath = Path.Combine(folderPath, fileName);

            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            var contentProvier = new FileExtensionContentTypeProvider();
            contentProvier.TryGetContentType(fileName, out string contentType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);

            return File(fileContents, contentType, fileName);
        }

        [HttpGet("get-pdfByAnnexId")]
        public async Task<ActionResult> GetPdfByAnnexId([FromQuery] int annexId)
        {
            // Znajdź pierwszy plik PDF przypisany do danego aneksu
            var pdf = await _context.Pdfs.FirstOrDefaultAsync(p => p.AnnexToTheContractId == annexId);

            // Sprawdź, czy znaleziono plik PDF dla danego aneksu
            if (pdf == null)
            {
                return NotFound("Brak pliku PDF dla podanego aneksu");
            }

            // Skonstruuj pełną ścieżkę do pliku na podstawie jego nazwy
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var filePath = Path.Combine(folderPath, pdf.FilePath);

            // Sprawdź, czy plik fizycznie istnieje
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound("Plik PDF nie istnieje na dysku");
            }

            // Pobierz typ zawartości (MIME type) pliku
            var contentProvier = new FileExtensionContentTypeProvider();
            contentProvier.TryGetContentType(pdf.FilePath, out string contentType);

            // Zwróć plik PDF jako odpowiedź HTTP
            var fileContents = System.IO.File.ReadAllBytes(filePath);
            return File(fileContents, contentType, pdf.FilePath);
        }


        [HttpPost]
        public async Task<IActionResult> UploadPdf([FromForm] PdfDto pdfDto)
        {
            IFormFile? file = pdfDto.File;

            if (file == null || file.Length == 0)
            {
                return BadRequest("Nie wybrano pliku");
            }

            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (string.IsNullOrEmpty(extension) || (extension != ".pdf"))
            {
                return BadRequest("Zły format pliku");
            }

            var newFileName = Path.GetRandomFileName() + extension;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            var filePath = Path.Combine(folderPath, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = Url.Content($"~/uploads/{newFileName}");

            var pdf = new Pdf
            {
                FilePath = newFileName,
                AnnexToTheContractId = pdfDto.AnnexToTheContractId
            };

            _context.Pdfs.Add(pdf);

            var annexToTheContract = await _context.AnnexToTheContracts.FindAsync(pdfDto.AnnexToTheContractId);
            if (annexToTheContract != null)
            {
                var existingPdf = await _context.Pdfs.FirstOrDefaultAsync(cp => cp.AnnexToTheContractId == pdfDto.AnnexToTheContractId);
                if (existingPdf != null)
                {
                    // Jeśli już istnieje powiązany plik PDF, zastąp go nowym
                    existingPdf.FilePath = newFileName;
                }
                else
                {
                    // Jeśli nie istnieje, utwórz nowy wpis w bazie danych
                    annexToTheContract.HasPdf = true;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { url = fileUrl });

        }
    }
}