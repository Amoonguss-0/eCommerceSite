using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    /// <summary>
    /// Represents a single game available for purchase
    /// </summary>
    public class Game
    {
        /// <summary>
        /// Unique identifier for each game product
        /// </summary>
        [Key]
        public int GameId { get; set; }

        /// <summary>
        /// Official title of the game
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// The sales price of the game 
        /// </summary>
        [Range(0, 4000)]
        public double Price { get; set; }


        // Todo: Add rating
    }

    public class CartGameViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; }   

        public double Price { get; set; }   
    }
}
