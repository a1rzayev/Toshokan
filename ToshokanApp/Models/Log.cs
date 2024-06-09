using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ToshokanApp.Models;

#pragma warning disable CS8618

public class Log
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Url { get; set; }
    [Required]
    public string RequestBody { get; set; }
    [Required]
    public string ResponseBody { get; set; }
    [Required]
    public DateTime CreationDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
    [Required]
    public int StatusCode { get; set; }
    [Required]
    public string HttpMethod { get; set; }
}
