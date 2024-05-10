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
    //[Route("contract-file")]
    [ApiController]
    public class ContractPdfController : Controller
    {
        private readonly DataContext _context;
        public ContractPdfController(DataContext context)
        {
            _context = context;
        }

        [HttpGet("get-contractPdfById")]
        public async Task<ActionResult> GetContractPdf([FromQuery] int contractPdfId)
        {
            // Znajdź plik PDF w bazie danych na podstawie jego ID
            var pdf = await _context.Pdfs.FindAsync(contractPdfId);

            // Sprawdź, czy plik PDF został znaleziony
            if (pdf == null)
            {
                return NotFound();
            }

            // Skonstruuj pełną ścieżkę do pliku na podstawie jego nazwy
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "contract files");
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

        [HttpGet("get-contractPdfByContractId")]
        public async Task<ActionResult> GetContractPdfByContractId([FromQuery] int contractId)
        {
            // Znajdź pierwszy plik PDF przypisany do danego aneksu
            var contractPdf = await _context.ContractPdfs.FirstOrDefaultAsync(p => p.ContractId == contractId);

            // Sprawdź, czy znaleziono plik PDF dla danego aneksu
            if (contractPdf == null)
            {
                return NotFound("Brak pliku PDF dla podanego kontraktu");
            }

            // Skonstruuj pełną ścieżkę do pliku na podstawie jego nazwy
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "contract files");
            var filePath = Path.Combine(folderPath, contractPdf.FilePath);

            // Sprawdź, czy plik fizycznie istnieje
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound("Plik PDF nie istnieje na dysku");
            }

            // Pobierz typ zawartości (MIME type) pliku
            var contentProvier = new FileExtensionContentTypeProvider();
            contentProvier.TryGetContentType(contractPdf.FilePath, out string contentType);

            // Zwróć plik PDF jako odpowiedź HTTP
            var fileContents = System.IO.File.ReadAllBytes(filePath);
            return File(fileContents, contentType, contractPdf.FilePath);
        }

        [HttpPost]
        public async Task<IActionResult> UploadPdf([FromForm] ContractPdfDto contractpdfDto)
        {
            IFormFile? file = contractpdfDto.File;

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

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "contract files");
            var filePath = Path.Combine(folderPath, newFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var fileUrl = Url.Content($"~/contract files/{newFileName}");

            var contractPdf = new ContractPdf
            {
                FilePath = newFileName,
                ContractId = contractpdfDto.ContractId
            };

            _context.ContractPdfs.Add(contractPdf);

            var contract = await _context.Contracts.FindAsync(contractpdfDto.ContractId);
            if (contract != null)
            {
                var existingPdf = await _context.ContractPdfs.FirstOrDefaultAsync(cp => cp.ContractId == contractpdfDto.ContractId);
                if (existingPdf != null)
                {
                    // Jeśli już istnieje powiązany plik PDF, zastąp go nowym
                    existingPdf.FilePath = newFileName;
                }
                else
                {
                    // Jeśli nie istnieje, utwórz nowy wpis w bazie danych
                    contract.HasPdf = true;
                }
            }

            await _context.SaveChangesAsync();

            return Ok(new { url = fileUrl });

        }

    }
}