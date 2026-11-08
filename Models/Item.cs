using System.ComponentModel.DataAnnotations;

namespace FindIt.Models
{
    public class Item
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [Display(Name = "Item Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please provide a description.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Category is required.")]
        public string Category { get; set; } // e.g., ID, Electronics, Tumbler, Umbrella

        [Required(ErrorMessage = "Location found is required.")]
        [Display(Name = "Location Found")]
        public string LocationFound { get; set; }

        [Display(Name = "Storage Locker / Cabinet")]
        public string StorageLocation { get; set; } // Where security stored it

        [Display(Name = "Date Found")]
        public DateTime DateFound { get; set; } = DateTime.Now;

        public string Status { get; set; } = "Unclaimed"; // Unclaimed, Claimed

        [Display(Name = "Security Proof Question")]
        public string SecretQuestion { get; set; } // e.g., "What sticker is on the laptop cover?"

        public string ClaimStatus { get; set; } = "None"; // None, Pending Verification, Verified/Claimed
    }
}