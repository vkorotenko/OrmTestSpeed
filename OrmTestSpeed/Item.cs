using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OrmTestSpeed
{
    /// <summary>
    /// Test stub item
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        /// <summary>
        /// Some stub key
        /// </summary>
        [Column("CoreId")]
        public Guid CoreId { get; set; }
        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// CityId identifier
        /// </summary>
        [Column("CityID")]
        public int CityId { get; set; }
    }
}