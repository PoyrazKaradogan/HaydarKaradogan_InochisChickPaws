using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Inochis.Shared.ViewModels
{
    public class AddProductViewModel
    {
        [JsonPropertyName("Name")]
        [DisplayName("Ürün")]
        [Required(ErrorMessage = "{0} Bu alan boş bırakılamaz.")]
        [MinLength(5, ErrorMessage = "{0} Bu alanın uzunluğu {1} karakterden küçük değer girilemez.")]
        [MaxLength(100, ErrorMessage = "{0} Bu alanın uzunluğu {1} karakterden büyük değer girilemez.")]
        public string Name { get; set; }


        [JsonPropertyName("Properties")]
        [DisplayName("Ürün Özellikleri")]
        public string Properties { get; set; }


        [JsonPropertyName("Price")]
        [DisplayName("Ürün Fiyatı")]
        [Required(ErrorMessage = "{0} Bu alan boş bırakılamaz.")]
        public decimal? Price { get; set; }


        [JsonPropertyName("ImageUrl")]
        [DisplayName("Resim")]
        public string ImageUrl { get; set; } = "";


        [JsonPropertyName("Url")]
        public string Url { get; set; }


        [JsonPropertyName("IsActive")]
        [DisplayName("Aktif Ürün")]
        public bool IsActive { get; set; }


        [JsonPropertyName("IsHome")]
        [DisplayName("Ana Sayfa Ürünü")]
        public bool IsHome { get; set; }


        [JsonPropertyName("CategoryIds")]
        public List<int> CategoryIds { get; set; } = new List<int>();


        [DisplayName("Kategoriler")]
        public List<CategoryViewModel> Categories { get; set; }
    }
}
