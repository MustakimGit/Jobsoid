using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Jobsoid.Models;

namespace Jobsoid.Controllers
{
    [ApiController]
    [Route("api/v1/jobs")]
    public class JobController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST /api/v1/jobs
        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] Job job)
        {
            job.Posteddate = DateTime.UtcNow;
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetJobById), new { id = job.Jobid }, job);
        }

        // PUT /api/v1/jobs/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(int id, [FromBody] Job job)
        {
            if (id != job.Jobid)
                return BadRequest("Job ID mismatch.");

            var existingJob = await _context.Jobs.FindAsync(id);
            if (existingJob == null)
                return NotFound();

            // Only update if incoming values are not null or default
            if (!string.IsNullOrWhiteSpace(job.Title))
                existingJob.Title = job.Title;

            if (!string.IsNullOrWhiteSpace(job.Description))
                existingJob.Description = job.Description;

            if (job.Departmentid != 0) // assuming 0 is invalid
                existingJob.Departmentid = job.Departmentid;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(existingJob);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Jobs.Any(j => j.Jobid == id))
                    return NotFound();
                throw;
            }
        }

        // GET /api/v1/jobs/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(int id)
        {
            var job = await _context.Jobs
                .Include(j => j.Department)
                .Include(j => j.Location)
                .FirstOrDefaultAsync(j => j.Jobid == id);

            if (job == null)
                return NotFound();

            return Ok(job);
        }

        // POST /api/jobs/list
        [HttpPost("/api/jobs/list")]
        public async Task<IActionResult> ListJobs([FromBody] JobSearchRequest req)
        {
            var query = _context.Jobs
                .Include(j => j.Department)
                .Include(j => j.Location)
                .AsQueryable();

            if (!string.IsNullOrEmpty(req.Q))
                query = query.Where(j => j.Title.Contains(req.Q) || j.Description.Contains(req.Q));

            if (req.LocationId.HasValue)
                query = query.Where(j => j.Locationid == req.LocationId.Value);

            if (req.DepartmentId.HasValue)
                query = query.Where(j => j.Departmentid == req.DepartmentId.Value);

            var total = await query.CountAsync();

            var data = await query
                .OrderByDescending(j => j.Posteddate)
                .Skip((req.PageNo - 1) * req.PageSize)
                .Take(req.PageSize)
                .Select(j => new
                {
                    j.Jobid,
                    Code = "JOB-" + j.Jobid,
                    j.Title,
                    Location = j.Location.Title,
                    Department = j.Department.Title,
                    j.Posteddate,
                    j.Closingdate
                })
                .ToListAsync();

            return Ok(new { total, data });
        }
    }

    public class JobSearchRequest
    {
        public string? Q { get; set; }
        public int PageNo { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int? LocationId { get; set; }
        public int? DepartmentId { get; set; }
    }
}
