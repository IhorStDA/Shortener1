using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2205.Entities
{
    public class UrlMap
    {
        [Key]
        public int urlMapId { get; set; }
        public string originalUrl { get; set; }

        public string shortenedUrl { get; set; }

        public int usegCounter { get; set; }

        public DateTime created  { get; set; }

        public override string ToString()
        {
            return $"id = {urlMapId}, originalUrl = {originalUrl}, " +
                $"shortenedUrl = {shortenedUrl}, usegCounter = {usegCounter}, created = {created}";
        }




        public UrlMap(int urlMapId, string originalUrl, string shortenedUrl, DateTime created)
        {
            this.urlMapId = urlMapId;
            this.originalUrl = originalUrl;
            this.shortenedUrl = shortenedUrl;
            this.created = created;
            
        }
    }
}
