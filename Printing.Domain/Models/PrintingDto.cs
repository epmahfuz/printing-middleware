using Newtonsoft.Json;

namespace Printing.Domain.Models
{
    public class PrintingDto
    {
        [JsonProperty("totalPayable")]
        public double TotalPayable { get; set; }
        [JsonProperty("kioskId")]
        public string KioskId { get; set; }
    }
}
